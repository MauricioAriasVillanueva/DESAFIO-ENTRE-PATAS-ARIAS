
using System;
using System.Collections.Generic;
using System.Globalization;

partial class Program
{
    // LISTAS PARA LA GESTION DE VENTAS
    static List<int> codigosVentas = new List<int>();
    static List<int> codigosProductosVendidos = new List<int>();
    static List<int> cantidadesVendidas = new List<int>();
    static List<decimal> totalesVentas = new List<decimal>();

    // DATOS HISTORICOS DE CADA VENTA
    static List<string> nombresProductosVendidos = new List<string>();
    static List<decimal> preciosUnitariosVentas = new List<decimal>();
    static List<string> fechasVentas = new List<string>();

    // RELACION OPCIONAL CON PROPIETARIO Y MASCOTA
    // El codigo 0 indica que no existe asociacion.
    static List<int> codigosPropietariosVentas = new List<int>();
    static List<int> codigosMascotasVentas = new List<int>();

    // GENERAR CODIGO AUTOMATICO DE VENTA
    static int SiguienteCodigoVenta()
    {
        int mayor = 5000;

        for (int i = 0; i < codigosVentas.Count; i++)
        {
            if (codigosVentas[i] > mayor)
                mayor = codigosVentas[i];
        }

        return mayor == int.MaxValue ? -1 : mayor + 1;
    }

    // VALIDAR QUE TODAS LAS LISTAS DE VENTAS ESTEN ALINEADAS
    static bool ValidarIntegridadListasVentas()
    {
        int cantidad = codigosVentas.Count;

        if (codigosProductosVendidos.Count != cantidad ||
            cantidadesVendidas.Count != cantidad ||
            totalesVentas.Count != cantidad ||
            nombresProductosVendidos.Count != cantidad ||
            preciosUnitariosVentas.Count != cantidad ||
            fechasVentas.Count != cantidad ||
            codigosPropietariosVentas.Count != cantidad ||
            codigosMascotasVentas.Count != cantidad)
        {
            return false;
        }

        for (int i = 0; i < cantidad; i++)
        {
            if (codigosVentas[i] < 5001 ||
                codigosProductosVendidos[i] < 4001 ||
                cantidadesVendidas[i] <= 0 ||
                preciosUnitariosVentas[i] <= 0 ||
                totalesVentas[i] <= 0 ||
                string.IsNullOrWhiteSpace(nombresProductosVendidos[i]) ||
                codigosPropietariosVentas[i] < 0 ||
                codigosMascotasVentas[i] < 0)
            {
                return false;
            }

            // VALIDAR FORMATO DE FECHA
            if (!DateTime.TryParseExact(
                fechasVentas[i],
                "yyyy-MM-dd HH:mm:ss",
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out _))
            {
                return false;
            }

            // UNA VENTA SIN PROPIETARIO NO PUEDE TENER MASCOTA
            if (codigosPropietariosVentas[i] == 0 &&
                codigosMascotasVentas[i] != 0)
            {
                return false;
            }

            // VALIDAR QUE EL TOTAL COINCIDA CON EL PRECIO Y LA CANTIDAD
            decimal totalCalculado;

            try
            {
                totalCalculado = preciosUnitariosVentas[i] *
                                 cantidadesVendidas[i];
            }
            catch (OverflowException)
            {
                return false;
            }

            if (Math.Abs(totalesVentas[i] - totalCalculado) > 0.01m)
                return false;

            // EVITAR CODIGOS DE VENTA DUPLICADOS
            for (int j = i + 1; j < cantidad; j++)
            {
                if (codigosVentas[i] == codigosVentas[j])
                    return false;
            }
        }

        return true;
    }

