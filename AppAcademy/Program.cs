using AppAcademy.Infrastucture;
using AppAcademy.Application;
using AppAcademy.Middleware;
using AppAcademy.Infrastucture.Identity;
using AppAcademy.Infrastucture.Persistence;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddInfrastuctureServices(builder.Configuration);
builder.Services.AddApplicationService();

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", builder => builder
    .WithOrigins("http://164.92.77.149", "http://164.92.77.149:8000", "http://localhost:4200")
    .AllowAnyMethod()
    .AllowAnyHeader()
    .AllowCredentials()
    );
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppAcademyDbContext>();
    db.Database.Migrate();   // ← aquí

    var services = scope.ServiceProvider;
    await IdentityInitializer.InitializeRoles(services);
    await IdentityInitializer.InitializeAdminUser(services);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionMiddleware>();

app.UseRouting();

app.UseCors("CorsPolicy");

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.UseStaticFiles();

app.Run();
