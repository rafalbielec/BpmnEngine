﻿using BpmnEngine.Camunda.Abstractions;
using BpmnEngine.Camunda.Client.Responses;
using BpmnEngine.Services.Abstractions;
using BpmnEngine.Services.Processes.Models;
using Microsoft.Extensions.Logging;

namespace BpmnEngine.Services.Processes;

public class ProcessRequestHandlingService : IProcessRequestHandlingService
{
    private readonly IProcessClient _client;
    private readonly ILogger<ProcessRequestHandlingService> _logger;

    public ProcessRequestHandlingService(IProcessClient client, ILogger<ProcessRequestHandlingService> logger)
    {
        _client = client;
        _logger = logger;
    }

    public async Task<ProcessStartResponse> StartProcessAsync(ProcessRequest request)
    {
        string processKey;

        switch (request.Process)
        {
            default:
                processKey = ServicesConstants.ProcessNames.Test;
                break;
            case ServicesConstants.Processes.CarHire:
                processKey = ServicesConstants.ProcessNames.Forms;
                break;
        }

        var businessKey = $"{request.Process}:{Guid.NewGuid():N}";

        var result = await _client.StartProcessAsync(processKey, businessKey, request.ProcessVariables);

        _logger.LogInformation($"Process Id: {result.Id} Business Key: {result.BusinessKey} Definition: {result.DefinitionId}");

        return result;
    }
}