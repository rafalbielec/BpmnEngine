namespace BpmnEngine.Services.Processes.Errors;

[Serializable]
public class MissingVariablesException : Exception
{
    public MissingVariablesException() : base("No ProcessVariables found")
    {
    }
}