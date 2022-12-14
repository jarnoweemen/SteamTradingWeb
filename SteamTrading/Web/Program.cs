using DAL.Context;
using DAL.DataAccess;
using IntefaceLogic.Model;
using InterfaceDal.Interface;
using InterfaceLogic.Inteface;
using Logic.Container;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddScoped<IUserDa, UserDa>();
builder.Services.AddScoped<IUserContainer, UserContainer>();
builder.Services.AddScoped<ICartDa, CartDa>();
builder.Services.AddScoped<ICartContainer, CartContainer>();

builder.Services.AddControllersWithViews();

builder.Services.AddSession(options =>
    {
        options.IdleTimeout = TimeSpan.FromHours(1);
    }
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
