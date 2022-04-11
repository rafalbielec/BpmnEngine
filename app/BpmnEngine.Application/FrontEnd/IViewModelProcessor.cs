using BpmnEngine.Application.Models;

namespace BpmnEngine.Application.FrontEnd;

public interface IViewModelProcessor
{
    Task<ProcessInfoViewModel> ProcessViewModelAsync(CarHireViewModel model);
    Task<MessageInfoViewModel> ProcessViewModelAsync(MessagesViewModel model);
}