using System;
using System.Collections.Generic;

class Program
{
    // Listas del propietario
    static List<int> codigosPropietarios = new List<int>();
    static List<string> propietarios = new List<string>();
    static List<string> telefonos = new List<string>();

    // Listas de la mascota
    static List<int> codigosMascotas = new List<int>();
    static List<string> mascotas = new List<string>();
    static List<string> especies = new List<string>();

    // Codigos automaticos
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
                    Eliminar();
                    break;

                case 5:
                    MostrarRegistros();
                    break;

                case 6:
                    Ordenar();
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
                Console.WriteLine("Presione una tecla para continuar...");
                Console.ReadKey();
            }

        } while (opcion != 7);
    }


    // REGISTRAR
    static void Registrar()
    {
        Console.Clear();

        Console.WriteLine("=== REGISTRO DE PROPIETARIO ===");

        Console.Write("Nombre del propietario: ");
        string nombrePropietario = Console.ReadLine() ?? "";

        Console.Write("Telefono: ");
        string telefono = Console.ReadLine() ?? "";

        Console.WriteLine();
        Console.WriteLine("=== REGISTRO DE MASCOTA ===");

        Console.Write("Nombre de la mascota: ");
        string nombreMascota = Console.ReadLine() ?? "";

        Console.Write("Especie: ");
        string especie = Console.ReadLine() ?? "";


        // Insertar datos en las listas
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


        // Aumentar codigos para el siguiente registro
        codigoPropietario++;
        codigoMascota++;
    }


    // BUSQUEDA LINEAL
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
            Console.WriteLine("REGISTRO ENCONTRADO");
            Console.WriteLine("-----------------------------");

            Console.WriteLine("DATOS DEL PROPIETARIO");
            Console.WriteLine("Codigo: " + codigosPropietarios[posicion]);
            Console.WriteLine("Nombre: " + propietarios[posicion]);
            Console.WriteLine("Telefono: " + telefonos[posicion]);

            Console.WriteLine();

            Console.WriteLine("DATOS DE LA MASCOTA");
            Console.WriteLine("Codigo: " + codigosMascotas[posicion]);
            Console.WriteLine("Nombre: " + mascotas[posicion]);
            Console.WriteLine("Especie: " + especies[posicion]);
        }
        else
        {
            Console.WriteLine();
            Console.WriteLine("Mascota no encontrada.");
        }
    }


    // MODIFICAR
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
            Console.WriteLine("Propietario actual: " + propietarios[posicion]);
            Console.WriteLine("Mascota actual: " + mascotas[posicion]);

            Console.WriteLine();

            Console.Write("Nuevo nombre del propietario: ");
            propietarios[posicion] = Console.ReadLine() ?? "";

            Console.Write("Nuevo telefono: ");
            telefonos[posicion] = Console.ReadLine() ?? "";

            Console.Write("Nuevo nombre de la mascota: ");
            mascotas[posicion] = Console.ReadLine() ?? "";

            Console.Write("Nueva especie: ");
            especies[posicion] = Console.ReadLine() ?? "";

            Console.WriteLine();
            Console.WriteLine("Registro modificado correctamente.");
        }
        else
        {
            Console.WriteLine();
            Console.WriteLine("Mascota no encontrada.");
        }
    }


    // ELIMINAR
    static void Eliminar()
    {
        Console.Clear();

        Console.WriteLine("=== ELIMINAR REGISTRO ===");

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
            codigosPropietarios.RemoveAt(posicion);
            propietarios.RemoveAt(posicion);
            telefonos.RemoveAt(posicion);

            codigosMascotas.RemoveAt(posicion);
            mascotas.RemoveAt(posicion);
            especies.RemoveAt(posicion);

            Console.WriteLine();
            Console.WriteLine("Registro eliminado correctamente.");
        }
        else
        {
            Console.WriteLine();
            Console.WriteLine("Mascota no encontrada.");
        }
    }


    // MOSTRAR REGISTROS
    static void MostrarRegistros()
    {
        Console.Clear();

        Console.WriteLine("=== LISTA DE REGISTROS ===");

        if (mascotas.Count == 0)
        {
            Console.WriteLine("No existen registros.");
        }
        else
        {
            for (int i = 0; i < mascotas.Count; i++)
            {
                Console.WriteLine();
                Console.WriteLine("REGISTRO " + (i + 1));
                Console.WriteLine("-----------------------------");

                Console.WriteLine("DATOS DEL PROPIETARIO");
                Console.WriteLine("Codigo: " + codigosPropietarios[i]);
                Console.WriteLine("Nombre: " + propietarios[i]);
                Console.WriteLine("Telefono: " + telefonos[i]);

                Console.WriteLine();

                Console.WriteLine("DATOS DE LA MASCOTA");
                Console.WriteLine("Codigo: " + codigosMascotas[i]);
                Console.WriteLine("Nombre: " + mascotas[i]);
                Console.WriteLine("Especie: " + especies[i]);

                Console.WriteLine("-----------------------------");
            }
        }
    }


    // ORDENAMIENTO BURBUJA
    static void Ordenar()
    {
        Console.Clear();

        Console.WriteLine("=== ORDENAR REGISTROS ===");

        if (codigosMascotas.Count < 2)
        {
            Console.WriteLine("No hay suficientes registros para ordenar.");
        }
        else
        {
            for (int pasada = 0; pasada < codigosMascotas.Count - 1; pasada++)
            {
                for (int i = 0; i < codigosMascotas.Count - 1 - pasada; i++)
                {
                    if (codigosMascotas[i] > codigosMascotas[i + 1])
                    {
                        // Codigo mascota
                        int tempCodigo = codigosMascotas[i];
                        codigosMascotas[i] = codigosMascotas[i + 1];
                        codigosMascotas[i + 1] = tempCodigo;

                        // Codigo propietario
                        tempCodigo = codigosPropietarios[i];
                        codigosPropietarios[i] = codigosPropietarios[i + 1];
                        codigosPropietarios[i + 1] = tempCodigo;

                        // Propietario
                        string tempTexto = propietarios[i];
                        propietarios[i] = propietarios[i + 1];
                        propietarios[i + 1] = tempTexto;

                        // Telefono
                        tempTexto = telefonos[i];
                        telefonos[i] = telefonos[i + 1];
                        telefonos[i + 1] = tempTexto;

                        // Mascota
                        tempTexto = mascotas[i];
                        mascotas[i] = mascotas[i + 1];
                        mascotas[i + 1] = tempTexto;

                        // Especie
                        tempTexto = especies[i];
                        especies[i] = especies[i + 1];
                        especies[i + 1] = tempTexto;
                    }
                }
            }

            Console.WriteLine("Registros ordenados correctamente por codigo de mascota.");
        }
    }
}