using RabbitMQLibrary.Implementation;
using RabbitMQLibrary.Extensions;
using DAPM.PipelineOrchestratorMS.Api.Consumers;
using RabbitMQLibrary.Messages.PipelineOrchestrator;
using DAPM.PipelineOrchestratorMS.Api.Engine.Interfaces;
using DAPM.PipelineOrchestratorMS.Api.Engine;
using DAPM.PipelineOrchestratorMS.Api.Services; 
using DAPM.PipelineOrchestratorMS.Api.Services.Interfaces; 
using DAPM.PipelineOrchestratorMS.Api.Controllers;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register the new PipelineResultsService
builder.Services.AddScoped<IPipelineResultsService, PipelineResultsService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader());
});


builder.Services.AddQueueing(new QueueingConfigurationSettings
{
    RabbitMqConsumerConcurrency = 5,
    RabbitMqHostname = "rabbitmq",
    RabbitMqPort = 5672,
    RabbitMqPassword = "guest",
    RabbitMqUsername = "guest"
});


builder.Services.AddQueueMessageConsumer<CreateInstanceExecutionConsumer, CreateInstanceExecutionMessage>();
builder.Services.AddQueueMessageConsumer<ActionResultConsumer, ActionResultMessage>();
builder.Services.AddQueueMessageConsumer<PipelineStartCommandConsumer, PipelineStartCommand>();
builder.Services.AddQueueMessageConsumer<GetPipelineExecutionStatusConsumer, GetPipelineExecutionStatusMessage>();


builder.Services.AddSingleton<IPipelineOrchestrationEngine, PipelineOrchestrationEngine>();

var app = builder.Build();

app.MapDefaultEndpoints();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();
