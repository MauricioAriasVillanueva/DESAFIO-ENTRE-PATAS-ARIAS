
using System;
using System.Collections.Generic;

partial class Program
{
    // LISTAS PARA LA GESTION DE PRODUCTOS

    static List<int> codigosProductos = new List<int>();
    static List<string> nombresProductos = new List<string>();
    static List<decimal> preciosProductos = new List<decimal>();
    static List<int> stocksProductos = new List<int>();

    // CODIGOS DE PRODUCTOS ASOCIADOS A VENTAS
    static List<int> codigosProductosVentas = new List<int>();

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

    // MOSTRAR DATOS DEL PRODUCTO
    static void MostrarDatosProducto(int posicion)
    {
        Console.WriteLine("\n========== PRODUCTO ENCONTRADO ==========");
        Console.WriteLine("Codigo: " + codigosProductos[posicion]);
        Console.WriteLine("Nombre: " + nombresProductos[posicion]);
        Console.WriteLine("Precio: " + preciosProductos[posicion].ToString("0.00"));
        Console.WriteLine("Stock: " + stocksProductos[posicion]);
    }

    // BUSCAR PRODUCTO POR CODIGO O NOMBRE
    static void BuscarProducto()
    {
        Console.WriteLine("\n========== BUSCAR PRODUCTO ==========");
        Console.WriteLine("1. Buscar por codigo");
        Console.WriteLine("2. Buscar por nombre");
        Console.WriteLine("0. Volver");

        Console.Write("Seleccione una opcion: ");

        if (!int.TryParse(Console.ReadLine(), out int opcion))
        {
            Console.WriteLine("Opcion invalida.");
            return;
        }

        if (opcion == 0)
        {
            return;
        }

        if (opcion == 1)
        {
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
        }
        else if (opcion == 2)
        {
            Console.Write("Ingrese el nombre del producto: ");
            string nombreBuscado = (Console.ReadLine() ?? "").Trim();

            if (nombreBuscado == "")
            {
                Console.WriteLine("Debe ingresar un nombre.");
                return;
            }

            bool encontrado = false;

            for (int i = 0; i < nombresProductos.Count; i++)
            {
                if (nombresProductos[i].IndexOf(
                    nombreBuscado,
                    StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    MostrarDatosProducto(i);
                    encontrado = true;
                }
            }

            if (!encontrado)
            {
                Console.WriteLine("No se encontraron productos con ese nombre.");
            }
        }
        else
        {
            Console.WriteLine("Opcion invalida.");
        }
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

        Console.WriteLine("\nIngrese los nuevos datos:");

        Console.Write("Nuevo nombre: ");
        string nombre = (Console.ReadLine() ?? "").Trim();

        if (nombre == "")
        {
            Console.WriteLine("El nombre no puede estar vacio.");
            return;
        }

        Console.Write("Nuevo precio: ");

        if (!decimal.TryParse(Console.ReadLine(), out decimal precio)
            || precio <= 0)
        {
            Console.WriteLine("El precio debe ser mayor que cero.");
            return;
        }

        Console.Write("Nuevo stock: ");

        if (!int.TryParse(Console.ReadLine(), out int stock)
            || stock < 0)
        {
            Console.WriteLine("El stock no puede ser negativo.");
            return;
        }

        Console.Write("Confirmar modificacion? (S/N): ");
        string respuesta = (Console.ReadLine() ?? "").Trim().ToUpperInvariant();

        if (respuesta != "S" && respuesta != "SI")
        {
            Console.WriteLine("Modificacion cancelada.");
            return;
        }

        nombresProductos[posicion] = nombre;
        preciosProductos[posicion] = precio;
        stocksProductos[posicion] = stock;

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

        // Verificar si el producto tiene ventas asociadas
        if (codigosProductosVentas.Contains(codigo))
        {
            Console.WriteLine("No se puede eliminar: el producto tiene ventas asociadas.");
            return;
        }

        MostrarDatosProducto(posicion);

        Console.Write("Confirma la eliminacion? (S/N): ");
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
        Console.WriteLine("1. Entrada de productos");
        Console.WriteLine("2. Salida de productos");
        Console.WriteLine("0. Volver");

        Console.Write("Seleccione una opcion: ");

        if (!int.TryParse(Console.ReadLine(), out int opcion))
        {
            Console.WriteLine("Opcion invalida.");
            return;
        }

        if (opcion == 0)
        {
            return;
        }

        if (opcion != 1 && opcion != 2)
        {
            Console.WriteLine("Opcion invalida.");
            return;
        }

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

        Console.Write("\nIngrese la cantidad: ");

        if (!int.TryParse(Console.ReadLine(), out int cantidad)
            || cantidad <= 0)
        {
            Console.WriteLine("La cantidad debe ser mayor que cero.");
            return;
        }

        if (opcion == 1)
        {
            if (stocksProductos[posicion] > int.MaxValue - cantidad)
            {
                Console.WriteLine("La cantidad supera el limite permitido.");
                return;
            }

            stocksProductos[posicion] += cantidad;

            Console.WriteLine("Entrada de stock registrada correctamente.");
        }
        else
        {
            if (cantidad > stocksProductos[posicion])
            {
                Console.WriteLine("Stock insuficiente para realizar la salida.");
                return;
            }

            stocksProductos[posicion] -= cantidad;

            Console.WriteLine("Salida de stock registrada correctamente.");
        }

        Console.WriteLine("Nuevo stock: " + stocksProductos[posicion]);
    }

    // VALIDAR INTEGRIDAD DE LAS LISTAS DE PRODUCTOS
    static bool ValidarIntegridadListasProductos()
    {
        int cantidad = codigosProductos.Count;

        if (nombresProductos.Count != cantidad ||
            preciosProductos.Count != cantidad ||
            stocksProductos.Count != cantidad)
        {
            return false;
        }

        for (int i = 0; i < cantidad; i++)
        {
            if (codigosProductos[i] <= 0 ||
                string.IsNullOrWhiteSpace(nombresProductos[i]) ||
                preciosProductos[i] <= 0 ||
                stocksProductos[i] < 0)
            {
                return false;
            }

            for (int j = i + 1; j < cantidad; j++)
            {
                if (codigosProductos[i] == codigosProductos[j])
                    return false;
            }
        }

        for (int i = 0; i < codigosProductosVentas.Count; i++)
        {
            if (!codigosProductos.Contains(codigosProductosVentas[i]))
                return false;

            for (int j = i + 1; j < codigosProductosVentas.Count; j++)
            {
                if (codigosProductosVentas[i] == codigosProductosVentas[j])
                    return false;
            }
        }

        return true;
    }
}
