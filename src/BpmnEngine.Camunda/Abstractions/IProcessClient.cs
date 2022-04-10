using BpmnEngine.Camunda.Client.Responses;

namespace BpmnEngine.Camunda.Abstractions;

public interface IProcessClient
{
    Task<ProcessCountResponse> CountProcessDefinitionsAsync(CancellationToken cancellationToken = default);
    Task<ProcessStartResponse> StartProcessAsync(string key, string businessKey, CancellationToken cancellationToken = default);
}