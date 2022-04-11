using BpmnEngine.Camunda.Abstractions;
using BpmnEngine.Camunda.Attributes;
using BpmnEngine.Camunda.External;
using BpmnEngine.Camunda.Results;
using BpmnEngine.Services.Models;
using Microsoft.Extensions.Logging;

namespace BpmnEngine.Services.Handlers;

[HandlerTopics("bou-verification", LockDuration = ServicesConstants.DefaultLockDuration)]
[HandlerVariables(AllVariables = true)]
public class BouVerificationHandler : IExternalTaskHandler
{
    private readonly ILogger<BouVerificationHandler> _logger;

    public BouVerificationHandler(ILogger<BouVerificationHandler> logger)
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