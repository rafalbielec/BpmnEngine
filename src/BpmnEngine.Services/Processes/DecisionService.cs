using BpmnEngine.Camunda.Abstractions;
using BpmnEngine.Camunda.Extensions;
using BpmnEngine.Camunda.External;
using BpmnEngine.Services.Abstractions;
using BpmnEngine.Services.Processes.Models;
using BpmnEngine.Storage.Abstractions;

namespace BpmnEngine.Services.Processes;

public class DecisionService : IDecisionService
{
    private readonly IFormsRepository _repository;
    private readonly IMessageClient _client;

    public DecisionService(IFormsRepository repository, IMessageClient client)
    {
        _repository = repository;
        _client = client;
    }

    public async Task<ExecutedProcess> GetExecutedProcessByIdAsync(Guid id)
    {
        var process = await _repository.SelectedExecutedProcessById(id);
        
        var dir = SerializerInstance.DeserializeFromString<IDictionary<string, Variable>>(process.Variables);

        var dictionary = dir.ToDictionary(a => a.Key, b => b.Value.AsString());

        var model = new ExecutedProcess(process.Id, process.BusinessKey, dictionary!);

        return model;
    }

    private async Task<int> SendMessageToProcessAsync(string businessKey, string message)
    {
        var response = await _client.SendMessageEventAsync(businessKey, message);

        return 0;
    }

    public async Task<int> AcceptMessageAsync(string businessKey)
    {
        return await SendMessageToProcessAsync(businessKey, "");
    }

    public async Task<int> DeclineMessageAsync(string businessKey)
    {
        return await SendMessageToProcessAsync(businessKey, "");
    }
}