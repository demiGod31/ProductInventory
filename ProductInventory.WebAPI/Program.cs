using MediatR;
using Microsoft.EntityFrameworkCore;
using ProductInventory.Application.CQRS.Commands;
using ProductInventory.Application.Mappings;
using ProductInventory.Application.Repository;
using ProductInventory.Persistence.EFCore.Context;
using ProductInventory.Persistence.EFCore.Repository;
using ProductInventory.WebAPI.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddAutoMapper(typeof(ProductProfile));
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssemblyContaining<CreateProduct.Handler>();

    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
});

// Add CORS services
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin", builder =>
    {
        builder.WithOrigins("https://productinventory.com")
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("AllowSpecificOrigin");
app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
