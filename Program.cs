using Microsoft.AspNetCore.SignalR;
using signalr_discount_code_handler;
using static signalr_discount_code_handler.CodesHub;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<CodeService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSignalR();

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

app.MapHub<CodesHub>("codes-hub");

app.Run();
