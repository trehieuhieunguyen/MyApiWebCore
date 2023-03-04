using Microsoft.EntityFrameworkCore;
using MyApiWebCore.Data;
using MyApiWebCore.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(option => option.AddDefaultPolicy(policy => 
    policy.AllowCredentials().AllowAnyHeader().AllowAnyMethod()));

builder.Services.AddDbContext<ProductStoreContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("ProductStore"));
});
builder.Services.AddAutoMapper(typeof(Program));
    
// life cycle DI: Addsingleton(), AddTransient(), AddScoped()
builder.Services.AddScoped<IProductRepository, ProductRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
