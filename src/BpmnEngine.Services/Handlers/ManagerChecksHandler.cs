using BpmnEngine.Camunda.Abstractions;
using BpmnEngine.Camunda.Attributes;
using BpmnEngine.Camunda.External;
using BpmnEngine.Camunda.Results;
using BpmnEngine.Services.Models;
using Microsoft.Extensions.Logging;

namespace BpmnEngine.Services.Handlers;

[HandlerTopics("manager-checks", LockDuration = ServicesConstants.DefaultLockDuration)]
[HandlerVariables(AllVariables = true)]
public class ManagerChecksHandler : IExternalTaskHandler
{
    private readonly ILogger<ManagerChecksHandler> _logger;

    public ManagerChecksHandler(ILogger<ManagerChecksHandler> logger)
    {
        _logger = logger;
    }

    public async Task<IExecutionResult> HandleAsync(ExternalTask externalTask, CancellationToken cancellationToken)
    {
        var context = new ExternalTaskContext(externalTask);

        _logger.LogInformation(context.ToString());

        await Task.Delay(5000, cancellationToken);
        
        return new CompleteResult
        {
            Variables = externalTask.Variables
        };
    }
}