using System.Reflection.Metadata.Ecma335;
using BpmnEngine.Application.Models;
using BpmnEngine.Camunda.Abstractions;
using BpmnEngine.Camunda.External;
using BpmnEngine.Services;
using BpmnEngine.Services.Abstractions;
using BpmnEngine.Services.Processes.Models;

namespace BpmnEngine.Application.FrontEnd;

public class ViewModelProcessor : IViewModelProcessor
{
    private readonly IProcessRequestHandlingService _service;
    private readonly IMessageClient _client;

    public ViewModelProcessor(IProcessRequestHandlingService service, IMessageClient client)
    {
        _service = service;
        _client = client;
    }

    public async Task<ProcessInfoViewModel> ProcessViewModelAsync(CarHireViewModel model)
    {
        var variables = new Dictionary<string, Variable>
        {
            [ServicesConstants.FormHandlingVariables.LastStep] = Variable.String(ServicesConstants.FormHandlingVariables.Start),
            [ServicesConstants.FormHandlingVariables.Position] = Variable.String(ServicesConstants.FormHandlingVariables.Employee),
            [ServicesConstants.FormHandlingVariables.Supervisor] = Variable.String(ServicesConstants.FormHandlingVariables.Manager),
            [ServicesConstants.FormHandlingVariables.PhoneNumber] = Variable.String(model.PhoneNumber ?? string.Empty),
            [ServicesConstants.FormHandlingVariables.Destination] = Variable.String(model.Destination ?? string.Empty)
        };

        var request = new ProcessRequest(ServicesConstants.Processes.CarHire, variables);

        var response = await _service.StartProcessAsync(request);

        var processInfo = new ProcessInfoViewModel
        {
            ProcessInstanceId = response.Id,
            BusinessKey = response.BusinessKey
        };

        return processInfo;
    }

    public async Task<MessageInfoViewModel> ProcessViewModelAsync(MessagesViewModel model)
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
            MessageContent = "Error",
            ResultType = "None"
        };
    }
}