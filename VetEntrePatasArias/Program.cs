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

    //CODIGOS AUTOMATICOS
static int codigoPropietario = 2001;
static int codigoMascota = 1001;
    static void Main()
    {
        MostrarRegistros();
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
        } while (opcion !=7);
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
                Console.WriteLine("------------------------------");
            }
        }
    }
}