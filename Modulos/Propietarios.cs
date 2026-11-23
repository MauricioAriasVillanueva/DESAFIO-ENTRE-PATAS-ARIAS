using System;

partial class Program
{
    // BUSCAR POSICION DEL PROPIETARIO
    static int BuscarPosicionPropietario(int codigo)
    {
        for (int i = 0; i < codigosPropietarios.Count; i++)
        {
            if (codigosPropietarios[i] == codigo)
            {
                return i;
            }
        }

        return -1;
    }

    // MOSTRAR DATOS DEL PROPIETARIO
    static void MostrarDatosPropietario(int posicion)
    {
        Console.WriteLine("Codigo: " + codigosPropietarios[posicion]);
        Console.WriteLine("Nombre: " + propietarios[posicion]);
        Console.WriteLine("DNI: " + dnisPropietarios[posicion]);
        Console.WriteLine("Telefono: " + telefonos[posicion]);
        Console.WriteLine("Direccion: " + direcciones[posicion]);
        Console.WriteLine("Correo: " + correos[posicion]);
    }

    // REGISTRAR PROPIETARIO
    static void RegistrarPropietario()
    {
        Console.WriteLine("\n--- REGISTRO DE PROPIETARIO ---");


        // VALIDAR NOMBRE
        Console.Write("Nombre del propietario: ");
        string nombre = (Console.ReadLine() ?? "").Trim();

        if (string.IsNullOrWhiteSpace(nombre))
        {
            Console.WriteLine("Error: el nombre del propietario es obligatorio.");
            return;
        }

        //VALIDAR DNI
        Console.Write("DNI del propietario: ");
        string dni = (Console.ReadLine() ?? "").Trim();

        bool dniValido = ValidarDigitos(dni, 8);

        if (!dniValido)
        {
            Console.WriteLine("Error: ingrese un DNI de 8 digitos.");
            return;
        }

        if (dnisPropietarios.Contains(dni))
        {
            Console.WriteLine("Error: el DNI ya esta registrado.");
            return;
        }

        // VALIDAR TELEFONO
        Console.Write("Telefono del propietario (9 digitos): ");
        string telefono = (Console.ReadLine() ?? "").Trim();

        bool telefonoValido = ValidarDigitos(telefono, 9);

        if (!telefonoValido)
        {
            Console.WriteLine("Error: ingrese un telefono de 9 digitos.");
            return;
        }

        //VALIDAR DIRECCION
        Console.Write("Direccion del propietario: ");
        string direccion = (Console.ReadLine() ?? "").Trim();

        if (string.IsNullOrWhiteSpace(direccion))
        {
            Console.WriteLine("Error: la direccion del propietario es obligatoria.");
            return;
        }

        //VALIDAR CORREO
        Console.Write("Correo del propietario: ");
        string correo = (Console.ReadLine() ?? "").Trim();

        if (!ValidarCorreo(correo))
        {
            Console.WriteLine("Error: ingrese un correo valido.");
            return;
        }

        // GUARDAR DATOS DEL PROPIETARIO
        codigosPropietarios.Add(codigoPropietario);
        propietarios.Add(nombre);
        dnisPropietarios.Add(dni);
        telefonos.Add(telefono);
        direcciones.Add(direccion);
        correos.Add(correo);

        // MOSTRAR CONFIRMACION
        Console.WriteLine("\n======================================");
        Console.WriteLine("       REGISTRO EXITOSO");
        Console.WriteLine("======================================");
        MostrarDatosPropietario(codigosPropietarios.Count - 1);
        Console.WriteLine("======================================");
        codigoPropietario++;
    }


    // BUSCAR PROPIETARIO POR CODIGO
    static void BuscarPropietario()
    {
        if (codigosPropietarios.Count == 0)
        {
            Console.WriteLine("No hay propietarios registrados.");
            return;
        }

        Console.Write("\nIngrese el codigo del propietario: ");
        int codigoBuscado;

        if (!int.TryParse(Console.ReadLine(), out codigoBuscado) || codigoBuscado <= 0)
        {
            Console.WriteLine("Error: ingrese un codigo valido.");
            return;
        }

        int posicion = BuscarPosicionPropietario(codigoBuscado);

        if (posicion == -1)
        {
            Console.WriteLine("No se encontro un propietario con ese codigo.");
            return;
        }

        // MOSTRAR DATOS ENCONTRADOS
        Console.WriteLine("\n===== PROPIETARIO ENCONTRADO =====");
    MostrarDatosPropietario(posicion);
        Console.WriteLine("=================================");
    }

