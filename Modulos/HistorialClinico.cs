
partial class Program
{
    // CONSULTAR HISTORIAL CLINICO DE UNA MASCOTA
    static void HistorialClinico()
    {
        Console.WriteLine("\n======================================");
        Console.WriteLine("       HISTORIAL CLINICO");
        Console.WriteLine("======================================");

        if (codigosMascotas.Count == 0)
        {
            Console.WriteLine("No hay mascotas registradas.");
            return;
        }

        Console.Write("Ingrese el codigo de la mascota: ");

        if (!int.TryParse(Console.ReadLine(), out int codigoMascota)
            || codigoMascota <= 0)
        {
            Console.WriteLine("Codigo invalido.");
            return;
        }

        int posicionMascota = BuscarPosicionMascota(codigoMascota);

        if (posicionMascota == -1)
        {
            Console.WriteLine("Mascota no encontrada.");
            return;
        }

        // MOSTRAR DATOS GENERALES
        Console.WriteLine("\n========== DATOS DE LA MASCOTA ==========");
        MostrarDatosMascota(posicionMascota);

        // MOSTRAR ATENCIONES MEDICAS
        Console.WriteLine("\n========== ATENCIONES MEDICAS ==========");

        int totalAtenciones = 0;

        for (int i = 0; i < codigosMascotasAtenciones.Count; i++)
        {
            if (codigosMascotasAtenciones[i] == codigoMascota)
            {
                MostrarDatosAtencion(i);
                totalAtenciones++;
            }
        }

        if (totalAtenciones == 0)
        {
            Console.WriteLine("La mascota no tiene atenciones registradas.");
        }
        else
        {
            Console.WriteLine("\nTotal de atenciones: " + totalAtenciones);
        }

        Console.WriteLine("======================================");
    }
}
