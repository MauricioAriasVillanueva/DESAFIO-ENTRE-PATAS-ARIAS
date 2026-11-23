
using System;
using System.Collections.Generic;

partial class Program
{
    // LISTAS PARA LA GESTION DE VENTAS
    static List<int> codigosVentas = new List<int>();
    static List<int> codigosProductosVendidos = new List<int>();
    static List<string> nombresProductosVendidos = new List<string>();
    static List<int> cantidadesVendidas = new List<int>();
    static List<decimal> preciosUnitariosVentas = new List<decimal>();
    static List<decimal> totalesVentas = new List<decimal>();
    static List<string> fechasVentas = new List<string>();

    // ASOCIACION OPCIONAL CON PROPIETARIOS Y MASCOTAS
    // El codigo 0 indica que la venta no tiene asociacion.
    static List<int> codigosPropietariosVentas = new List<int>();
    static List<int> codigosMascotasVentas = new List<int>();

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

        decimal precioUnitario = preciosProductos[posicion];
        decimal total = precioUnitario * cantidad;

        // DATOS DE ASOCIACION OPCIONAL
        int codigoPropietario = 0;
        int codigoMascota = 0;

        Console.Write("\nDesea asociar la venta a un propietario? (S/N): ");
        string asociar = (Console.ReadLine() ?? "").Trim().ToUpperInvariant();

        if (asociar != "S" && asociar != "SI"
            && asociar != "N" && asociar != "NO")
        {
            Console.WriteLine("Respuesta invalida.");
            return;
        }

        if (asociar == "S" || asociar == "SI")
        {
            if (codigosPropietarios.Count == 0)
            {
                Console.WriteLine("No hay propietarios registrados.");
                return;
            }

            Console.Write("Ingrese el codigo del propietario: ");

            if (!int.TryParse(Console.ReadLine(), out codigoPropietario))
            {
                Console.WriteLine("Codigo de propietario invalido.");
                return;
            }

            int posicionPropietario =
                BuscarPosicionPropietario(codigoPropietario);

            if (posicionPropietario == -1)
            {
                Console.WriteLine("Propietario no encontrado.");
                return;
            }

            Console.Write("\nDesea asociar tambien una mascota? (S/N): ");
            string asociarMascota =
                (Console.ReadLine() ?? "").Trim().ToUpperInvariant();

            if (asociarMascota != "S" && asociarMascota != "SI"
                && asociarMascota != "N" && asociarMascota != "NO")
            {
                Console.WriteLine("Respuesta invalida.");
                return;
            }

            if (asociarMascota == "S" || asociarMascota == "SI")
            {
                Console.Write("Ingrese el codigo de la mascota: ");

                if (!int.TryParse(Console.ReadLine(), out codigoMascota))
                {
                    Console.WriteLine("Codigo de mascota invalido.");
                    return;
                }

                int posicionMascota = BuscarPosicionMascota(codigoMascota);

                if (posicionMascota == -1)
                {
                    Console.WriteLine("Mascota no encontrada.");
                    return;
                }

                if (codigosPropietariosMascotas[posicionMascota]
                    != codigoPropietario)
                {
                    Console.WriteLine(
                        "La mascota no pertenece al propietario indicado.");
                    return;
                }
            }
        }

        // RESUMEN DE LA VENTA
        Console.WriteLine("\n========== RESUMEN DE VENTA ==========");
        Console.WriteLine("Producto: " + nombresProductos[posicion]);
        Console.WriteLine("Cantidad: " + cantidad);
        Console.WriteLine("Precio unitario: " +
            precioUnitario.ToString("0.00"));
        Console.WriteLine("Total: " + total.ToString("0.00"));

        if (codigoPropietario == 0)
        {
            Console.WriteLine("Tipo de venta: Venta libre");
        }
        else
        {
            Console.WriteLine("Codigo de propietario: " + codigoPropietario);

            if (codigoMascota != 0)
            {
                Console.WriteLine("Codigo de mascota: " + codigoMascota);
            }
            else
            {
                Console.WriteLine("Venta sin mascota asociada.");
            }
        }

