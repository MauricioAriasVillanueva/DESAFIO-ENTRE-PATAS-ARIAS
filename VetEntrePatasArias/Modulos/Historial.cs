using System;
using System.Collections.Generic;

partial class Program
{
    static void HistorialClinico()
    {
        Console.WriteLine("\n========== HISTORIAL CLINICO ==========");
        if (codigosMascotas.Count == 0) { Console.WriteLine("No hay mascotas registradas."); return; }
        if (!ValidarIntegridadListasMascotas())
{
    Console.WriteLine("Error: las listas de mascotas estan desincronizadas.");
    return;
}
        if (!int.TryParse(LeerPetShop("Codigo de mascota: "), out int codigo) || codigo <= 0)
        { Console.WriteLine("Codigo invalido."); return; }
        int i = BuscarPosicionMascota(codigo);
        if (i < 0) { Console.WriteLine("Mascota no encontrada."); return; }
        MostrarDatosMascota(i);
        if (!ValidarIntegridadListasAtenciones() || !ValidarIntegridadListasServicios())
        { Console.WriteLine("Error: listas clinicas desincronizadas."); return; }
        MostrarSeccionHistorial("ATENCIONES MEDICAS", codigo, codigosMascotasAtenciones, MostrarDatosAtencion);
        MostrarSeccionHistorial("SERVICIOS VETERINARIOS", codigo, codigosMascotasServicios, MostrarDatosServicio);
        Console.WriteLine("========== FIN DEL HISTORIAL ==========");
    }

    // Un recorrido comun conserva la consulta por codigo e incluye anulados.
    static void MostrarSeccionHistorial(string titulo, int codigo, List<int> referencias, Action<int> mostrar)
    {
        Console.WriteLine("\n========== " + titulo + " ==========");
        int total = 0;
        for (int i = 0; i < referencias.Count; i++)
        {
            if (referencias[i] != codigo) continue;
            mostrar(i);
            total++;
        }
        Console.WriteLine(total == 0 ? "Sin registros asociados." : "Total de registros: " + total);
    }
}
