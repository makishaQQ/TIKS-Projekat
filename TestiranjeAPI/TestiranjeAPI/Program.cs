using Microsoft.EntityFrameworkCore;
using TestiranjeAPI.Models;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<PartyContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("BazaCS"));
});
builder.Services.AddCors(options =>
{
    options.AddPolicy("CORS", builder =>
    {
        builder.SetIsOriginAllowed(host => true).
        AllowAnyHeader()
        .AllowAnyMethod();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("CORS");

app.UseAuthorization();

app.MapControllers();

app.Run();
