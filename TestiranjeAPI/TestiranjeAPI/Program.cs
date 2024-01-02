using Microsoft.EntityFrameworkCore;
using TestiranjeAPI.IRepository;
using TestiranjeAPI.Models;
using TestiranjeAPI.Repository;

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
        builder.WithOrigins(new string[] {"http://127.0.0.1:5500"})
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IPartyRepository, PartyRepository>();

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
