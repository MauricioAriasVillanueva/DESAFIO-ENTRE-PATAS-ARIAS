using System;
using System.Globalization;

partial class Program
{
    // BUSCAR POSICION DE LA MASCOTA
    static int BuscarPosicionMascota(int codigo)
    {
        for (int i = 0; i < codigosMascotas.Count; i++)
        {
            if (codigosMascotas[i] == codigo)
            {
                return i;
            }
        }

        return -1;
    }

    // OBTENER TIPO DE ANIMAL
    static string ObtenerTipoAnimal(string dato)
    {
        if (dato.Equals("Canino", StringComparison.OrdinalIgnoreCase))
        {
            return "Canino";
        }

        if (dato.Equals("Felino", StringComparison.OrdinalIgnoreCase))
        {
            return "Felino";
        }

        return "";
    }

    // OBTENER SEXO DE LA MASCOTA
    static string ObtenerSexo(string dato)
    {
        if (dato.Equals("Macho", StringComparison.OrdinalIgnoreCase))
        {
            return "Macho";
        }

        if (dato.Equals("Hembra", StringComparison.OrdinalIgnoreCase))
        {
            return "Hembra";
        }

        return "";
    }

    // VALIDAR FECHA DE NACIMIENTO
    static bool ValidarFechaNacimiento(string dato, out DateTime fecha)
    {
        if (!DateTime.TryParseExact(dato,"dd/MM/yyyy",CultureInfo.InvariantCulture,DateTimeStyles.None,out fecha))
        {
            return false;
        }

        return fecha.Date <= DateTime.Today;
    }

    // MOSTRAR DATOS DE LA MASCOTA
    static void MostrarDatosMascota(int posicion)
    {
        Console.WriteLine("Codigo: " + codigosMascotas[posicion]);
        Console.WriteLine("Nombre: " + mascotas[posicion]);
        Console.WriteLine("Tipo: " + tiposAnimales[posicion]);
        Console.WriteLine("Especie o raza: " + especies[posicion]);
        Console.WriteLine("Sexo: " + sexos[posicion]);
        Console.WriteLine("Fecha de nacimiento: " +
            fechasNacimiento[posicion].ToString("dd/MM/yyyy"));

        int codigoDueno = codigosPropietariosMascotas[posicion];
        int posicionDueno = BuscarPosicionPropietario(codigoDueno);

        Console.WriteLine("Codigo propietario: " + codigoDueno);

        if (posicionDueno != -1)
        {
            Console.WriteLine("Propietario: " + propietarios[posicionDueno]);
        }
    }

