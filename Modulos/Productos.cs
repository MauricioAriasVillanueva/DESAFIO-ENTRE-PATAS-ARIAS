using System;
using System.Collections.Generic;
using System.Globalization;

partial class Program
{
    static List<int> codigosProductos = new();
    static List<string> nombresProductos = new();
    static List<decimal> preciosProductos = new();
    static List<int> stocksProductos = new();
    static List<int> codigosProductosVentas = new();

    // Utilidades compartidas del Pet Shop: entrada, confirmacion y codigos.
    static string LeerPetShop(string mensaje)
    {
        Console.Write(mensaje);
        return (Console.ReadLine() ?? "").Trim();
    }

    static bool ConfirmarPetShop(string mensaje)
    {
        string respuesta = LeerPetShop(mensaje).ToUpperInvariant();
        return respuesta == "S" || respuesta == "SI";
    }

    static int CodigoPetShop(List<int> codigos, int minimo)
    {
        int mayor = minimo;
        foreach (int codigo in codigos) if (codigo > mayor) mayor = codigo;
        return mayor == int.MaxValue ? -1 : mayor + 1;
    }

    static int BuscarPosicionProducto(int codigo) => codigosProductos.IndexOf(codigo);
    static int SiguienteCodigoProducto() => CodigoPetShop(codigosProductos, 4000);

    static int SolicitarProducto()
    {
        if (!int.TryParse(LeerPetShop("Codigo de producto: "), out int codigo) || codigo <= 0)
        {
            Console.WriteLine("Codigo invalido.");
            return -1;
        }
        int i = BuscarPosicionProducto(codigo);
        if (i == -1) Console.WriteLine("Producto no encontrado.");
        return i;
    }

    static bool LeerDatosProducto(out string nombre, out decimal precio, out int stock)
    {
        nombre = LeerPetShop("Nombre del producto: ");
        precio = 0;
        stock = 0;
        if (nombre == "") { Console.WriteLine("El nombre es obligatorio."); return false; }
        if (!decimal.TryParse(LeerPetShop("Precio (punto decimal): "),
            NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out precio) || precio <= 0)
        { Console.WriteLine("Precio invalido; debe ser mayor que cero."); return false; }
        if (!int.TryParse(LeerPetShop("Stock: "), out stock) || stock < 0)
        { Console.WriteLine("El stock debe ser un entero no negativo."); return false; }
        return true;
    }

    static void MostrarDatosProducto(int i)
    {
        Console.WriteLine($"\nCodigo: {codigosProductos[i]} | Producto: {nombresProductos[i]}");
        Console.WriteLine($"Precio: S/ {preciosProductos[i].ToString("0.00", CultureInfo.InvariantCulture)} | Stock: {stocksProductos[i]}");
    }

    static void RegistrarProducto()
    {
        Console.WriteLine("\n========== REGISTRAR PRODUCTO ==========");
        if (!LeerDatosProducto(out string nombre, out decimal precio, out int stock)) return;
        int codigo = SiguienteCodigoProducto();
        if (codigo == -1) { Console.WriteLine("No hay codigos disponibles."); return; }
        Console.WriteLine($"Producto: {nombre} | Precio: {precio} | Stock: {stock}");
        if (!ConfirmarPetShop("Guardar producto? (S/N): "))
        { Console.WriteLine("Registro cancelado."); return; }
        codigosProductos.Add(codigo);
        nombresProductos.Add(nombre);
        preciosProductos.Add(precio);
        stocksProductos.Add(stock);
        Console.WriteLine("Producto registrado. Codigo: " + codigo);
    }

    static void BuscarProducto()
    {
        Console.WriteLine("\n1. Buscar por codigo  2. Buscar por nombre  0. Volver");
        int opcion = LeerOpcion();
        if (opcion == 0) return;
        if (opcion == 1)
        {
            int i = SolicitarProducto();
            if (i >= 0) MostrarDatosProducto(i);
            return;
        }
        if (opcion != 2) { Console.WriteLine("Opcion invalida."); return; }
        string nombre = LeerPetShop("Nombre a buscar: ");
        if (nombre == "") { Console.WriteLine("Ingrese un nombre."); return; }
        bool encontrado = false;
        for (int i = 0; i < nombresProductos.Count; i++)
        {
            if (nombresProductos[i].IndexOf(nombre, StringComparison.OrdinalIgnoreCase) < 0) continue;
            MostrarDatosProducto(i);
            encontrado = true;
        }
        if (!encontrado) Console.WriteLine("No se encontraron productos.");
    }

    static void ModificarProducto()
    {
        int i = SolicitarProducto();
        if (i < 0) return;
        MostrarDatosProducto(i);
        if (!LeerDatosProducto(out string nombre, out decimal precio, out int stock)) return;
        if (!ConfirmarPetShop("Confirmar modificacion? (S/N): "))
        { Console.WriteLine("Modificacion cancelada."); return; }
        nombresProductos[i] = nombre;
        preciosProductos[i] = precio;
        stocksProductos[i] = stock;
        Console.WriteLine("Producto modificado correctamente.");
    }

    static void EliminarProducto()
    {
        int i = SolicitarProducto();
        if (i < 0) return;
        if (codigosProductosVentas.Contains(codigosProductos[i]) ||
            codigosProductosVendidos.Contains(codigosProductos[i]))
        { Console.WriteLine("No se puede eliminar: el producto tiene ventas asociadas."); return; }
        MostrarDatosProducto(i);
        if (!ConfirmarPetShop("Confirmar eliminacion? (S/N): "))
        { Console.WriteLine("Eliminacion cancelada."); return; }
        codigosProductos.RemoveAt(i);
        nombresProductos.RemoveAt(i);
        preciosProductos.RemoveAt(i);
        stocksProductos.RemoveAt(i);
        Console.WriteLine("Producto eliminado correctamente.");
    }

    static void RegistrarMovimientoStock()
    {
        Console.WriteLine("\n1. Entrada  2. Salida  0. Volver");
        int opcion = LeerOpcion();
        if (opcion == 0) return;
        if (opcion != 1 && opcion != 2) { Console.WriteLine("Opcion invalida."); return; }
        int i = SolicitarProducto();
        if (i < 0) return;
        MostrarDatosProducto(i);
        if (!int.TryParse(LeerPetShop("Cantidad: "), out int cantidad) || cantidad <= 0)
        { Console.WriteLine("La cantidad debe ser mayor que cero."); return; }
        if (opcion == 1 && stocksProductos[i] > int.MaxValue - cantidad)
        { Console.WriteLine("La cantidad supera el limite permitido."); return; }
        if (opcion == 2 && cantidad > stocksProductos[i])
        { Console.WriteLine("Stock insuficiente."); return; }
        stocksProductos[i] += opcion == 1 ? cantidad : -cantidad;
        Console.WriteLine("Nuevo stock: " + stocksProductos[i]);
    }

    static bool ValidarIntegridadListasProductos()
    {
        int n = codigosProductos.Count;
        if (nombresProductos.Count != n || preciosProductos.Count != n || stocksProductos.Count != n) return false;
        var usados = new HashSet<int>();
        for (int i = 0; i < n; i++)
            if (codigosProductos[i] < 4001 || !usados.Add(codigosProductos[i]) ||
                string.IsNullOrWhiteSpace(nombresProductos[i]) || preciosProductos[i] <= 0 || stocksProductos[i] < 0) return false;
        usados.Clear();
        foreach (int codigo in codigosProductosVentas)
            if (!codigosProductos.Contains(codigo) || !usados.Add(codigo)) return false;
        return true;
    }
}
