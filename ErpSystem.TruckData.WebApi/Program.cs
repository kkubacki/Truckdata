using ErpSystem.TruckData.Application;
using ErpSystem.TruckData.Contracts;
using ErpSystem.TruckData.Infrastructure;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("ConnectionString");

builder.Services.AddDbContext<TruckDataDbContext>(options =>
{
    options.UseSqlServer(connectionString);
});


builder.Services.AddScoped<ITruckRepository, TruckEfRepository>();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>(x =>
{
    var context = x.GetRequiredService<TruckDataDbContext>();

    return new UnitOfWork(context);
});

builder.Services.AddScoped<ITruckValidationApplicationService, TruckValidationApplicationService>();
builder.Services.AddScoped<ITruckApplicationService, TruckApplicationService>();


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
