using BpmnEngine.Camunda.Abstractions;
using BpmnEngine.Camunda.External;
using BpmnEngine.Camunda.Results;
using BpmnEngine.Services.Models;
using Microsoft.Extensions.Logging;

namespace BpmnEngine.Services.Handlers;

public abstract class BaseHandler<T>
{
    protected readonly ILogger<T> Logger;

    protected BaseHandler(ILogger<T> logger)
    {
        Logger = logger;
    }

    protected async Task<IExecutionResult> BaseHandleAsync(ExternalTask externalTask, CancellationToken cancellationToken)
    {
        var context = new ExternalTaskContext(externalTask);

        Logger.LogInformation($"{context} has started");

        await Task.Delay(5000, cancellationToken);

        Logger.LogInformation($"External Service Task for '{context.TopicName}' in {context.BusinessKey} has ended");

        context.UpdateLastStep();
        return new CompleteResult
        {
            Variables = context.Variables
        };
    }
}