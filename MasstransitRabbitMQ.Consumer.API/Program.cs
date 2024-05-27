using MasstransitRabbitMQ.Consumer.API.DependencyInjection.Extentions;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddConfigurationMasstransitRabbitMQ(builder.Configuration);

builder.Services.AddServiceCollection(builder.Configuration);

//Add Serilog
_ = builder.Host.UseSerilog((httppHost, config) =>
     _ = config.ReadFrom.Configuration(builder.Configuration));


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
