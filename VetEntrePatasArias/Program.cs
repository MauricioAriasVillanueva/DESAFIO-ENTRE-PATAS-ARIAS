using System.Collections.Generic;
using System;
using System.IO;
using System.Globalization;

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

    //CONTROL DE CARGA CSV
static bool cargaCSVExistosa = false;
    static void Main()
    {
        CargarDatosCSV();
        ActualizarCodigos();

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
            Console.WriteLine("7. Guardar datos en CSV");
            Console.WriteLine("8. Salir");
            Console.Write("Seleccione una opcion: ");

            if (!int.TryParse(Console.ReadLine(), out opcion))
            {
                opcion = 0; // Opción inválida
            }

            if (opcion == 5)
            {
                MostrarRegistros();
            }
            else if (opcion == 6)
            {
                Ordenar();
                MostrarRegistros();
            }
            else if (opcion == 7)
            {
                if (cargaCSVExistosa)
                {
                    GuardarPropietariosCSV();
                    GuardarMascotasCSV();
                }
                else
                {
                    Console.WriteLine("No se puede guardar los datos porque no se cargaron correctamente desde el CSV.");
                }
            }

        } while (opcion !=8);
    }

    static void CargaDatosPrueba()
    {
        //JUAN Y FIRULAIS

        codigosPropietarios.Add(2001);
        propietarios.Add("Juan Perez");
        telefonos.Add("123456789");

        codigosMascotas.Add(1001);
        mascotas.Add("Chocha");
        especies.Add("Perro");
        tiposAnimales.Add("Canino");
        sexos.Add("Macho");
        fechasNacimiento.Add(new DateTime(2020, 5, 15));

        //MARIA Y LUNA

        codigosPropietarios.Add(2002);
        propietarios.Add("Maria Gomez");
        telefonos.Add("987654321");
        codigosMascotas.Add(1002);

        mascotas.Add("Luna");
        especies.Add("Gato");
        tiposAnimales.Add("Felino");
        sexos.Add("Hembra");
        fechasNacimiento.Add(new DateTime(2020, 3, 10));

        //PEDRO Y SCOTT

        codigosPropietarios.Add(2003);
        propietarios.Add("Pedro Rodriguez");
        telefonos.Add("555555555");
        codigosMascotas.Add(1003);

        mascotas.Add("Scott");
        especies.Add("Perro");
        tiposAnimales.Add("Canino");
        sexos.Add("Macho");
        fechasNacimiento.Add(new DateTime(2020, 1, 1));
    }

    static void GuardarPropietariosCSV()
    {
        Directory.CreateDirectory("Datos");
        string rutaArchivo = Path.Combine("Datos", "propietarios.csv");
        using (StreamWriter Archivo = new StreamWriter(rutaArchivo))
        {
            Archivo.WriteLine("CodigoPropietario,Nombre,Telefono");
            for (int i = 0; i < propietarios.Count; i++)
            {
                Archivo.WriteLine($"{codigosPropietarios[i]},{propietarios[i]},{telefonos[i]}");
            }
        }

        Console.WriteLine("Datos de propietarios guardados Correctamente.");

    }

    static void GuardarMascotasCSV()
    {
        Directory.CreateDirectory("Datos");
        string rutaArchivo = Path.Combine("Datos", "mascotas.csv");
        using (StreamWriter Archivo = new StreamWriter(rutaArchivo))
        {
            Archivo.WriteLine("CodigoMascota,CodigoPropietario,Mascota,Especie,TipoAnimal,Sexo,FechaNacimiento");
            for (int i = 0; i < mascotas.Count; i++)
            {
                Archivo.WriteLine($"{codigosMascotas[i]},{codigosPropietarios[i]},{mascotas[i]},{especies[i]},{tiposAnimales[i]},{sexos[i]},{fechasNacimiento[i]:yyyy-MM-dd}");
            }
        }

        Console.WriteLine("Datos de mascotas guardados correctamente.");
    }

    static void CargarDatosCSV()
    {
        string rutaArchivo = Path.Combine("Datos", "propietarios.csv");
        string rutaArchivoMascotas = Path.Combine("Datos", "mascotas.csv");

        if (!File.Exists(rutaArchivo) || !File.Exists(rutaArchivoMascotas))
        {
            Console.WriteLine("No se encontraron archivos CSV para cargar los datos.");
            return;
        }

        string[] lineasPropietarios = File.ReadAllLines(rutaArchivo);
        string[] lineasMascotas = File.ReadAllLines(rutaArchivoMascotas);

        if (lineasPropietarios.Length != lineasMascotas.Length)
        {
            Console.WriteLine("El número de líneas en los archivos CSV no coincide.");
            return;
        }

        List<int> codigosPropietariosTemp = new List<int>();
        List<string> propietariosTemp = new List<string>();
        List<string> telefonosTemp = new List<string>();

        List<int> codigosMascotasTemp = new List<int>();
        List<string> mascotasTemp = new List<string>();
        List<string> especiesTemp = new List<string>();
        List<string> tiposAnimalesTemp = new List<string>();
        List<string> sexosTemp = new List<string>();
        List<DateTime> fechasNacimientoTemp = new List<DateTime>();

        for (int i = 1; i < lineasPropietarios.Length; i++)
        {
            string[] datosPropietario = lineasPropietarios[i].Split(',');
            string[] datosMascota = lineasMascotas[i].Split(',');

            if (datosPropietario.Length != 3 || datosMascota.Length != 7)
            {
                Console.WriteLine($"Formato incorrecto en la línea {i + 1} de los archivos CSV.");
                return;
            }


            if (!int.TryParse(datosPropietario[0], out int codigoDueño))
            {
                Console.WriteLine("El codigo del propietario no es valido.");
                return;
            }

            if (!int.TryParse(datosMascota[0], out int codigoMascotaCSV) || !int.TryParse(datosMascota[1], out int codigoPropietarioMascota))
            {
                Console.WriteLine("Los codigos de mascota o propietario no son validos.");
                return;
            }

            if (codigoDueño != codigoPropietarioMascota)
            {
                Console.WriteLine("Los codigos de propietario no coinciden.");
                return;
            }

            if (!DateTime.TryParseExact(datosMascota[6], "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime fechaLeida))
            {
                Console.WriteLine("La fecha de nacimiento no es valida.");
                return;
            }

            codigosPropietariosTemp.Add(codigoDueño);
            propietariosTemp.Add(datosPropietario[1]);
            telefonosTemp.Add(datosPropietario[2]);

            codigosMascotasTemp.Add(codigoMascotaCSV);
            mascotasTemp.Add(datosMascota[2]);
            especiesTemp.Add(datosMascota[3]);
            tiposAnimalesTemp.Add(datosMascota[4]);
            sexosTemp.Add(datosMascota[5]);

            fechasNacimientoTemp.Add(fechaLeida);
        }
        codigosPropietarios.AddRange(codigosPropietariosTemp);
        propietarios.AddRange(propietariosTemp);
        telefonos.AddRange(telefonosTemp);

        codigosMascotas.AddRange(codigosMascotasTemp);
        mascotas.AddRange(mascotasTemp);
        especies.AddRange(especiesTemp);
        tiposAnimales.AddRange(tiposAnimalesTemp);
        sexos.AddRange(sexosTemp);
        fechasNacimiento.AddRange(fechasNacimientoTemp);

        cargaCSVExistosa = true;
        Console.WriteLine("Datos recuperados correctamente.");
    }

    static void ActualizarCodigos()
    {
        for (int i = 0; i < codigosPropietarios.Count; i++)
        {
            if (codigosPropietarios[i] >= codigoPropietario)
            {
                codigoPropietario = codigosPropietarios[i] + 1;
            }
        }

        for (int i = 0; i < codigosMascotas.Count; i++)
        {
            if (codigosMascotas[i] >= codigoMascota)
            {
                codigoMascota = codigosMascotas[i] + 1;
            }
        }
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
}