
using System;

partial class Program
{
    // CONSULTAR HISTORIAL CLINICO COMPLETO DE UNA MASCOTA
    static void HistorialClinico()
    {
        Console.WriteLine("\n========== HISTORIAL CLINICO DE MASCOTA ==========");

        if (codigosMascotas.Count == 0)
        {
            Console.WriteLine("No hay mascotas registradas.");
            return;
        }

        Console.Write("Ingrese el codigo de la mascota: ");

        if (!int.TryParse(Console.ReadLine(), out int codigo)
            || codigo <= 0)
        {
            Console.WriteLine("Codigo de mascota invalido.");
            return;
        }

        int posicionMascota = BuscarPosicionMascota(codigo);

        if (posicionMascota == -1)
        {
            Console.WriteLine("Mascota no encontrada.");
            return;
        }

        // MOSTRAR DATOS DE LA MASCOTA
        Console.WriteLine("\n========== DATOS DE LA MASCOTA ==========");
        MostrarDatosMascota(posicionMascota);

        // MOSTRAR ATENCIONES MEDICAS
        Console.WriteLine("\n========== ATENCIONES MEDICAS ==========");

        int totalAtenciones = 0;

        for (int i = 0; i < codigosMascotasAtenciones.Count; i++)
        {
            if (codigosMascotasAtenciones[i] == codigo)
            {
                MostrarDatosAtencion(i);
                totalAtenciones++;
            }
        }

        if (totalAtenciones == 0)
        {
            Console.WriteLine("La mascota no tiene atenciones medicas registradas.");
        }
        else
        {
            Console.WriteLine("Total de atenciones: " + totalAtenciones);
        }

        // MOSTRAR SERVICIOS VETERINARIOS
        Console.WriteLine("\n========== SERVICIOS VETERINARIOS ==========");

        int totalServicios = 0;

        for (int i = 0; i < codigosMascotasServicios.Count; i++)
        {
            if (codigosMascotasServicios[i] == codigo)
            {
                MostrarDatosServicio(i);
                totalServicios++;
            }
        }

        if (totalServicios == 0)
        {
            Console.WriteLine("La mascota no tiene servicios registrados.");
        }
        else
        {
            Console.WriteLine("Total de servicios: " + totalServicios);
        }

        Console.WriteLine("\n========== FIN DEL HISTORIAL CLINICO ==========");
    }
}
