using System;
using System.Collections.Generic;
using System.Globalization;

// BASE COLABORATIVA + MODULOS DE PIERO (INTEGRANTE 3)
// Misma estructura del Program.cs de Integrante-1: listas, Main y metodos.
class Program
{
    // DATOS DE PROPIETARIOS (9 listas compartidas con el equipo)
    static List<int> codigosPropietarios = new List<int>();
    static List<string> propietarios = new List<string>();
    static List<string> telefonos = new List<string>();

    // DATOS DE MASCOTAS
    static List<int> codigosMascotas = new List<int>();
    static List<string> mascotas = new List<string>();
    static List<string> especies = new List<string>();
    static List<string> tiposAnimales = new List<string>();
    static List<string> sexos = new List<string>();
    static List<DateTime> fechasNacimiento = new List<DateTime>();

    // CODIGOS AUTOMATICOS (base del equipo)
    static int codigoPropietario = 2001;
    static int codigoMascota = 1001;

    static void Main()
    {
        // SOLO PARA PROBAR EL MODULO INDIVIDUAL.
        // Cuando se integre Registrar() de Joaquin, quitar esta llamada.
        CargarDatosPrueba();

        int opcion;
        do
        {
            Console.WriteLine("================================");
            Console.WriteLine(" VETERINARIA ENTRE PATAS - ARIAS");
            Console.WriteLine("================================");
            Console.WriteLine("1. Registrar propietario y mascota");
            Console.WriteLine("2. Buscar mascota");
            Console.WriteLine("3. Modificar registro");
            Console.WriteLine("4. Eliminar registro");
            Console.WriteLine("5. Mostrar registros");
            Console.WriteLine("6. Ordenar registros");
            Console.WriteLine("7. Salir");
            Console.Write("Seleccione una opcion: ");

            if (!int.TryParse(Console.ReadLine(), out opcion))
            {
                opcion = 0;
            }

            switch (opcion)
            {
                case 3:
                    Modificar();
                    break;
                case 4:
                    Eliminar();
                    break;
                case 5:
                    // Ayuda de PRUEBA. El metodo oficial de listar es de Integrante-1.
                    MostrarDatosPrueba();
                    break;
                case 7:
                    Console.WriteLine("Saliendo del programa...");
                    break;
                default:
                    Console.WriteLine("Opcion pendiente de integrar o invalida.");
                    break;
            }
        } while (opcion != 7);
    }

    // INTEGRANTE 3 - BUSCAR POSICION POR CODIGO
    static int BuscarPosicion(int codigo)
    {
        for (int i = 0; i < codigosMascotas.Count; i++)
        {
            if (codigosMascotas[i] == codigo)
                return i;
        }
        return -1;
    }

    // INTEGRANTE 3 - MODIFICAR UN DATO DEL REGISTRO
    static void Modificar()
    {
        if (mascotas.Count == 0)
        {
            Console.WriteLine("No hay mascotas registradas.");
            return;
        }

        Console.Write("Codigo de mascota: ");
        int codigo;
        if (!int.TryParse(Console.ReadLine(), out codigo))
        {
            Console.WriteLine("Codigo invalido.");
            return;
        }

        int posicion = BuscarPosicion(codigo);
        if (posicion == -1)
        {
            Console.WriteLine("Mascota no encontrada.");
            return;
        }

        Console.WriteLine("Mascota: " + mascotas[posicion]);
        Console.WriteLine("1. Nombre de mascota");
        Console.WriteLine("2. Propietario");
        Console.WriteLine("3. Telefono");
        Console.WriteLine("4. Tipo de animal");
        Console.WriteLine("5. Especie o raza");
        Console.WriteLine("6. Sexo");
        Console.WriteLine("7. Fecha de nacimiento");
        Console.Write("Dato a modificar: ");

        int campo;
        if (!int.TryParse(Console.ReadLine(), out campo) || campo < 1 || campo > 7)
        {
            Console.WriteLine("Opcion invalida.");
            return;
        }

        Console.Write("Nuevo dato: ");
        string dato = (Console.ReadLine() ?? "").Trim();
        if (dato == "")
        {
            Console.WriteLine("No puede estar vacio.");
            return;
        }

        // Validaciones sencillas antes de guardar.
        bool valido = true;
        if (campo == 3)
        {
            valido = dato.Length == 9;
            for (int i = 0; i < dato.Length; i++)
            {
                if (dato[i] < '0' || dato[i] > '9')
                    valido = false;
            }
        }
        if (campo == 4 || campo == 6)
            dato = dato.ToUpper();
        if (campo == 4 && dato != "CANINO" && dato != "FELINO")
            valido = false;
        if (campo == 6 && dato != "MACHO" && dato != "HEMBRA")
            valido = false;

        DateTime fechaNueva = fechasNacimiento[posicion];
        if (campo == 7)
        {
            if (!DateTime.TryParse(dato, CultureInfo.GetCultureInfo("es-PE"),
                DateTimeStyles.None, out fechaNueva) || fechaNueva.Date > DateTime.Today)
                valido = false;
        }
        if (!valido)
        {
            Console.WriteLine("Dato invalido. No se modifico el registro.");
            return;
        }

        // Solo cambia el campo seleccionado. Los codigos se mantienen.
        switch (campo)
        {
            case 1: mascotas[posicion] = dato; break;
            case 2: propietarios[posicion] = dato; break;
            case 3: telefonos[posicion] = dato; break;
            case 4: tiposAnimales[posicion] = dato; break;
            case 5: especies[posicion] = dato; break;
            case 6: sexos[posicion] = dato; break;
            case 7: fechasNacimiento[posicion] = fechaNueva; break;
        }
        Console.WriteLine("Dato modificado correctamente.");
    }

