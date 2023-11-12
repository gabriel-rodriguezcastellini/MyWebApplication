global using FastEndpoints;
global using FastEndpoints.Security;
using FastEndpoints.Swagger;
using MongoDB.Entities;
using MyWebApplication.Binders;
using MyWebApplication.Parsers;
using System.Security.Claims;

WebApplicationBuilder builder = WebApplication.CreateBuilder();
builder.Services.AddSingleton(typeof(IRequestBinder<>), typeof(MyWebApplication.Binders.RequestBinder<>));
builder.Services.AddFastEndpoints(o => o.IncludeAbstractValidators = true).AddResponseCaching().AddAuthorization();
builder.Services.AddJWTBearerAuth(builder.Configuration["JWTSigningKey"]!);
builder.Services.SwaggerDocument();
WebApplication app = builder.Build();
app.UseAuthentication();
app.UseAuthorization();

app.UseResponseCaching().UseFastEndpoints(c => c.Binding.Modifier = (req, tReq, ctx, ct) =>
{
    _ = c.Binding.ValueParserFor<Guid>(Parsers.GuidParser);
    if (req is IHasRole r)
    {
        r.Role = ctx.HttpContext.User.ClaimValue(ClaimTypes.Role) ?? "Guest";
    }
});

app.UseSwaggerGen();
await DB.InitAsync(app.Configuration["MongoAddress"]!);
app.Run();