    // REGISTRAR MASCOTA
    static void RegistrarMascota()
    {
        Console.WriteLine("\n--- REGISTRAR MASCOTA ---");

        // VERIFICAR PROPIETARIOS
        if (codigosPropietarios.Count == 0)
        {
            Console.WriteLine("Primero debe registrar un propietario.");
            return;
        }

        // BUSCAR PROPIETARIO
        Console.Write("Codigo del propietario: ");
        int codigoBuscado;

        if (!int.TryParse(Console.ReadLine(), out codigoBuscado) || codigoBuscado <= 0)
        {
            Console.WriteLine("Error: ingrese un codigo valido.");
            return;
        }

        int posicionPropietario = BuscarPosicionPropietario(codigoBuscado);

        if (posicionPropietario == -1)
        {
            Console.WriteLine("Error: el propietario no existe.");
            return;
        }

        Console.WriteLine("Propietario: " + propietarios[posicionPropietario]);

        string respuesta = "S";
        do
        {

            // VALIDAR NOMBRE
            Console.Write("Nombre de la mascota: ");
            string nombre = (Console.ReadLine() ?? "").Trim();

            if (string.IsNullOrWhiteSpace(nombre))
            {
                Console.WriteLine("Error: el nombre es obligatorio.");
                continue;
            }

            // VALIDAR TIPO
        Console.Write("Tipo de animal (Canino/Felino): ");
        string tipoIngresado = (Console.ReadLine() ?? "").Trim();

        string tipoAnimal = ObtenerTipoAnimal(tipoIngresado);

        if (tipoAnimal == "")
        {
            Console.WriteLine("Error: ingrese Canino o Felino.");
            continue;
        }

            // VALIDAR ESPECIE O RAZA
            Console.Write("Especie o raza: ");
            string especie = (Console.ReadLine() ?? "").Trim();

            if (string.IsNullOrWhiteSpace(especie))
            {
                Console.WriteLine("Error: la especie o raza es obligatoria.");
                continue;
            }

            // VALIDAR SEXO
            Console.Write("Sexo (Macho/Hembra): ");
            string sexoIngresado = (Console.ReadLine() ?? "").Trim();

            string sexo = ObtenerSexo(sexoIngresado);

            if (sexo == "")
            {
                Console.WriteLine("Error: ingrese Macho o Hembra.");
                continue;
            }

            // VALIDAR FECHA DE NACIMIENTO
            Console.Write("Fecha de nacimiento (dd/MM/yyyy): ");
            string fechaIngresada = (Console.ReadLine() ?? "").Trim();

            DateTime fechaNacimiento;

            if (!ValidarFechaNacimiento(fechaIngresada, out fechaNacimiento))
            {
                Console.WriteLine("Error: ingrese una fecha valida y no futura.");
                continue;
            }

            // GUARDAR MASCOTA
            codigosMascotas.Add(codigoMascota);
            mascotas.Add(nombre);
            tiposAnimales.Add(tipoAnimal);
            especies.Add(especie);
            sexos.Add(sexo);
            fechasNacimiento.Add(fechaNacimiento);

            // RELACIONAR MASCOTA CON PROPIETARIO
            codigosPropietariosMascotas.Add(codigoBuscado);

            // MOSTRAR CONFIRMACION
            Console.WriteLine("\n======================================");
            Console.WriteLine("        MASCOTA REGISTRADA");
            Console.WriteLine("======================================");

            MostrarDatosMascota(codigosMascotas.Count - 1);

            Console.WriteLine("======================================");

            // ACTUALIZAR CODIGO
            codigoMascota++;

            //PREGUNTAR SI DESEA REGISTRAR OTRA MASCOTA
            Console.Write("\n¿Desea registrar otra mascota para el mismo propietario? (S/N): ");
            respuesta = (Console.ReadLine() ?? "").Trim().ToUpper();
        }while (respuesta == "S" || respuesta == "SI");
    }


