using Rentful.Api;
using Rentful.Application;
using Rentful.Application.Middlewares;
using Rentful.Infrastructure;
using Rentful.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

builder.ConfigureAuthentication(builder.Configuration);
builder.Services.AddAuthorization();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.CustomSchemaIds(type => $"{type.Name}_{Guid.NewGuid()}");
});
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
builder.Services.AddHttpContextAccessor();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();
builder.Services.AddControllers();

builder.ConfigureLogging();
builder.ConfigureOptions();
builder.ConfigureMassTransit();
builder.ConfigureCors();

builder.Services.AddSignalR();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ErrorHandlingMiddleware>();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<ValidationMiddleware>();

app.MapControllers();
app.UseCors();


app.MapHub<NotificationHub>("/notificationHub");


//app.ApplyMigrations();

app.Run();

public partial class Program { }