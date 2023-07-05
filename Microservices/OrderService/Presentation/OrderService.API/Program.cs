using Microsoft.AspNetCore.Connections;
using OrderService.API;
using OrderService.API.Extensions.EventHandlerRegistration;
using OrderService.Application;
using OrderService.Infrastucture;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRabbitMqOnEventBus();
builder.Services.AddEventHandlerServices();
builder.Services.AddInfrastuctureServices();
builder.Services.AddApplicationServices();
builder.Services.AddJwtAuthentication();
//
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

app.Services.ConfigureEventBusForSubs();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

var factory = new ConnectionFactory() { HostName = "localhost" };
using (var connection = factory.CreateConnection())
using (var channel = connection.CreateModel())
{
    channel.QueueDeclare(queue: "CartCheckOut",
        durable: false,
        exclusive: false,
        autoDelete: false,
        arguments: null);

    var consumer = new EventingBasicConsumer(channel);
    consumer.Received += (model, ea) =>
    {
        var body = ea.Body.ToArray();
        var message = Encoding.UTF8.GetString(body);

    };

    channel.BasicConsume(queue: "CartCheckOut", autoAck: true, consumer: consumer);

}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
