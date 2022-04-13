namespace BpmnEngine.Application.Models;

public class DecisionViewModel : BaseViewModel
{
    public Guid ProcessInstanceId { get; set; }
    public string BusinessKey { get; set; }
    public bool Accepted { get; set; }
    public IDictionary<string, string> Variables { get; set; }
}