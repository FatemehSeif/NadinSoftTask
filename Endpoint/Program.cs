using Application.Services.Account;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Presistance.Context;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<EmailService>();

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
