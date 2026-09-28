using GestorTareas.Data;
using GestorTareas.Models;
using Microsoft.AspNetCore.Mvc;

namespace GestorTareas.Controllers;

// Todas las URLs que empiezan por /Tareas llegan a este controlador.
// Cada método público es una "acción": /Tareas/Create llama a Create(), etc.
//
// Uso métodos síncronos (ToList, Find, SaveChanges) porque se leen más fácil.
// En una app real se usarían sus versiones async (ToListAsync, FindAsync,
// SaveChangesAsync) para que el servidor no se quede bloqueado esperando a
// la base de datos y pueda atender a otros usuarios mientras tanto.
public class TareasController : Controller
{
    private readonly ApplicationDbContext _context;

    // No creamos el contexto con "new": ASP.NET nos lo pasa ya configurado
    // gracias al AddDbContext de Program.cs (inyección de dependencias).
    public TareasController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: /Tareas  o  /Tareas?estado=Completada
    // El parámetro "estado" llega del desplegable de filtro. Es opcional (?):
    // si no viene, se muestran todas las tareas.
    public IActionResult Index(EstadoTarea? estado)
    {
        // Mientras no se llame a ToList(), la consulta no se ejecuta: solo se
        // va construyendo. Así podemos añadir el Where solo si hace falta y
        // EF genera un único SELECT con todo al final.
        var tareas = _context.Tareas.AsQueryable();

        if (estado != null)
        {
            tareas = tareas.Where(t => t.Estado == estado);
        }

        // Guardamos el filtro elegido para que el desplegable lo siga
        // mostrando seleccionado después de filtrar.
        ViewBag.EstadoSeleccionado = estado;

        // Las más recientes primero.
        return View(tareas.OrderByDescending(t => t.FechaCreacion).ToList());
    }

    // GET: /Tareas/Details/5
    public IActionResult Details(int id)
    {
        // Find busca por clave primaria y devuelve null si no existe.
        var tarea = _context.Tareas.Find(id);
        if (tarea == null)
        {
            return NotFound();
        }

        return View(tarea);
    }

    // GET: /Tareas/Create
    // Solo muestra el formulario vacío.
    public IActionResult Create()
    {
        return View();
    }

    // POST: /Tareas/Create
    // Recibe los datos cuando se envía el formulario.
    // [Bind] indica qué campos se aceptan del formulario. Id y FechaCreacion
    // no están en la lista para que nadie pueda colarlos modificando el HTML:
    // el Id lo pone MySQL y la fecha se rellena sola (ver Tarea.cs).
    // [ValidateAntiForgeryToken] comprueba que el formulario viene de nuestra
    // propia web y no de otra página que intente enviarlo en tu nombre.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create([Bind("Titulo,Descripcion,Estado,FechaLimite")] Tarea tarea)
    {
        // ModelState.IsValid comprueba los atributos del modelo ([Required],
        // [StringLength]...). Si algo falla, se vuelve a mostrar el formulario
        // con los datos que había escrito el usuario y los mensajes de error.
        if (!ModelState.IsValid)
        {
            return View(tarea);
        }

        _context.Tareas.Add(tarea);
        _context.SaveChanges(); // aquí es cuando se ejecuta el INSERT

        // Redirigimos en vez de devolver una vista para que, si el usuario
        // recarga la página, el navegador no reenvíe el formulario y cree
        // la tarea dos veces.
        return RedirectToAction(nameof(Index));
    }

    // GET: /Tareas/Edit/5
    // Muestra el formulario relleno con los datos actuales de la tarea.
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
            // El formulario no envía el Id, así que lo ponemos para que la
            // vista sepa a qué tarea pertenece al volver a mostrarse.
            tareaEditada.Id = id;
            return View(tareaEditada);
        }

        // Cargamos la tarea original de la base de datos y solo copiamos los
        // campos editables. Así FechaCreacion no se toca: si guardáramos
        // directamente "tareaEditada", se sobrescribiría con la fecha de hoy.
        var tarea = _context.Tareas.Find(id);
        if (tarea == null)
        {
            return NotFound();
        }

        tarea.Titulo = tareaEditada.Titulo;
        tarea.Descripcion = tareaEditada.Descripcion;
        tarea.Estado = tareaEditada.Estado;
        tarea.FechaLimite = tareaEditada.FechaLimite;

        // EF detecta solo qué propiedades han cambiado y genera el UPDATE.
        _context.SaveChanges();

        return RedirectToAction(nameof(Index));
    }

    // GET: /Tareas/Delete/5
    // No borra nada: solo muestra la página de "¿Seguro que quieres borrarla?".
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
    // En C# no puede haber dos métodos con el mismo nombre y los mismos
    // parámetros, así que este se llama DeleteConfirmed. ActionName("Delete")
    // hace que siga respondiendo a la URL /Tareas/Delete.
    // Borrar siempre por POST, nunca por GET: un enlace (GET) lo puede abrir
    // un buscador o el navegador al precargar páginas y borrar datos sin querer.
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(int id)
    {
        var tarea = _context.Tareas.Find(id);
        if (tarea != null)
        {
            _context.Tareas.Remove(tarea);
            _context.SaveChanges(); // aquí se ejecuta el DELETE
        }

        return RedirectToAction(nameof(Index));
    }
}
