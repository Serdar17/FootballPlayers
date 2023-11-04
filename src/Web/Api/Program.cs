using Api.Configuration;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

services.AddHttpContextAccessor()
    .AddAppController()
    .AddAppCors()
    .AddAppAutoMappers()
    .AddEndpointsApiExplorer()
    .AddSwaggerGen();
    
builder.AddAppLogger();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAppMiddlewares();
app.UseAppCors();
app.UseAppController();

app.Run();