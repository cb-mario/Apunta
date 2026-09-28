using GestorTareas.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Lee la cadena de conexión de appsettings. En desarrollo, el valor de
// appsettings.Development.json (el que tiene los datos reales y no se
// sube a git) sustituye al placeholder de appsettings.json.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Registra el ApplicationDbContext: aquí se crean las "options" que recibe
// su constructor. A partir de ahora, cualquier controlador que pida un
// ApplicationDbContext en su constructor lo recibirá ya configurado.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    // Pomelo necesita saber la versión exacta del servidor para generar
    // SQL compatible. XAMPP trae MariaDB 10.4, no MySQL.
    options.UseMySql(connectionString, new MariaDbServerVersion(new Version(10, 4, 28))));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
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