    // SOLICITAR ASOCIACION OPCIONAL CON PROPIETARIO Y MASCOTA
    static bool SolicitarAsociacionVenta(
        out int codigoPropietario,
        out int codigoMascota)
    {
        codigoPropietario = 0;
        codigoMascota = 0;

        Console.Write("\nDesea asociar la venta a un propietario? (S/N): ");
        string respuesta = (Console.ReadLine() ?? "").Trim().ToUpperInvariant();

        if (respuesta == "N" || respuesta == "NO")
            return true;

        if (respuesta != "S" && respuesta != "SI")
        {
            Console.WriteLine("Respuesta invalida.");
            return false;
        }

        Console.Write("Ingrese el codigo del propietario: ");

        if (!int.TryParse(Console.ReadLine(), out codigoPropietario) ||
            codigoPropietario <= 0)
        {
            Console.WriteLine("Codigo de propietario invalido.");
            return false;
        }

        int posicionPropietario =
            BuscarPosicionPropietario(codigoPropietario);

        if (posicionPropietario == -1)
        {
            Console.WriteLine("Propietario no encontrado.");
            return false;
        }

        Console.Write("Desea asociar tambien una mascota? (S/N): ");
        respuesta = (Console.ReadLine() ?? "").Trim().ToUpperInvariant();

        if (respuesta == "N" || respuesta == "NO")
            return true;

        if (respuesta != "S" && respuesta != "SI")
        {
            Console.WriteLine("Respuesta invalida.");
            return false;
        }

        Console.Write("Ingrese el codigo de la mascota: ");

        if (!int.TryParse(Console.ReadLine(), out codigoMascota) ||
            codigoMascota <= 0)
        {
            Console.WriteLine("Codigo de mascota invalido.");
            return false;
        }

        int posicionMascota = BuscarPosicionMascota(codigoMascota);

        if (posicionMascota == -1)
        {
            Console.WriteLine("Mascota no encontrada.");
            return false;
        }

        if (codigosPropietariosMascotas[posicionMascota] != codigoPropietario)
        {
            Console.WriteLine("La mascota no pertenece al propietario indicado.");
            return false;
        }

        return true;
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

        if (!ValidarIntegridadListasVentas())
        {
            Console.WriteLine("Error: las listas de ventas no estan integras.");
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

        if (!int.TryParse(Console.ReadLine(), out int cantidad) ||
            cantidad <= 0)
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
        decimal total;

        try
        {
            total = precioUnitario * cantidad;
        }
        catch (OverflowException)
        {
            Console.WriteLine("El importe de la venta es demasiado grande.");
            return;
        }

        int codigoPropietario;
        int codigoMascota;

        if (!SolicitarAsociacionVenta(
            out codigoPropietario, out codigoMascota))
        {
            Console.WriteLine("No se registro la venta.");
            return;
        }

        Console.WriteLine("\n========== RESUMEN DE VENTA ==========");
        Console.WriteLine("Producto: " + nombresProductos[posicion]);
        Console.WriteLine("Cantidad: " + cantidad);
        Console.WriteLine("Precio unitario: " + precioUnitario.ToString("0.00"));
        Console.WriteLine("Total: " + total.ToString("0.00"));

        if (codigoPropietario == 0)
        {
            Console.WriteLine("Tipo de venta: Venta libre");
        }
        else
        {
            Console.WriteLine("Codigo de propietario: " + codigoPropietario);
            Console.WriteLine("Codigo de mascota: " +
                (codigoMascota == 0 ? "Sin asociar" : codigoMascota.ToString()));
        }

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

        // REGISTRAR TODOS LOS DATOS DE LA VENTA
        codigosVentas.Add(codigoVenta);
        codigosProductosVendidos.Add(codigoProducto);
        cantidadesVendidas.Add(cantidad);
        totalesVentas.Add(total);

        nombresProductosVendidos.Add(nombresProductos[posicion]);
        preciosUnitariosVentas.Add(precioUnitario);
        fechasVentas.Add(DateTime.Now.ToString(
            "yyyy-MM-dd HH:mm:ss",
            CultureInfo.InvariantCulture));

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

        if (!ValidarIntegridadListasVentas())
        {
            Console.WriteLine("Error: las listas de ventas no estan integras.");
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
        Console.WriteLine("Fecha: " + fechasVentas[posicion]);
        Console.WriteLine("Codigo de producto: " +
            codigosProductosVendidos[posicion]);
        Console.WriteLine("Producto: " + nombresProductosVendidos[posicion]);
        Console.WriteLine("Cantidad vendida: " + cantidadesVendidas[posicion]);
        Console.WriteLine("Precio unitario: " +
            preciosUnitariosVentas[posicion].ToString("0.00"));
        Console.WriteLine("Total de venta: " +
            totalesVentas[posicion].ToString("0.00"));

        int codigoPropietario = codigosPropietariosVentas[posicion];
        int codigoMascota = codigosMascotasVentas[posicion];

        if (codigoPropietario == 0)
        {
            Console.WriteLine("Tipo de venta: Venta libre");
        }
        else
        {
            Console.WriteLine("Codigo de propietario: " + codigoPropietario);

            int posicionPropietario =
                BuscarPosicionPropietario(codigoPropietario);

            if (posicionPropietario != -1)
                MostrarDatosPropietario(posicionPropietario);

            if (codigoMascota != 0)
            {
                Console.WriteLine("Codigo de mascota: " + codigoMascota);

                int posicionMascota = BuscarPosicionMascota(codigoMascota);

                if (posicionMascota != -1)
                    MostrarDatosMascota(posicionMascota);
            }
            else
            {
                Console.WriteLine("Mascota: Sin asociar");
            }
        }
    }
}
