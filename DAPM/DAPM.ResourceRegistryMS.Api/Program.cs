using DAPM.ResourceRegistryMS.Api.Repositories;
using DAPM.ResourceRegistryMS.Api.Repositories.Interfaces;
using DAPM.ResourceRegistryMS.Api.Services;
using DAPM.ResourceRegistryMS.Api.Services.Interfaces;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.WebHost.UseKestrel(o => o.Limits.MaxRequestBodySize = null);

builder.Services.Configure<FormOptions>(x =>
{
    x.ValueLengthLimit = int.MaxValue;
    x.MultipartBodyLengthLimit = int.MaxValue;
    x.MultipartBoundaryLengthLimit = int.MaxValue;
    x.MultipartHeadersCountLimit = int.MaxValue;
    x.MultipartHeadersLengthLimit = int.MaxValue;
});

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add Scoped Service
builder.Services.AddScoped<IResourceService, ResourceService>();
builder.Services.AddScoped<IPeerService, PeerService>();
builder.Services.AddScoped<IRepositoryService, RepositoryService>();
builder.Services.AddScoped<IResourceTypeService, ResourceTypeService>();

// Add Scoped ResourceRegistry
builder.Services.AddScoped<IResourceRepository, ResourceRepository>();
builder.Services.AddScoped<IRepositoryRepository, RepositoryRepository>();
builder.Services.AddScoped<IResourceTypeRepository, ResourceTypeRepository>();
builder.Services.AddScoped<IPeerRepository, PeerRepository>();


builder.Services.AddDbContext<ResourceRegistryDbContext>(options =>
{ options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")); }
);

var app = builder.Build();

app.MapDefaultEndpoints();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
