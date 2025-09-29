using Gestran.Backend.Application.Interfaces;
using Gestran.Backend.Application.Interfaces.Services;
using Gestran.Backend.Application.Services;
using Gestran.Backend.Infrastructure.Persistence;
using Gestran.Backend.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<AppDbContextInitialiser>();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ICheckListCollectionRepository, CheckListCollectionRepository>();
builder.Services.AddScoped<ICheckListRepository, CheckListRepository>();
builder.Services.AddScoped<ICheckListItemTypeRepository, CheckListItemTypeRepository>();

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ICheckListService, CheckListService>();
builder.Services.AddScoped<ICheckListItemTypeService, CheckListItemTypeService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularDev",
        policy =>
        {
            policy.WithOrigins("http://localhost:4200", "https://localhost:4200") // Next.js local
                  .AllowAnyHeader()
                  .AllowAnyMethod()
                  .AllowCredentials();
        });
});


var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var initialiser = scope.ServiceProvider.GetRequiredService<AppDbContextInitialiser>();
    try
    {
        // Aplica migrations automaticamente
        await initialiser.InitialiseAsync();

        // Popula dados do seed
        await initialiser.SeedAsync();
    }
    catch (Exception ex)
    {
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Erro ao inicializar o banco de dados.");
    }
}


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAngularDev");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
