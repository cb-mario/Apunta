# 📋 Gestor de Tareas

Aplicación web sencilla de gestión de tareas (CRUD) hecha con **ASP.NET Core MVC (C#)** y **MySQL**, como proyecto de portfolio.

Proyecto pensado para practicar y demostrar el patrón **Modelo-Vista-Controlador**, el uso de **Entity Framework Core** como ORM, y operaciones CRUD completas contra una base de datos relacional.

## ✨ Funcionalidades

- Listar todas las tareas, con filtro por estado (Pendiente / En progreso / Completada)
- Ver el detalle de una tarea
- Crear una nueva tarea
- Editar una tarea existente
- Borrar una tarea (con confirmación previa)

## 🛠️ Tecnologías

| Capa           | Tecnología                                      |
|----------------|--------------------------------------------------|
| Backend        | C# · ASP.NET Core MVC (.NET 10)                   |
| Vistas         | Razor Views (.cshtml) · Bootstrap 5              |
| ORM            | Entity Framework Core                            |
| Base de datos  | MySQL (gestionada con phpMyAdmin en local)       |
| Paquete MySQL  | Pomelo.EntityFrameworkCore.MySql                 |

## 📂 Estructura del proyecto

```
GestorTareas/
├── Controllers/
│   └── TareasController.cs
├── Models/
│   ├── Tarea.cs
│   └── EstadoTarea.cs
├── Data/
│   └── ApplicationDbContext.cs
├── Views/
│   ├── Tareas/
│   │   ├── Index.cshtml
│   │   ├── Details.cshtml
│   │   ├── Create.cshtml
│   │   ├── Edit.cshtml
│   │   └── Delete.cshtml
│   └── Shared/
│       └── _Layout.cshtml
├── Migrations/
├── appsettings.json
└── Program.cs
```

## 🚀 Puesta en marcha

### Requisitos previos

- [.NET SDK 10.0](https://dotnet.microsoft.com/download) o superior
- MySQL Server (por ejemplo, vía XAMPP/Laragon) y phpMyAdmin
- (Opcional) Visual Studio, Rider o VS Code con la extensión de C#

### 1. Clonar el repositorio

```bash
git clone https://github.com/<tu-usuario>/gestor-tareas.git
cd gestor-tareas
```

### 2. Configurar la base de datos

Crea una base de datos vacía en phpMyAdmin, por ejemplo `gestor_tareas`.

Edita `appsettings.json` (o mejor, crea un `appsettings.Development.json`
que no se sube a git) con tu cadena de conexión:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Port=3306;Database=gestor_tareas;User=root;Password=TU_PASSWORD;"
  }
}
```

### 3. Restaurar dependencias y aplicar migraciones

```bash
dotnet restore
dotnet ef database update
```

### 4. Ejecutar el proyecto

```bash
dotnet run
```

La aplicación estará disponible en `https://localhost:5001` (o el puerto que indique la consola).

## 🖼️ Capturas

_(Pendiente: añadir capturas del listado, formulario de creación y edición)_

## 📌 Motivación del proyecto

Este proyecto forma parte de mi portfolio como estudiante de Desarrollo de
Aplicaciones Web (DAW). Su objetivo es demostrar de forma clara y sin
complejidad innecesaria:

- Cómo se estructura una aplicación siguiendo el patrón MVC
- Cómo se modela una entidad y se persiste con Entity Framework Core
- Cómo se implementa un CRUD completo con validaciones básicas
- Buenas prácticas simples: separación de responsabilidades, nombres
  descriptivos, y no subir credenciales al repositorio

## 🔜 Posibles mejoras (v2)

- [ ] Autenticación de usuarios (login/registro)
- [ ] Tests unitarios con xUnit
- [ ] Paginación en el listado
- [ ] Despliegue en un hosting gratuito (Railway, Render...)

## 👤 Autor

**Mario Cerdá** — Desarrollador Full Stack
[LinkedIn](https://linkedin.com) · cerbano.m@gmail.com
