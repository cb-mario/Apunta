using GestorTareas.Models;
using Microsoft.EntityFrameworkCore;

namespace GestorTareas.Data;

public class ApplicationDbContext : DbContext
{
    // La configuración (MySQL y cadena de conexión) llega desde Program.cs
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Tarea> Tareas => Set<Tarea>();
}
