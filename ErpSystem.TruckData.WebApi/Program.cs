using ErpSystem.TruckData.Contracts;
using ErpSystem.TruckData.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<ITruckRepository, InMemoryTruckRepository>();


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
