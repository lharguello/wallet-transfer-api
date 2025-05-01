using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Microsoft.OpenApi.Models;
using WalletTransfer.Api.Core.Interfaces.Repositories;
using WalletTransfer.Api.Infrastructure.Data.EntityFramework;
using WalletTransfer.Api.Infrastructure.Data.Repositories;
using FluentValidation;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Converters;
using WalletTransfer.Api.Application.Behaviors;
using WalletTransfer.Api.Presentation.Filters;
using FluentValidation.AspNetCore;
using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
namespace WalletTransfer.Api.Presentation.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddDependency(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDBContext>(options => options.UseMySql(
        configuration.GetConnectionString("DefaultConnection"),
        ServerVersion.AutoDetect(configuration.GetConnectionString("DefaultConnection"))));

        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddHttpContextAccessor();

        services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddScoped<IWalletRepository, WalletRepository>();
        services.AddScoped<ITransactionRepository, TransactionRepository>();

        return services;
    }
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddControllers(options =>
        {
            options.Filters.Add(typeof(ApiKeyAuthorizationFilter));
        }).ConfigureApiBehaviorOptions(BehaviorBadRequest.ParseModelErrors)
        .AddNewtonsoftJson(options =>
        {
            options.SerializerSettings.Converters.Add(new StringEnumConverter
            {
                NamingStrategy = new CamelCaseNamingStrategy()
            });
        });
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGenNewtonsoftSupport();
        services.AddSwaggerGen(conf =>
        {
            conf.IncludeXmlComments(XmlCommentsFilePath);
            conf.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1.0",
                Title = "WalletTransfer.Api",
                Description = "This API will be responsible for managing balance transfers between two wallets.",
                Contact = new OpenApiContact
                {
                    Name = "Luis H. Arguello",
                    Email = "luis.arguello.caamal@gmail.com",
                    Url = new Uri("https://www.linkedin.com/in/lharguello"),
                }
            });
            conf.AddSecurityDefinition("x-api-key", new OpenApiSecurityScheme
            {
                Description = "Input your api key in the text field to access this API",
                Type = SecuritySchemeType.ApiKey,
                Name = "x-api-key",
                In = ParameterLocation.Header
            });
            conf.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                     new OpenApiSecurityScheme
                     {
                         Reference = new OpenApiReference
                         {
                             Type = ReferenceType.SecurityScheme,
                             Id = "x-api-key",
                         },
                         Type = SecuritySchemeType.ApiKey,
                         In = ParameterLocation.Header,
                         Name = "x-api-key"
                     }, new List<string>()
                 },
            });
            conf.UseInlineDefinitionsForEnums();
        });

        return services;
    }

    static string XmlCommentsFilePath
    {
        get
        {
            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            return xmlPath;
        }
    }

    public static void AddFluentValidationsExtension(this IServiceCollection services)
    {
        services.AddFluentValidationRulesToSwagger();
        services.AddFluentValidationAutoValidation();
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        services.Configure<ApiBehaviorOptions>(options =>
        {
            options.SuppressModelStateInvalidFilter = false;
        });
    }

    public static void AddCorsExtension(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy("AllowAll",
                  builder =>
                  {
                      builder.AllowAnyOrigin()
                             .AllowAnyHeader()
                             .AllowAnyMethod();
                  });
        });
    }
}
