
using System;
using System.Collections.Generic;
using System.Globalization;

partial class Program
{
    // LISTAS PARA LA GESTION DE PRODUCTOS

    static List<int> codigosProductos = new();
    static List<string> nombresProductos = new();
    static List<decimal> preciosProductos = new();
    static List<int> stocksProductos = new();

    // CODIGOS DE PRODUCTOS QUE TIENEN VENTAS ASOCIADAS
    static List<int> codigosProductosVentas = new();

    // METODOS AUXILIARES

    static string LeerDatoProducto(string mensaje)
    {
        Console.Write(mensaje);
        return (Console.ReadLine() ?? "").Trim();
    }

    static bool ConfirmarAccionProducto(string mensaje)
    {
        string respuesta = LeerDatoProducto(mensaje).ToUpperInvariant();
        return respuesta == "S" || respuesta == "SI";
    }

    static int LeerOpcionProducto()
    {
        if (int.TryParse(LeerDatoProducto("Seleccione una opcion: "), out int opcion))
            return opcion;

        return -1;
    }

    static bool LeerPrecioProducto(out decimal precio)
    {
        string entrada = LeerDatoProducto("Precio del producto: ");

        bool valido =
            decimal.TryParse(
                entrada,
                NumberStyles.Number,
                CultureInfo.CurrentCulture,
                out precio
            )
            ||
            decimal.TryParse(
                entrada,
                NumberStyles.Number,
                CultureInfo.InvariantCulture,
                out precio
            );

        return valido && precio > 0;
    }

    // BUSCAR LA POSICION DE UN PRODUCTO POR SU CODIGO

    static int BuscarPosicionProducto(int codigo)
    {
        return codigosProductos.IndexOf(codigo);
    }

    // GENERAR EL SIGUIENTE CODIGO AUTOMATICO

    static int SiguienteCodigoProducto()
    {
        int mayor = 4000;

        foreach (int codigo in codigosProductos)
        {
            if (codigo > mayor)
                mayor = codigo;
        }

        return mayor == int.MaxValue ? -1 : mayor + 1;
    }

    // VALIDAR LA INTEGRIDAD DE LAS LISTAS

    static bool ValidarIntegridadListasProductos()
    {
        int cantidad = codigosProductos.Count;

        if (nombresProductos.Count != cantidad ||
            preciosProductos.Count != cantidad ||
            stocksProductos.Count != cantidad)
        {
            return false;
        }

        HashSet<int> codigosRegistrados = new();

        for (int i = 0; i < cantidad; i++)
        {
            if (codigosProductos[i] < 4001 ||
                !codigosRegistrados.Add(codigosProductos[i]) ||
                string.IsNullOrWhiteSpace(nombresProductos[i]) ||
                preciosProductos[i] <= 0 ||
                stocksProductos[i] < 0)
            {
                return false;
            }
        }

        HashSet<int> productosConVentas = new();

        foreach (int codigo in codigosProductosVentas)
        {
            if (!codigosRegistrados.Contains(codigo) ||
                !productosConVentas.Add(codigo))
            {
                return false;
            }
        }

        return true;
    }

    // SOLICITAR UN PRODUCTO EXISTENTE

    static int SolicitarProducto()
    {
        if (!int.TryParse(
            LeerDatoProducto("Ingrese el codigo del producto: "),
            out int codigo) || codigo <= 0)
        {
            Console.WriteLine("Codigo de producto invalido.");
            return -1;
        }

        int posicion = BuscarPosicionProducto(codigo);

        if (posicion == -1)
            Console.WriteLine("No se encontro el producto.");

        return posicion;
    }

    // LEER LOS DATOS DE UN PRODUCTO

    static bool LeerDatosProducto(
        out string nombre,
        out decimal precio,
        out int stock)
    {
        nombre = LeerDatoProducto("Nombre del producto: ");
        precio = 0;
        stock = 0;

        if (string.IsNullOrWhiteSpace(nombre))
        {
            Console.WriteLine("El nombre del producto es obligatorio.");
            return false;
        }

        if (!LeerPrecioProducto(out precio))
        {
            Console.WriteLine("El precio debe ser un numero mayor que cero.");
            return false;
        }

        if (!int.TryParse(
            LeerDatoProducto("Stock inicial: "),
            out stock) || stock < 0)
        {
            Console.WriteLine("El stock debe ser un entero no negativo.");
            return false;
        }

        return true;
    }

