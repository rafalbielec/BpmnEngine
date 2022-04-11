using BpmnEngine.Camunda.External;

namespace BpmnEngine.Services.Processes.Models;

public record ProcessRequest(
    ServicesConstants.Processes Process, 
    Dictionary<string, Variable> ProcessVariables);