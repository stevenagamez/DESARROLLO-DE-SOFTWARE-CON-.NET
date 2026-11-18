using ColeccionApp.Data;
using ColeccionApp.Models;

namespace ColeccionApp.Services;

/// <summary>Reglas de negocio y validaciones; delega la persistencia al DAO.</summary>
public class ProductoServicio
{
    private readonly ProductoDAO _dao = new();

    public List<Producto> Listar() => _dao.ObtenerTodos();

    public void Guardar(Producto p)
    {
        Validar(p);
        _dao.Insertar(p);
    }

    public void Modificar(Producto p)
    {
        if (p.Id <= 0) throw new ArgumentException("Seleccione un elemento de la lista para modificar.");
        Validar(p);
        _dao.Actualizar(p);
    }

    public void Borrar(int id)
    {
        if (id <= 0) throw new ArgumentException("Seleccione un elemento de la lista para eliminar.");
        _dao.Eliminar(id);
    }

    private static void Validar(Producto p)
    {
        if (string.IsNullOrWhiteSpace(p.Nombre))
            throw new ArgumentException("El nombre es obligatorio.");
        if (p.Tipo != "Figura" && p.Tipo != "Videojuego")
            throw new ArgumentException("Seleccione un tipo válido (Figura o Videojuego).");
        if (p.Precio < 0)
            throw new ArgumentException("El precio no puede ser negativo.");
        if (p.FechaAdquisicion.Date > DateTime.Today)
            throw new ArgumentException("La fecha de adquisición no puede ser futura.");
        if (string.IsNullOrWhiteSpace(p.Estado))
            throw new ArgumentException("Seleccione un estado.");
    }
}
