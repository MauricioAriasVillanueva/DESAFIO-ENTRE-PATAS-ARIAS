
using System;
using System.Collections.Generic;
using System.Globalization;

partial class Program
{
    // LISTAS PARA LA GESTION DE PRODUCTOS
    static List<int> codigosProductos = new List<int>();
    static List<string> nombresProductos = new List<string>();
    static List<decimal> preciosProductos = new List<decimal>();
    static List<int> stocksProductos = new List<int>();

    // CODIGOS DE PRODUCTOS QUE YA TIENEN VENTAS
    static List<int> codigosProductosVentas = new List<int>();

    // GENERAR CODIGO AUTOMATICO
    static int SiguienteCodigoProducto()
    {
        int mayor = 4000;

        for (int i = 0; i < codigosProductos.Count; i++)
        {
            if (codigosProductos[i] > mayor)
                mayor = codigosProductos[i];
        }

        return mayor == int.MaxValue ? -1 : mayor + 1;
    }

    // BUSCAR POSICION POR CODIGO
    static int BuscarPosicionProducto(int codigo)
    {
        for (int i = 0; i < codigosProductos.Count; i++)
        {
            if (codigosProductos[i] == codigo)
                return i;
        }

        return -1;
    }

    // MOSTRAR DATOS DEL PRODUCTO
    static void MostrarDatosProducto(int posicion)
    {
        Console.WriteLine("--------------------------------");
        Console.WriteLine("Codigo: " + codigosProductos[posicion]);
        Console.WriteLine("Producto: " + nombresProductos[posicion]);
        Console.WriteLine("Precio: S/ " +
            preciosProductos[posicion].ToString("0.00"));
        Console.WriteLine("Stock: " + stocksProductos[posicion]);
        Console.WriteLine("--------------------------------");
    }

