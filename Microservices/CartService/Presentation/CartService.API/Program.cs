using CartService.API;
using CartService.API.IntegrationEvents.EventHandlers;
using CartService.API.IntegrationEvents.Events;
using CartService.Persistence;
using EventBus.Base.Abstraction;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRabbitMqOnEventBus();
builder.Services.AddJwtAuthentication();
builder.Services.AddPersistenceServices();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddTransient<OrderCreatedIntegrationEventHandler>();



var app = builder.Build();

app.Services.SubscribeOrderCreatedEvent();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();


