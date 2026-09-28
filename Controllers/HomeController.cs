using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using GestorTareas.Models;

namespace GestorTareas.Controllers;

public class HomeController : Controller
{
    // No hay portada, la raíz lleva directamente al listado
    public IActionResult Index()
    {
        return RedirectToAction("Index", "Tareas");
    }

    // Solo se usa en producción (UseExceptionHandler en Program.cs)
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