    // BUSCAR MASCOTA POR CODIGO O NOMBRE
    static void BuscarMascota()
    {
        Console.WriteLine("\n--- BUSCAR MASCOTA ---");

        if (codigosMascotas.Count == 0)
        {
            Console.WriteLine("No hay mascotas registradas.");
            return;
        }

        Console.Write("Ingrese el codigo o nombre de la mascota: ");
        string busqueda = (Console.ReadLine() ?? "").Trim();

        if (string.IsNullOrWhiteSpace(busqueda))
        {
            Console.WriteLine("Error: debe ingresar un codigo o nombre.");
            return;
        }

        // BUSQUEDA POR CODIGO
        if (int.TryParse(busqueda, out int codigo))
        {
            int posicion = BuscarPosicionMascota(codigo);

            if (posicion == -1)
            {
                Console.WriteLine("Mascota no encontrada.");
                return;
            }

            MostrarDatosMascota(posicion);
        }
        else
        {
            // BUSQUEDA POR NOMBRE
            int encontrados = 0;

            for (int i = 0; i < mascotas.Count; i++)
            {
                if (mascotas[i].Equals(busqueda,
                    StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine("\n--- MASCOTA ENCONTRADA ---");
                    MostrarDatosMascota(i);
                    encontrados++;
                }
            }

            if (encontrados == 0)
            {
                Console.WriteLine("No se encontraron mascotas con ese nombre.");
            }
            else
            {
                Console.WriteLine("\nTotal encontrado: " + encontrados);
            }
        }
    }

    // MODIFICAR MASCOTA
    static void ModificarMascota()
    {
        Console.WriteLine("\n--- MODIFICAR MASCOTA ---");

        if (codigosMascotas.Count == 0)
        {
            Console.WriteLine("No hay mascotas registradas.");
            return;
        }

        Console.Write("Codigo de la mascota: ");

        if (!int.TryParse(Console.ReadLine(), out int codigo) || codigo <= 0)
        {
            Console.WriteLine("Codigo invalido.");
            return;
        }

        int posicion = BuscarPosicionMascota(codigo);

        if (posicion == -1)
        {
            Console.WriteLine("Mascota no encontrada.");
            return;
        }

        // MOSTRAR DATOS ACTUALES
        MostrarDatosMascota(posicion);

        Console.WriteLine("\n--- DATOS A MODIFICAR ---");
        Console.WriteLine("1. Nombre");
        Console.WriteLine("2. Tipo de animal");
        Console.WriteLine("3. Especie o raza");
        Console.WriteLine("4. Sexo");
        Console.WriteLine("5. Fecha de nacimiento");
        Console.WriteLine("6. Cambiar propietario");
        Console.WriteLine("0. Cancelar");
        Console.Write("Seleccione una opcion: ");

        int campo = LeerOpcion();

        if (campo == 0)
        {
            Console.WriteLine("Modificacion cancelada.");
            return;
        }

        if (campo < 1 || campo > 6)
        {
            Console.WriteLine("Opcion invalida.");
            return;
        }

        Console.Write("Ingrese el nuevo dato: ");
        string dato = (Console.ReadLine() ?? "").Trim();

        if (string.IsNullOrWhiteSpace(dato))
        {
            Console.WriteLine("Error: el dato no puede estar vacio.");
            return;
        }

        switch (campo)
        {
            case 1:
                mascotas[posicion] = dato;
                break;

            case 2:
                string nuevoTipo = ObtenerTipoAnimal(dato);

                if (nuevoTipo == "")
                {
                    Console.WriteLine("Error: ingrese Canino o Felino.");
                    return;
                }

                tiposAnimales[posicion] = nuevoTipo;
                break;

            case 3:
                especies[posicion] = dato;
                break;

            case 4:
                string nuevoSexo = ObtenerSexo(dato);

                if (nuevoSexo == "")
                {
                    Console.WriteLine("Error: ingrese Macho o Hembra.");
                    return;
                }

                sexos[posicion] = nuevoSexo;
                break;

            case 5:
                DateTime fechaNueva;

                if (!ValidarFechaNacimiento(dato, out fechaNueva))
                {
                    Console.WriteLine("Error: fecha invalida o futura.");
                    return;
                }

                fechasNacimiento[posicion] = fechaNueva;
                break;

            case 6:
                if (!int.TryParse(dato, out int nuevoPropietario) ||
                    BuscarPosicionPropietario(nuevoPropietario) == -1)
                {
                    Console.WriteLine("Error: el propietario no existe.");
                    return;
                }

                codigosPropietariosMascotas[posicion] = nuevoPropietario;
                break;
        }

        Console.WriteLine("\nMascota modificada correctamente.");
        MostrarDatosMascota(posicion);
    }


    // ELIMINAR MASCOTA
    static void EliminarMascota()
    {
        Console.WriteLine("\n--- ELIMINAR MASCOTA ---");

        if (codigosMascotas.Count == 0)
        {
            Console.WriteLine("No hay mascotas registradas.");
            return;
        }

        Console.Write("Codigo de la mascota: ");

        if (!int.TryParse(Console.ReadLine(), out int codigo) || codigo <= 0)
        {
            Console.WriteLine("Error: ingrese un codigo valido.");
            return;
        }

        int posicion = BuscarPosicionMascota(codigo);

        if (posicion == -1)
        {
            Console.WriteLine("Mascota no encontrada.");
            return;
        }

        // MOSTRAR DATOS ANTES DE ELIMINAR
        MostrarDatosMascota(posicion);

        // CONFIRMAR ELIMINACION
        Console.Write("\nConfirmar eliminacion? (S/N): ");
        string respuesta = (Console.ReadLine() ?? "").Trim().ToUpper();

        if (respuesta != "S" && respuesta != "SI")
        {
            Console.WriteLine("Eliminacion cancelada.");
            return;
        }

        // ELIMINAR DATOS DE LA MASCOTA
        codigosMascotas.RemoveAt(posicion);
        mascotas.RemoveAt(posicion);
        tiposAnimales.RemoveAt(posicion);
        especies.RemoveAt(posicion);
        sexos.RemoveAt(posicion);
        fechasNacimiento.RemoveAt(posicion);
        codigosPropietariosMascotas.RemoveAt(posicion);

        Console.WriteLine("\nMascota eliminada correctamente.");
    }
}