using Microsoft.EntityFrameworkCore;
using Size.Receivables.Data.Database;
using Size.Receivables.IoC;

var builder = WebApplication.CreateBuilder(args);

builder.Services.DependencyRegistry();

builder.Services.AddDbContext<ReceivablesDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("ConnectionStrings")));

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors("AllowAllOrigins");

app.MapControllers();

app.Run();
