global using FastEndpoints;
global using FastEndpoints.Security;
using FastEndpoints.ClientGen.Kiota;
using FastEndpoints.Swagger;
using Kiota.Builder;
using Microsoft.AspNetCore.Authentication;
using MongoDB.Entities;
using MyWebApplication;
using NSwag;
using System.Text.Json.Serialization;

WebApplicationBuilder bld = WebApplication.CreateBuilder();
bld.Services
   .AddFastEndpoints()
   .AddAuthorization()
   .AddAuthentication(ApikeyAuth._schemeName)
   .AddScheme<AuthenticationSchemeOptions, ApikeyAuth>(ApikeyAuth._schemeName, null);
bld.Services.AddJWTBearerAuth(bld.Configuration["JWTSigningKey"]!);
bld.Services
   .SwaggerDocument(o =>
   {
       o.DocumentSettings = s =>
       {
           _ = s.AddAuth(ApikeyAuth._schemeName, new()
           {
               Name = ApikeyAuth._headerName,
               In = OpenApiSecurityApiKeyLocation.Header,
               Type = OpenApiSecuritySchemeType.ApiKey,
           });
           s.DocumentName = "v1";
       };
       o.ExcludeNonFastEndpoints = true;
       o.EndpointFilter = ep => ep.EndpointTags?.Contains("include me") is true;
       o.AutoTagPathSegmentIndex = 0;
       o.TagDescriptions = t =>
       {
           t["Users"] = "tag description";
       };
       o.SerializerSettings = s =>
       {
           s.PropertyNamingPolicy = null;
           s.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
       };
       o.NewtonsoftSettings = s =>
       {
           s.Converters.Add(new KeysJsonConverter());
       };
       o.ShortSchemaNames = true;
       o.RemoveEmptyRequestSchema = true;
   });

WebApplication app = bld.Build();

app.MapApiClientEndpoint("/cs-client", c =>
{
    c.SwaggerDocumentName = "v1"; //must match document name set above
    c.Language = GenerationLanguage.CSharp;
    c.ClientNamespaceName = "MyCompanyName";
    c.ClientClassName = "MyCsClient";
},
o => //endpoint customization settings
{
    _ = o.CacheOutput(p => p.Expire(TimeSpan.FromDays(365))); //cache the zip
    _ = o.ExcludeFromDescription(); //hides this endpoint from swagger docs
});
app.UseAuthentication()
   .UseAuthorization()
   .UseFastEndpoints(c =>
   {
       c.Endpoints.ShortNames = true;
   })
   .UseSwaggerGen();

await app.GenerateApiClientsAndExitAsync(
    c =>
    {
        c.SwaggerDocumentName = "v1"; //must match doc name above
        c.Language = GenerationLanguage.CSharp;
        c.OutputPath = Path.Combine(app.Environment.WebRootPath, "ApiClients", "CSharp");
        c.ClientNamespaceName = "MyCompanyName";
        c.ClientClassName = "MyCsClient";
        c.CreateZipArchive = true; //if you'd like a zip file as well
    },
    c =>
    {
        c.SwaggerDocumentName = "v1";
        c.Language = GenerationLanguage.TypeScript;
        c.OutputPath = Path.Combine(app.Environment.WebRootPath, "ApiClients", "Typescript");
        c.ClientNamespaceName = "MyCompanyName";
        c.ClientClassName = "MyTsClient";
    });
await DB.InitAsync(app.Configuration["MongoAddress"]!);

app.Run();