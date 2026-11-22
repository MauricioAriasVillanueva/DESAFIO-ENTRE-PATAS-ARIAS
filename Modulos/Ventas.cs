
using System;
using System.Collections.Generic;

partial class Program
{
    // LISTAS PARA LA GESTION DE VENTAS
    static List<int> codigosVentas = new List<int>();
    static List<int> codigosProductosVendidos = new List<int>();
    static List<int> cantidadesVendidas = new List<int>();
    static List<decimal> totalesVentas = new List<decimal>();

    // GENERAR CODIGO AUTOMATICO DE VENTA
    static int SiguienteCodigoVenta()
    {
        int mayor = 5000;

        for (int i = 0; i < codigosVentas.Count; i++)
        {
            if (codigosVentas[i] > mayor)
            {
                mayor = codigosVentas[i];
            }
        }

        return mayor == int.MaxValue ? -1 : mayor + 1;
    }

    // REGISTRAR VENTA

    static void RegistrarVenta()
    {
        Console.WriteLine("\n========== REGISTRAR VENTA ==========");

        if (codigosProductos.Count == 0)
        {
            Console.WriteLine("No hay productos registrados.");
            return;
        }

        Console.Write("Ingrese el codigo del producto: ");

        if (!int.TryParse(Console.ReadLine(), out int codigoProducto))
        {
            Console.WriteLine("Codigo de producto invalido.");
            return;
        }

        int posicion = BuscarPosicionProducto(codigoProducto);

        if (posicion == -1)
        {
            Console.WriteLine("Producto no encontrado.");
            return;
        }

        MostrarDatosProducto(posicion);

        Console.Write("\nIngrese la cantidad a vender: ");

        if (!int.TryParse(Console.ReadLine(), out int cantidad)
            || cantidad <= 0)
        {
            Console.WriteLine("La cantidad debe ser mayor que cero.");
            return;
        }

        if (cantidad > stocksProductos[posicion])
        {
            Console.WriteLine("Stock insuficiente para realizar la venta.");
            return;
        }

        decimal total = preciosProductos[posicion] * cantidad;

        Console.WriteLine("\n========== RESUMEN DE VENTA ==========");
        Console.WriteLine("Producto: " + nombresProductos[posicion]);
        Console.WriteLine("Cantidad: " + cantidad);
        Console.WriteLine("Precio unitario: " +
            preciosProductos[posicion].ToString("0.00"));
        Console.WriteLine("Total: " + total.ToString("0.00"));

        Console.Write("\nConfirmar venta? (S/N): ");
        string respuesta = (Console.ReadLine() ?? "").Trim().ToUpperInvariant();

        if (respuesta != "S" && respuesta != "SI")
        {
            Console.WriteLine("Venta cancelada.");
            return;
        }

        int codigoVenta = SiguienteCodigoVenta();

        if (codigoVenta == -1)
        {
            Console.WriteLine("No hay codigos de venta disponibles.");
            return;
        }

        codigosVentas.Add(codigoVenta);
        codigosProductosVendidos.Add(codigoProducto);
        cantidadesVendidas.Add(cantidad);
        totalesVentas.Add(total);

        stocksProductos[posicion] -= cantidad;

        // Evitar eliminar productos asociados a ventas
        if (!codigosProductosVentas.Contains(codigoProducto))
        {
            codigosProductosVentas.Add(codigoProducto);
        }

        Console.WriteLine("\nVenta registrada correctamente.");
        Console.WriteLine("Codigo de venta: " + codigoVenta);
        Console.WriteLine("Stock restante: " + stocksProductos[posicion]);
    }
}
