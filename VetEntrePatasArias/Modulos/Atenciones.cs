using System;
using System.Collections.Generic;
using System.Globalization;

partial class Program
{
    // Las listas comparten posicion: cada indice representa una atencion.
    // Los otros modulos de Program pueden consultar estas listas.
    static List<int> codigosAtenciones = new List<int>();
    static List<int> codigosMascotasAtenciones = new List<int>();
    static List<string> especialidadesAtenciones = new List<string>();
    static List<string> veterinariosAtenciones = new List<string>();
    static List<DateTime> fechasAtenciones = new List<DateTime>();
    static List<string> horasAtenciones = new List<string>();
    static List<string> motivosAtenciones = new List<string>();
    static List<decimal> costosAtenciones = new List<decimal>();
    static List<string> diagnosticosAtenciones = new List<string>();
    static List<string> tratamientosAtenciones = new List<string>();
    static List<string> estadosAtenciones = new List<string>();
    static List<string> situacionesAtenciones = new List<string>(); // Programada o Realizada

    static int BuscarPosicionAtencion(int codigo)
    {
        for (int i = 0; i < codigosAtenciones.Count; i++)
            if (codigosAtenciones[i] == codigo) return i;
        return -1;
    }

    // Calcula a partir de los datos cargados: no depende de un contador en memoria.
    static int SiguienteCodigoAtencion()
    {
        int mayor = 1000;
        for (int i = 0; i < codigosAtenciones.Count; i++)
            if (codigosAtenciones[i] > mayor) mayor = codigosAtenciones[i];
        return mayor == int.MaxValue ? -1 : mayor + 1;
    }

    static bool FechaAtencionValida(string texto, out DateTime fecha)
    {
        return DateTime.TryParseExact(texto, "dd/MM/yyyy", CultureInfo.InvariantCulture,
            DateTimeStyles.None, out fecha);
    }

    static bool HoraAtencionValida(string texto)
    {
        return DateTime.TryParseExact(texto, "HH:mm", CultureInfo.InvariantCulture,
            DateTimeStyles.None, out DateTime hora);
    }

    static bool CostoAtencionValido(string texto, out decimal costo)
    {
        // Se acepta el punto decimal para facilitar el intercambio con CSV.
        return decimal.TryParse(texto, NumberStyles.AllowDecimalPoint,
            CultureInfo.InvariantCulture, out costo) && costo >= 0;
    }

    static string LeerTextoAtencion(string mensaje)
    {
        Console.Write(mensaje);
        return (Console.ReadLine() ?? "").Trim();
    }

    static bool ConfirmarAtencion(string mensaje)
    {
        string respuesta = LeerTextoAtencion(mensaje).ToUpperInvariant();
        return respuesta == "S" || respuesta == "SI";
    }

    static bool SituacionFechaValida(string situacion, DateTime fecha, string hora)
    {
        DateTime momento = fecha.Date.AddHours(int.Parse(hora.Substring(0, 2)))
            .AddMinutes(int.Parse(hora.Substring(3, 2)));
        if (situacion == "Realizada") return momento <= DateTime.Now;
        return momento >= DateTime.Now;
    }

