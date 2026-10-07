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
                    Buscar();
                    break;

                case 3:
                    Modificar();
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


    static void Buscar()
    {
        Console.Clear();

        Console.WriteLine("=== BUSCAR MASCOTA ===");

        Console.Write("Ingrese codigo de mascota: ");
        int codigoBuscado = Convert.ToInt32(Console.ReadLine());

        int posicion = -1;

        for (int i = 0; i < codigosMascotas.Count; i++)
        {
            if (codigosMascotas[i] == codigoBuscado)
            {
                posicion = i;
                break;
            }
        }

        if (posicion != -1)
        {
            Console.WriteLine();
            Console.WriteLine("Registro encontrado");
            Console.WriteLine("-----------------------------");

            Console.WriteLine("Codigo propietario: " + codigosPropietarios[posicion]);
            Console.WriteLine("Propietario: " + propietarios[posicion]);
            Console.WriteLine("Telefono: " + telefonos[posicion]);

            Console.WriteLine();

            Console.WriteLine("Codigo mascota: " + codigosMascotas[posicion]);
            Console.WriteLine("Mascota: " + mascotas[posicion]);
            Console.WriteLine("Especie: " + especies[posicion]);
        }
        else
        {
            Console.WriteLine();
            Console.WriteLine("Mascota no encontrada.");
        }
    }


    static void Modificar()
    {
        Console.Clear();

        Console.WriteLine("=== MODIFICAR REGISTRO ===");

        Console.Write("Ingrese codigo de mascota: ");
        int codigoBuscado = Convert.ToInt32(Console.ReadLine());

        int posicion = -1;

        for (int i = 0; i < codigosMascotas.Count; i++)
        {
            if (codigosMascotas[i] == codigoBuscado)
            {
                posicion = i;
                break;
            }
        }

        if (posicion != -1)
        {
            Console.WriteLine();
            Console.WriteLine("Registro encontrado.");
            Console.WriteLine("Mascota actual: " + mascotas[posicion]);
            Console.WriteLine("Propietario actual: " + propietarios[posicion]);

            Console.WriteLine();

            Console.Write("Nuevo nombre del propietario: ");
            propietarios[posicion] = Console.ReadLine();

            Console.Write("Nuevo telefono: ");
            telefonos[posicion] = Console.ReadLine();

            Console.Write("Nuevo nombre de la mascota: ");
            mascotas[posicion] = Console.ReadLine();

            Console.Write("Nueva especie: ");
            especies[posicion] = Console.ReadLine();

            Console.WriteLine();
            Console.WriteLine("Registro modificado correctamente.");
        }
        else
        {
            Console.WriteLine();
            Console.WriteLine("Mascota no encontrada.");
        }
    }
}