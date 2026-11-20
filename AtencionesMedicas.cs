
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

public static class AtencionesMedicas
{
    private class Atencion
    {
        public int Codigo { get; set; }
        public int CodigoMascota { get; set; }
        public string NombreMascota { get; set; } = "";
        public string Propietario { get; set; } = "";
        public string Especialidad { get; set; } = "";
        public string Veterinario { get; set; } = "";
        public DateTime Fecha { get; set; }
        public string Hora { get; set; } = "";
        public string Motivo { get; set; } = "";
        public string Estado { get; set; } = "Activa";
    }

    private static readonly List<Atencion> atenciones = new();
    private static int siguienteCodigo = 1001;

    // Recibe la lista de mascotas registradas en el sistema.
    public static void MostrarMenu(List<Mascota> mascotas)
    {
        int opcion;

        do
        {
            Console.WriteLine("\n========================================");
            Console.WriteLine("           ATENCIONES MEDICAS");
            Console.WriteLine("========================================");
            Console.WriteLine("1. Registrar atencion");
            Console.WriteLine("2. Buscar atencion por codigo");
            Console.WriteLine("3. Modificar atencion");
            Console.WriteLine("4. Anular atencion");
            Console.WriteLine("0. Volver al menu principal");
            Console.Write("Seleccione una opcion: ");

            if (!int.TryParse(Console.ReadLine(), out opcion))
            {
                Console.WriteLine("Ingrese una opcion numerica.");
                continue;
            }

            switch (opcion)
            {
                case 1:
                    RegistrarAtencion(mascotas);
                    break;
                case 2:
                    BuscarAtencion();
                    break;
                case 3:
                    ModificarAtencion();
                    break;
                case 4:
                    AnularAtencion();
                    break;
                case 0:
                    Console.WriteLine("Volviendo al menu principal...");
                    break;
                default:
                    Console.WriteLine("Opcion no valida.");
                    break;
            }

        } while (opcion != 0);
    }

    private static void RegistrarAtencion(List<Mascota> mascotas)
    {
        Console.WriteLine("\n--- REGISTRAR ATENCION ---");

        if (mascotas.Count == 0)
        {
            Console.WriteLine("No existen mascotas registradas.");
            return;
        }

        Console.WriteLine("\nMASCOTAS REGISTRADAS");
        foreach (var mascota in mascotas)
        {
            Console.WriteLine(
                $"Codigo: {mascota.Codigo} | Mascota: {mascota.Nombre} | Propietario: {mascota.Propietario}");
        }

        Console.Write("\nIngrese codigo de mascota: ");
        if (!int.TryParse(Console.ReadLine(), out int codigoMascota))
        {
            Console.WriteLine("Codigo incorrecto.");
            return;
        }

        Mascota? seleccionada = mascotas.FirstOrDefault(
            m => m.Codigo == codigoMascota);

        if (seleccionada == null)
        {
            Console.WriteLine("La mascota no esta registrada.");
            return;
        }

        Atencion nueva = new Atencion
        {
            Codigo = siguienteCodigo++,
            CodigoMascota = seleccionada.Codigo,
            NombreMascota = seleccionada.Nombre,
            Propietario = seleccionada.Propietario,
            Especialidad = LeerTexto("Especialidad: "),
            Veterinario = LeerTexto("Nombre del veterinario: "),
            Motivo = LeerTexto("Motivo de consulta: ")
        };

        Console.Write("Fecha (dd/MM/yyyy): ");
        if (!DateTime.TryParseExact(
            Console.ReadLine(), "dd/MM/yyyy",
            CultureInfo.InvariantCulture,
            DateTimeStyles.None, out DateTime fecha)
            || fecha.Date < DateTime.Today)
        {
            siguienteCodigo--;
            Console.WriteLine("Fecha incorrecta o anterior a hoy.");
            return;
        }

        nueva.Fecha = fecha;

        Console.Write("Hora (HH:mm): ");
        if (!TimeSpan.TryParseExact(
            Console.ReadLine(), @"hh\:mm",
            CultureInfo.InvariantCulture, out TimeSpan hora))
        {
            siguienteCodigo--;
            Console.WriteLine("Formato de hora incorrecto.");
            return;
        }

        nueva.Hora = hora.ToString(@"hh\:mm");

        Console.Write("¿Desea guardar la atencion? (S/N): ");
        if ((Console.ReadLine() ?? "").Trim().ToUpper() == "S")
        {
            atenciones.Add(nueva);
            Console.WriteLine($"Atencion registrada. Codigo: {nueva.Codigo}");
        }
        else
        {
            siguienteCodigo--;
            Console.WriteLine("Registro cancelado.");
        }
    }