    // VALIDAR CANTIDAD DE DIGITOS
    static bool ValidarDigitos(string dato, int cantidad)
    {
        if (dato.Length != cantidad)
        {
            return false;
        }

        foreach (char caracter in dato)
        {
            if (caracter < '0' || caracter > '9')
            {
                return false;
            }
        }

        return true;
    }


 // VALIDAR CORREO ELECTRONICO
    static bool ValidarCorreo(string correo)
    {
        int arroba = correo.IndexOf('@');
        int punto = correo.LastIndexOf('.');

        if (arroba <= 0 ||
            punto <= arroba + 1 ||
            punto == correo.Length - 1 ||
            correo.Contains(' ') ||
            correo.IndexOf('@', arroba + 1) != -1)
        {
            return false;
        }

        return true;
    }

    // MODIFICAR PROPIETARIO
    static void ModificarPropietario()
    {
        Console.WriteLine("\n--- MODIFICAR PROPIETARIO ---");

        if (codigosPropietarios.Count == 0)
        {
            Console.WriteLine("No hay propietarios registrados.");
            return;
        }

        Console.Write("Ingrese el codigo del propietario: ");
        int codigoBuscado;

        if (!int.TryParse(Console.ReadLine(), out codigoBuscado) || codigoBuscado <= 0)
        {
            Console.WriteLine("Error: ingrese un codigo valido.");
            return;
        }

        int posicion = BuscarPosicionPropietario(codigoBuscado);

        if (posicion == -1)
        {
            Console.WriteLine("No se encontro un propietario con ese codigo.");
            return;
        }

        // MOSTRAR DATOS ACTUALES
        MostrarDatosPropietario(posicion);

        // SELECCIONAR CAMPO
        Console.WriteLine("\n--- DATOS A MODIFICAR ---");
        Console.WriteLine("1. Nombre completo");
        Console.WriteLine("2. DNI");
        Console.WriteLine("3. Telefono");
        Console.WriteLine("4. Direccion");
        Console.WriteLine("5. Correo electronico");
        Console.WriteLine("0. Cancelar");
        Console.Write("Seleccione una opcion: ");

        int campo = LeerOpcion();

        if (campo == 0)
        {
            Console.WriteLine("Modificacion cancelada.");
            return;
        }

        if (campo < 1 || campo > 5)
        {
            Console.WriteLine("Opcion no valida.");
            return;
        }

        Console.Write("Ingrese el nuevo dato: ");
        string nuevoDato = (Console.ReadLine() ?? "").Trim();

        if (string.IsNullOrWhiteSpace(nuevoDato))
        {
            Console.WriteLine("Error: el campo no puede estar vacio.");
            return;
        }

        // VALIDAR Y MODIFICAR EL CAMPO SELECCIONADO
        switch (campo)
        {
            case 1:
                propietarios[posicion] = nuevoDato;
                break;

            case 2:
                // VALIDAR DNI
                bool dniValido = ValidarDigitos(nuevoDato, 8);

                if (!dniValido)
                {
                    Console.WriteLine("Error: el DNI debe tener 8 digitos.");
                    return;
                }

                for (int i = 0; i < dnisPropietarios.Count; i++)
                {
                    if (i != posicion && dnisPropietarios[i] == nuevoDato)
                    {
                        Console.WriteLine("Error: el DNI ya pertenece a otro propietario.");
                        return;
                    }
                }

                dnisPropietarios[posicion] = nuevoDato;
                break;

            case 3:
                // VALIDAR TELEFONO
                bool telefonoValido = ValidarDigitos(nuevoDato, 9);

                if (!telefonoValido)
                {
                    Console.WriteLine("Error: el telefono debe tener 9 digitos.");
                    return;
                }

                telefonos[posicion] = nuevoDato;
                break;

            case 4:
                direcciones[posicion] = nuevoDato;
                break;

            case 5:
            if (!ValidarCorreo(nuevoDato))
            {
                Console.WriteLine("Error: ingrese un correo valido.");
                return;
            }

            correos[posicion] = nuevoDato;
            break;
        }

        Console.WriteLine("\nPropietario modificado correctamente.");
        Console.WriteLine("Codigo: " + codigosPropietarios[posicion]);
    }


