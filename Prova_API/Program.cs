using Prova_API.Configurations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers().AddJsonOptions(JsonOptionsConfiguration.Configure);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Registro DbContexts
builder.Services.AddCustomDbContexts(builder.Configuration);

// Registro dos repositórios
builder.Services.ResolveDependencies();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();