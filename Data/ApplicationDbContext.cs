using GestorTareas.Models;
using Microsoft.EntityFrameworkCore;

namespace GestorTareas.Data;

// El DbContext representa la base de datos dentro de la app: es la clase
// a través de la cual se consultan, crean, editan y borran las tareas.
// Hereda de DbContext (de EF Core), que ya trae todo el trabajo de
// conectarse y traducir a SQL; aquí solo decimos qué tablas hay.
public class ApplicationDbContext : DbContext
{
    // Las opciones (qué base de datos usar y su cadena de conexión) no se
    // escriben aquí, sino en Program.cs, y ASP.NET las pasa al crear el
    // contexto. Así esta clase no sabe si detrás hay MySQL u otra base de
    // datos, y la contraseña no queda escrita en el código.
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    // Cada DbSet es una tabla. EF Core crea la tabla "Tareas" a partir de
    // la clase Tarea, y en el controlador se usará como _context.Tareas.
    // Set<Tarea>() pide la tabla a EF Core cada vez que se usa, así la
    // propiedad nunca es null y el compilador no da avisos.
    public DbSet<Tarea> Tareas => Set<Tarea>();
}
