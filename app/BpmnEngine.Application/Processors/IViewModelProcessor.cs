using BpmnEngine.Application.Models;

namespace BpmnEngine.Application.Processors;

public interface IViewModelProcessor
{
    Task<ProcessInfoViewModel> ProcessViewModelAsync<T>(T model) where T : BaseViewModel;
    Task<MessageInfoViewModel> ProcessMessagesViewModelAsync(MessagesViewModel model);
}