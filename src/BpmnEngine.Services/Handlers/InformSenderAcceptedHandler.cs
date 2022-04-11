using BpmnEngine.Camunda.Abstractions;
using BpmnEngine.Camunda.Attributes;
using BpmnEngine.Camunda.External;
using BpmnEngine.Camunda.Results;
using BpmnEngine.Services.Models;
using Microsoft.Extensions.Logging;

namespace BpmnEngine.Services.Handlers;

[HandlerTopics("inform-sender-accepted", LockDuration = ServicesConstants.DefaultLockDuration)]
[HandlerVariables(AllVariables = true)]
public class InformSenderAcceptedHandler : IExternalTaskHandler
{
    private readonly ILogger<InformSenderAcceptedHandler> _logger;

    public InformSenderAcceptedHandler(ILogger<InformSenderAcceptedHandler> logger)
    {
        _logger = logger;
    }

    public async Task<IExecutionResult> HandleAsync(ExternalTask externalTask, CancellationToken cancellationToken)
    {
        var context = new ExternalTaskContext(externalTask);

        _logger.LogInformation(context.ToString());

        await Task.Delay(5000, cancellationToken);

        _logger.LogInformation($"Wniosek {context.BusinessKey} zaakceptowany");

        return new CompleteResult
        {
            Variables = externalTask.Variables
        };
    }
}