using Api;
using Api.Configuration;
using FootballPlayers.Infrastructure.Context.Setup;
using FootballPlayers.Players;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

services.AddHttpContextAccessor()
    .AddAppController()
    .AddAppCors()
    .AddAppAutoMappers()
    .AddAppSwagger()
    .AddAppSignalR()
    .RegisterAppServices();
    
builder.AddAppLogger();

var app = builder.Build();

app.UseAppCors();
app.UseAppMiddlewares();
app.UseAppSwagger();
app.UseAppController();
app.UseAppSignalHub();
DbInitializer.Execute(app.Services);
DbSeeder.Execute(app.Services, true);

app.Run();