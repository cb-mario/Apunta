using System.ComponentModel.DataAnnotations;

namespace GestorTareas.Models;

public class Tarea
{
    public int Id { get; set; }

    [Required(ErrorMessage = "El título es obligatorio.")]
    [StringLength(100, ErrorMessage = "El título no puede tener más de 100 caracteres.")]
    [Display(Name = "Título")]
    public string Titulo { get; set; } = string.Empty;

    [Display(Name = "Descripción")]
    public string? Descripcion { get; set; }

    public EstadoTarea Estado { get; set; } = EstadoTarea.Pendiente;

    [DataType(DataType.Date)]
    [Display(Name = "Fecha límite")]
    public DateTime? FechaLimite { get; set; }

    [Display(Name = "Fecha de creación")]
    public DateTime FechaCreacion { get; set; } = DateTime.Now;

    // Solo tiene get, así que EF no crea columna: se calcula al leerla
    public bool EstaVencida =>
        FechaLimite != null
        && FechaLimite.Value.Date < DateTime.Today
        && Estado != EstadoTarea.Completada;
}