        Console.Write("Confirmar venta? (S/N): ");
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

        // GUARDAR LOS DATOS DE LA VENTA
        codigosVentas.Add(codigoVenta);
        codigosProductosVendidos.Add(codigoProducto);
        nombresProductosVendidos.Add(nombresProductos[posicion]);
        cantidadesVendidas.Add(cantidad);
        preciosUnitariosVentas.Add(precioUnitario);
        totalesVentas.Add(total);
        fechasVentas.Add(DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));

        codigosPropietariosVentas.Add(codigoPropietario);
        codigosMascotasVentas.Add(codigoMascota);

        // DESCONTAR STOCK DESPUES DE CONFIRMAR
        stocksProductos[posicion] -= cantidad;

        // EVITAR ELIMINAR PRODUCTOS ASOCIADOS A VENTAS
        if (!codigosProductosVentas.Contains(codigoProducto))
        {
            codigosProductosVentas.Add(codigoProducto);
        }

        Console.WriteLine("\nVenta registrada correctamente.");
        Console.WriteLine("Codigo de venta: " + codigoVenta);
        Console.WriteLine("Stock restante: " + stocksProductos[posicion]);
    }

    // CONSULTAR VENTA POR CODIGO
    static void ConsultarVentaPorCodigo()
    {
        Console.WriteLine("\n========== CONSULTAR VENTA ==========");

        if (codigosVentas.Count == 0)
        {
            Console.WriteLine("No existen ventas registradas.");
            return;
        }

        Console.Write("Ingrese el codigo de la venta: ");

        if (!int.TryParse(Console.ReadLine(), out int codigo))
        {
            Console.WriteLine("Codigo invalido.");
            return;
        }

        int posicion = codigosVentas.IndexOf(codigo);

        if (posicion == -1)
        {
            Console.WriteLine("No se encontro la venta.");
            return;
        }

        Console.WriteLine("\n========== DETALLE DE VENTA ==========");
        Console.WriteLine("Codigo de venta: " + codigosVentas[posicion]);
        Console.WriteLine("Codigo de producto: " +
            codigosProductosVendidos[posicion]);
        Console.WriteLine("Producto: " +
            nombresProductosVendidos[posicion]);
        Console.WriteLine("Cantidad vendida: " +
            cantidadesVendidas[posicion]);
        Console.WriteLine("Fecha: " + fechasVentas[posicion]);
        Console.WriteLine("Precio unitario: " +
            preciosUnitariosVentas[posicion].ToString("0.00"));
        Console.WriteLine("Total de venta: " +
            totalesVentas[posicion].ToString("0.00"));

        // MOSTRAR PROPIETARIO ASOCIADO, SI EXISTE
        int codigoPropietario = codigosPropietariosVentas[posicion];

        if (codigoPropietario == 0)
        {
            Console.WriteLine("Tipo de venta: Venta libre.");
        }
        else
        {
            Console.WriteLine("\nPropietario asociado:");
            int posicionPropietario =
                BuscarPosicionPropietario(codigoPropietario);

            if (posicionPropietario != -1)
            {
                MostrarDatosPropietario(posicionPropietario);
            }
            else
            {
                Console.WriteLine("Codigo de propietario: " +
                    codigoPropietario);
            }

            // MOSTRAR MASCOTA ASOCIADA, SI EXISTE
            int codigoMascota = codigosMascotasVentas[posicion];

            if (codigoMascota != 0)
            {
                Console.WriteLine("\nMascota asociada:");
                int posicionMascota =
                    BuscarPosicionMascota(codigoMascota);

                if (posicionMascota != -1)
                {
                    MostrarDatosMascota(posicionMascota);
                }
                else
                {
                    Console.WriteLine("Codigo de mascota: " +
                        codigoMascota);
                }
            }
            else
            {
                Console.WriteLine("Venta sin mascota asociada.");
            }
        }
    }
}
