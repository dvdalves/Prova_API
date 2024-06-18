using Prova_MVC.Configurations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
ConfigurationHelper.ConfigureServices(builder.Services);

var app = builder.Build();

// Configure the HTTP request pipeline.
ConfigurationHelper.Configure(app);

app.Run();
