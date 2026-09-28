using System.ComponentModel.DataAnnotations;

namespace GestorTareas.Models;

// Se guarda en la BD como número. Los valores van fijados a mano para que
// reordenar el enum no cambie el estado de las tareas ya guardadas.
public enum EstadoTarea
{
    Pendiente = 0,

    [Display(Name = "En progreso")]
    EnProgreso = 1,

    Completada = 2
}
