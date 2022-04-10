using BpmnEngine.Camunda.Abstractions;
using BpmnEngine.Camunda.Client.Requests;
using BpmnEngine.Camunda.Client.Responses;

namespace BpmnEngine.Camunda.Client;

public class MessageClient : BaseClient, IMessageClient
{
    public MessageClient(HttpClient httpClient) : base(httpClient)
    {
    }

    public async Task<MessageResponse[]> SendMessageEventAsync(string businessKey, string message,
        CancellationToken cancellationToken = default)
    {
        var request = new MessageRequest(businessKey, message);

        using var response = await SendMessageAsync(request, cancellationToken);
        await EnsureSuccessAsync(response);

        var result = await response.ReadJsonAsync<MessageResponse[]>();
        return result;
    }
}