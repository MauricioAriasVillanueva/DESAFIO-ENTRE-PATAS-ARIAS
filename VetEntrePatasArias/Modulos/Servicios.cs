using System;
using System.Collections.Generic;
using System.Globalization;

partial class Program
{
    // Cada posicion de estas listas representa un servicio prestado o programado.
    // Se relaciona con la mascota por su codigo, no por su posicion.
    static List<int> codigosServicios = new List<int>();
    static List<int> codigosMascotasServicios = new List<int>();
    static List<string> nombresServicios = new List<string>();
    static List<string> responsablesServicios = new List<string>();
    static List<DateTime> fechasServicios = new List<DateTime>();
    static List<string> horasServicios = new List<string>();
    static List<decimal> costosServicios = new List<decimal>();
    static List<string> observacionesServicios = new List<string>();
    static List<string> estadosServicios = new List<string>();

    static int BuscarPosicionServicio(int codigo)
    {
        for (int i = 0; i < codigosServicios.Count; i++)
            if (codigosServicios[i] == codigo) return i;
        return -1;
    }

    // Se basa en los codigos existentes, incluso despues de cargar datos.
    static int SiguienteCodigoServicio()
    {
        int mayor = 3000;
        for (int i = 0; i < codigosServicios.Count; i++)
            if (codigosServicios[i] > mayor) mayor = codigosServicios[i];
        return mayor == int.MaxValue ? -1 : mayor + 1;
    }

    static string LeerTextoServicio(string mensaje)
    {
        Console.Write(mensaje);
        return (Console.ReadLine() ?? "").Trim();
    }

    static bool ConfirmarServicio(string mensaje)
    {
        string respuesta = LeerTextoServicio(mensaje).ToUpperInvariant();
        return respuesta == "S" || respuesta == "SI";
    }

    static bool ValidarFechaServicio(string dato, out DateTime fecha)
    {
        return DateTime.TryParseExact(dato, "dd/MM/yyyy", CultureInfo.InvariantCulture,
            DateTimeStyles.None, out fecha);
    }

    static bool ValidarHoraServicio(string dato)
    {
        return DateTime.TryParseExact(dato, "HH:mm", CultureInfo.InvariantCulture,
            DateTimeStyles.None, out DateTime hora);
    }

    static bool ValidarCostoServicio(string dato, out decimal costo)
    {
        return decimal.TryParse(dato, NumberStyles.AllowDecimalPoint,
            CultureInfo.InvariantCulture, out costo) && costo >= 0;
    }

    static void RegistrarServicio()
    {
        Console.WriteLine("\n--- REGISTRAR SERVICIO ---");
        if (codigosMascotas.Count == 0)
        {
            Console.WriteLine("Primero debe registrar una mascota.");
            return;
        }
        if (!int.TryParse(LeerTextoServicio("Codigo de mascota (0 para cancelar): "), out int codigo) || codigo < 0)
        { Console.WriteLine("Codigo invalido."); return; }
        if (codigo == 0) { Console.WriteLine("Registro cancelado."); return; }
        int mascota = BuscarPosicionMascota(codigo);
        if (mascota < 0) { Console.WriteLine("Mascota no encontrada."); return; }
        int dueno = BuscarPosicionPropietario(codigosPropietariosMascotas[mascota]);
        if (dueno < 0) { Console.WriteLine("La mascota no tiene un propietario valido."); return; }
        Console.WriteLine("Mascota: " + mascotas[mascota] + " | Propietario: " + propietarios[dueno]);
        string nombre = LeerTextoServicio("Nombre del servicio: ");
        string responsable = LeerTextoServicio("Responsable: ");
        if (nombre == "" || responsable == "")
        { Console.WriteLine("El servicio y el responsable son obligatorios."); return; }
        if (!ValidarFechaServicio(LeerTextoServicio("Fecha (dd/MM/yyyy): "), out DateTime fecha))
        { Console.WriteLine("Fecha invalida."); return; }
        string hora = LeerTextoServicio("Hora (HH:mm): ");
        if (!ValidarHoraServicio(hora)) { Console.WriteLine("Hora invalida."); return; }
        if (!ValidarCostoServicio(LeerTextoServicio("Costo (use punto decimal): "), out decimal costo))
        { Console.WriteLine("Costo invalido."); return; }
        string observacion = LeerTextoServicio("Observaciones (opcional): ");
        int codigoServicio = SiguienteCodigoServicio();
        if (codigoServicio == -1) { Console.WriteLine("No hay mas codigos disponibles."); return; }
        if (!ConfirmarServicio("Guardar servicio? (S/N): ")) { Console.WriteLine("Registro cancelado."); return; }
        codigosServicios.Add(codigoServicio);
        codigosMascotasServicios.Add(codigo);
        nombresServicios.Add(nombre);
        responsablesServicios.Add(responsable);
        fechasServicios.Add(fecha);
        horasServicios.Add(hora);
        costosServicios.Add(costo);
        observacionesServicios.Add(observacion);
        estadosServicios.Add("Activo");
        Console.WriteLine("Servicio registrado. Codigo: " + codigoServicio);
    }

