using BpmnEngine.Services.Processes.Models;

namespace BpmnEngine.Services.Abstractions;

public interface IDecisionService
{
    Task<ExecutedProcess> GetExecutedProcessByIdAsync(Guid id);
    Task<int> AcceptMessageAsync(string businessKey);
    Task<int> DeclineMessageAsync(string businessKey);
}