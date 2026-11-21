using System;
using System.Collections.Generic;

// Version individual del EF: propietarios y mascotas.
// Mantiene los mismos datos y metodos que el proyecto de integracion.
partial class Program
{
    // DATOS DE PROPIETARIOS
    static List<int> codigosPropietarios = new List<int>();
    static List<string> propietarios = new List<string>();
    static List<string> telefonos = new List<string>();
    static List<string> dnisPropietarios = new List<string>();
    static List<string> direcciones = new List<string>();
    static List<string> correos = new List<string>();

    // DATOS DE MASCOTAS Y RELACION CON PROPIETARIOS
    static List<int> codigosPropietariosMascotas = new List<int>();
    static List<int> codigosMascotas = new List<int>();
    static List<string> mascotas = new List<string>();
    static List<string> especies = new List<string>();
    static List<string> tiposAnimales = new List<string>();
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
            Console.WriteLine();
            Console.WriteLine("========================================");
            Console.WriteLine("       VETERINARIA ENTRE PATAS - ARIAS");
            Console.WriteLine("         APORTE DEL INTEGRANTE 1");
            Console.WriteLine("========================================");
            Console.WriteLine("1. Gestionar propietarios");
            Console.WriteLine("2. Gestionar mascotas");
            Console.WriteLine("0. Salir");
            Console.Write("Seleccione una opcion: ");
            opcion = LeerOpcion();

            switch (opcion)
            {
                case 1:
                    MostrarSubmenu("GESTIONAR PROPIETARIOS", new string[]
                    {
                        "Registrar propietario",
                        "Buscar propietario por codigo",
                        "Modificar propietario",
                        "Eliminar propietario",
                        "Ver mascotas asociadas"
                    }, 1);
                    break;
                case 2:
                    MostrarSubmenu("GESTIONAR MASCOTAS", new string[]
                    {
                        "Registrar mascota",
                        "Buscar mascota por codigo o nombre",
                        "Modificar mascota",
                        "Eliminar mascota"
                    }, 2);
                    break;
                case 0:
                    Console.WriteLine("Saliendo del programa...");
                    break;
                default:
                    Console.WriteLine("Opcion no valida.");
                    break;
            }
        } while (opcion != 0);
    }

    static int LeerOpcion()
    {
        if (int.TryParse(Console.ReadLine(), out int opcion))
            return opcion;
        return -1;
    }

    static void MostrarSubmenu(string titulo, string[] opciones, int modulo)
    {
        int opcionSubmenu;
        do
        {
            Console.WriteLine();
            Console.WriteLine("========================================");
            Console.WriteLine("          " + titulo);
            Console.WriteLine("========================================");
            for (int i = 0; i < opciones.Length; i++)
                Console.WriteLine((i + 1) + ". " + opciones[i]);

            Console.WriteLine("0. Volver al menu principal");
            Console.Write("Seleccione una opcion: ");
            opcionSubmenu = LeerOpcion();

            if (modulo == 1)
            {
                switch (opcionSubmenu)
                {
                    case 1: RegistrarPropietario(); break;
                    case 2: BuscarPropietario(); break;
                    case 3: ModificarPropietario(); break;
                    case 4: EliminarPropietario(); break;
                    case 5: VerMascotasAsociadas(); break;
                    case 0: break;
                    default: Console.WriteLine("Opcion no valida."); break;
                }
            }
            else if (modulo == 2)
            {
                switch (opcionSubmenu)
                {
                    case 1: RegistrarMascota(); break;
                    case 2: BuscarMascota(); break;
                    case 3: ModificarMascota(); break;
                    case 4: EliminarMascota(); break;
                    case 0: break;
                    default: Console.WriteLine("Opcion no valida."); break;
                }
            }
        } while (opcionSubmenu != 0);
    }
}
