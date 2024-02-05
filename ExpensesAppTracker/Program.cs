using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ExpensesAppTracker.Data;
using Microsoft.EntityFrameworkCore.Storage;
using ExpensesAppTracker.Models;
using ExpensesTrackerApp.Models;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ExpensesAppTrackerContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ExpensesAppTrackerContext") ?? throw new InvalidOperationException("Connection string 'ExpensesAppTrackerContext' not found.")));

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login"; // Œcie¿ka do akcji logowania
        options.AccessDeniedPath = "/Account/AccessDenied"; // Œcie¿ka do akcji dostêpu zabronionego
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("RequireAdmin", policy => policy.RequireRole("Admin"));
    options.AddPolicy("RequireAdmin", policy => policy.RequireRole("User"));
    // Dodaj inne polityki uprawnieñ wed³ug potrzeb
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    SeedData.Initialize(services);
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=ExpenseItems}/{action=Index}/{id?}")
    .RequireAuthorization();

app.Run();
