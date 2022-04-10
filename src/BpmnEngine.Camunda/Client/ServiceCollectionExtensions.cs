using BpmnEngine.Camunda.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace BpmnEngine.Camunda.Client;

public static class ServiceCollectionExtensions
{
    public static IHttpClientBuilder AddTaskClient(this IServiceCollection services, Action<HttpClient> configureClient)
    {
        return services.AddHttpClient<IExternalTaskClient, ExternalTaskClient>(configureClient);
    }
    
    public static IHttpClientBuilder AddProcessClient(this IServiceCollection services, Action<HttpClient> configureClient)
    {
        return services.AddHttpClient<IProcessClient, ProcessClient>(configureClient);
    }
    
    public static IHttpClientBuilder AddMessageClient(this IServiceCollection services, Action<HttpClient> configureClient)
    {
        return services.AddHttpClient<IMessageClient, MessageClient>(configureClient);
    }
}
