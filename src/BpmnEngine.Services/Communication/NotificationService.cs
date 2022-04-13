using System.Net.Mail;
using BpmnEngine.Services.Abstractions;
using BpmnEngine.Storage.Abstractions;
using Microsoft.Extensions.Logging;

namespace BpmnEngine.Services.Communication;

public class NotificationService : INotificationService
{
    private readonly IFormsRepository _formsRepository;
    private readonly IUserActionsRepository _repository;
    private readonly ILogger<NotificationService> _logger;

    public NotificationService(IFormsRepository formsRepository, IUserActionsRepository repository, ILogger<NotificationService> logger)
    {
        _formsRepository = formsRepository;
        _repository = repository;
        _logger = logger;
    }

    public async Task<bool> ConfirmSavedProcessId(Guid id)
    {
        var exists = await _formsRepository.ExecutedProcessById(id);

        return exists;
    }

    public async Task<bool> SendNotificationAsync(Guid actionId, Guid processId, string topicName, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Adding {actionId}");

        var ix = await _repository.InsertUserActionAsync(actionId, processId, topicName, cancellationToken);

        if (ix > 0)
        {
            var body = $"https://localhost:7000/processes/decision/{actionId}";

            _logger.LogInformation(body);

            SendMail($"Powiadomienie z Camunda. Decyzja: {actionId}", body);
            return true;
        }

        return false;
    }

    public bool InformSenderAcceptedAsync(string businessKey)
    {
        SendMail("Powiadomienie z Camunda. Wniosek został zaakceptowany", $"{businessKey} został zaakceptowany");

        return true;
    }

    public bool InformSenderRejectedAsync(string businessKey)
    {
        SendMail("Powiadomienie z Camunda. Wniosek został odrzucony", $"{businessKey} został odrzucony");

        return true;
    }

    private void SendMail(string subject, string body)
    {
        try
        {
            var mailObj = new MailMessage("camunda@mail.local", "raf@mail.local", subject, body);
            var smtpClient = new SmtpClient("127.0.0.1");

            smtpClient.Send(mailObj);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, nameof(SendMail));

            throw;
        }
    }
}