    private static void BuscarAtencion()
    {
        Console.WriteLine("\n--- BUSCAR ATENCION ---");
        int codigo = LeerCodigo("Ingrese codigo de atencion: ");

        Atencion? encontrada = atenciones.FirstOrDefault(
            a => a.Codigo == codigo);

        if (encontrada == null)
        {
            Console.WriteLine("No se encontro la atencion.");
            return;
        }

        MostrarDatos(encontrada);
    }

    private static void ModificarAtencion()
    {
        Console.WriteLine("\n--- MODIFICAR ATENCION ---");
        int codigo = LeerCodigo("Ingrese codigo de atencion: ");

        Atencion? atencion = atenciones.FirstOrDefault(
            a => a.Codigo == codigo && a.Estado == "Activa");

        if (atencion == null)
        {
            Console.WriteLine("No se encontro una atencion activa.");
            return;
        }

        MostrarDatos(atencion);

        Console.WriteLine("\n1. Especialidad");
        Console.WriteLine("2. Veterinario");
        Console.WriteLine("3. Fecha");
        Console.WriteLine("4. Hora");
        Console.WriteLine("5. Motivo de consulta");
        Console.Write("Seleccione el dato que desea modificar: ");

        if (!int.TryParse(Console.ReadLine(), out int opcion))
        {
            Console.WriteLine("Opcion incorrecta.");
            return;
        }

        switch (opcion)
        {
            case 1:
                atencion.Especialidad = LeerTexto("Nueva especialidad: ");
                break;
            case 2:
                atencion.Veterinario = LeerTexto("Nuevo veterinario: ");
                break;
            case 3:
                Console.Write("Nueva fecha (dd/MM/yyyy): ");
                if (!DateTime.TryParseExact(
                    Console.ReadLine(), "dd/MM/yyyy",
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.None, out DateTime fecha)
                    || fecha.Date < DateTime.Today)
                {
                    Console.WriteLine("Fecha incorrecta.");
                    return;
                }
                atencion.Fecha = fecha;
                break;
            case 4:
                Console.Write("Nueva hora (HH:mm): ");
                if (!TimeSpan.TryParseExact(
                    Console.ReadLine(), @"hh\:mm",
                    CultureInfo.InvariantCulture, out TimeSpan hora))
                {
                    Console.WriteLine("Hora incorrecta.");
                    return;
                }
                atencion.Hora = hora.ToString(@"hh\:mm");
                break;
            case 5:
                atencion.Motivo = LeerTexto("Nuevo motivo: ");
                break;
            default:
                Console.WriteLine("Opcion incorrecta.");
                return;
        }

        Console.WriteLine("Atencion modificada correctamente.");
    }

    private static void AnularAtencion()
    {
        Console.WriteLine("\n--- ANULAR ATENCION ---");
        int codigo = LeerCodigo("Ingrese codigo de atencion: ");

        Atencion? atencion = atenciones.FirstOrDefault(
            a => a.Codigo == codigo && a.Estado == "Activa");

        if (atencion == null)
        {
            Console.WriteLine("No se encontro una atencion activa.");
            return;
        }

        MostrarDatos(atencion);

        Console.Write("¿Confirma la anulacion? (S/N): ");
        if ((Console.ReadLine() ?? "").Trim().ToUpper() == "S")
        {
            atencion.Estado = "Anulada";
            Console.WriteLine("Atencion anulada correctamente.");
        }
        else
        {
            Console.WriteLine("Anulacion cancelada.");
        }
    }

    private static void MostrarDatos(Atencion a)
    {
        Console.WriteLine("\n========== DATOS DE ATENCION ==========");
        Console.WriteLine($"Codigo: {a.Codigo}");
        Console.WriteLine($"Codigo de mascota: {a.CodigoMascota}");
        Console.WriteLine($"Mascota: {a.NombreMascota}");
        Console.WriteLine($"Propietario: {a.Propietario}");
        Console.WriteLine($"Especialidad: {a.Especialidad}");
        Console.WriteLine($"Veterinario: {a.Veterinario}");
        Console.WriteLine($"Fecha: {a.Fecha:dd/MM/yyyy}");
        Console.WriteLine($"Hora: {a.Hora}");
        Console.WriteLine($"Motivo: {a.Motivo}");
        Console.WriteLine($"Estado: {a.Estado}");
        Console.WriteLine("=======================================");
    }

    private static int LeerCodigo(string mensaje)
    {
        Console.Write(mensaje);
        while (!int.TryParse(Console.ReadLine(), out int codigo) || codigo <= 0)
        {
            Console.Write("Codigo incorrecto. Intente nuevamente: ");
        }
        return codigo;
    }

    private static string LeerTexto(string mensaje)
    {
        string texto;
        do
        {
            Console.Write(mensaje);
            texto = Console.ReadLine() ?? "";

            if (string.IsNullOrWhiteSpace(texto))
                Console.WriteLine("El campo no puede estar vacio.");

        } while (string.IsNullOrWhiteSpace(texto));

        return texto.Trim();
    }
}
