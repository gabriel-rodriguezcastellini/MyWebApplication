using FastEndpoints;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MyWebApplication;
using MyWebApplication.Entities;
using MyWebApplication.Services;
using System.Text;

WebApplicationBuilder bld = WebApplication.CreateBuilder();
bld.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<SchoolContext>().AddDefaultTokenProviders();
bld.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options =>
    {
        options.SaveToken = true;
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidAudience = bld.Configuration["JWTKey:ValidAudience"],
            ValidIssuer = bld.Configuration["JWTKey:ValidIssuer"],
            ClockSkew = TimeSpan.Zero,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(bld.Configuration["JWTKey:Secret"]!))
        };
    });
bld.Services.AddFastEndpoints().AddAuthorization();
bld.Services.AddDbContextPool<SchoolContext>(options => options.UseSqlServer(bld.Configuration.GetConnectionString("TestConnection")));
bld.Services.AddScoped<IAuthenticationService, AuthenticationService>();
WebApplication app = bld.Build();
app.UseAuthentication().UseAuthorization().UseFastEndpoints();
app.Run();
