using Microsoft.EntityFrameworkCore;
using ProductService.Data;
using Microsoft.OpenApi.Models;
using ProductService.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddDbContext<ProductServiceDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// Register EntityService
builder.Services.AddScoped<EntityService>();

// Add AutoMapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Add Swagger services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "ProductService API", Version = "v1" });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "ProductService API v1");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

try
{
    DataSeeder.InitData(app);
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}

app.Run();
