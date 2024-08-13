using Rentful.Api;
using Rentful.Application;
using Rentful.Application.Middlewares;
using Rentful.Infrastructure;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);
builder.ConfigureOptions();
builder.ConfigureAuthentication(builder.Configuration);
//builder.ConfigureLogging();
builder.Services.AddAuthorization();


builder.Services.AddControllers();

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

app.Run();
