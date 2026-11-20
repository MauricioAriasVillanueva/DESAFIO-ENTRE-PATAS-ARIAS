using System.Globalization;
using System.Collections.Generic;
using System;

partial class Program
{
    // DATOS DE PROPIETARIOS
    static List<int> codigosPropietarios = new List<int>();
    static List<string> propietarios = new List<string>();
    static List<string> telefonos = new List<string>();
    static List<string> dnisPropietarios = new List<string>();
    static List<string> direcciones = new List<string>();
    static List<string> correos = new List<string>();
    static List<int> codigosPropietariosMascotas = new List<int>();

    // DATOS DE MASCOTAS
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
            MostrarMenuPrincipal();
            Console.Write("Seleccione una opcion: ");
            opcion = LeerOpcion();

            if (opcion == 1)
            {
                MostrarSubmenu("GESTIONAR PROPIETARIOS", new string[]
                {
                    "Registrar propietario",
                    "Buscar propietario por codigo",
                    "Modificar propietario",
                    "Eliminar propietario",
                    "Ver mascotas asociadas"
                }, 1);
            }
            else if (opcion == 2)
            {
                MostrarSubmenu("GESTIONAR MASCOTAS", new string[]
                {
                    "Registrar mascota",
                    "Buscar mascota por codigo o nombre",
                    "Modificar mascota",
                    "Eliminar mascota"
                }, 2);
            }
            else if (opcion == 3)
            {
                MostrarSubmenu("ATENCIONES MEDICAS", new string[]
                {
                    "Registrar atencion",
                    "Modificar atencion",
                    "Anular atencion"
                });
            }
            else if (opcion == 4)
            {
                MostrarSubmenu("SERVICIOS VETERINARIOS", new string[]
                {
                    "Registrar servicio",
                    "Modificar servicio",
                    "Anular servicio"
                });
            }
            else if (opcion == 5)
            {
                MostrarSubmenu("PET SHOP", new string[]
                {
                    "Gestion de productos",
                    "Gestion de ventas"
                }, 5);
            }
            else if (opcion == 6)
            {
                MostrarSubmenu("HISTORIAL CLINICO DE MASCOTAS", new string[]
                {
                    "Consultar historial completo por mascota"
                });
            }
            else if (opcion == 7)
            {
                MostrarSubmenu("LISTADOS Y ORDENAMIENTOS", new string[]
                {
                    "Listar propietarios",
                    "Listar mascotas",
                    "Listar atenciones medicas",
                    "Listar servicios veterinarios",
                    "Listar productos del Pet Shop",
                    "Listar ventas del Pet Shop",
                    "Ordenar mascotas por codigo",
                    "Ordenar mascotas por nombre"
                });
            }
            else if (opcion == 8)
            {
                MostrarSubmenu("ADMINISTRACION DE REGISTROS", new string[]
                {
                    "Guardar datos en CSV",
                    "Cargar datos desde CSV",
                    "Crear respaldo binario",
                    "Restaurar respaldo binario",
                    "Consultar estado de carga"
                });
            }
            else if (opcion == 9)
            {
                Console.WriteLine("Saliendo del programa...");
            }
            else
            {
                Console.WriteLine("Opcion no valida.");
            }

        } while (opcion != 9);

        Console.WriteLine("Programa finalizado.");
    }

    // MOSTRAR MENU PRINCIPAL
    static void MostrarMenuPrincipal()
    {
        Console.WriteLine();
        Console.WriteLine("========================================");
        Console.WriteLine("      VETERINARIA ENTRE PATAS - ARIAS");
        Console.WriteLine("========================================");
        Console.WriteLine("1. Gestionar propietarios");
        Console.WriteLine("2. Gestionar mascotas");
        Console.WriteLine("3. Atenciones medicas");
        Console.WriteLine("4. Servicios veterinarios");
        Console.WriteLine("5. Pet Shop");
        Console.WriteLine("6. Historial clinico de mascotas");
        Console.WriteLine("7. Listados y ordenamientos");
        Console.WriteLine("8. Administracion de registros");
        Console.WriteLine("9. Salir");
    }

    // LEER OPCION DEL MENU
    static int LeerOpcion()
    {
        int opcion;

        if (int.TryParse(Console.ReadLine(), out opcion))
        {
            return opcion;
        }

        return -1;
    }

    // NAVEGAR POR LOS SUBMENUS
    static void MostrarSubmenu(string titulo, string[] opciones, int modulo = 0,string textoVolver = "Volver al menu principal")
    {
        int opcionSubmenu;

        do
        {
            Console.WriteLine();
            Console.WriteLine("========================================");
            Console.WriteLine("          " + titulo);
            Console.WriteLine("========================================");

            for (int i = 0; i < opciones.Length; i++)
            {
                Console.WriteLine((i + 1) + ". " + opciones[i]);
            }

            Console.WriteLine("0. " + textoVolver);
            Console.Write("Seleccione una opcion: ");
            opcionSubmenu = LeerOpcion();

            if (opcionSubmenu >= 1 && opcionSubmenu <= opciones.Length)
            {
                if (modulo == 1 && opcionSubmenu == 1)
                {
                    RegistrarPropietario();
                }
                else if (modulo == 1 && opcionSubmenu == 2)
                {
                    BuscarPropietario();
                }
                else if (modulo == 1 && opcionSubmenu == 3)
                {
                    ModificarPropietario();
                }
                else if (modulo == 1 && opcionSubmenu == 4)
                {
                    EliminarPropietario();
                }
                else if (modulo == 1 && opcionSubmenu == 5)
                {
                    VerMascotasAsociadas();
                }
                else if (modulo == 2 && opcionSubmenu == 1)
                {
                    RegistrarMascota();
                }
                else if (modulo == 2 && opcionSubmenu == 2)
                {
                    BuscarMascota();
                }
                else if (modulo == 2 && opcionSubmenu == 3)
                {
                    ModificarMascota();
                }
                else if (modulo == 2 && opcionSubmenu == 4)
                {
                    EliminarMascota();
                }
                else if (modulo == 5 && opcionSubmenu == 1)
                {
                    MostrarSubmenu("GESTION DE PRODUCTOS", new string[]
                    {
                        "Registrar producto",
                        "Buscar producto por codigo o nombre",
                        "Modificar producto",
                        "Eliminar producto",
                        "Registrar movimiento de stock"
                    }, 0, "Volver a Pet Shop");
                }
                else if (modulo == 5 && opcionSubmenu == 2)
                {
                    MostrarSubmenu("GESTION DE VENTAS", new string[]
                    {
                        "Registrar venta",
                        "Consultar venta por codigo"
                    }, 0, "Volver a Pet Shop");
                }
                else
                {
                    Console.WriteLine("Funcionalidad no implementada para esta opcion.");
                }
            }
            else if (opcionSubmenu != 0)
            {
                Console.WriteLine("Opcion no valida. Intente nuevamente.");
            }

        } while (opcionSubmenu != 0);
    }
}
