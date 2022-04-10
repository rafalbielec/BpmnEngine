using BpmnEngine.Camunda.Abstractions;
using BpmnEngine.Camunda.Attributes;
using BpmnEngine.Camunda.External;
using BpmnEngine.Camunda.Results;
using Microsoft.Extensions.Logging;

namespace BpmnEngine.Services.Handlers;

[HandlerTopics("request_approval", LockDuration = 10000)]
//[HandlerVariables("USERNAME", AllVariables = true)]
public class RequestApprovalHandler : IExternalTaskHandler
{
    private readonly ILogger<RequestApprovalHandler> _logger;

    public RequestApprovalHandler(ILogger<RequestApprovalHandler> logger)
    {
        _logger = logger;
    }

    public async Task<IExecutionResult> HandleAsync(ExternalTask externalTask, CancellationToken cancellationToken)
    {
        await Task.Delay(1, cancellationToken);

        _logger.LogInformation(nameof(RequestApprovalHandler));

        return new CompleteResult();
    }
}