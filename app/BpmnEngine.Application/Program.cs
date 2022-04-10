using BpmnEngine.Camunda;
using BpmnEngine.Camunda.Client;
using BpmnEngine.Camunda.Configuration;
using BpmnEngine.Services.Handlers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

var configuration = builder.Configuration;

var engineRestUri = configuration.GetRequiredSection(CamundaConstants.EngineRestAddress).Value;

builder.Services.AddTaskClient(client => client.BaseAddress = new Uri(engineRestUri));
builder.Services.AddProcessClient(client => client.BaseAddress = new Uri(engineRestUri));
builder.Services.AddMessageClient(client => client.BaseAddress = new Uri(engineRestUri));

builder.Services.AddCamundaWorker(CamundaConstants.WorkerName)
    .AddHandler<RequestApprovalHandler>()
    .ConfigurePipeline(pipeline =>
    {
        pipeline.Use(next => async context =>
        {
            var logger = context.ServiceProvider.GetRequiredService<ILogger<Program>>();
            logger.LogInformation("Task {Id} has started", context.Task.Id);
            await next(context);
            logger.LogInformation("Task {Id} has been processed", context.Task.Id);
        });
    });

builder.Services.AddHealthChecks();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.UseHealthChecks("/health");

app.Run();
