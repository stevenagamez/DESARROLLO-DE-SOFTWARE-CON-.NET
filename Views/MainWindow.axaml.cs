using System.Globalization;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media;
using ColeccionApp.Data;
using ColeccionApp.Models;
using ColeccionApp.Services;
using Microsoft.Data.Sqlite;

namespace ColeccionApp.Views;

public partial class MainWindow : Window
{
    private readonly ProductoServicio _servicio = new();
    private int _idSeleccionado = 0;

    public MainWindow()
    {
        InitializeComponent();
        Database.Inicializar();
        CmbTipo.ItemsSource = new[] { "Figura", "Videojuego" };
        CmbEstado.ItemsSource = new[] { "Nuevo", "Usado", "Completado" };
        LimpiarFormulario();
        CargarLista();
    }

    private void CargarLista()
    {
        try { LstProductos.ItemsSource = _servicio.Listar(); }
        catch (SqliteException ex) { Mensaje("Error de base de datos: " + ex.Message, false); }
    }

    // ---- Eventos de los botones ----
    private void BtnGuardar_Click(object? sender, RoutedEventArgs e) =>
        Ejecutar(() => { _servicio.Guardar(LeerFormulario()); }, "Producto guardado correctamente.");

    private void BtnActualizar_Click(object? sender, RoutedEventArgs e) =>
        Ejecutar(() => { var p = LeerFormulario(); p.Id = _idSeleccionado; _servicio.Modificar(p); },
                 "Producto actualizado correctamente.");

    private void BtnEliminar_Click(object? sender, RoutedEventArgs e) =>
        Ejecutar(() => _servicio.Borrar(_idSeleccionado), "Producto eliminado correctamente.");

    private void BtnLimpiar_Click(object? sender, RoutedEventArgs e)
    {
        LimpiarFormulario();
        Mensaje("", true);
    }

    private void LstProductos_SelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        if (LstProductos.SelectedItem is not Producto p) return;
        _idSeleccionado = p.Id;
        TxtNombre.Text = p.Nombre;
        CmbTipo.SelectedItem = p.Tipo;
        TxtFranquicia.Text = p.Franquicia;
        TxtPrecio.Text = p.Precio.ToString("0.00", CultureInfo.InvariantCulture);
        DpFecha.SelectedDate = new DateTimeOffset(p.FechaAdquisicion);
        CmbEstado.SelectedItem = p.Estado;
        TxtNotas.Text = p.Notas;
    }

    // ---- Apoyo ----
    // Ejecuta una operación, captura errores sin cerrar la app y refresca la lista.
    private void Ejecutar(Action operacion, string exito)
    {
        try
        {
            operacion();
            Mensaje(exito, true);
            LimpiarFormulario();
            CargarLista();
        }
        catch (ArgumentException ex) { Mensaje(ex.Message, false); }
        catch (SqliteException ex) { Mensaje("Error de base de datos: " + ex.Message, false); }
    }

    private Producto LeerFormulario()
    {
        var texto = (TxtPrecio.Text ?? "").Trim().Replace(',', '.');
        if (!decimal.TryParse(texto, NumberStyles.Number, CultureInfo.InvariantCulture, out var precio))
            throw new ArgumentException("El precio debe ser un número válido.");
        if (DpFecha.SelectedDate is null)
            throw new ArgumentException("Seleccione la fecha de adquisición.");

        return new Producto
        {
            Nombre = TxtNombre.Text ?? "",
            Tipo = CmbTipo.SelectedItem as string ?? "",
            Franquicia = (TxtFranquicia.Text ?? "").Trim(),
            Precio = precio,
            FechaAdquisicion = DpFecha.SelectedDate.Value.DateTime,
            Estado = CmbEstado.SelectedItem as string ?? "",
            Notas = (TxtNotas.Text ?? "").Trim()
        };
    }

    private void LimpiarFormulario()
    {
        _idSeleccionado = 0;
        LstProductos.SelectedItem = null;
        TxtNombre.Text = TxtFranquicia.Text = TxtPrecio.Text = TxtNotas.Text = "";
        CmbTipo.SelectedIndex = 0;
        CmbEstado.SelectedIndex = 0;
        DpFecha.SelectedDate = DateTimeOffset.Now;
    }

    private void Mensaje(string texto, bool ok)
    {
        TxtMensaje.Text = texto;
        TxtMensaje.Foreground = ok ? Brushes.SeaGreen : Brushes.Firebrick;
    }
}
