using BpmnEngine.Camunda.Abstractions;
using BpmnEngine.Camunda.Attributes;
using BpmnEngine.Camunda.External;
using Microsoft.Extensions.Logging;

namespace BpmnEngine.Services.Handlers;

[HandlerTopics("director-checks", LockDuration = ServicesConstants.DefaultLockDuration)]
[HandlerVariables(AllVariables = true)]
public class DirectorChecksHandler : BaseHandler<DirectorChecksHandler>, IExternalTaskHandler
{
    public DirectorChecksHandler(ILogger<DirectorChecksHandler> logger) : base(logger)
    {
    }

    public async Task<IExecutionResult> HandleAsync(ExternalTask externalTask, CancellationToken cancellationToken)
    {
        return await BaseHandleAsync(externalTask, cancellationToken);
    }
}