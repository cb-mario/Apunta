namespace GestorTareas.Models;

// Todo lo que necesita la vista del listado. No es una tabla, solo sirve
// para pasar los datos a Index.cshtml.
public class TareasIndexViewModel
{
    public List<Tarea> Tareas { get; set; } = new();

    // null = sin filtro
    public EstadoTarea? EstadoSeleccionado { get; set; }

    // Siempre sobre todas las tareas, aunque haya filtro
    public int TotalPendientes { get; set; }
    public int TotalEnProgreso { get; set; }
    public int TotalCompletadas { get; set; }

    public int Total => TotalPendientes + TotalEnProgreso + TotalCompletadas;
}
