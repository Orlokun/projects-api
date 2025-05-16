using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace Tinkr.Api.Authorization;
public static class AuthorizationExtenstions
{
    public static IServiceCollection AddProjectAuthorization(this IServiceCollection services)
    {

        services.AddScoped<IClaimsTransformation, ScopeTransformation>()
        .AddAuthorization(options =>
        {
            options.AddPolicy(Policies.ReadAccess, policy =>
                policy.RequireClaim("scope", "projects:read"));
            options.AddPolicy(Policies.WriteAccess, policy =>
                policy.RequireClaim("scope", "projects:write")
                .RequireRole("admin"));
        });

        return services;
    }
}