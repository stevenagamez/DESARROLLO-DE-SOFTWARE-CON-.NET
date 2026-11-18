namespace ColeccionApp.Models;

/// <summary>Representa una figura o videojuego de la colección.</summary>
public class Producto
{
    private string _nombre = "";

    public int Id { get; set; }

    // Encapsulamiento: el nombre siempre se guarda sin espacios sobrantes.
    public string Nombre
    {
        get => _nombre;
        set => _nombre = (value ?? "").Trim();
    }

    public string Tipo { get; set; } = "Figura";
    public string Franquicia { get; set; } = "";
    public decimal Precio { get; set; }
    public DateTime FechaAdquisicion { get; set; } = DateTime.Today;
    public string Estado { get; set; } = "Nuevo";
    public string Notas { get; set; } = "";

    public Producto() { }

    public Producto(int id, string nombre, string tipo, string franquicia,
                    decimal precio, DateTime fecha, string estado, string notas)
    {
        Id = id; Nombre = nombre; Tipo = tipo; Franquicia = franquicia;
        Precio = precio; FechaAdquisicion = fecha; Estado = estado; Notas = notas;
    }

    // Lo usa el ListBox para mostrar cada elemento.
    public override string ToString() =>
        $"[{Tipo}] {Nombre} - {Franquicia} | ${Precio:N2} | {Estado} | {FechaAdquisicion:yyyy-MM-dd}";
}
