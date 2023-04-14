using Arduino.Business.Abstract;
using Arduino.Business.Concrete;
using Arduino.DataAccess.Abstract;
using Arduino.DataAccess.Concrete;
using Swashbuckle.AspNetCore.SwaggerUI;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddSingleton<IArduinoService, ArduinoManagenment>();
builder.Services.AddSingleton<IDataRepository, DataRepository>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerDocument(config =>
{
    config.PostProcess = (doc =>
    {
        doc.Info.Title = "Arduino Data Api";
        doc.Info.Version="1.0";
        doc.Info.Contact = new NSwag.OpenApiContact()
        {
            Name = "Gokhan DONMEZ",
            Email = "gokhan01donmez@gmail.com",
        };
    });
    
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
   app.UseOpenApi();
   //app.UseSwagger();
   app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
