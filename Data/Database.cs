using Microsoft.Data.Sqlite;

namespace ColeccionApp.Data;

/// <summary>Configura la conexión y crea la base de datos si no existe.</summary>
public static class Database
{
    private static readonly string Ruta =
        Path.Combine(AppContext.BaseDirectory, "coleccion.db");

    public static string ConnectionString => $"Data Source={Ruta}";

    public static void Inicializar()
    {
        using var conn = new SqliteConnection(ConnectionString);
        conn.Open();
        using var cmd = conn.CreateCommand();
        cmd.CommandText = @"
            CREATE TABLE IF NOT EXISTS Productos (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                Nombre TEXT NOT NULL,
                Tipo TEXT NOT NULL,
                Franquicia TEXT,
                Precio REAL NOT NULL,
                FechaAdquisicion TEXT NOT NULL,
                Estado TEXT NOT NULL,
                Notas TEXT
            );";
        cmd.ExecuteNonQuery();
    }
}
