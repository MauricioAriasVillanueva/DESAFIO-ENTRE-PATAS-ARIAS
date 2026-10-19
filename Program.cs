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

            if (opcion == 2)
            {
                Buscar();
            }
        } while (opcion !=7);
    }

    // INTEGRANTE 4: BUSQUEDA LINEAL DE MASCOTAS POR CODIGO
    static void Buscar()
    {
        if (codigosMascotas.Count == 0)
        {
            Console.WriteLine("No hay mascotas registradas.");
            return;
        }

        Console.Write("Ingrese el codigo de la mascota: ");
        int codigoBuscado;
        if (!int.TryParse(Console.ReadLine(), out codigoBuscado))
        {
            Console.WriteLine("Codigo invalido. Debe ingresar un numero entero.");
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

        // Las nueve listas deben conservar el mismo orden y cantidad de registros.
        if (posicion >= codigosPropietarios.Count || posicion >= propietarios.Count ||
            posicion >= telefonos.Count || posicion >= mascotas.Count ||
            posicion >= especies.Count || posicion >= tiposAnimales.Count ||
            posicion >= sexos.Count || posicion >= fechasNacimiento.Count)
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