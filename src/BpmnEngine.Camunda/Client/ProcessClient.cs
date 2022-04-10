using BpmnEngine.Camunda.Abstractions;
using BpmnEngine.Camunda.Client.Requests;
using BpmnEngine.Camunda.Client.Responses;
using BpmnEngine.Camunda.External;

namespace BpmnEngine.Camunda.Client;

public class ProcessClient : BaseClient, IProcessClient
{
    public ProcessClient(HttpClient httpClient):base(httpClient)
    {
    }
    
    public async Task<ProcessCountResponse> CountProcessDefinitionsAsync(CancellationToken cancellationToken = default)
    {
        using var response = await SendGetProcessDefinitionAsync("count", cancellationToken);
        await EnsureSuccessAsync(response);

        var result = await response.ReadJsonAsync<ProcessCountResponse>();
        return result;
    }

    public async Task<ProcessStartResponse> StartProcessAsync(string key, string businessKey, CancellationToken cancellationToken = default)
    {
        var variables = new Dictionary<string, Variable>
        {
            ["supervisor"] = Variable.String("director")
        };

        var request = new ProcessStartRequest(businessKey, variables);
        using var response = await SendPostProcessDefinitionAsync($"key/{key}/start", request, cancellationToken);
        await EnsureSuccessAsync(response);

        var result = await response.ReadJsonAsync<ProcessStartResponse>();
        return result;
    }
}