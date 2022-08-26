using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using API.Helpers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductTypeRepository, ProductTypeRepository>();
builder.Services.AddScoped<IProductBrandRepository, ProductBrandRepository>();
builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//ADICIONANDO DB CONTEXT
builder.Services.AddDbContext<StoreContext>(x => x.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));
//ADICIONANDO O MAPPING PROFILE DO AUTOMAPPER
builder.Services.AddAutoMapper(typeof(MappingProfiles));
var app = builder.Build();
using(var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var loggerFactory = services.GetRequiredService<ILoggerFactory>(); 
    try
    {
        var context = services.GetRequiredService<StoreContext>();
        //ADICIONANDO MIGRATIONS ASINCRONAS
        await context.Database.MigrateAsync();
        //ADICIONANDO O SEEDING
        await StoreContextSeed.SeedAsync(context, loggerFactory);
    }
    catch(Exception ex)
    {
        var logger = loggerFactory.CreateLogger<Program>();
        logger.LogError(ex, "Deu erro nas migrations");
    }
}
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseStaticFiles();
app.MapControllers();

app.Run();