    // REGISTRAR PRODUCTO
    static void RegistrarProducto()
    {
        Console.WriteLine("\n========== REGISTRAR PRODUCTO ==========");

        Console.Write("Nombre del producto: ");
        string nombre = (Console.ReadLine() ?? "").Trim();

        if (string.IsNullOrWhiteSpace(nombre))
        {
            Console.WriteLine("El nombre no puede estar vacio.");
            return;
        }

        foreach (string nombreRegistrado in nombresProductos)
        {
            if (nombreRegistrado.Equals(
                nombre, StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("Ya existe un producto con ese nombre.");
                return;
            }
        }

        Console.Write("Precio del producto: S/ ");

        if (!decimal.TryParse(
            Console.ReadLine(),
            NumberStyles.Number,
            CultureInfo.InvariantCulture,
            out decimal precio) || precio < 0)
        {
            Console.WriteLine("Ingrese un precio valido y no negativo.");
            return;
        }

        Console.Write("Stock inicial: ");

        if (!int.TryParse(Console.ReadLine(), out int stock) || stock < 0)
        {
            Console.WriteLine("El stock debe ser un entero no negativo.");
            return;
        }

        int codigo = SiguienteCodigoProducto();

        if (codigo == -1)
        {
            Console.WriteLine("No hay codigos disponibles.");
            return;
        }

        codigosProductos.Add(codigo);
        nombresProductos.Add(nombre);
        preciosProductos.Add(precio);
        stocksProductos.Add(stock);

        Console.WriteLine("\nProducto registrado correctamente.");
        Console.WriteLine("Codigo asignado: " + codigo);
    }

    // BUSCAR PRODUCTO POR CODIGO O NOMBRE
    static void BuscarProducto()
    {
        Console.WriteLine("\n========== BUSCAR PRODUCTO ==========");

        if (codigosProductos.Count == 0)
        {
            Console.WriteLine("No hay productos registrados.");
            return;
        }

        Console.Write("Ingrese el codigo o nombre del producto: ");
        string busqueda = (Console.ReadLine() ?? "").Trim();

        if (string.IsNullOrWhiteSpace(busqueda))
        {
            Console.WriteLine("Debe ingresar un dato para buscar.");
            return;
        }

        bool encontrado = false;

        for (int i = 0; i < codigosProductos.Count; i++)
        {
            bool coincideCodigo =
                codigosProductos[i].ToString() == busqueda;

            bool coincideNombre =
                nombresProductos[i].IndexOf(
                    busqueda, StringComparison.OrdinalIgnoreCase) >= 0;

            if (coincideCodigo || coincideNombre)
            {
                MostrarDatosProducto(i);
                encontrado = true;
            }
        }

        if (!encontrado)
            Console.WriteLine("No se encontraron productos.");
    }

    // MODIFICAR PRODUCTO
    static void ModificarProducto()
    {
        Console.WriteLine("\n========== MODIFICAR PRODUCTO ==========");

        Console.Write("Ingrese el codigo del producto: ");

        if (!int.TryParse(Console.ReadLine(), out int codigo))
        {
            Console.WriteLine("Codigo invalido.");
            return;
        }

        int posicion = BuscarPosicionProducto(codigo);

        if (posicion == -1)
        {
            Console.WriteLine("Producto no encontrado.");
            return;
        }

        MostrarDatosProducto(posicion);

        Console.Write("Nuevo nombre: ");
        string nombre = (Console.ReadLine() ?? "").Trim();

        if (string.IsNullOrWhiteSpace(nombre))
        {
            Console.WriteLine("El nombre no puede estar vacio.");
            return;
        }

        for (int i = 0; i < nombresProductos.Count; i++)
        {
            if (i != posicion && nombresProductos[i].Equals(
                nombre, StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("Ya existe otro producto con ese nombre.");
                return;
            }
        }

        Console.Write("Nuevo precio: S/ ");

        if (!decimal.TryParse(
            Console.ReadLine(),
            NumberStyles.Number,
            CultureInfo.InvariantCulture,
            out decimal precio) || precio < 0)
        {
            Console.WriteLine("Precio invalido.");
            return;
        }

        nombresProductos[posicion] = nombre;
        preciosProductos[posicion] = precio;

        Console.WriteLine("Producto modificado correctamente.");
    }

    // ELIMINAR PRODUCTO
    static void EliminarProducto()
    {
        Console.WriteLine("\n========== ELIMINAR PRODUCTO ==========");

        Console.Write("Ingrese el codigo del producto: ");

        if (!int.TryParse(Console.ReadLine(), out int codigo))
        {
            Console.WriteLine("Codigo invalido.");
            return;
        }

        int posicion = BuscarPosicionProducto(codigo);

        if (posicion == -1)
        {
            Console.WriteLine("Producto no encontrado.");
            return;
        }

        if (codigosProductosVentas.Contains(codigo))
        {
            Console.WriteLine(
                "No se puede eliminar: el producto tiene ventas registradas.");
            return;
        }

        MostrarDatosProducto(posicion);

        Console.Write("¿Confirma la eliminacion? (S/N): ");
        string respuesta = (Console.ReadLine() ?? "").Trim().ToUpperInvariant();

        if (respuesta != "S" && respuesta != "SI")
        {
            Console.WriteLine("Eliminacion cancelada.");
            return;
        }

        codigosProductos.RemoveAt(posicion);
        nombresProductos.RemoveAt(posicion);
        preciosProductos.RemoveAt(posicion);
        stocksProductos.RemoveAt(posicion);

        Console.WriteLine("Producto eliminado correctamente.");
    }

    // REGISTRAR MOVIMIENTO DE STOCK
    static void RegistrarMovimientoStock()
    {
        Console.WriteLine("\n========== MOVIMIENTO DE STOCK ==========");

        Console.Write("Ingrese el codigo del producto: ");

        if (!int.TryParse(Console.ReadLine(), out int codigo))
        {
            Console.WriteLine("Codigo invalido.");
            return;
        }

        int posicion = BuscarPosicionProducto(codigo);

        if (posicion == -1)
        {
            Console.WriteLine("Producto no encontrado.");
            return;
        }

        MostrarDatosProducto(posicion);

        Console.WriteLine("1. Entrada de stock");
        Console.WriteLine("2. Salida de stock");
        Console.Write("Seleccione el movimiento: ");

        if (!int.TryParse(Console.ReadLine(), out int tipo) ||
            (tipo != 1 && tipo != 2))
        {
            Console.WriteLine("Opcion invalida.");
            return;
        }

        Console.Write("Ingrese la cantidad: ");

        if (!int.TryParse(Console.ReadLine(), out int cantidad) ||
            cantidad <= 0)
        {
            Console.WriteLine("La cantidad debe ser mayor que cero.");
            return;
        }

        if (tipo == 1)
        {
            if (cantidad > int.MaxValue - stocksProductos[posicion])
            {
                Console.WriteLine("La cantidad supera el limite permitido.");
                return;
            }

            stocksProductos[posicion] += cantidad;
            Console.WriteLine("Entrada de stock registrada.");
        }
        else
        {
            if (cantidad > stocksProductos[posicion])
            {
                Console.WriteLine("No hay stock suficiente.");
                return;
            }

            stocksProductos[posicion] -= cantidad;
            Console.WriteLine("Salida de stock registrada.");
        }

        Console.WriteLine("Stock actualizado: " + stocksProductos[posicion]);
    }
}
