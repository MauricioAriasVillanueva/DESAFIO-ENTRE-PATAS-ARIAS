
using System;
using System.Collections.Generic;

partial class Program
{
    // CONSULTAR EL HISTORIAL CLINICO COMPLETO DE UNA MASCOTA

    static void HistorialClinico()
    {
        Console.WriteLine("\n========================================");
        Console.WriteLine("          HISTORIAL CLINICO");
        Console.WriteLine("========================================");

        // COMPROBAR QUE EXISTAN MASCOTAS

        if (codigosMascotas.Count == 0)
        {
            Console.WriteLine("No hay mascotas registradas.");
            return;
        }

        // VALIDAR LA INFORMACION DE LAS MASCOTAS

        if (!ValidarIntegridadListasMascotas())
        {
            Console.WriteLine(
                "Error: las listas de mascotas estan desincronizadas.");
            return;
        }

        // SOLICITAR EL CODIGO DE LA MASCOTA

        Console.Write("Ingrese el codigo de la mascota: ");

        if (!int.TryParse(Console.ReadLine(), out int codigoMascota) ||
            codigoMascota <= 0)
        {
            Console.WriteLine("Codigo de mascota invalido.");
            return;
        }

        // BUSCAR LA MASCOTA

        int posicionMascota = BuscarPosicionMascota(codigoMascota);

        if (posicionMascota == -1)
        {
            Console.WriteLine("No se encontro una mascota con ese codigo.");
            return;
        }

        // VALIDAR LAS LISTAS CLINICAS

        if (!ValidarIntegridadListasAtenciones())
        {
            Console.WriteLine(
                "Error: las listas de atenciones medicas no estan integras.");
            return;
        }

        if (!ValidarIntegridadListasServicios())
        {
            Console.WriteLine(
                "Error: las listas de servicios veterinarios no estan integras.");
            return;
        }

        // MOSTRAR INFORMACION GENERAL DE LA MASCOTA

        Console.WriteLine("\n========================================");
        Console.WriteLine("         DATOS DE LA MASCOTA");
        Console.WriteLine("========================================");

        MostrarDatosMascota(posicionMascota);

        // MOSTRAR ATENCIONES MEDICAS

        MostrarSeccionHistorial(
            "ATENCIONES MEDICAS",
            codigoMascota,
            codigosMascotasAtenciones,
            MostrarDatosAtencion
        );

        // MOSTRAR SERVICIOS VETERINARIOS

        MostrarSeccionHistorial(
            "SERVICIOS VETERINARIOS",
            codigoMascota,
            codigosMascotasServicios,
            MostrarDatosServicio
        );

        Console.WriteLine("\n========================================");
        Console.WriteLine("       FIN DEL HISTORIAL CLINICO");
        Console.WriteLine("========================================");
    }

    // MOSTRAR LOS REGISTROS DE UNA SECCION DEL HISTORIAL

    static void MostrarSeccionHistorial(
        string titulo,
        int codigoMascota,
        List<int> codigosMascotas,
        Action<int> mostrarRegistro)
    {
        Console.WriteLine("\n========================================");
        Console.WriteLine("        " + titulo);
        Console.WriteLine("========================================");

        int totalRegistros = 0;

        for (int i = 0; i < codigosMascotas.Count; i++)
        {
            if (codigosMascotas[i] != codigoMascota)
                continue;

            mostrarRegistro(i);
            totalRegistros++;
        }

        if (totalRegistros == 0)
        {
            Console.WriteLine(
                "No existen registros asociados a esta mascota.");
        }
        else
        {
            Console.WriteLine("\nTotal de registros encontrados: " +
                totalRegistros);
        }
    }
}
