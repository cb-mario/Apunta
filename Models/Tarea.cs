using System.ComponentModel.DataAnnotations;

namespace GestorTareas.Models;

// Esta clase es a la vez el modelo de la app y la "plantilla" de la tabla:
// EF Core creará una tabla Tareas con una columna por cada propiedad.
// Los atributos entre corchetes ([Required], [StringLength]...) sirven
// para dos cosas: validar el formulario y definir cómo es la columna.
public class Tarea
{
    // Por convención, EF Core sabe que una propiedad llamada "Id" es la
    // clave primaria, y al ser int la hace autoincremental en MySQL.
    public int Id { get; set; }

    [Required(ErrorMessage = "El título es obligatorio.")]
    [StringLength(100, ErrorMessage = "El título no puede tener más de 100 caracteres.")]
    [Display(Name = "Título")]
    public string Titulo { get; set; } = string.Empty;

    // El "?" indica que puede ser null, es decir, que es opcional.
    // Al no ponerle límite de longitud, en MySQL será de tipo longtext.
    [Display(Name = "Descripción")]
    public string? Descripcion { get; set; }

    // Si no se elige nada, toma el primer valor del enum (Pendiente).
    public EstadoTarea Estado { get; set; } = EstadoTarea.Pendiente;

    // DateTime? (con "?") porque una tarea puede no tener fecha límite.
    // DataType.Date hace que el formulario muestre un selector de fecha
    // sin la hora, que aquí no nos interesa.
    [DataType(DataType.Date)]
    [Display(Name = "Fecha límite")]
    public DateTime? FechaLimite { get; set; }

    // Se rellena sola con la fecha y hora del momento en que se crea el
    // objeto. El usuario nunca la escribe en el formulario.
    [Display(Name = "Fecha de creación")]
    public DateTime FechaCreacion { get; set; } = DateTime.Now;

    // Propiedad calculada: no se guarda, se calcula cada vez que se lee a
    // partir de otras propiedades. Como solo tiene "get" (no tiene "set"),
    // EF Core no crea ninguna columna para ella en la base de datos.
    // Está en el modelo y no en la vista porque es una regla del negocio
    // ("qué significa estar vencida"), y así la puede usar cualquier vista.
    public bool EstaVencida =>
        FechaLimite != null
        && FechaLimite.Value.Date < DateTime.Today
        && Estado != EstadoTarea.Completada;
}
