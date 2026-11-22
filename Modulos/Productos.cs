
using System;
using System.Collections.Generic;

partial class Program
{
    // LISTAS PARA LA GESTION DE PRODUCTOS
 
    static List<int> codigosProductos = new List<int>();
    static List<string> nombresProductos = new List<string>();
    static List<decimal> preciosProductos = new List<decimal>();
    static List<int> stocksProductos = new List<int>();
    
    // BUSCAR POSICION DEL PRODUCTO POR CODIGO

    static int BuscarPosicionProducto(int codigo)
    {
        for (int i = 0; i < codigosProductos.Count; i++)
        {
            if (codigosProductos[i] == codigo)
            {
                return i;
            }
        }

        return -1;
    }

    // GENERAR CODIGO AUTOMATICO DEL PRODUCTO

    static int SiguienteCodigoProducto()
    {
        int mayor = 4000;

        for (int i = 0; i < codigosProductos.Count; i++)
        {
            if (codigosProductos[i] > mayor)
            {
                mayor = codigosProductos[i];
            }
        }

        return mayor == int.MaxValue ? -1 : mayor + 1;
    }
     // REGISTRAR PRODUCTO
    static void RegistrarProducto()
    {
        Console.WriteLine("\n========== REGISTRAR PRODUCTO ==========");

        string nombre;

        do
        {
            Console.Write("Nombre del producto: ");
            nombre = (Console.ReadLine() ?? "").Trim();

            if (nombre == "")
            {
                Console.WriteLine("El nombre es obligatorio.");
            }

        } while (nombre == "");

        Console.Write("Precio del producto: ");

        if (!decimal.TryParse(Console.ReadLine(), out decimal precio)
            || precio <= 0)
        {
            Console.WriteLine("El precio debe ser mayor que cero.");
            return;
        }

        Console.Write("Stock inicial: ");

        if (!int.TryParse(Console.ReadLine(), out int stock)
            || stock < 0)
        {
            Console.WriteLine("El stock debe ser un numero entero no negativo.");
            return;
        }

        int codigo = SiguienteCodigoProducto();

        if (codigo == -1)
        {
            Console.WriteLine("No hay codigos disponibles.");
            return;
        }

        Console.WriteLine("\n========== RESUMEN DEL PRODUCTO ==========");
        Console.WriteLine("Codigo: " + codigo);
        Console.WriteLine("Nombre: " + nombre);
        Console.WriteLine("Precio: " + precio.ToString("0.00"));
        Console.WriteLine("Stock: " + stock);

        Console.Write("Desea guardar el producto? (S/N): ");
        string respuesta = (Console.ReadLine() ?? "").Trim().ToUpperInvariant();

        if (respuesta != "S" && respuesta != "SI")
        {
            Console.WriteLine("Registro cancelado.");
            return;
        }

        codigosProductos.Add(codigo);
        nombresProductos.Add(nombre);
        preciosProductos.Add(precio);
        stocksProductos.Add(stock);

        Console.WriteLine("\nProducto registrado correctamente.");
        Console.WriteLine("Codigo asignado: " + codigo);
    }
}
