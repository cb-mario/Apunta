using GestorTareas.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

// En local la cadena real está en appsettings.Development.json (fuera de git)
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// XAMPP trae MariaDB, no MySQL, y Pomelo necesita saber la versión exacta
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(connectionString, new MariaDbServerVersion(new Version(10, 4, 28))));

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();
