using System.Globalization;
using System.Collections.Generic;
using System;

class Program
{
    //DATOS DE PROPIETARIOS
static List<int> codigosPropietarios = new List<int>();
static List<string> propietarios = new List<string>();
static List<string> telefonos = new List<string>();

    //DATOS DE MASCOTAS
static List<int> codigosMascotas = new List<int>();
static List<string> mascotas = new List<string>();
static List<string> especies = new List<string>();
static List<string> tiposAnimales = new List<string>();
static List<string> sexos = new List<string>();
static List<DateTime> fechasNacimiento = new List<DateTime>();

    //CODIGOS AUTOMATICOS
static int codigoPropietario = 2001;
static int codigoMascota = 1001;
    static void Main()
    {
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
                opcion = 0; // Opción inválida
            }

            switch (opcion)
            {
                case 1:
                    Registrar();
                    break;
    // BUSQUEDA DE MASCOTAS
                case 2: 
                    Buscar();
                    break;

                case 5:
                    MostrarRegistros();
                    break;

                case 6:
                    Ordenar();
                    MostrarRegistros();
                    break;

                case 7:
                    Console.WriteLine("Saliendo del programa...");
                    break;

                default:
                    Console.WriteLine("Opcion invalida. Intente nuevamente.");
                    break;
            }
        } while (opcion !=7);
    }

    // REGISTRAR PROPIETARIO Y MASCOTA
    static void Registrar()
    {
        Console.WriteLine("\n--- REGISTRO DE PROPIETARIO Y MASCOTA ---");

        // DATOS DEL PROPIETARIO
        Console.Write("Nombre del propietario: ");
        string nombrePropietario = Console.ReadLine() ?? "";

        string telefono;

        while (true)
        {
            Console.Write("Telefono del propietario (9 digitos): ");
            telefono = (Console.ReadLine() ?? "").Trim();

            bool valido = telefono.Length == 9;

            foreach (char caracter in telefono)
        {
            if (!char.IsDigit(caracter))
            {
                valido = false;
                break;
            }
        }

            if (valido)
        {
            break;
        }

            Console.WriteLine("Error: ingrese un telefono de 9 digitos.");
        }
        // DATOS DE LA MASCOTA
        Console.Write("Nombre de la mascota: ");
        string nombreMascota = Console.ReadLine() ?? "";

        // TIPO DE ANIMAL
        Console.Write("Tipo de animal (Canino/Felino): ");
        string tipoIngresado = Console.ReadLine() ?? "";
        string tipoAnimal;

        if (tipoIngresado.Trim().Equals(
            "Canino", StringComparison.OrdinalIgnoreCase))
        {
            tipoAnimal = "Canino";
        }
        else if (tipoIngresado.Trim().Equals(
            "Felino", StringComparison.OrdinalIgnoreCase))
        {
            tipoAnimal = "Felino";
        }
        else
        {
            Console.WriteLine(
                "Error: escriba Canino o Felino.");
            return;
        }

        // ESPECIE O RAZA
        Console.Write("Especie o raza de la mascota: ");
        string especie = Console.ReadLine() ?? "";

        // SEXO DE LA MASCOTA
        Console.Write("Sexo de la mascota (Macho/Hembra): ");
        string sexoIngresado = Console.ReadLine() ?? "";
        string sexo;

        if (sexoIngresado.Trim().Equals(
            "Macho", StringComparison.OrdinalIgnoreCase))
        {
            sexo = "Macho";
        }
        else if (sexoIngresado.Trim().Equals(
            "Hembra", StringComparison.OrdinalIgnoreCase))
        {
            sexo = "Hembra";
        }
        else
        {
            Console.WriteLine(
                "Error: escriba Macho o Hembra.");
            return;
        }

        // FECHA DE NACIMIENTO

        Console.Write("Fecha de nacimiento de la mascota: ");
        string fechaIngresada = Console.ReadLine() ?? "";

        if (!DateTime.TryParse(
            fechaIngresada,
            CultureInfo.GetCultureInfo("es-PE"),
            DateTimeStyles.None,
            out DateTime fechaNacimiento))
        {
            Console.WriteLine(
            "Error: ingrese una fecha valida, por ejemplo 15/03/2024.");
            return;
        }

        if (fechaNacimiento.Date > DateTime.Today)
    {
            Console.WriteLine(
            "Error: la fecha de nacimiento no puede ser futura.");
            return;
        }



        // VALIDAR CAMPOS OBLIGATORIOS
        if (string.IsNullOrWhiteSpace(nombrePropietario) ||
            string.IsNullOrWhiteSpace(telefono) ||
            string.IsNullOrWhiteSpace(nombreMascota) ||
            string.IsNullOrWhiteSpace(especie))
        {
            Console.WriteLine(
                "Error: todos los campos son obligatorios.");
            return;
        }

        // GUARDAR DATOS DEL PROPIETARIO
        codigosPropietarios.Add(codigoPropietario);
        propietarios.Add(nombrePropietario.Trim());
        telefonos.Add(telefono.Trim());

        // GUARDAR DATOS DE LA MASCOTA
        codigosMascotas.Add(codigoMascota);
        mascotas.Add(nombreMascota.Trim());
        tiposAnimales.Add(tipoAnimal);
        especies.Add(especie.Trim());
        sexos.Add(sexo);
        fechasNacimiento.Add(fechaNacimiento);

        // MOSTRAR CONFIRMACION

        Console.WriteLine("\n======================================");
        Console.WriteLine("       REGISTRO EXITOSO");
        Console.WriteLine("======================================");

        Console.WriteLine("\nDATOS DEL PROPIETARIO");
        Console.WriteLine($"Codigo: {codigoPropietario}");
        Console.WriteLine($"Nombre: {nombrePropietario.Trim()}");
        Console.WriteLine($"Telefono: {telefono.Trim()}");

        Console.WriteLine("\nDATOS DE LA MASCOTA");
        Console.WriteLine($"Codigo: {codigoMascota}");
        Console.WriteLine($"Nombre: {nombreMascota.Trim()}");
        Console.WriteLine($"Tipo: {tipoAnimal}");
        Console.WriteLine($"Especie o raza: {especie.Trim()}");
        Console.WriteLine($"Sexo: {sexo}");
        Console.WriteLine($"Fecha de nacimiento: {fechaNacimiento:dd/MM/yyyy}");

        Console.WriteLine("\n======================================");


        // ACTUALIZAR CODIGOS PARA EL SIGUIENTE REGISTRO
        codigoPropietario++;
        codigoMascota++;
    }

    static void MostrarRegistros()
    {
        if (mascotas.Count == 0)
        {
            Console.WriteLine("No hay registros disponibles.");
            return;
        }
        else
        {
            for (int  i = 0; i <mascotas.Count; i++)
            {
                Console.WriteLine("Mascota: " + mascotas[i]);
                Console.WriteLine("Propietario: " + propietarios[i]);
                Console.WriteLine("Teléfono: " + telefonos[i]);
                Console.WriteLine("Código de la Mascota: " + codigosMascotas[i]);
                Console.WriteLine("Código del Propietario: " + codigosPropietarios[i]);
                Console.WriteLine("Especie: " + especies[i]);
                Console.WriteLine("Tipo de animal: " + tiposAnimales[i]);
                Console.WriteLine("Sexo: " + sexos[i]);
                Console.WriteLine("Fecha de nacimiento: " + fechasNacimiento[i].ToString("dd/MM/yyyy"));
                Console.WriteLine("------------------------------");
            }
        }
    }

    static void Ordenar()
    {
        if (mascotas.Count < 2)
        {
            Console.WriteLine("No hay registros disponibles para ordenar.");
            return;
        }
        for (int pasada = 0; pasada < mascotas.Count - 1; pasada++)
        {
            Console.WriteLine("Pasada: " + (pasada + 1));
            for (int j = 0; j < mascotas.Count - 1 - pasada ; j++)
            {
                Console.WriteLine("Comparando posición " + j + " con " + (j + 1));
                if (string.Compare(mascotas[j], mascotas[j + 1], StringComparison.OrdinalIgnoreCase) > 0)
                {
                    Console.WriteLine("Estas mascotas deben intercambiarse.");
                    string temporal = mascotas[j];
                    mascotas[j] = mascotas[j + 1];
                    mascotas[j + 1] = temporal;
                    int temporalCodigo = codigosMascotas[j];
                    codigosMascotas[j] = codigosMascotas[j + 1];
                    codigosMascotas[j + 1] = temporalCodigo;
                    int temporalPropietario = codigosPropietarios[j];
                    codigosPropietarios[j] = codigosPropietarios[j + 1];
                    codigosPropietarios[j + 1] = temporalPropietario;
                    string temporalNombre = propietarios[j];
                    propietarios[j] = propietarios[j + 1];
                    propietarios[j + 1] = temporalNombre;
                    string temporalTelefono = telefonos[j];
                    telefonos[j] = telefonos[j + 1];
                    telefonos[j + 1] = temporalTelefono;
                    string temporalEspecie = especies[j];
                    especies[j] = especies[j + 1];
                    especies[j + 1] = temporalEspecie;
                    string temporalTipo = tiposAnimales[j];
                    tiposAnimales[j] = tiposAnimales[j + 1];
                    tiposAnimales[j + 1] = temporalTipo;
                    string temporalSexo = sexos[j];
                    sexos[j] = sexos[j + 1];
                    sexos[j + 1] = temporalSexo;
                    DateTime temporalFecha = fechasNacimiento[j];
                    fechasNacimiento[j] = fechasNacimiento[j + 1];
                    fechasNacimiento[j + 1] = temporalFecha;
                }
            }
        }
    }



    // BUSQUEDA LINEAL DE MASCOTAS POR CODIGO
    static void Buscar()
    {
        if (codigosMascotas.Count == 0)
        {
            Console.WriteLine("No hay mascotas registradas.");
            return;
        }

        Console.Write("Ingrese el codigo de la mascota: ");

        int codigoBuscado;
        if (!int.TryParse(Console.ReadLine(), out codigoBuscado) || codigoBuscado <= 0)
        {
            Console.WriteLine("Codigo invalido. Ingrese un numero entero positivo.");
            return;
        }

        int posicion = -1;

        for (int i = 0; i < codigosMascotas.Count; i++)
        {
            if (codigosMascotas[i] == codigoBuscado)
            {
                posicion = i;
                break;
            }
        }

        if (posicion == -1)
        {
            Console.WriteLine("No se encontro una mascota con ese codigo.");
            return;
        }

        // Verificar que las nueve listas tengan los datos correspondientes
        if (posicion >= codigosPropietarios.Count ||
            posicion >= propietarios.Count ||
            posicion >= telefonos.Count ||
            posicion >= mascotas.Count ||
            posicion >= especies.Count ||
            posicion >= tiposAnimales.Count ||
            posicion >= sexos.Count ||
            posicion >= fechasNacimiento.Count)
        {
            Console.WriteLine("Error: los datos del registro estan incompletos.");
            return;
        }

        Console.WriteLine("=== DATOS DE LA MASCOTA ENCONTRADA ===");
        Console.WriteLine("Codigo de propietario: " + codigosPropietarios[posicion]);
        Console.WriteLine("Nombre del propietario: " + propietarios[posicion]);
        Console.WriteLine("Telefono: " + telefonos[posicion]);
        Console.WriteLine("Codigo de mascota: " + codigosMascotas[posicion]);
        Console.WriteLine("Nombre de mascota: " + mascotas[posicion]);
        Console.WriteLine("Especie: " + especies[posicion]);
        Console.WriteLine("Tipo de animal: " + tiposAnimales[posicion]);
        Console.WriteLine("Sexo: " + sexos[posicion]);
        Console.WriteLine("Fecha de nacimiento: " + fechasNacimiento[posicion].ToString("dd/MM/yyyy"));
    }
}