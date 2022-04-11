using BpmnEngine.Camunda.Abstractions;
using BpmnEngine.Camunda.External;
using BpmnEngine.Services;
using Microsoft.AspNetCore.Mvc;

namespace BpmnEngine.Application.Controllers;

[Route("api/test")]
public class TestController : ControllerBase
{
    private readonly IProcessClient _client;
    private readonly IMessageClient _messageClient;

    public TestController(IProcessClient client, IMessageClient messageClient)
    {
        _client = client;
        _messageClient = messageClient;
    }

    [HttpGet("message/{businessKey}")]
    public async Task<IActionResult> GetMessageAsync([FromRoute] string businessKey)
    {
        var result = await _messageClient.SendMessageEventAsync(businessKey,
            ServicesConstants.Messages.VerificationDone);

        return Ok(result);
    }

    [Route("count")]
    public async Task<IActionResult> CountAsync()
    {
        var result = await _client.CountProcessDefinitionsAsync();

        return Ok(result);
    }

    [Route("start")]
    public async Task<IActionResult> StartAsync()
    {
        var variables = new Dictionary<string, Variable>
        {
            [ServicesConstants.FormHandlingVariables.LastStep] = Variable.String(ServicesConstants.FormHandlingVariables.Start)
        };

        var result = await _client.StartProcessAsync(
            ServicesConstants.ProcessNames.Test,
            Guid.NewGuid().ToString("N"), 
            variables);

        return Ok(result);
    }
}