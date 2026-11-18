using ColeccionApp.Models;
using Microsoft.Data.Sqlite;

namespace ColeccionApp.Data;

/// <summary>Acceso a datos (CRUD) con ADO.NET y comandos parametrizados.</summary>
public class ProductoDAO
{
    public void Insertar(Producto p)
    {
        using var conn = new SqliteConnection(Database.ConnectionString);
        conn.Open();
        using var cmd = conn.CreateCommand();
        cmd.CommandText = @"INSERT INTO Productos
            (Nombre, Tipo, Franquicia, Precio, FechaAdquisicion, Estado, Notas)
            VALUES ($n, $t, $f, $p, $fe, $e, $no);";
        Parametros(cmd, p);
        cmd.ExecuteNonQuery();
    }

    public List<Producto> ObtenerTodos()
    {
        var lista = new List<Producto>();
        using var conn = new SqliteConnection(Database.ConnectionString);
        conn.Open();
        using var cmd = conn.CreateCommand();
        cmd.CommandText = "SELECT * FROM Productos ORDER BY Nombre;";
        using var r = cmd.ExecuteReader();
        while (r.Read())
        {
            lista.Add(new Producto(
                r.GetInt32(r.GetOrdinal("Id")),
                r.GetString(r.GetOrdinal("Nombre")),
                r.GetString(r.GetOrdinal("Tipo")),
                r.IsDBNull(r.GetOrdinal("Franquicia")) ? "" : r.GetString(r.GetOrdinal("Franquicia")),
                (decimal)r.GetDouble(r.GetOrdinal("Precio")),
                DateTime.Parse(r.GetString(r.GetOrdinal("FechaAdquisicion"))),
                r.GetString(r.GetOrdinal("Estado")),
                r.IsDBNull(r.GetOrdinal("Notas")) ? "" : r.GetString(r.GetOrdinal("Notas"))));
        }
        return lista;
    }

    public void Actualizar(Producto p)
    {
        using var conn = new SqliteConnection(Database.ConnectionString);
        conn.Open();
        using var cmd = conn.CreateCommand();
        cmd.CommandText = @"UPDATE Productos SET Nombre=$n, Tipo=$t, Franquicia=$f,
            Precio=$p, FechaAdquisicion=$fe, Estado=$e, Notas=$no WHERE Id=$id;";
        Parametros(cmd, p);
        cmd.Parameters.AddWithValue("$id", p.Id);
        cmd.ExecuteNonQuery();
    }

    public void Eliminar(int id)
    {
        using var conn = new SqliteConnection(Database.ConnectionString);
        conn.Open();
        using var cmd = conn.CreateCommand();
        cmd.CommandText = "DELETE FROM Productos WHERE Id=$id;";
        cmd.Parameters.AddWithValue("$id", id);
        cmd.ExecuteNonQuery();
    }

    private static void Parametros(SqliteCommand cmd, Producto p)
    {
        cmd.Parameters.AddWithValue("$n", p.Nombre);
        cmd.Parameters.AddWithValue("$t", p.Tipo);
        cmd.Parameters.AddWithValue("$f", p.Franquicia);
        cmd.Parameters.AddWithValue("$p", (double)p.Precio);
        cmd.Parameters.AddWithValue("$fe", p.FechaAdquisicion.ToString("yyyy-MM-dd"));
        cmd.Parameters.AddWithValue("$e", p.Estado);
        cmd.Parameters.AddWithValue("$no", p.Notas);
    }
}
