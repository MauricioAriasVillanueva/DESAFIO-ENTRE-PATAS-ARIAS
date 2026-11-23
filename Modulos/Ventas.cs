
using System;
using System.Collections.Generic;
using System.Globalization;

partial class Program
{
    // LISTAS PARA ALMACENAR LAS VENTAS

    static List<int> codigosVentas = new();
    static List<int> codigosProductosVendidos = new();
    static List<int> cantidadesVendidas = new();
    static List<decimal> totalesVentas = new();
    static List<string> nombresProductosVendidos = new();
    static List<decimal> preciosUnitariosVentas = new();
    static List<string> fechasVentas = new();
    static List<int> codigosPropietariosVentas = new();
    static List<int> codigosMascotasVentas = new();

    // METODOS AUXILIARES

    static string LeerDatoVenta(string mensaje)
    {
        Console.Write(mensaje);
        return (Console.ReadLine() ?? "").Trim();
    }

    static bool ConfirmarVenta()
    {
        string respuesta = LeerDatoVenta(
            "Confirmar operacion? (S/N): ").ToUpperInvariant();

        return respuesta == "S" || respuesta == "SI";
    }

    // GENERAR CODIGO AUTOMATICO DE VENTA

    static int SiguienteCodigoVenta()
    {
        int mayor = 5000;

        foreach (int codigo in codigosVentas)
        {
            if (codigo > mayor)
                mayor = codigo;
        }

        return mayor == int.MaxValue ? -1 : mayor + 1;
    }

    // VALIDAR LA INTEGRIDAD DE LAS LISTAS DE VENTAS

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

        HashSet<int> codigosRegistrados = new();

        for (int i = 0; i < cantidad; i++)
        {
            if (codigosVentas[i] < 5001 ||
                !codigosRegistrados.Add(codigosVentas[i]) ||
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

            if (!DateTime.TryParseExact(
                fechasVentas[i],
                "yyyy-MM-dd HH:mm:ss",
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out _))
            {
                return false;
            }

            if (codigosPropietariosVentas[i] == 0 &&
                codigosMascotasVentas[i] != 0)
            {
                return false;
            }

            if (!codigosProductos.Contains(codigosProductosVendidos[i]))
                return false;

            if (codigosPropietariosVentas[i] != 0 &&
                !codigosPropietarios.Contains(codigosPropietariosVentas[i]))
            {
                return false;
            }

            if (codigosMascotasVentas[i] != 0)
            {
                int posicionMascota =
                    BuscarPosicionMascota(codigosMascotasVentas[i]);

                if (posicionMascota == -1 ||
                    posicionMascota >= codigosPropietariosMascotas.Count ||
                    codigosPropietariosMascotas[posicionMascota] !=
                    codigosPropietariosVentas[i])
                {
                    return false;
                }
            }

            decimal totalCalculado;

            try
            {
                totalCalculado =
                    preciosUnitariosVentas[i] * cantidadesVendidas[i];
            }
            catch (OverflowException)
            {
                return false;
            }

            if (totalesVentas[i] != totalCalculado)
                return false;
        }

        return true;
    }

    // ASOCIAR UNA VENTA A UN PROPIETARIO Y A UNA MASCOTA

    static bool SolicitarAsociacionVenta(
        out int codigoPropietario,
        out int codigoMascota)
    {
        codigoPropietario = 0;
        codigoMascota = 0;

        string respuesta = LeerDatoVenta(
            "\nDesea asociar la venta a un propietario? (S/N): ")
            .ToUpperInvariant();

        if (respuesta == "N" || respuesta == "NO")
            return true;

        if (respuesta != "S" && respuesta != "SI")
        {
            Console.WriteLine("Respuesta invalida.");
            return false;
        }

        if (!int.TryParse(
            LeerDatoVenta("Codigo del propietario: "),
            out codigoPropietario) || codigoPropietario <= 0)
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

        respuesta = LeerDatoVenta(
            "Desea asociar tambien una mascota? (S/N): ")
            .ToUpperInvariant();

        if (respuesta == "N" || respuesta == "NO")
            return true;

        if (respuesta != "S" && respuesta != "SI")
        {
            Console.WriteLine("Respuesta invalida.");
            return false;
        }

        if (!int.TryParse(
            LeerDatoVenta("Codigo de la mascota: "),
            out codigoMascota) || codigoMascota <= 0)
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

        if (codigosPropietariosMascotas[posicionMascota] !=
            codigoPropietario)
        {
            Console.WriteLine(
                "La mascota no pertenece al propietario indicado.");
            return false;
        }

        return true;
    }

    // REGISTRAR UNA NUEVA VENTA

