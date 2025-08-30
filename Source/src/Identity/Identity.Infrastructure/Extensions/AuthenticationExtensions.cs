using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Identity.Infrastructure.Extensions
{
    public static class AuthenticationExtensions
    {
        /// <summary>
        /// Registers JWT Bearer Authentication + Authorization with
        ///  - 15 min expiry validation
        ///  - Zero clock skew
        ///  - Custom JSON responses for expired/unauthorized/forbidden
        /// </summary>
        public static IServiceCollection AddIdentityAuthentication(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var jwt = configuration.GetSection("JwtSettings");
            var issuer = jwt["Issuer"]!;
            var audience = jwt["Audience"]!;
            var secret = jwt["SecretKey"]!;

            services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;     // set true in production behind HTTPS
                    options.SaveToken = true;

                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,                // ✅ enforce expiry
                        ValidateIssuerSigningKey = true,
                        ClockSkew = TimeSpan.Zero,              // ✅ no 5-min grace
                        ValidIssuer = issuer,
                        ValidAudience = audience,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret)),
                    };

                    // ✅ Events: return clean JSON for common auth failures
                    options.Events = new JwtBearerEvents
                    {
                        OnAuthenticationFailed = async ctx =>
                        {
                            // e.g., signature invalid or token expired
                            if (ctx.Exception is SecurityTokenExpiredException)
                            {
                                if (!ctx.Response.HasStarted)
                                {
                                    ctx.Response.StatusCode = StatusCodes.Status401Unauthorized;
                                    ctx.Response.ContentType = "application/json";
                                    await ctx.Response.WriteAsync("{\"error\":\"Session expired. Please login again.\"}");
                                }
                                // prevent default handler
                                ctx.NoResult();
                            }
                        },
                        OnChallenge = async ctx =>
                        {
                            // Fires when no/invalid token provided (and before default 401 body)
                            ctx.HandleResponse(); // suppress default
                            if (!ctx.Response.HasStarted)
                            {
                                ctx.Response.StatusCode = StatusCodes.Status401Unauthorized;
                                ctx.Response.ContentType = "application/json";
                                await ctx.Response.WriteAsync("{\"error\":\"Unauthorized. Provide a valid Bearer token.\"}");
                            }
                        },
                        OnForbidden = async ctx =>
                        {
                            // Authenticated but not authorized (e.g., missing role/policy)
                            if (!ctx.Response.HasStarted)
                            {
                                ctx.Response.StatusCode = StatusCodes.Status403Forbidden;
                                ctx.Response.ContentType = "application/json";
                                await ctx.Response.WriteAsync("{\"error\":\"Forbidden. You do not have access to this resource.\"}");
                            }
                        }
                    };
                });

            services.AddAuthorization(); // add policies if you need

            return services;
        }
    }
}
