using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using GestorTareas.Models;

namespace GestorTareas.Controllers;

public class HomeController : Controller
{
    // La ruta por defecto de Program.cs ({controller=Home}/{action=Index})
    // manda aquí a quien entra en la raíz "/". Como la app solo gestiona
    // tareas, en vez de tener una portada propia lo mandamos al listado.
    public IActionResult Index()
    {
        // Aquí hace falta indicar también el controlador ("Tareas"), porque
        // la acción Index está en otro controlador, no en este.
        return RedirectToAction("Index", "Tareas");
    }

    // Página de error que se muestra cuando la app falla en producción
    // (lo configura app.UseExceptionHandler("/Home/Error") en Program.cs).
    // En desarrollo no se usa: ahí se ve la página de error detallada de .NET.
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
