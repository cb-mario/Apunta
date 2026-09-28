# CLAUDE.md

Este archivo da contexto a Claude Code cuando trabaja en este repositorio.

## Qué es este proyecto

"Gestor de Tareas" es un proyecto de **portfolio educativo**, no una app en
producción. Lo está construyendo Mario, estudiante de 2º de DAW, y el
objetivo principal es que **él entienda cada línea de código** para poder
explicarlo en una entrevista técnica. Prioriza siempre la claridad y la
sencillez sobre la elegancia técnica o los patrones avanzados.

## Stack

- **Backend:** ASP.NET Core MVC, C#, .NET 10
- **Vistas:** Razor Views (.cshtml) + Bootstrap 5 vía CDN (sin CSS custom
  complejo, sin frameworks de frontend)
- **ORM:** Entity Framework Core
- **Base de datos:** MySQL, mediante el paquete `Pomelo.EntityFrameworkCore.MySql`
- **Gestión de la BD:** phpMyAdmin en local (no hay Docker en este proyecto)

## Reglas de trabajo

1. **Ir paso a paso.** No generar toda la aplicación de una vez. Completar
   una parte (por ejemplo, el modelo), explicar brevemente qué hace, y
   esperar confirmación antes de continuar con la siguiente.
2. **Comentar el código en español**, con comentarios que aporten
   (explicar el *por qué*, no el *qué* obvio). Nada de comentarios
   genéricos tipo `// constructor`.
3. **Explicar alternativas cuando existan.** Si hay una forma "simple" y
   una forma "más correcta en producción" de hacer algo, mencionar ambas
   y explicar por qué se elige la simple aquí.
4. **No añadir complejidad no pedida**: nada de autenticación, Docker,
   tests automatizados, API REST separada, ni patrones como Repository
   o CQRS. Si Mario los pide más adelante, se añaden entonces.
5. **No subir secretos al repo.** La cadena de conexión con contraseña va
   en `appsettings.Development.json`, que debe estar en `.gitignore`.
   `appsettings.json` solo lleva un placeholder.
6. **Las migraciones de Entity Framework** (`dotnet ef migrations add`,
   `dotnet ef database update`) se proponen como comando, pero es Mario
   quien las ejecuta él mismo para que vea qué hace cada una.

## Estructura de carpetas

```
Controllers/   → TareasController.cs, HomeController.cs
Models/        → Tarea.cs, EstadoTarea.cs (enum)
Data/          → ApplicationDbContext.cs
Views/Tareas/  → Index, Details, Create, Edit, Delete (.cshtml)
Views/Shared/  → _Layout.cshtml
Migrations/    → migraciones de EF Core
```

## Modelo de datos

**Tarea**
- `Id` (int, PK, autoincremental)
- `Titulo` (string, obligatorio, máx. 100 caracteres)
- `Descripcion` (string, opcional)
- `Estado` (enum `EstadoTarea`: Pendiente, EnProgreso, Completada)
- `FechaLimite` (DateTime, opcional)
- `FechaCreacion` (DateTime, se asigna automáticamente al crear)

## Comandos habituales

```bash
dotnet restore              # restaurar paquetes NuGet
dotnet ef migrations add X  # crear una migración llamada X
dotnet ef database update   # aplicar migraciones pendientes a MySQL
dotnet run                  # levantar el proyecto en local
```

## Qué evitar

- No introducir librerías o paquetes NuGet que no sean estrictamente
  necesarios para el CRUD.
- No usar `async/await` de forma inconsistente: si se introduce, explicar
  brevemente por qué en el primer sitio donde aparezca.
- No generar vistas con JavaScript custom — solo HTML/Razor + Bootstrap.