    static void MostrarDatosServicio(int i)
    {
        Console.WriteLine("\n--- DATOS DEL SERVICIO ---");
        Console.WriteLine("Codigo: " + codigosServicios[i]);
        Console.WriteLine("Codigo de mascota: " + codigosMascotasServicios[i]);
        int mascota = BuscarPosicionMascota(codigosMascotasServicios[i]);
        if (mascota >= 0)
        {
            Console.WriteLine("Mascota: " + mascotas[mascota]);
            int dueno = BuscarPosicionPropietario(codigosPropietariosMascotas[mascota]);
            Console.WriteLine("Propietario: " + (dueno >= 0 ? propietarios[dueno] : "No encontrado"));
        }
        else Console.WriteLine("Mascota no encontrada: referencia pendiente de revisar.");
        Console.WriteLine("Servicio: " + nombresServicios[i]);
        Console.WriteLine("Responsable: " + responsablesServicios[i]);
        Console.WriteLine("Fecha: " + fechasServicios[i].ToString("dd/MM/yyyy"));
        Console.WriteLine("Hora: " + horasServicios[i]);
        Console.WriteLine("Costo: " + costosServicios[i].ToString("0.00", CultureInfo.InvariantCulture));
        Console.WriteLine("Observaciones: " + observacionesServicios[i]);
        Console.WriteLine("Estado: " + estadosServicios[i]);
    }

    static int SolicitarPosicionServicio()
    {
        if (!int.TryParse(LeerTextoServicio("Codigo de servicio: "), out int codigo) || codigo <= 0)
        { Console.WriteLine("Codigo invalido."); return -1; }
        int posicion = BuscarPosicionServicio(codigo);
        if (posicion == -1) Console.WriteLine("Servicio no encontrado.");
        return posicion;
    }

    static void BuscarServicio()
    {
        int i = SolicitarPosicionServicio();
        if (i >= 0) MostrarDatosServicio(i);
    }

    static void ModificarServicio()
    {
        int i = SolicitarPosicionServicio();
        if (i < 0) return;
        if (estadosServicios[i] == "Anulado") { Console.WriteLine("No se puede modificar un servicio anulado."); return; }
        MostrarDatosServicio(i);
        Console.WriteLine("1. Servicio  2. Responsable  3. Fecha  4. Hora");
        Console.WriteLine("5. Costo  6. Observaciones  0. Cancelar");
        Console.Write("Seleccione una opcion: ");
        int campo = LeerOpcion();
        if (campo == 0) { Console.WriteLine("Modificacion cancelada."); return; }
        if (campo < 1 || campo > 6) { Console.WriteLine("Opcion invalida."); return; }
        string dato = LeerTextoServicio("Nuevo dato: ");
        if (dato == "" && campo != 6) { Console.WriteLine("El dato es obligatorio."); return; }
        switch (campo)
        {
            case 1: nombresServicios[i] = dato; break;
            case 2: responsablesServicios[i] = dato; break;
            case 3:
                if (!ValidarFechaServicio(dato, out DateTime fecha)) { Console.WriteLine("Fecha invalida."); return; }
                fechasServicios[i] = fecha; break;
            case 4:
                if (!ValidarHoraServicio(dato)) { Console.WriteLine("Hora invalida."); return; }
                horasServicios[i] = dato; break;
            case 5:
                if (!ValidarCostoServicio(dato, out decimal costo)) { Console.WriteLine("Costo invalido."); return; }
                costosServicios[i] = costo; break;
            case 6: observacionesServicios[i] = dato; break;
        }
        Console.WriteLine("Servicio modificado correctamente.");
    }

    static void AnularServicio()
    {
        int i = SolicitarPosicionServicio();
        if (i < 0) return;
        MostrarDatosServicio(i);
        if (estadosServicios[i] == "Anulado") { Console.WriteLine("El servicio ya esta anulado."); return; }
        if (ConfirmarServicio("Confirmar anulacion? (S/N): "))
        { estadosServicios[i] = "Anulado"; Console.WriteLine("Servicio anulado."); }
        else Console.WriteLine("Anulacion cancelada.");
    }
}