    // ELIMINAR PROPIETARIO
    static void EliminarPropietario()
    {
        Console.WriteLine("\n--- ELIMINAR PROPIETARIO ---");

        if (codigosPropietarios.Count == 0)
        {
            Console.WriteLine("No hay propietarios registrados.");
            return;
        }

        Console.Write("Ingrese el codigo del propietario: ");
        int codigoBuscado;

        if (!int.TryParse(Console.ReadLine(), out codigoBuscado) || codigoBuscado <= 0)
        {
            Console.WriteLine("Error: ingrese un codigo valido.");
            return;
        }

        int posicion = BuscarPosicionPropietario(codigoBuscado);

        if (posicion == -1)
        {
            Console.WriteLine("No se encontro un propietario con ese codigo.");
            return;
        }

    // VERIFICAR SI EL PROPIETARIO TIENE MASCOTAS
    if (codigosPropietariosMascotas.Contains(codigoBuscado))
    {
        Console.WriteLine("No se puede eliminar el propietario porque tiene mascotas asociadas.");
        return;
    }

        // TODO (MAXIMO - PET SHOP / INTEGRACION):
        // Cuando Ventas.cs tenga la lista codigosPropietariosVentas, activar esta validacion.
        // Una venta LIBRE debe guardar codigo de propietario 0 y no bloquear eliminaciones.
        // Solo impedir eliminar si existe una venta vinculada a ESTE propietario.
        // Coordinar la activacion con el responsable de Propietarios y Mascotas.
        //
        // if (codigosPropietariosVentas.Contains(codigoBuscado))
        // {
        //     Console.WriteLine("No se puede eliminar el propietario porque tiene ventas vinculadas.");
        //     return;
        // }

        // MOSTRAR DATOS DEL PROPIETARIO
        Console.WriteLine("\n===== PROPIETARIO ENCONTRADO =====");
        MostrarDatosPropietario(posicion);

        // CONFIRMAR ELIMINACION
        Console.Write("\nConfirmar eliminacion? (S/N): ");
        string respuesta = (Console.ReadLine() ?? "").Trim().ToUpper();

        if (respuesta != "S" && respuesta != "SI")
        {
            Console.WriteLine("Eliminacion cancelada.");
            return;
        }

        // ELIMINAR DATOS DEL PROPIETARIO
        codigosPropietarios.RemoveAt(posicion);
        propietarios.RemoveAt(posicion);
        dnisPropietarios.RemoveAt(posicion);
        telefonos.RemoveAt(posicion);
        direcciones.RemoveAt(posicion);
        correos.RemoveAt(posicion);

        Console.WriteLine("\nPropietario eliminado correctamente.");
    }


    // VER MASCOTAS ASOCIADAS A UN PROPIETARIO
    static void VerMascotasAsociadas()
    {
        Console.WriteLine("\n--- MASCOTAS ASOCIADAS ---");

        if (codigosPropietarios.Count == 0)
        {
            Console.WriteLine("No hay propietarios registrados.");
            return;
        }

        Console.Write("Ingrese el codigo del propietario: ");
        int codigoBuscado;

        if (!int.TryParse(Console.ReadLine(), out codigoBuscado) || codigoBuscado <= 0)
        {
            Console.WriteLine("Error: ingrese un codigo valido.");
            return;
        }

        int posicion = BuscarPosicionPropietario(codigoBuscado);

        if (posicion == -1)
        {
            Console.WriteLine("Propietario no encontrado.");
            return;
        }

        Console.WriteLine("\nPropietario: " + propietarios[posicion]);
        Console.WriteLine("Codigo: " + codigoBuscado);
        Console.WriteLine("\n--- MASCOTAS REGISTRADAS ---");

        int cantidad = 0;

        // BUSCAR MASCOTAS RELACIONADAS POR CODIGO
        for (int i = 0; i < codigosMascotas.Count; i++)
        {
            if (i < codigosPropietariosMascotas.Count &&
                codigosPropietariosMascotas[i] == codigoBuscado)
            {
                Console.WriteLine("Codigo mascota: " + codigosMascotas[i]);
                Console.WriteLine("Nombre: " + mascotas[i]);
                Console.WriteLine("Tipo: " + tiposAnimales[i]);
                Console.WriteLine("Especie o raza: " + especies[i]);
                Console.WriteLine("-----------------------------");

                cantidad++;
            }
        }

        if (cantidad == 0)
        {
            Console.WriteLine("Este propietario no tiene mascotas asociadas.");
        }
        else
        {
            Console.WriteLine("Total de mascotas: " + cantidad);
        }
    }
}