    static void RegistrarVenta()
    {
        Console.WriteLine("\n========== REGISTRAR VENTA ==========");

        if (codigosProductos.Count == 0)
        {
            Console.WriteLine("No existen productos registrados.");
            return;
        }

        if (!ValidarIntegridadListasProductos() ||
            !ValidarIntegridadListasVentas())
        {
            Console.WriteLine(
                "Error: las listas de productos o ventas no estan integras.");
            return;
        }

        if (!int.TryParse(
            LeerDatoVenta("Codigo del producto: "),
            out int codigoProducto) || codigoProducto <= 0)
        {
            Console.WriteLine("Codigo de producto invalido.");
            return;
        }

        int posicionProducto = BuscarPosicionProducto(codigoProducto);

        if (posicionProducto == -1)
        {
            Console.WriteLine("Producto no encontrado.");
            return;
        }

        MostrarDatosProducto(posicionProducto);

        if (!int.TryParse(
            LeerDatoVenta("\nCantidad que desea vender: "),
            out int cantidad) || cantidad <= 0)
        {
            Console.WriteLine("La cantidad debe ser mayor que cero.");
            return;
        }

        if (cantidad > stocksProductos[posicionProducto])
        {
            Console.WriteLine("Stock insuficiente.");
            return;
        }

        decimal precioUnitario = preciosProductos[posicionProducto];
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
            out codigoPropietario,
            out codigoMascota))
        {
            Console.WriteLine("No se registro la venta.");
            return;
        }

        int codigoVenta = SiguienteCodigoVenta();

        if (codigoVenta == -1)
        {
            Console.WriteLine("No hay codigos de venta disponibles.");
            return;
        }

        Console.WriteLine("\n========== RESUMEN DE VENTA ==========");
        Console.WriteLine("Codigo de venta: " + codigoVenta);
        Console.WriteLine("Producto: " +
            nombresProductos[posicionProducto]);
        Console.WriteLine("Cantidad: " + cantidad);
        Console.WriteLine("Precio unitario: S/ " +
            precioUnitario.ToString("0.00"));
        Console.WriteLine("Total: S/ " + total.ToString("0.00"));

        if (codigoPropietario == 0)
        {
            Console.WriteLine("Tipo de venta: Venta libre.");
        }
        else
        {
            Console.WriteLine("Codigo de propietario: " +
                codigoPropietario);

            Console.WriteLine("Codigo de mascota: " +
                (codigoMascota == 0
                    ? "Sin asociar"
                    : codigoMascota.ToString()));
        }

        if (!ConfirmarVenta())
        {
            Console.WriteLine("Venta cancelada. No se modifico el stock.");
            return;
        }

        // GUARDAR LOS DATOS DE LA VENTA

        codigosVentas.Add(codigoVenta);
        codigosProductosVendidos.Add(codigoProducto);
        cantidadesVendidas.Add(cantidad);
        totalesVentas.Add(total);
        nombresProductosVendidos.Add(
            nombresProductos[posicionProducto]);
        preciosUnitariosVentas.Add(precioUnitario);

        fechasVentas.Add(DateTime.Now.ToString(
            "yyyy-MM-dd HH:mm:ss",
            CultureInfo.InvariantCulture));

        codigosPropietariosVentas.Add(codigoPropietario);
        codigosMascotasVentas.Add(codigoMascota);

        // ACTUALIZAR EL STOCK DEL PRODUCTO

        stocksProductos[posicionProducto] -= cantidad;

        // REGISTRAR EL PRODUCTO COMO ASOCIADO A UNA VENTA

        if (!codigosProductosVentas.Contains(codigoProducto))
            codigosProductosVentas.Add(codigoProducto);

        Console.WriteLine("\nVenta registrada correctamente.");
        Console.WriteLine("Codigo de venta: " + codigoVenta);
        Console.WriteLine("Stock restante: " +
            stocksProductos[posicionProducto]);
    }

    // CONSULTAR UNA VENTA POR SU CODIGO

    static void ConsultarVentaPorCodigo()
    {
        Console.WriteLine("\n========== CONSULTAR VENTA ==========");

        if (codigosVentas.Count == 0)
        {
            Console.WriteLine("No existen ventas registradas.");
            return;
        }

        if (!ValidarIntegridadListasVentas() ||
            !ValidarIntegridadListasProductos())
        {
            Console.WriteLine(
                "Error: las listas de ventas o productos no estan integras.");
            return;
        }

        if (!int.TryParse(
            LeerDatoVenta("Ingrese el codigo de la venta: "),
            out int codigo) || codigo <= 0)
        {
            Console.WriteLine("Codigo de venta invalido.");
            return;
        }

        int posicion = codigosVentas.IndexOf(codigo);

        if (posicion == -1)
        {
            Console.WriteLine("No se encontro la venta.");
            return;
        }

        MostrarDatosVenta(posicion);
    }

    // MOSTRAR LOS DATOS COMPLETOS DE UNA VENTA

    static void MostrarDatosVenta(int posicion)
    {
        if (posicion < 0 || posicion >= codigosVentas.Count)
        {
            Console.WriteLine("Posicion de venta invalida.");
            return;
        }

        Console.WriteLine("\n========== DETALLE DE VENTA ==========");
        Console.WriteLine("Codigo de venta: " + codigosVentas[posicion]);
        Console.WriteLine("Fecha: " + fechasVentas[posicion]);
        Console.WriteLine("Codigo de producto: " +
            codigosProductosVendidos[posicion]);
        Console.WriteLine("Producto: " +
            nombresProductosVendidos[posicion]);
        Console.WriteLine("Cantidad vendida: " +
            cantidadesVendidas[posicion]);
        Console.WriteLine("Precio unitario: S/ " +
            preciosUnitariosVentas[posicion].ToString("0.00"));
        Console.WriteLine("Total de venta: S/ " +
            totalesVentas[posicion].ToString("0.00"));

        int codigoPropietario =
            codigosPropietariosVentas[posicion];

        int codigoMascota =
            codigosMascotasVentas[posicion];

        if (codigoPropietario == 0)
        {
            Console.WriteLine("Tipo de venta: Venta libre.");
            return;
        }

        Console.WriteLine("Codigo de propietario: " +
            codigoPropietario);

        int posicionPropietario =
            BuscarPosicionPropietario(codigoPropietario);

        if (posicionPropietario != -1)
            MostrarDatosPropietario(posicionPropietario);

        if (codigoMascota == 0)
        {
            Console.WriteLine("Mascota: Sin asociar.");
            return;
        }

        Console.WriteLine("Codigo de mascota: " + codigoMascota);

        int posicionMascota =
            BuscarPosicionMascota(codigoMascota);

        if (posicionMascota != -1)
            MostrarDatosMascota(posicionMascota);
    }
}
