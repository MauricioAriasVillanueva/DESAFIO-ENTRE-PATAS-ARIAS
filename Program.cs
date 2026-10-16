
using System;
using System.Collections.Generic;
using System.Globalization;

class Program
{
    // DATOS DE LOS PROPIETARIOS
    static List<int> codigosPropietarios = new List<int>();
    static List<string> propietarios = new List<string>();
    static List<string> telefonos = new List<string>();

    // DATOS DE LAS MASCOTAS
    static List<int> codigosMascotas = new List<int>();
    static List<string> mascotas = new List<string>();
    static List<string> tiposAnimales = new List<string>();
    static List<string> especies = new List<string>();
    static List<string> sexos = new List<string>();
    static List<DateTime> fechasNacimiento = new List<DateTime>();

    // CODIGOS AUTOMATICOS
    static int codigoPropietario = 2001;
    static int codigoMascota = 1001;

    static void Main()
    {
        int opcion;

        do
        {
            Console.WriteLine("\n======================================");
            Console.WriteLine("   VETERINARIA ENTRE PATAS - ARIAS");
            Console.WriteLine("======================================");
            Console.WriteLine("1. Registrar propietario y mascota");
            Console.WriteLine("2. Buscar mascota");
            Console.WriteLine("3. Modificar registro");
            Console.WriteLine("4. Eliminar registro");
            Console.WriteLine("5. Mostrar registros");
            Console.WriteLine("6. Ordenar registros");
            Console.WriteLine("7. Salir");
            Console.Write("\nSeleccione una opcion: ");

            if (!int.TryParse(Console.ReadLine(), out opcion))
            {
                opcion = 0;
            }

            switch (opcion)
            {
                case 1:
                    Registrar();
                    break;

                case 7:
                    Console.WriteLine("\nSaliendo del sistema...");
                    break;

                default:
                    Console.WriteLine(
                        "\nEsta opcion aun no esta implementada.");
                    break;
            }

        } while (opcion != 7);
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
}
