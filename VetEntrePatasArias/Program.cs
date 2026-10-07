using System;
using System.Collections.Generic;

class Program
{
    static List<int> codigosPropietarios = new List<int>();
    static List<string> propietarios = new List<string>();
    static List<string> telefonos = new List<string>();

    static List<int> codigosMascotas = new List<int>();
    static List<string> mascotas = new List<string>();
    static List<string> especies = new List<string>();

    static int codigoPropietario = 2001;
    static int codigoMascota = 1001;

    static void Main()
    {
        int opcion;

        do
        {
            Console.Clear();

            Console.WriteLine("====================================");
            Console.WriteLine("       SISTEMA VETERINARIA");
            Console.WriteLine("====================================");
            Console.WriteLine("1. Registrar propietario y mascota");
            Console.WriteLine("2. Buscar mascota");
            Console.WriteLine("3. Modificar registro");
            Console.WriteLine("4. Eliminar registro");
            Console.WriteLine("5. Mostrar registros");
            Console.WriteLine("6. Ordenar registros");
            Console.WriteLine("7. Salir");
            Console.WriteLine("====================================");
            Console.Write("Seleccione una opcion: ");

            opcion = Convert.ToInt32(Console.ReadLine());

            switch (opcion)
            {
                case 1:
                    Registrar();
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
                    Console.WriteLine("Opcion incorrecta");
                    break;
            }

            if (opcion != 7)
            {
                Console.WriteLine();
                Console.WriteLine("Presione una tecla para continuar...");
                Console.ReadKey();
            }

        } while (opcion != 7);
    }


    static void Registrar()
    {
        Console.Clear();

        Console.WriteLine("=== REGISTRO DE PROPIETARIO ===");

        Console.Write("Nombre del propietario: ");
        string nombrePropietario = Console.ReadLine();

        Console.Write("Telefono: ");
        string telefono = Console.ReadLine();

        Console.WriteLine();
        Console.WriteLine("=== REGISTRO DE MASCOTA ===");

        Console.Write("Nombre de la mascota: ");
        string nombreMascota = Console.ReadLine();

        Console.Write("Especie: ");
        string especie = Console.ReadLine();


        codigosPropietarios.Add(codigoPropietario);
        propietarios.Add(nombrePropietario);
        telefonos.Add(telefono);

        codigosMascotas.Add(codigoMascota);
        mascotas.Add(nombreMascota);
        especies.Add(especie);


        Console.WriteLine();
        Console.WriteLine("Registro realizado correctamente.");
        Console.WriteLine("Codigo propietario: " + codigoPropietario);
        Console.WriteLine("Codigo mascota: " + codigoMascota);


        codigoPropietario++;
        codigoMascota++;
    }
}