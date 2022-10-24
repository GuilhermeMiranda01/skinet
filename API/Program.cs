using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using API.Helpers;
using API.Middleware;
using Microsoft.AspNetCore.Mvc;
using API.Controllers.Errors;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductTypeRepository, ProductTypeRepository>();
builder.Services.AddScoped<IProductBrandRepository, ProductBrandRepository>();
builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
builder.Services.AddControllers();
builder.Services.Configure<ApiBehaviorOptions>(opt => 
{
    opt.InvalidModelStateResponseFactory = actionContext =>
    {
        var errors = actionContext.ModelState.Where(e => e.Value.Errors.Count > 0).SelectMany(x => x.Value.Errors).Select(x => x.ErrorMessage).ToArray();
        var errorResponse = new ApiValidationErrorResponse
        {
            Errors = errors
        };

        return new BadRequestObjectResult(errorResponse);
    };
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//ADICIONANDO DB CONTEXT
builder.Services.AddDbContext<StoreContext>(x => x.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));
//ADICIONANDO O MAPPING PROFILE DO AUTOMAPPER
builder.Services.AddAutoMapper(typeof(MappingProfiles));
builder.Services.AddCors(opt => {
    opt.AddPolicy("CorsPolicy", policy =>{
        policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:4200");
    });
});
var app = builder.Build();
app.UseCors("CorsPolicy");
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
app.UseMiddleware<ExceptionMiddleware>();
// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();
app.UseStatusCodePagesWithReExecute("/errors/{0}");
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseStaticFiles();
app.MapControllers();

app.Run();