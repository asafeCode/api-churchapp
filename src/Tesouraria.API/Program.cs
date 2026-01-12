using Tesouraria.API.Converters;
using Tesouraria.API.Extensions;
using Tesouraria.API.Filters;
using Tesouraria.API.Token;
using Tesouraria.Application;
using Tesouraria.Domain.Services.Token;
using Tesouraria.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowTreasuryApp", policy =>
    {
        policy
            //.WithOrigins("http://localhost:5173")
            .WithOrigins("church-app.up.railway.app")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials(); 
    });
});
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

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowTreasuryApp");
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MigrateDatabase(builder.Configuration);

await app.RunAsync();

namespace Tesouraria.API
{
    public partial class Program 
    {
        protected Program(){}
    }
}

