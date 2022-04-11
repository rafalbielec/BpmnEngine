using BpmnEngine.Services.Abstractions;
using BpmnEngine.Services.Processes.Models;

namespace BpmnEngine.Services.Processes;

public class BusinessKeyGenerator : IBusinessKeyGenerator
{
    public string GenerateBusinessKey(ProcessRequest request)
    {
        var key = $"{Guid.NewGuid():N}_{request.Process}_{DateTime.UtcNow:yyyy_MM_dd_HH_mm_ss}";
        
        return key;
    }
}