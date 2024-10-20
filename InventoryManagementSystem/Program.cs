using Autofac.Extensions.DependencyInjection;
using Autofac;
using InventoryManagementSystem;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using InventoryManagementSystem.Constants;
using System.Text;
using InventoryManagementSystem.Repositories.Base;
using InventoryManagementSystem.Mappings;
using InventoryManagementSystem.Helpers;
using System.Net;
using System.Net.Mail;
using AutoMapper;
using InventoryManagementSystem.CQRS.Users.Commands;
using DotNetEnv;
using AspNetCoreRateLimit;
using Hangfire;

var builder = WebApplication.CreateBuilder(args);


var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(RegisterUserCommandHandler).Assembly));

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Food App Api", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. " +
                        "\r\n\r\n Enter 'Bearer' [space] and then your token in the text input below." +
                        "\r\n\r\nExample: \"Bearer abcdefghijklmnopqrstuvwxyz\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header,

            },
            new List<string>()
        }
    });
});
//Enviroment
Env.Load();
builder.Services.AddAuthentication(opts =>
{
    opts.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opts.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(opts =>
{
    opts.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidIssuer = JwtSettings.Issuer,
        ValidAudience = JwtSettings.Audience,
        ClockSkew = TimeSpan.Zero, // Reduce the default clock skew (allowable token time discrepancy)
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(JwtSettings.SecretKey))
    };
});

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(builder =>
    builder.RegisterModule(new AutoFacModule()));

builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddAutoMapper(typeof(UserProfile),typeof(ProductProfile));
builder.Services.AddScoped<IFluentEmailService, FluentEmailService>();

var port = builder.Configuration.GetSection("EmailCredentials").GetValue<int>("Port");
var from = builder.Configuration.GetSection("EmailCredentials").GetValue<string>("From");
var password = builder.Configuration.GetSection("EmailCredentials").GetValue<string>("Password");
var host = builder.Configuration.GetSection("EmailCredentials").GetValue<string>("Host");

var sender = new SmtpClient
{
    Host = host,
    UseDefaultCredentials = false,
    Credentials = new NetworkCredential(from, password),
    EnableSsl = true,
    Port = port
};
builder.Services.AddFluentEmail(from, "Upskilling").AddSmtpSender(sender);

builder.Services.Configure<IpRateLimitOptions>(configuration.GetSection("IpRateLimiting"));

builder.Services.AddInMemoryRateLimiting();

builder.Services.AddHangfire(cfg =>
            cfg.UseSqlServerStorage(configuration.GetConnectionString("Default")));
builder.Services.AddHangfireServer();


builder.Services.AddSingleton<IRateLimitConfiguration,
    RateLimitConfiguration>();

var app = builder.Build();
MapperHelper.Mapper = app.Services.GetService<IMapper>();


app.UseHangfireDashboard("/hangfire");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseIpRateLimiting();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
