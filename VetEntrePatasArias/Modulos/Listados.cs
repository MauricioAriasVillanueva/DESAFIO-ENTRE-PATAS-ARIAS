using System;
using System.Collections.Generic;

partial class Program
{
    // Reutiliza las mismas fichas de consulta en los seis listados.
    static void ListarEntidad(string titulo, int cantidad, bool integra, Action<int> mostrar)
    {
        Console.WriteLine("\n========== " + titulo + " ==========");
        if (!integra) { Console.WriteLine("Error: listas desincronizadas o datos invalidos."); return; }
        if (cantidad == 0) { Console.WriteLine("No hay registros."); return; }
        for (int i = 0; i < cantidad; i++) mostrar(i);
        Console.WriteLine("Total de registros: " + cantidad);
    }

    static void ListarPropietarios() =>
        ListarEntidad("PROPIETARIOS", codigosPropietarios.Count, ValidarIntegridadListasPropietarios(), MostrarDatosPropietario);

    static void ListarMascotas() =>
        ListarEntidad("MASCOTAS", codigosMascotas.Count, ValidarIntegridadListasMascotas(), MostrarDatosMascota);

    static void ListarAtenciones() =>
        ListarEntidad("ATENCIONES MEDICAS", codigosAtenciones.Count, ValidarIntegridadListasAtenciones(), MostrarDatosAtencion);

    static void ListarServicios() =>
        ListarEntidad("SERVICIOS VETERINARIOS", codigosServicios.Count, ValidarIntegridadListasServicios(), MostrarDatosServicio);

    static void ListarProductos() =>
        ListarEntidad("PRODUCTOS", codigosProductos.Count, ValidarIntegridadListasProductos(), MostrarDatosProducto);

    static void ListarVentas() =>
        ListarEntidad("VENTAS", codigosVentas.Count, ValidarIntegridadListasVentas(), MostrarDatosVenta);

    static void OrdenarMascotasPorCodigo() => OrdenarMascotas(false);
    static void OrdenarMascotasPorNombre() => OrdenarMascotas(true);

    // Burbuja mejorada: se mantiene el algoritmo trabajado en clase.
    static void OrdenarMascotas(bool porNombre)
    {
        if (!ValidarIntegridadListasMascotas())
        { Console.WriteLine("Error: listas de mascotas desincronizadas."); return; }
        if (codigosMascotas.Count == 0)
        { Console.WriteLine("No hay mascotas para ordenar."); return; }
        for (int i = 0; i < codigosMascotas.Count - 1; i++)
        {
            bool huboIntercambio = false;
            for (int j = 0; j < codigosMascotas.Count - 1 - i; j++)
            {
                bool intercambiar = porNombre
                    ? string.Compare(mascotas[j], mascotas[j + 1], StringComparison.OrdinalIgnoreCase) > 0
                    : codigosMascotas[j] > codigosMascotas[j + 1];
                if (!intercambiar) continue;
                IntercambiarMascotas(j, j + 1);
                huboIntercambio = true;
            }
            if (!huboIntercambio) break;
        }
        Console.WriteLine(porNombre ? "Mascotas ordenadas por nombre." : "Mascotas ordenadas por codigo.");
        ListarMascotas();
    }

    static void IntercambiarDato<T>(List<T> lista, int a, int b)
    {
        T temporal = lista[a];
        lista[a] = lista[b];
        lista[b] = temporal;
    }

    static void IntercambiarMascotas(int a, int b)
    {
        IntercambiarDato(codigosMascotas, a, b);
        IntercambiarDato(mascotas, a, b);
        IntercambiarDato(especies, a, b);
        IntercambiarDato(tiposAnimales, a, b);
        IntercambiarDato(sexos, a, b);
        IntercambiarDato(fechasNacimiento, a, b);
        IntercambiarDato(codigosPropietariosMascotas, a, b);
    }

    static int BuscarIndicePropietarioPorCodigo(int codigo) => BuscarPosicionPropietario(codigo);
    static int BuscarIndiceMascotaPorCodigo(int codigo) => BuscarPosicionMascota(codigo);

    static bool ValidarIntegridadListasPropietarios()
    {
        int total = codigosPropietarios.Count;
        return propietarios.Count == total &&
               telefonos.Count == total &&
               dnisPropietarios.Count == total &&
               direcciones.Count == total &&
               correos.Count == total;
    }

    static bool ValidarIntegridadListasMascotas()
    {
        int total = codigosMascotas.Count;
        return mascotas.Count == total &&
               especies.Count == total &&
               tiposAnimales.Count == total &&
               sexos.Count == total &&
               fechasNacimiento.Count == total &&
               codigosPropietariosMascotas.Count == total;
    }

    static bool ValidarIntegridadListasAtenciones()
    {
        int total = codigosAtenciones.Count;
        return codigosMascotasAtenciones.Count == total &&
               especialidadesAtenciones.Count == total &&
               veterinariosAtenciones.Count == total &&
               fechasAtenciones.Count == total &&
               horasAtenciones.Count == total &&
               motivosAtenciones.Count == total &&
               costosAtenciones.Count == total &&
               diagnosticosAtenciones.Count == total &&
               tratamientosAtenciones.Count == total &&
               estadosAtenciones.Count == total &&
               situacionesAtenciones.Count == total;
    }

    static bool ValidarIntegridadListasServicios()
    {
        int total = codigosServicios.Count;
        return codigosMascotasServicios.Count == total &&
               nombresServicios.Count == total &&
               responsablesServicios.Count == total &&
               fechasServicios.Count == total &&
               horasServicios.Count == total &&
               costosServicios.Count == total &&
               observacionesServicios.Count == total &&
               estadosServicios.Count == total;
    }
}
