using BpmnEngine.Application.Models;
using BpmnEngine.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace BpmnEngine.Application.Controllers;

[Route("processes")]
public class ProcessController : Controller
{
    private readonly IDecisionService _decisionService;

    public ProcessController(IDecisionService decisionService)
    {
        _decisionService = decisionService;
    }

    [HttpGet("decision/{id:guid}")]
    public async Task<IActionResult> Decision(Guid id)
    {
        var (processId, businessKey, formValues) = await _decisionService.GetExecutedProcessByIdAsync(id);

        var model = new DecisionViewModel
        {
            ProcessInstanceId = processId,
            BusinessKey = businessKey,
            Variables = formValues
        };

        return View(model);
    }

    [HttpPost("decline")]
    public async Task<IActionResult> Decline(DecisionViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View("Decision", model);
        }

        await _decisionService.DeclineMessageAsync(model.BusinessKey);

        return View("DecisionMade", model);
    }
    
    [HttpPost("accept")]
    public async Task<IActionResult> Accept(DecisionViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View("Decision", model);
        }

        await _decisionService.AcceptMessageAsync(model.BusinessKey);

        return View("DecisionMade", model);
    }
}