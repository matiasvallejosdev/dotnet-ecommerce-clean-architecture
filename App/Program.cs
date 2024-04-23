using Microsoft.EntityFrameworkCore;
using App.Data;
using App.Contracts;
using App.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
// Swagger services
builder.Services.AddSwaggerGen();

// Add DbContext
string connectionString = builder.Configuration.GetConnectionString("Default");
if (builder.Environment.IsDevelopment())
{
    connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING") ?? string.Empty;
}
builder.Services.AddDbContext<EntityDataContext>(options =>
{
    options.UseSqlServer(connectionString);
});

// Inject services and repositories
builder.Services.AddScoped<IFamilyService, FamilyService>();
builder.Services.AddScoped<IBrandService, BrandService>();
builder.Services.AddScoped<IProductService, ProductService>();

builder.Services.AddScoped<IFamilyRepository, FamilyRepository>();
builder.Services.AddScoped<IBrandRepository, BrandRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ITagRepository, TagRepository>();

// Lowercase all URLs
builder.Services.AddRouting(options => options.LowercaseUrls = true);

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
