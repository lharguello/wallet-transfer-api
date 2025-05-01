using Serilog;
using WalletTransfer.Api.Presentation.Extensions;
using WalletTransfer.Api.Presentation.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add Logger
builder.Host.UseSerilog((context, configuration) => configuration.ReadFrom.Configuration(context.Configuration));

// Add services to the container.
builder.Services.AddApplication();
builder.Services.AddDependency(builder.Configuration);
builder.Services.AddCorsExtension();

//API Fluent Validator
builder.Services.AddFluentValidationsExtension();
// generate lowercase URLs
builder.Services.Configure<RouteOptions>(options =>
{
    options.LowercaseUrls = true;
});

//Health Check
builder.Services.AddHealthChecks();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<GlobalExceptionHandlerMiddleware>();

app.UseCors("AllowAll");

app.UseSerilogRequestLogging();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapHealthChecks("/health");

app.MapControllers();

app.Run();
