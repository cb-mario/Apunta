using System.ComponentModel.DataAnnotations;

namespace GestorTareas.Models;

// Un enum limita el estado a estos tres valores: así es imposible guardar
// algo como "Terminada" o "pendiente" (con minúscula) por error.
// En la base de datos se guarda como número (0, 1, 2), no como texto.
public enum EstadoTarea
{
    // Pongo los números a mano para que, si algún día se reordena la lista,
    // las tareas que ya están guardadas no cambien de estado sin querer.
    Pendiente = 0,

    // En C# no puede haber espacios en un nombre, así que [Display] indica
    // el texto "bonito" que se mostrará en las vistas y en el desplegable.
    [Display(Name = "En progreso")]
    EnProgreso = 1,

    Completada = 2
}
