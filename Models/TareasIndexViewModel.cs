namespace GestorTareas.Models;

// Un ViewModel es una clase pensada solo para llevar a una vista todo lo
// que necesita, aunque venga de sitios distintos. No es una tabla (no hay
// ningún DbSet de ella), así que EF Core no la toca.
// Sustituye al ViewBag: aquí cada dato tiene su tipo y, si en la vista
// escribimos mal un nombre, el compilador nos avisa.
public class TareasIndexViewModel
{
    // Las tareas que se muestran en la tabla (ya filtradas)
    public List<Tarea> Tareas { get; set; } = new();

    // El filtro elegido en el desplegable (null = todos)
    public EstadoTarea? EstadoSeleccionado { get; set; }

    // Contadores para el resumen de arriba. Son siempre sobre TODAS las
    // tareas, no sobre las filtradas, para que el resumen no cambie al filtrar.
    public int TotalPendientes { get; set; }
    public int TotalEnProgreso { get; set; }
    public int TotalCompletadas { get; set; }

    // Propiedad calculada, igual que EstaVencida en Tarea
    public int Total => TotalPendientes + TotalEnProgreso + TotalCompletadas;
}
