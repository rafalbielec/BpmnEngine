using System.Diagnostics;
using BpmnEngine.Application.FrontEnd;
using BpmnEngine.Application.Models;
using Microsoft.AspNetCore.Mvc;

namespace BpmnEngine.Application.Controllers;

[Route("forms")]
public class FormsController : Controller
{
    private readonly IViewModelProcessor _processor;

    public FormsController(IViewModelProcessor processor)
    {
        _processor = processor;
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
    }
    
    [HttpGet("car")]
    public IActionResult CarHire()
    {
        var model = new CarHireViewModel();

        return View(model);
    }
    
    [HttpGet("messages")]
    public IActionResult Messages()
    {
        var model = new MessagesViewModel();

        return View(model);
    }
    
    [HttpPost("messages")]
    public async Task<IActionResult> Messages(MessagesViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var viewModel = await _processor.ProcessViewModelAsync(model);

        return View("MessageInfo", viewModel);
    }
    
    [HttpPost("car")]
    public async Task<IActionResult> CarHire(CarHireViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var viewModel = await _processor.ProcessViewModelAsync(model);
        
        return View("ProcessInfo", viewModel);
    }
}