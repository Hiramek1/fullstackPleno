using Microsoft.EntityFrameworkCore;
using Size.Receivables.Data.Database;
using Size.Receivables.IoC;
using Microsoft.AspNetCore.Cors;

var builder = WebApplication.CreateBuilder(args);

builder.Services.DependencyRegistry();

builder.Services.AddDbContext<ReceivablesDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("ConnectionStrings")));

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        policy => policy.WithOrigins("http://localhost:3005")
                        .AllowAnyHeader()
                        .AllowAnyMethod());
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors("AllowFrontend");

app.MapControllers();

app.Run();
