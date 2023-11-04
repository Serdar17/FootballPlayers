using Api.Configuration;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

services.AddHttpContextAccessor()
    .AddAppController()
    .AddAppCors()
    .AddAppAutoMappers()
    .AddAppSwagger();
    
builder.AddAppLogger();

var app = builder.Build();

app.UseAppCors();
app.UseAppMiddlewares();
app.UseAppSwagger();
app.UseAppController();

app.Run();