    // INTEGRANTE 3 - ELIMINAR REGISTRO COMPLETO
    static void Eliminar()
    {
        if (mascotas.Count == 0)
        {
            Console.WriteLine("No hay mascotas registradas.");
            return;
        }

        Console.Write("Codigo de mascota a eliminar: ");
        int codigo;
        if (!int.TryParse(Console.ReadLine(), out codigo))
        {
            Console.WriteLine("Codigo invalido.");
            return;
        }

        int posicion = BuscarPosicion(codigo);
        if (posicion == -1)
        {
            Console.WriteLine("Mascota no encontrada.");
            return;
        }

        Console.WriteLine("Mascota: " + mascotas[posicion]);
        Console.Write("Confirmar eliminacion? (S/N): ");
        string respuesta = (Console.ReadLine() ?? "").ToUpper();
        if (respuesta != "S" && respuesta != "SI")
        {
            Console.WriteLine("Eliminacion cancelada.");
            return;
        }

        // Eliminar el mismo indice en las nueve listas.
        mascotas.RemoveAt(posicion);
        propietarios.RemoveAt(posicion);
        telefonos.RemoveAt(posicion);
        especies.RemoveAt(posicion);
        codigosMascotas.RemoveAt(posicion);
        codigosPropietarios.RemoveAt(posicion);
        tiposAnimales.RemoveAt(posicion);
        sexos.RemoveAt(posicion);
        fechasNacimiento.RemoveAt(posicion);

        Console.WriteLine("Mascota eliminada correctamente.");
    }

    // SOLO DATOS DE PRUEBA: mismo conjunto utilizado en el PSeInt de Integrante-1.
    // Quitar este metodo y su llamada al unir el modulo Registrar del equipo.
    static void CargarDatosPrueba()
    {
        codigosPropietarios.Add(2001);
        propietarios.Add("Juan");
        telefonos.Add("999111222");
        codigosMascotas.Add(1001);
        mascotas.Add("Zeus");
        especies.Add("Labrador");
        tiposAnimales.Add("Canino");
        sexos.Add("Macho");
        fechasNacimiento.Add(new DateTime(2024, 3, 15));

        codigosPropietarios.Add(2002);
        propietarios.Add("Maria");
        telefonos.Add("999333444");
        codigosMascotas.Add(1002);
        mascotas.Add("Firulais");
        especies.Add("Siames");
        tiposAnimales.Add("Felino");
        sexos.Add("Hembra");
        fechasNacimiento.Add(new DateTime(2023, 6, 10));

        codigosPropietarios.Add(2003);
        propietarios.Add("Pedro");
        telefonos.Add("999555666");
        codigosMascotas.Add(1003);
        mascotas.Add("Bobby");
        especies.Add("Beagle");
        tiposAnimales.Add("Canino");
        sexos.Add("Macho");
        fechasNacimiento.Add(new DateTime(2025, 1, 20));
    }

    // SOLO PARA COMPROBAR QUE LOS 9 DATOS SIGAN ASOCIADOS.
    // En la integracion se reemplaza por MostrarRegistros() de Integrante-1.
    static void MostrarDatosPrueba()
    {
        if (mascotas.Count == 0)
        {
            Console.WriteLine("No hay registros.");
            return;
        }
        for (int i = 0; i < mascotas.Count; i++)
        {
            Console.WriteLine("------------------------------");
            Console.WriteLine("Mascota: " + mascotas[i]);
            Console.WriteLine("Codigo mascota: " + codigosMascotas[i]);
            Console.WriteLine("Propietario: " + propietarios[i]);
            Console.WriteLine("Codigo propietario: " + codigosPropietarios[i]);
            Console.WriteLine("Telefono: " + telefonos[i]);
            Console.WriteLine("Especie o raza: " + especies[i]);
            Console.WriteLine("Tipo de animal: " + tiposAnimales[i]);
            Console.WriteLine("Sexo: " + sexos[i]);
            Console.WriteLine("Fecha: " + fechasNacimiento[i].ToString("dd/MM/yyyy"));
        }
    }
}
