using GestorTareas.Data;
using GestorTareas.Models;
using Microsoft.AspNetCore.Mvc;

namespace GestorTareas.Controllers;

// Uso los métodos síncronos de EF por simplicidad. Con muchos usuarios
// convendría pasar a las versiones async (ToListAsync, SaveChangesAsync...).
public class TareasController : Controller
{
    private readonly ApplicationDbContext _context;

    public TareasController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: /Tareas?estado=Completada
    public IActionResult Index(EstadoTarea? estado)
    {
        // La consulta no se ejecuta hasta el ToList(), así el filtro
        // se añade solo si hace falta y sale un único SELECT
        var tareas = _context.Tareas.AsQueryable();

        if (estado != null)
        {
            tareas = tareas.Where(t => t.Estado == estado);
        }

        // Un solo GROUP BY en vez de tres Count() separados
        var contadores = _context.Tareas
            .GroupBy(t => t.Estado)
            .Select(g => new { Estado = g.Key, Cantidad = g.Count() })
            .ToDictionary(x => x.Estado, x => x.Cantidad);

        var viewModel = new TareasIndexViewModel
        {
            Tareas = tareas.OrderByDescending(t => t.FechaCreacion).ToList(),
            EstadoSeleccionado = estado,
            // Si un estado no tiene tareas no aparece en el diccionario, de ahí el OrDefault
            TotalPendientes = contadores.GetValueOrDefault(EstadoTarea.Pendiente),
            TotalEnProgreso = contadores.GetValueOrDefault(EstadoTarea.EnProgreso),
            TotalCompletadas = contadores.GetValueOrDefault(EstadoTarea.Completada)
        };

        return View(viewModel);
    }

    // GET: /Tareas/Details/5
    public IActionResult Details(int id)
    {
        var tarea = _context.Tareas.Find(id);
        if (tarea == null)
        {
            return NotFound();
        }

        return View(tarea);
    }

    // GET: /Tareas/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: /Tareas/Create
    // Id y FechaCreacion no están en el Bind para que no se puedan colar desde el formulario
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create([Bind("Titulo,Descripcion,Estado,FechaLimite")] Tarea tarea)
    {
        if (!ModelState.IsValid)
        {
            return View(tarea);
        }

        _context.Tareas.Add(tarea);
        _context.SaveChanges();

        // TempData aguanta la redirección, ViewBag no
        TempData["Mensaje"] = $"Tarea \"{tarea.Titulo}\" creada correctamente.";

        // Redirijo (PRG) para que al recargar no se cree la tarea otra vez
        return RedirectToAction(nameof(Index));
    }

    // GET: /Tareas/Edit/5
    public IActionResult Edit(int id)
    {
        var tarea = _context.Tareas.Find(id);
        if (tarea == null)
        {
            return NotFound();
        }

        return View(tarea);
    }

    // POST: /Tareas/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int id, [Bind("Titulo,Descripcion,Estado,FechaLimite")] Tarea tareaEditada)
    {
        if (!ModelState.IsValid)
        {
            // El Id no viene en el formulario y la vista lo necesita para la URL del form
            tareaEditada.Id = id;
            return View(tareaEditada);
        }

        // Cargo la original y copio solo los campos editables. Si guardara
        // tareaEditada tal cual, FechaCreacion se machacaría con la de hoy.
        var tarea = _context.Tareas.Find(id);
        if (tarea == null)
        {
            return NotFound();
        }

        tarea.Titulo = tareaEditada.Titulo;
        tarea.Descripcion = tareaEditada.Descripcion;
        tarea.Estado = tareaEditada.Estado;
        tarea.FechaLimite = tareaEditada.FechaLimite;

        _context.SaveChanges();

        TempData["Mensaje"] = $"Tarea \"{tarea.Titulo}\" actualizada correctamente.";
        return RedirectToAction(nameof(Index));
    }

    // POST: /Tareas/Completar/5
    // Sin confirmación porque se puede deshacer editando la tarea
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Completar(int id)
    {
        var tarea = _context.Tareas.Find(id);
        if (tarea == null)
        {
            return NotFound();
        }

        tarea.Estado = EstadoTarea.Completada;
        _context.SaveChanges();

        TempData["Mensaje"] = $"Tarea \"{tarea.Titulo}\" marcada como completada.";
        return RedirectToAction(nameof(Index));
    }

    // GET: /Tareas/Delete/5 (página de confirmación)
    public IActionResult Delete(int id)
    {
        var tarea = _context.Tareas.Find(id);
        if (tarea == null)
        {
            return NotFound();
        }

        return View(tarea);
    }

    // POST: /Tareas/Delete/5
    // Tiene los mismos parámetros que el GET, por eso cambia de nombre y
    // ActionName mantiene la URL /Tareas/Delete
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(int id)
    {
        var tarea = _context.Tareas.Find(id);
        if (tarea != null)
        {
            _context.Tareas.Remove(tarea);
            _context.SaveChanges();

            TempData["Mensaje"] = $"Tarea \"{tarea.Titulo}\" borrada.";
        }

        return RedirectToAction(nameof(Index));
    }
}
