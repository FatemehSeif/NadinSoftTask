using Application.Services.Account;
using Application.Services.Product;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Presistance.Context;
using AutoMapper;

using Infrustructure;
using Application.Contexts;
using Application.Services.Product.CRUD.Commands;
using MediatR;
using Application.Services.Product.CRUD.Queries;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<EmailService>();
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddMediatR(typeof(AddProductCommand).Assembly);
builder.Services.AddMediatR(typeof(GetProductByIdQuery).Assembly);
builder.Services.AddMediatR(typeof(EditProductCommand).Assembly);
builder.Services.AddMediatR(typeof(DeleteProductCommand).Assembly);
builder.Services.AddMediatR(typeof(GetProductsByUserQuery).Assembly);
builder.Services.AddScoped<IAddProductCommand, AddProductCommand>();
builder.Services.AddScoped<IDataBaseContext, DataBaseContext>();
builder.Services.AddScoped<IIdentityDataBaseContext, IdentityDataBaseContext>();
builder.Services.AddScoped<IEditProductCommandHandler, EditProductCommandHandler>();
builder.Services.AddScoped<IDeleteProductCommandHandler, DeleteProductCommandHandler>();
builder.Services.AddScoped<IGetProductsByUserQueryHandler, GetProductsByUserQueryHandler>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


var Connection = builder.Configuration.GetConnectionString("DefaultConnection"); 
builder.Services.AddDbContext<DataBaseContext>(option => option.UseSqlServer(Connection, b => b.MigrationsAssembly("Presistance")));

builder.Services.AddIdentity<IdentityUser, IdentityRole>(p =>
{
    p.User.RequireUniqueEmail =true;

    p.SignIn.RequireConfirmedEmail = false;
}).AddEntityFrameworkStores<IdentityDataBaseContext>()
.AddDefaultTokenProviders();
builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 6;
});

builder.Services.AddDbContext<IdentityDataBaseContext>(option => option.UseSqlServer(Connection, a => a.MigrationsAssembly("Presistance")));

var app = builder.Build();
//using (var scope = app.Services.CreateAsyncScope()) 
//{
//    var dbContext = scope.ServiceProvider.GetRequiredService<DataBaseContext>();
//    await dbContext.Database.MigrateAsync(); 
//}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
