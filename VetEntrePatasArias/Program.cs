using System;

class Program
{
    const int MAX_REGISTROS = 100;

    // Datos del propietario
    static int[] codigoPropietario = new int[MAX_REGISTROS];
    static string[] propietario = new string[MAX_REGISTROS];
    static string[] telefono = new string[MAX_REGISTROS];

    // Datos de la mascota
    static int[] codigoMascota = new int[MAX_REGISTROS];
    static string[] mascota = new string[MAX_REGISTROS];
    static string[] especie = new string[MAX_REGISTROS];

    // Control
    static int cantidad = 0;
    static int correlativoPropietario = 0;
    static int correlativoMascota = 0;

    static void Main()
    {
        int opcion;

        do
        {
            Console.Clear();

            Console.WriteLine("======================================");
            Console.WriteLine("        SISTEMA VETERINARIA");
            Console.WriteLine("======================================");
            Console.WriteLine("1. Registrar propietario y mascota");
            Console.WriteLine("2. Buscar mascota por codigo");
            Console.WriteLine("3. Modificar registro");
            Console.WriteLine("4. Eliminar registro");
            Console.WriteLine("5. Mostrar registros");
            Console.WriteLine("6. Ordenar registros");
            Console.WriteLine("7. Salir");
            Console.WriteLine("======================================");
            Console.Write("Seleccione una opcion: ");

            opcion = int.Parse(Console.ReadLine()!);

            Console.Clear();

            switch (opcion)
            {
                case 1:
                    RegistrarMascota();
                    break;

                case 2:
                    Console.WriteLine("BUSCAR MASCOTA");
                    break;

                case 3:
                    Console.WriteLine("MODIFICAR REGISTRO");
                    break;

                case 4:
                    Console.WriteLine("ELIMINAR REGISTRO");
                    break;

                case 5:
                    Console.WriteLine("MOSTRAR REGISTROS");
                    break;

                case 6:
                    Console.WriteLine("ORDENAR REGISTROS");
                    break;

                case 7:
                    Console.WriteLine("Saliendo del sistema...");
                    break;

                default:
                    Console.WriteLine("Opcion incorrecta.");
                    break;
            }

            if (opcion != 7)
            {
                Console.WriteLine();
                Console.WriteLine("Presione una tecla para volver al menu...");
                Console.ReadKey();
            }

        } while (opcion != 7);
    }


    static void RegistrarMascota()
    {
        if (cantidad >= MAX_REGISTROS)
        {
            Console.WriteLine("No se pueden registrar mas datos.");
            return;
        }

        correlativoPropietario++;
        correlativoMascota++;

        codigoPropietario[cantidad] = 2000 + correlativoPropietario;
        codigoMascota[cantidad] = 1000 + correlativoMascota;

        Console.WriteLine("======================================");
        Console.WriteLine("        REGISTRO DE PROPIETARIO");
        Console.WriteLine("======================================");

        Console.Write("Ingrese nombre del propietario: ");
        propietario[cantidad] = Console.ReadLine()!;

        Console.Write("Ingrese telefono del propietario: ");
        telefono[cantidad] = Console.ReadLine()!;

        Console.WriteLine();
        Console.WriteLine("Codigo de propietario generado: "
            + codigoPropietario[cantidad]);

        Console.WriteLine();
        Console.WriteLine("======================================");
        Console.WriteLine("          REGISTRO DE MASCOTA");
        Console.WriteLine("======================================");

        Console.Write("Ingrese nombre de la mascota: ");
        mascota[cantidad] = Console.ReadLine()!;

        Console.Write("Ingrese especie: ");
        especie[cantidad] = Console.ReadLine()!;

        Console.WriteLine();
        Console.WriteLine("Codigo de mascota generado: "
            + codigoMascota[cantidad]);

        cantidad++;

        Console.WriteLine();
        Console.WriteLine("Registro realizado correctamente.");
    }
}