    static void RegistrarAtencion()
    {
        Console.WriteLine("\n--- REGISTRAR ATENCION ---");
        if (codigosMascotas.Count == 0)
        {
            Console.WriteLine("No existen mascotas registradas.");
            return;
        }
        if (!int.TryParse(LeerTextoAtencion("Codigo de mascota (0 para cancelar): "), out int codigoMascotaAtencion) || codigoMascotaAtencion < 0)
        {
            Console.WriteLine("Codigo invalido."); return;
        }
        if (codigoMascotaAtencion == 0) { Console.WriteLine("Registro cancelado."); return; }
        int mascota = BuscarPosicionMascota(codigoMascotaAtencion);
        if (mascota == -1) { Console.WriteLine("La mascota no esta registrada."); return; }
        int propietario = BuscarPosicionPropietario(codigosPropietariosMascotas[mascota]);
        if (propietario == -1) { Console.WriteLine("La mascota no tiene un propietario valido."); return; }
        Console.WriteLine("Mascota: " + mascotas[mascota] + " | Propietario: " + propietarios[propietario]);
        string especialidad = LeerTextoAtencion("Especialidad: ");
        string veterinario = LeerTextoAtencion("Veterinario: ");
        string motivo = LeerTextoAtencion("Motivo: ");
        if (especialidad == "" || veterinario == "" || motivo == "")
        { Console.WriteLine("Los campos indicados son obligatorios."); return; }
        string tipo = LeerTextoAtencion("1. Programada / 2. Realizada: ");
        if (tipo != "1" && tipo != "2") { Console.WriteLine("Situacion invalida."); return; }
        string situacion = tipo == "1" ? "Programada" : "Realizada";
        if (!FechaAtencionValida(LeerTextoAtencion("Fecha (dd/MM/yyyy): "), out DateTime fecha))
        { Console.WriteLine("Fecha invalida."); return; }
        string hora = LeerTextoAtencion("Hora (HH:mm): ");
        if (!HoraAtencionValida(hora) || !SituacionFechaValida(situacion, fecha, hora))
        { Console.WriteLine("Hora invalida o fecha incompatible con la situacion."); return; }
        if (!CostoAtencionValido(LeerTextoAtencion("Costo (punto decimal): "), out decimal costo))
        { Console.WriteLine("Costo invalido."); return; }
        string diagnostico = "", tratamiento = "";
        if (situacion == "Realizada")
        {
            diagnostico = LeerTextoAtencion("Diagnostico: ");
            tratamiento = LeerTextoAtencion("Tratamiento: ");
            if (diagnostico == "" || tratamiento == "")
            { Console.WriteLine("Diagnostico y tratamiento son obligatorios para una atencion realizada."); return; }
        }
        int codigo = SiguienteCodigoAtencion();
        if (codigo == -1) { Console.WriteLine("No hay mas codigos disponibles."); return; }
        if (!ConfirmarAtencion("Guardar atencion? (S/N): ")) { Console.WriteLine("Registro cancelado."); return; }
        codigosAtenciones.Add(codigo);
        codigosMascotasAtenciones.Add(codigoMascotaAtencion);
        especialidadesAtenciones.Add(especialidad);
        veterinariosAtenciones.Add(veterinario);
        fechasAtenciones.Add(fecha);
        horasAtenciones.Add(hora);
        motivosAtenciones.Add(motivo);
        costosAtenciones.Add(costo);
        diagnosticosAtenciones.Add(diagnostico);
        tratamientosAtenciones.Add(tratamiento);
        estadosAtenciones.Add("Activa");
        situacionesAtenciones.Add(situacion);
        Console.WriteLine("Atencion registrada. Codigo: " + codigo);
    }

    static void MostrarDatosAtencion(int i)
    {
        Console.WriteLine("\n--- DATOS DE ATENCION ---");
        Console.WriteLine("Codigo: " + codigosAtenciones[i]);
        Console.WriteLine("Codigo de mascota: " + codigosMascotasAtenciones[i]);
        int mascota = BuscarPosicionMascota(codigosMascotasAtenciones[i]);
        if (mascota >= 0)
        {
            Console.WriteLine("Mascota: " + mascotas[mascota]);
            int dueno = BuscarPosicionPropietario(codigosPropietariosMascotas[mascota]);
            Console.WriteLine("Propietario: " + (dueno >= 0 ? propietarios[dueno] : "No encontrado"));
        }
        else Console.WriteLine("Mascota no encontrada: referencia pendiente de revisar.");
        Console.WriteLine("Especialidad: " + especialidadesAtenciones[i]);
        Console.WriteLine("Veterinario: " + veterinariosAtenciones[i]);
        Console.WriteLine("Fecha: " + fechasAtenciones[i].ToString("dd/MM/yyyy"));
        Console.WriteLine("Hora: " + horasAtenciones[i]);
        Console.WriteLine("Motivo: " + motivosAtenciones[i]);
        Console.WriteLine("Costo: " + costosAtenciones[i].ToString("0.00", CultureInfo.InvariantCulture));
        Console.WriteLine("Situacion: " + situacionesAtenciones[i]);
        Console.WriteLine("Diagnostico: " + (diagnosticosAtenciones[i] == "" ? "Pendiente" : diagnosticosAtenciones[i]));
        Console.WriteLine("Tratamiento: " + (tratamientosAtenciones[i] == "" ? "Pendiente" : tratamientosAtenciones[i]));
        Console.WriteLine("Estado: " + estadosAtenciones[i]);
    }

