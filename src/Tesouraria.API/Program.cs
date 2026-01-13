using Microsoft.Extensions.Diagnostics.HealthChecks;
using Tesouraria.API.Converters;
using Tesouraria.API.Extensions;
using Tesouraria.API.Filters;
using Tesouraria.API.Middleware;
using Tesouraria.API.Token;
using Tesouraria.Application;
using Tesouraria.Domain.Services.Token;
using Tesouraria.Infrastructure;
using Tesouraria.Infrastructure.DataAccess;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddJsonOptions(opt =>
{
    opt.JsonSerializerOptions.Converters.Add(new NullableGuidConverter());
    opt.JsonSerializerOptions.Converters.Add(new StringConverter());
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opt => opt.AddSwaggerGenOptions());

builder.Services.AddMvc(options => options.Filters.Add(typeof(ExceptionFilter)));

builder.Services.AddRouting(options => options.LowercaseUrls = true);

builder.Services.AddHttpContextAccessor();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();
builder.Services.AddScoped<ITokenProvider, HttpContextTokenValue>();

builder.Services.AddHealthChecks().AddDbContextCheck<TesourariaDbContext>();

var frontendUrl =  builder.Configuration.GetValue<string>("Settings:Invite:FrontendUrl");
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy
            .WithOrigins(frontendUrl!)
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});


var app = builder.Build();
app.MapHealthChecks("/Health", new Microsoft.AspNetCore.Diagnostics.HealthChecks.HealthCheckOptions
{
    AllowCachingResponses = false,
    ResultStatusCodes =
    {
        [HealthStatus.Healthy] = StatusCodes.Status200OK,
        [HealthStatus.Unhealthy] = StatusCodes.Status503ServiceUnavailable
    }
});

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<CultureMiddleware>();
app.UseCors("AllowFrontend");
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MigrateDatabase(builder.Configuration);

await app.RunAsync();

namespace Tesouraria.API
{
    public partial class Program
    {
        protected Program()
        {
        }
    }
}