    // MOSTRAR LOS DATOS DE UN PRODUCTO

    static void MostrarDatosProducto(int posicion)
    {
        if (posicion < 0 || posicion >= codigosProductos.Count)
        {
            Console.WriteLine("No existe un producto en esa posicion.");
            return;
        }

        Console.WriteLine("\n========== DATOS DEL PRODUCTO ==========");
        Console.WriteLine("Codigo: " + codigosProductos[posicion]);
        Console.WriteLine("Nombre: " + nombresProductos[posicion]);
        Console.WriteLine("Precio: S/ " +
            preciosProductos[posicion].ToString("0.00"));
        Console.WriteLine("Stock: " + stocksProductos[posicion]);
    }

    // REGISTRAR UN NUEVO PRODUCTO

    static void RegistrarProducto()
    {
        Console.WriteLine("\n========== REGISTRAR PRODUCTO ==========");

        if (!ValidarIntegridadListasProductos())
        {
            Console.WriteLine("Error: las listas de productos no estan integras.");
            return;
        }

        if (!LeerDatosProducto(
            out string nombre,
            out decimal precio,
            out int stock))
        {
            Console.WriteLine("No se registro el producto.");
            return;
        }

        int codigo = SiguienteCodigoProducto();

        if (codigo == -1)
        {
            Console.WriteLine("No hay codigos disponibles.");
            return;
        }

        Console.WriteLine("\n========== RESUMEN DEL PRODUCTO ==========");
        Console.WriteLine("Codigo asignado: " + codigo);
        Console.WriteLine("Nombre: " + nombre);
        Console.WriteLine("Precio: S/ " + precio.ToString("0.00"));
        Console.WriteLine("Stock inicial: " + stock);

        if (!ConfirmarAccionProducto("Confirmar registro? (S/N): "))
        {
            Console.WriteLine("Registro cancelado.");
            return;
        }

        codigosProductos.Add(codigo);
        nombresProductos.Add(nombre);
        preciosProductos.Add(precio);
        stocksProductos.Add(stock);

        Console.WriteLine("Producto registrado correctamente.");
    }

    // BUSCAR PRODUCTOS POR CODIGO O NOMBRE

    static void BuscarProducto()
    {
        Console.WriteLine("\n========== BUSCAR PRODUCTO ==========");
        Console.WriteLine("1. Buscar por codigo");
        Console.WriteLine("2. Buscar por nombre");
        Console.WriteLine("0. Volver");

        int opcion = LeerOpcionProducto();

        if (opcion == 0)
            return;

        if (!ValidarIntegridadListasProductos())
        {
            Console.WriteLine("Error: las listas de productos no estan integras.");
            return;
        }

        if (opcion == 1)
        {
            int posicion = SolicitarProducto();

            if (posicion >= 0)
                MostrarDatosProducto(posicion);

            return;
        }

        if (opcion == 2)
        {
            string nombreBuscado =
                LeerDatoProducto("Ingrese el nombre que desea buscar: ");

            if (string.IsNullOrWhiteSpace(nombreBuscado))
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
                Console.WriteLine("No se encontraron productos con ese nombre.");

            return;
        }

        Console.WriteLine("Opcion invalida.");
    }

    // MODIFICAR LOS DATOS DE UN PRODUCTO