    static int SolicitarPosicionAtencion()
    {
        if (!int.TryParse(LeerTextoAtencion("Codigo de atencion: "), out int codigo) || codigo <= 0)
        { Console.WriteLine("Codigo invalido."); return -1; }
        int posicion = BuscarPosicionAtencion(codigo);
        if (posicion == -1) Console.WriteLine("Atencion no encontrada.");
        return posicion;
    }

    static void BuscarAtencion()
    {
        int i = SolicitarPosicionAtencion();
        if (i >= 0) MostrarDatosAtencion(i);
    }

    static void AnularAtencion()
    {
        int i = SolicitarPosicionAtencion();
        if (i < 0) return;
        MostrarDatosAtencion(i);
        if (estadosAtenciones[i] == "Anulada") { Console.WriteLine("La atencion ya esta anulada."); return; }
        if (ConfirmarAtencion("Confirmar anulacion? (S/N): "))
        { estadosAtenciones[i] = "Anulada"; Console.WriteLine("Atencion anulada."); }
        else Console.WriteLine("Anulacion cancelada.");
    }

    static void ModificarAtencion()
    {
        int i = SolicitarPosicionAtencion();
        if (i < 0) return;
        if (estadosAtenciones[i] == "Anulada") { Console.WriteLine("No se puede modificar una atencion anulada."); return; }
        MostrarDatosAtencion(i);
        Console.WriteLine("1. Especialidad  2. Veterinario  3. Fecha  4. Hora");
        Console.WriteLine("5. Motivo  6. Costo  7. Diagnostico  8. Tratamiento");
        Console.WriteLine("9. Marcar como realizada  0. Cancelar");
        int campo = LeerOpcion();
        if (campo == 0) { Console.WriteLine("Modificacion cancelada."); return; }
        if (campo < 1 || campo > 9) { Console.WriteLine("Opcion invalida."); return; }
        if (campo == 9)
        {
            if (situacionesAtenciones[i] == "Realizada") { Console.WriteLine("Ya esta realizada."); return; }
            if (!SituacionFechaValida("Realizada", fechasAtenciones[i], horasAtenciones[i]))
            { Console.WriteLine("Una atencion futura no puede marcarse realizada."); return; }
            string diagnostico = LeerTextoAtencion("Diagnostico: ");
            string tratamiento = LeerTextoAtencion("Tratamiento: ");
            if (diagnostico == "" || tratamiento == "") { Console.WriteLine("Ambos datos son obligatorios."); return; }
            diagnosticosAtenciones[i] = diagnostico;
            tratamientosAtenciones[i] = tratamiento;
            situacionesAtenciones[i] = "Realizada";
            Console.WriteLine("Atencion marcada como realizada."); return;
        }
        if ((campo == 7 || campo == 8) && situacionesAtenciones[i] != "Realizada")
        { Console.WriteLine("Primero marque la atencion como realizada."); return; }
        string dato = LeerTextoAtencion("Nuevo dato: ");
        if (dato == "") { Console.WriteLine("El dato es obligatorio."); return; }
        switch (campo)
        {
            case 1: especialidadesAtenciones[i] = dato; break;
            case 2: veterinariosAtenciones[i] = dato; break;
            case 3:
                if (!FechaAtencionValida(dato, out DateTime fecha) || !SituacionFechaValida(situacionesAtenciones[i], fecha, horasAtenciones[i]))
                { Console.WriteLine("Fecha invalida o incompatible con la situacion."); return; }
                fechasAtenciones[i] = fecha; break;
            case 4:
                if (!HoraAtencionValida(dato) || !SituacionFechaValida(situacionesAtenciones[i], fechasAtenciones[i], dato))
                { Console.WriteLine("Hora invalida o incompatible con la situacion."); return; }
                horasAtenciones[i] = dato; break;
            case 5: motivosAtenciones[i] = dato; break;
            case 6:
                if (!CostoAtencionValido(dato, out decimal costo)) { Console.WriteLine("Costo invalido."); return; }
                costosAtenciones[i] = costo; break;
            case 7: diagnosticosAtenciones[i] = dato; break;
            case 8: tratamientosAtenciones[i] = dato; break;
        }
        Console.WriteLine("Atencion modificada correctamente.");
    }
}
