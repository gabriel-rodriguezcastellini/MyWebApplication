global using FastEndpoints;
global using FastEndpoints.Security;
using FastEndpoints.Swagger;
using MongoDB.Entities;

WebApplicationBuilder builder = WebApplication.CreateBuilder();
builder.Services.AddFastEndpoints().AddAuthorization();
builder.Services.AddJWTBearerAuth(builder.Configuration["JWTSigningKey"]!);
builder.Services.SwaggerDocument();

WebApplication app = builder.Build();
app.UseAuthentication();
app.UseAuthorization();
app.UseFastEndpoints();
app.UseSwaggerGen();

await DB.InitAsync(app.Configuration["MongoAddress"]!);

app.Run();
