using BpmnEngine.Camunda.Abstractions;
using BpmnEngine.Camunda.Attributes;
using BpmnEngine.Camunda.External;
using BpmnEngine.Camunda.Results;
using BpmnEngine.Services.Models;
using Microsoft.Extensions.Logging;

namespace BpmnEngine.Services.Handlers;

[HandlerTopics("inform-sender-rejected", LockDuration = ServicesConstants.DefaultLockDuration)]
[HandlerVariables(AllVariables = true)]
public class InformSenderRejectedHandler : BaseHandler<InformSenderRejectedHandler>, IExternalTaskHandler
{
    public InformSenderRejectedHandler(ILogger<InformSenderRejectedHandler> logger) : base(logger)
    {
    }

    public async Task<IExecutionResult> HandleAsync(ExternalTask externalTask, CancellationToken cancellationToken)
    {
        var context = new ExternalTaskContext(externalTask);

        Logger.LogInformation($"{context} has started");

        await Task.Delay(5000, cancellationToken);

        Logger.LogInformation($"External Service Task for '{context.TopicName}' in {context.BusinessKey} has ended");

        Logger.LogInformation($"Wniosek {context.BusinessKey} został odrzucony");

        context.UpdateLastStep();
        return new CompleteResult
        {
            Variables = context.Variables
        };
    }
}