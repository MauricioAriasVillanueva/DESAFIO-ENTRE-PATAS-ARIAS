using System.Collections;
using System.Globalization;
using System.Reflection;
using System.Text;

internal static class PruebasIntegracion
{
    static int Main() => Program.EjecutarPruebasIntegracion();
}

partial class Program
{
    // Se compila solo en este proyecto de pruebas. Los datos son ficticios y temporales.
    static int comprobaciones;

    static void Comprobar(bool condicion, string nombre)
    {
        if (!condicion) throw new InvalidOperationException(nombre);
        comprobaciones++;
        Console.WriteLine("OK: " + nombre);
    }

    static string ConEntrada(Action operacion, string entrada = "")
    {
        TextReader lector = Console.In;
        TextWriter salida = Console.Out;
        using var captura = new StringWriter();
        try
        {
            Console.SetIn(new StringReader(entrada));
            Console.SetOut(captura);
            operacion();
            return captura.ToString();
        }
        finally { Console.SetIn(lector); Console.SetOut(salida); }
    }

    static FieldInfo[] ListasSistema() => typeof(Program)
        .GetFields(BindingFlags.Static | BindingFlags.NonPublic)
        .Where(f => typeof(IList).IsAssignableFrom(f.FieldType)).ToArray();

    static Dictionary<string, object[]> FotoDatos() => ListasSistema()
        .ToDictionary(f => f.Name, f => ((IList)f.GetValue(null)!).Cast<object>().ToArray());

    static bool MismosDatos(Dictionary<string, object[]> foto) => ListasSistema()
        .All(f => foto[f.Name].SequenceEqual(((IList)f.GetValue(null)!).Cast<object>()));

    static void VaciarDatos()
    {
        foreach (FieldInfo campo in ListasSistema()) ((IList)campo.GetValue(null)!).Clear();
        RecalcularSiguientesCodigos();
    }

    static void CrearBinarioAntiguo()
    {
        using var archivo = File.Create(RutaRespaldoBinario);
        using var escritor = new BinaryWriter(archivo, Encoding.UTF8);
        escritor.Write("VET_EP_ARIAS_BIN");
        escritor.Write(2);
        escritor.Write(1);
        escritor.Write(2001);
        foreach (string dato in new[] { "Ana", "987654321", "12345678", "Lima", "ana@example.com" })
            escritor.Write(dato);
        escritor.Write(0); // Mascotas.
        escritor.Write(0); // Atenciones.
        escritor.Write(0); // Servicios.
    }