    static void ModificarProducto()
    {
        Console.WriteLine("\n========== MODIFICAR PRODUCTO ==========");

        if (!ValidarIntegridadListasProductos())
        {
            Console.WriteLine("Error: las listas de productos no estan integras.");
            return;
        }

        int posicion = SolicitarProducto();

        if (posicion == -1)
            return;

        MostrarDatosProducto(posicion);

        Console.WriteLine("\nIngrese los nuevos datos:");

        if (!LeerDatosProducto(
            out string nuevoNombre,
            out decimal nuevoPrecio,
            out int nuevoStock))
        {
            Console.WriteLine("No se modificaron los datos.");
            return;
        }

        Console.WriteLine("\n========== RESUMEN DE MODIFICACION ==========");
        Console.WriteLine("Nuevo nombre: " + nuevoNombre);
        Console.WriteLine("Nuevo precio: S/ " + nuevoPrecio.ToString("0.00"));
        Console.WriteLine("Nuevo stock: " + nuevoStock);

        if (!ConfirmarAccionProducto("Confirmar modificacion? (S/N): "))
        {
            Console.WriteLine("Modificacion cancelada.");
            return;
        }

        nombresProductos[posicion] = nuevoNombre;
        preciosProductos[posicion] = nuevoPrecio;
        stocksProductos[posicion] = nuevoStock;

        Console.WriteLine("Producto modificado correctamente.");
    }

    // ELIMINAR UN PRODUCTO

    static void EliminarProducto()
    {
        Console.WriteLine("\n========== ELIMINAR PRODUCTO ==========");

        if (!ValidarIntegridadListasProductos())
        {
            Console.WriteLine("Error: las listas de productos no estan integras.");
            return;
        }

        int posicion = SolicitarProducto();

        if (posicion == -1)
            return;

        int codigo = codigosProductos[posicion];

        if (codigosProductosVentas.Contains(codigo))
        {
            Console.WriteLine(
                "No se puede eliminar: el producto tiene ventas asociadas.");
            return;
        }

        MostrarDatosProducto(posicion);

        if (!ConfirmarAccionProducto("Confirmar eliminacion? (S/N): "))
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

    // REGISTRAR ENTRADAS Y SALIDAS DE STOCK

    static void RegistrarMovimientoStock()
    {
        Console.WriteLine("\n========== MOVIMIENTO DE STOCK ==========");
        Console.WriteLine("1. Entrada de productos");
        Console.WriteLine("2. Salida de productos");
        Console.WriteLine("0. Volver");

        int opcion = LeerOpcionProducto();

        if (opcion == 0)
            return;

        if (opcion != 1 && opcion != 2)
        {
            Console.WriteLine("Opcion invalida.");
            return;
        }

        if (!ValidarIntegridadListasProductos())
        {
            Console.WriteLine("Error: las listas de productos no estan integras.");
            return;
        }

        int posicion = SolicitarProducto();

        if (posicion == -1)
            return;

        MostrarDatosProducto(posicion);

        if (!int.TryParse(
            LeerDatoProducto("Ingrese la cantidad: "),
            out int cantidad) || cantidad <= 0)
        {
            Console.WriteLine("La cantidad debe ser un entero mayor que cero.");
            return;
        }

        int stockActual = stocksProductos[posicion];
        int nuevoStock;

        if (opcion == 1)
        {
            if (stockActual > int.MaxValue - cantidad)
            {
                Console.WriteLine("La cantidad supera el limite permitido.");
                return;
            }

            nuevoStock = stockActual + cantidad;
        }
        else
        {
            if (cantidad > stockActual)
            {
                Console.WriteLine("Stock insuficiente para realizar la salida.");
                return;
            }

            nuevoStock = stockActual - cantidad;
        }

        Console.WriteLine("\n========== RESUMEN DEL MOVIMIENTO ==========");
        Console.WriteLine("Producto: " + nombresProductos[posicion]);
        Console.WriteLine("Stock anterior: " + stockActual);
        Console.WriteLine("Cantidad: " + cantidad);
        Console.WriteLine("Tipo: " + (opcion == 1 ? "Entrada" : "Salida"));
        Console.WriteLine("Stock resultante: " + nuevoStock);

        if (!ConfirmarAccionProducto("Confirmar movimiento? (S/N): "))
        {
            Console.WriteLine("Movimiento cancelado. El stock no fue modificado.");
            return;
        }

        stocksProductos[posicion] = nuevoStock;

        Console.WriteLine("Movimiento registrado correctamente.");
        Console.WriteLine("Stock actualizado: " + stocksProductos[posicion]);
    }
}
