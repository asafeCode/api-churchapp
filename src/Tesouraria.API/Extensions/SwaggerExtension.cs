using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Tesouraria.API.Extensions;

public static class SwaggerExtension
{
    public static void AddSwaggerGenOptions(this SwaggerGenOptions options)
    {
        const string authenticationType = "Bearer";
        options.AddSecurityDefinition(authenticationType, new OpenApiSecurityScheme
        {
            Description = @"JWT Authorization header using the Bearer scheme.
                      Enter 'Bearer' [space] and then your token in the text input below.
                      Example: 'Bearer 12345abcdef'",
            Name = "Authorization",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.ApiKey,
            Scheme = authenticationType
        });

        options.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = authenticationType
                    },
                    Scheme = "oauth2",
                    Name = authenticationType,
                    In = ParameterLocation.Header
                },
                new List<string>()
            }
        });
    }
}