    internal static int EjecutarPruebasIntegracion()
    {
        string directorioOriginal = Directory.GetCurrentDirectory();
        string temporal = Path.Combine(Path.GetTempPath(), "vet-pruebas-" + Guid.NewGuid());
        CultureInfo culturaOriginal = CultureInfo.CurrentCulture;
        Directory.CreateDirectory(temporal);
        try
        {
            Directory.SetCurrentDirectory(temporal);
            CultureInfo.CurrentCulture = CultureInfo.GetCultureInfo("es-ES");
            VaciarDatos();

            ConEntrada(RegistrarProducto, "Alimento, \"premium\"\n12.50\n10\nS\n");
            Comprobar(codigosProductos.SequenceEqual(new[] { 4001 }) && preciosProductos[0] == 12.50m &&
                ValidarIntegridadListasProductos(), "Primer producto 4001 y precio con punto en cultura es-ES");
            var foto = FotoDatos();
            ConEntrada(RegistrarProducto, "Invalido\n-1\n");
            ConEntrada(RegistrarProducto, "Cancelado\n8\n3\nN\n");
            Comprobar(MismosDatos(foto), "Precio invalido y registro cancelado conservan los datos");

            ConEntrada(RegistrarMovimientoStock, "1\n4001\n2\n");
            ConEntrada(RegistrarMovimientoStock, "2\n4001\n100\n");
            Comprobar(stocksProductos[0] == 12, "Entrada de stock y rechazo de salida insuficiente");
            stocksProductos[0] = int.MaxValue;
            ConEntrada(RegistrarMovimientoStock, "1\n4001\n1\n");
            Comprobar(stocksProductos[0] == int.MaxValue, "Entrada de stock evita desbordamiento");
            stocksProductos[0] = 12;

            foto = FotoDatos();
            ConEntrada(RegistrarVenta, "4001\n1\nN\nN\n");
            ConEntrada(RegistrarVenta, "4001\n100\n");
            Comprobar(MismosDatos(foto), "Venta cancelada o sin stock no modifica ninguna lista");
            ConEntrada(RegistrarVenta, "4001\n2\nN\nS\n");
            Comprobar(codigosVentas.SequenceEqual(new[] { 5001 }) && stocksProductos[0] == 10 &&
                totalesVentas[0] == 25m && codigosPropietariosVentas[0] == 0 && codigosMascotasVentas[0] == 0,
                "Venta libre registra total y descuenta stock una sola vez");

            foto = FotoDatos();
            ConEntrada(CrearRespaldoBinario);
            Comprobar(File.Exists(RutaRespaldoBinario), "Respaldo permite datos exclusivos del Pet Shop");
            VaciarDatos();
            ConEntrada(RestaurarRespaldoBinario);
            Comprobar(MismosDatos(foto), "Binario v3 recupera Pet Shop sin propietarios ni mascotas");

            ConEntrada(RegistrarPropietario, "Ana Torres\n12345678\n987654321\nLima\nana@example.com\n");
            ConEntrada(RegistrarPropietario, "Luis Garcia\n23456789\n987654322\nLima\nluis@example.com\n");
            ConEntrada(RegistrarPropietario, "Marta Diaz\n34567890\n987654323\nLima\nmarta@example.com\n");
            ConEntrada(RegistrarMascota, "2001\nZeta\nCanino\nMestizo\nMacho\n01/01/2020\nN\n");
            ConEntrada(RegistrarMascota, "2002\nAlfa\nFelino\nMestizo\nHembra\n02/01/2020\nN\n");
            ConEntrada(RegistrarMascota, "2001\nLuna\nCanino\nMestizo\nHembra\n03/01/2020\nN\n");
            Comprobar(codigosPropietarios.Count == 3 && codigosMascotas.Count == 3,
                "Registro real de propietarios y mascotas alimenta las listas compartidas");
            foto = FotoDatos();
            ConEntrada(RegistrarVenta, "4001\n1\nS\n2002\nS\n1001\n");
            Comprobar(MismosDatos(foto), "Se rechaza mascota de otro propietario sin descontar stock");
            ConEntrada(RegistrarVenta, "4001\n1\nS\n2001\nS\n1003\nS\n");
            Comprobar(codigosPropietariosVentas[1] == 2001 && codigosMascotasVentas[1] == 1003 &&
                stocksProductos[0] == 9 && ValidarIntegridadListasVentas(), "Venta asociada por codigos a propietario y mascota");
            foto = FotoDatos();
            Comprobar(ConEntrada(EliminarMascota, "1003\nS\n").Contains("ventas vinculadas") && MismosDatos(foto),
                "Mascota con venta vinculada conserva su registro");
            ConEntrada(RegistrarVenta, "4001\n1\nS\n2003\nN\nS\n");
            foto = FotoDatos();
            Comprobar(ConEntrada(EliminarPropietario, "2003\nS\n").Contains("ventas vinculadas") && MismosDatos(foto),
                "Propietario con venta y sin mascotas no puede eliminarse");
            ConEntrada(EliminarProducto, "4001\nS\n");
            Comprobar(MismosDatos(foto), "Producto con ventas no puede eliminarse");
            ConEntrada(EliminarMascota, "1002\nS\n");
            ConEntrada(EliminarPropietario, "2002\nS\n");
            Comprobar(!codigosMascotas.Contains(1002) && !codigosPropietarios.Contains(2002),
                "Ventas ajenas y ventas libres permiten eliminar registros sin dependencias");

            string fecha = DateTime.Today.AddDays(-1).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
            ConEntrada(RegistrarAtencion, $"1001\nGeneral\nCarlos Ramirez\nControl\n2\n{fecha}\n10:30\n50.50\nSano\nControl anual\nS\n");
            ConEntrada(RegistrarServicio, $"1001\nBano\nMaria\n{fecha}\n11:30\n25.50\nSin observaciones\nS\n");
            Comprobar(codigosAtenciones.Count == 1 && codigosServicios.Count == 1,
                "Atencion realizada y servicio se integran con las mascotas registradas");
            estadosAtenciones[0] = "Anulada";
            estadosServicios[0] = "Anulado";
            ConEntrada(OrdenarMascotasPorNombre);
            Comprobar(codigosMascotas.SequenceEqual(new[] { 1003, 1001 }) &&
                codigosPropietariosMascotas.SequenceEqual(new[] { 2001, 2001 }) && mascotas[0] == "Luna",
                "Ordenamiento por nombre conserva los datos de cada mascota");
            string historial = ConEntrada(HistorialClinico, "1001\n");
            Comprobar(historial.Contains("Anulada") && historial.Contains("Anulado") && historial.Contains("Carlos Ramirez"),
                "Historial encuentra atencion y servicio anulados despues de ordenar");
            Comprobar(!ConEntrada(HistorialClinico, "1003\n").Contains("Carlos Ramirez"),
                "Historial filtra por codigo y no por posicion");
            ConEntrada(OrdenarMascotasPorCodigo);
            Comprobar(codigosMascotas.SequenceEqual(new[] { 1001, 1003 }) && mascotas[0] == "Zeta",
                "Ordenamiento por codigo conserva la correspondencia entre listas");
            foreach (Action listar in new Action[] { ListarPropietarios, ListarMascotas, ListarAtenciones,
                ListarServicios, ListarProductos, ListarVentas })
                Comprobar(ConEntrada(listar).Contains("Total de registros:"), "Listado " + listar.Method.Name);

            foto = FotoDatos();
            ConEntrada(GuardarDatosCSV);
            VaciarDatos();
            ConEntrada(CargarDatosCSV);
            Comprobar(MismosDatos(foto) && codigoPropietario == 2004 && codigoMascota == 1004,
                "CSV recupera las seis entidades, comillas, coma, stock y siguientes codigos");
            ConEntrada(CrearRespaldoBinario);
            byte[] respaldo = File.ReadAllBytes(RutaRespaldoBinario);
            VaciarDatos();
            ConEntrada(RestaurarRespaldoBinario);
            Comprobar(MismosDatos(foto) && ValidarIntegridadListasVentas(),
                "Binario v3 recupera las seis entidades y reconstruye productos vendidos");

            stocksProductos[0]++;
            foto = FotoDatos();
            ConEntrada(RestaurarRespaldoBinario, "N\n");
            Comprobar(MismosDatos(foto), "Cancelar restauracion conserva todos los datos actuales");
            ConEntrada(RestaurarRespaldoBinario, "S\n");
            Comprobar(stocksProductos[0] == 8, "Confirmar restauracion reemplaza tambien el stock del Pet Shop");
            foto = FotoDatos();
            File.WriteAllBytes(RutaRespaldoBinario, respaldo[..^1]);
            ConEntrada(RestaurarRespaldoBinario, "S\n");
            Comprobar(MismosDatos(foto), "Binario truncado se rechaza sin modificar la memoria");
            File.WriteAllBytes(RutaRespaldoBinario, respaldo.Concat(new byte[] { 0 }).ToArray());
            ConEntrada(RestaurarRespaldoBinario, "S\n");
            Comprobar(MismosDatos(foto), "Binario con bytes residuales se rechaza sin modificar la memoria");
            byte[] corrupto = (byte[])respaldo.Clone();
            BitConverter.GetBytes(9999).CopyTo(corrupto, corrupto.Length - sizeof(int));
            File.WriteAllBytes(RutaRespaldoBinario, corrupto);
            ConEntrada(RestaurarRespaldoBinario, "S\n");
            Comprobar(MismosDatos(foto), "Binario con mascota inexistente en venta se rechaza");
            string csvVentas = File.ReadAllText(RutaCsvVentas);
            File.WriteAllText(RutaCsvVentas, csvVentas.Replace(",4001,", ",9999,"));
            ConEntrada(CargarDatosCSV, "S\n");
            Comprobar(MismosDatos(foto), "CSV con producto inexistente en venta conserva la memoria");

            CrearBinarioAntiguo();
            Comprobar(ConEntrada(RestaurarRespaldoBinario, "S\n").Contains("v2 no incluye Pet Shop") &&
                MismosDatos(foto), "Respaldo v2 no borra Pet Shop existente");
            VaciarDatos();
            ConEntrada(RestaurarRespaldoBinario);
            Comprobar(codigosPropietarios.SequenceEqual(new[] { 2001 }) && propietarios[0] == "Ana" &&
                codigosProductos.Count == 0, "Compatibilidad con respaldo clinico antiguo v2");

            Console.WriteLine($"{comprobaciones} comprobaciones correctas.");
            return 0;
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine("FALLO: " + ex.Message);
            return 1;
        }
        finally
        {
            CultureInfo.CurrentCulture = culturaOriginal;
            Directory.SetCurrentDirectory(directorioOriginal);
            Directory.Delete(temporal, true);
        }
    }
}
