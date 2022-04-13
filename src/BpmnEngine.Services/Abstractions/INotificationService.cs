namespace BpmnEngine.Services.Abstractions;

public interface INotificationService
{
    Task<bool> ConfirmSavedProcessId(Guid id);
    Task<bool> SendNotificationAsync(Guid actionId, Guid processId, string topicName, CancellationToken cancellationToken);

    bool InformSenderAcceptedAsync(string businessKey);
    bool InformSenderRejectedAsync(string businessKey);
}