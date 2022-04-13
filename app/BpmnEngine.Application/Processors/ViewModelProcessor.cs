using BpmnEngine.Application.Models;
using BpmnEngine.Camunda.Abstractions;
using BpmnEngine.Camunda.External;
using BpmnEngine.Services;
using BpmnEngine.Services.Abstractions;
using BpmnEngine.Services.Processes.Models;
using BpmnEngine.Storage;

namespace BpmnEngine.Application.Processors;

public class ViewModelProcessor : IViewModelProcessor
{
    private readonly IProcessRequestHandlingService _service;
    private readonly IMessageClient _client;
    
    public ViewModelProcessor(
        IProcessRequestHandlingService service, 
        IMessageClient client)
    {
        _service = service;
        _client = client;
    }

    /// <summary>
    ///     Converts HTML form values to process variables.
    /// </summary>
    private static void AddViewModelVariable<T>(T model, IDictionary<string, Variable> variables) where T : BaseViewModel
    {
        switch (model)
        {
            case CarHireViewModel m:
                variables[ServicesConstants.FormHandlingVariables.Destination] = Variable.String(m.Destination ?? string.Empty);
                variables[ServicesConstants.FormHandlingVariables.PhoneNumber] = Variable.String(m.PhoneNumber ?? string.Empty);
                break;
            case RoomBookingViewModel r:
                variables[ServicesConstants.FormHandlingVariables.NumberOfPeople] = Variable.Integer(r.NumberOfPeople);
                variables[ServicesConstants.FormHandlingVariables.Room] = Variable.String(r.Room ?? string.Empty);
                break;
        }
    }

    /// <summary>
    ///     Converts user profile to process variables.
    /// </summary>
    private static void AddUserVariables<T>(T model, IDictionary<string, Variable> variables) where T : BaseViewModel
    {
        variables[ServicesConstants.FormHandlingVariables.Supervisor] = Variable.String(ServicesConstants.FormHandlingVariables.Manager);
        variables[ServicesConstants.FormHandlingVariables.Position] = Variable.String(ServicesConstants.FormHandlingVariables.Employee);
    }

    /// <summary>
    ///     Starts the process in the BPMN engine.
    /// </summary>
    public async Task<ProcessInfoViewModel> ProcessViewModelAsync<T>(T model) where T : BaseViewModel
    {
        var variables = new Dictionary<string, Variable>
        {
            [ServicesConstants.FormHandlingVariables.LastStep] = Variable.String(ServicesConstants.FormHandlingVariables.Start)
        };

        AddUserVariables(model, variables);
        AddViewModelVariable(model, variables);
        
        var request = new ProcessRequest(StorageConstants.ProcessName.CarHire, variables);

        var response = await _service.StartProcessAsync(request);

        var processInfo = new ProcessInfoViewModel
        {
            ProcessInstanceId = response.Id,
            BusinessKey = response.BusinessKey
        };

        return processInfo;
    }

    public async Task<MessageInfoViewModel> ProcessMessagesViewModelAsync(MessagesViewModel model)
    {
        var response = await _client.SendMessageEventAsync(model.BusinessKey, model.MessageContent);
        if (response.Any())
        {
            var first = response.First();
            return new MessageInfoViewModel
            {
                BusinessKey = model.BusinessKey,
                MessageContent = model.MessageContent,
                ResultType = first.ResultType
            };
        }

        return new MessageInfoViewModel
        {
            BusinessKey = model.BusinessKey,
            MessageContent = PolishConstants.MissingBusinessKeyError,
            ResultType = string.Empty
        };
    }
}