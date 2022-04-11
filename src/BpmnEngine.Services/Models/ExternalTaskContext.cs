using BpmnEngine.Camunda;
using BpmnEngine.Camunda.External;
using BpmnEngine.Services.Processes.Errors;

namespace BpmnEngine.Services.Models;

public class ExternalTaskContext
{
    public ExternalTaskContext(ExternalTask externalTask)
    {
        TaskId = Guard.NotEmptyAndNotNull(externalTask.Id, nameof(externalTask.Id));
        ProcessInstanceId = Guard.NotEmptyAndNotNull(externalTask.ProcessInstanceId, nameof(externalTask.ProcessInstanceId));
        BusinessKey = Guard.NotEmptyAndNotNull(externalTask.BusinessKey, nameof(externalTask.BusinessKey));
        TopicName = Guard.NotEmptyAndNotNull(externalTask.TopicName, nameof(externalTask.TopicName));
        Variables = externalTask.Variables ?? new Dictionary<string, Variable>();

        if (externalTask.Variables == null)
            throw new MissingVariablesException();

        if (externalTask.Variables.TryGetValue(ServicesConstants.FormHandlingVariables.LastStep, out var step))
            LastStep = step.AsString();
        else
            throw new MissingVariableException(ServicesConstants.FormHandlingVariables.LastStep);

        if (externalTask.Variables.TryGetValue(ServicesConstants.FormHandlingVariables.Position, out var position))
            UserJobPosition = position.AsString();
        else
            throw new MissingVariableException(ServicesConstants.FormHandlingVariables.Position);

        if (externalTask.Variables.TryGetValue(ServicesConstants.FormHandlingVariables.Supervisor, out var supervisor))
            UserSupervisor = supervisor.AsString();
        else
            throw new MissingVariableException(ServicesConstants.FormHandlingVariables.Supervisor);
    }

    public IDictionary<string, Variable> Variables { get; set; }

    public string TopicName { get; set; }
    public string TaskId { get; }
    public string ProcessInstanceId { get; }
    public string BusinessKey { get; }
    public string LastStep { get; }

    public string UserJobPosition { get; }
    public string UserSupervisor { get; }

    public void UpdateLastStep()
    {
        var value = TopicName;

        if (Variables.ContainsKey(ServicesConstants.FormHandlingVariables.LastStep))
            Variables[ServicesConstants.FormHandlingVariables.LastStep] = Variable.String(value);
        else
            Variables.Add(ServicesConstants.FormHandlingVariables.LastStep, Variable.String(value));
    }

    public override string ToString() => $"External Service Task for '{TopicName}' in {BusinessKey}";
}