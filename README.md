# Mi Colección de Figuras y Videojuegos

## 1. Información general
- **Proyecto:** ColeccionApp
- **Estudiante:**STEVEN ANDRES AGAMEZ OROZCO
- **Descripción:** aplicación de escritorio para registrar y administrar una colección personal de figuras y videojuegos.
- **Problema que resuelve:** llevar el control de lo que se tiene, cuánto costó, cuándo se adquirió y en qué estado está.
- **Objetivo:** aplicar POO, Avalonia UI y ADO.NET con SQLite en un CRUD completo.

## 2. Funcionalidades
- **Guardar:** registra un producto nuevo.
- **Consultar:** lista todos los productos almacenados.
- **Actualizar:** al seleccionar un elemento se cargan sus datos y se pueden modificar.
- **Eliminar:** borra el elemento seleccionado.
- **Limpiar:** reinicia el formulario.
- **Validaciones:** nombre obligatorio, tipo válido, precio numérico y no negativo, fecha no futura. Los errores se muestran en pantalla sin cerrar la aplicación.

## 3. Tecnologías utilizadas
- .NET 8 (SDK 8.0)
- C#
- Avalonia UI 11.2.3 (`Avalonia`, `Avalonia.Desktop`, `Avalonia.Themes.Fluent`)
- ADO.NET con `Microsoft.Data.Sqlite` 8.0.11
- SQLite

## 4. Estructura del proyecto
```
ColeccionApp/
├── Models/Producto.cs
├── Data/Database.cs
├── Data/ProductoDAO.cs
├── Services/ProductoServicio.cs
├── Views/MainWindow.axaml
├── Views/MainWindow.axaml.cs
├── App.axaml / App.axaml.cs
├── Program.cs
├── ColeccionApp.csproj
├── evidencias/
└── README.md
```

| Clase | Responsabilidad |
|---|---|
| `Producto` | Modelo con propiedades, dos constructores y `ToString()`. |
| `Database` | Cadena de conexión y creación de la base y la tabla. |
| `ProductoDAO` | Operaciones CRUD con ADO.NET. |
| `ProductoServicio` | Validaciones y reglas; intermedia entre la vista y el DAO. |
| `MainWindow` | Interfaz y eventos de los botones. |

**Flujo:** `MainWindow` → `ProductoServicio` → `ProductoDAO` → SQLite.

**POO aplicada:**
- *Encapsulamiento:* campo privado `_nombre` con propiedad que recorta espacios (`Producto`).
- *Constructores y métodos:* `Producto`, `ToString()`, métodos del DAO y del servicio.
- *Separación de responsabilidades:* vista, lógica y acceso a datos en clases distintas.

## 5. Instalación de dependencias
1. Instalar el [SDK de .NET 8](https://dotnet.microsoft.com/download).
2. Clonar el repositorio: `git clone <URL-DEL-REPOSITORIO>`
3. Entrar a la carpeta: `cd ColeccionApp`
4. Restaurar paquetes: `dotnet restore`

No requiere configuración adicional.

## 6. Ejecución del programa
```
cd ColeccionApp
dotnet restore
dotnet run
```

## 7. Base de datos
- **Archivo:** `coleccion.db`, junto al ejecutable. Se crea automáticamente al abrir la app, sin pasos previos.
- **Tabla `Productos`:** `Id` (PK autoincremental), `Nombre`, `Tipo`, `Franquicia`, `Precio`, `FechaAdquisicion`, `Estado`, `Notas`.
- **Conexión:** `SqliteConnection` con la cadena de `Database.ConnectionString`.
- **CRUD:** en `Data/ProductoDAO.cs`, con comandos parametrizados y bloques `using`.
- **Inicialización:** `Database.Inicializar()` ejecuta `CREATE TABLE IF NOT EXISTS`.



## 8. Problemas encontrados y soluciones
 el precio con coma decimal fallaba al convertirse, y se resolvió reemplazando la coma por punto y usando `CultureInfo.InvariantCulture`; o el `DataGrid` requería un paquete aparte, y se usó un `ListBox`.)_
