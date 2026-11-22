using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

partial class Program
{
    static readonly string CarpetaDatos = "Datos";
    static readonly string RutaCsvPropietarios = Path.Combine(CarpetaDatos, "propietarios.csv");
    static readonly string RutaCsvMascotas = Path.Combine(CarpetaDatos, "mascotas.csv");
    static readonly string RutaCsvAtenciones = Path.Combine(CarpetaDatos, "atenciones.csv");
    static readonly string RutaCsvServicios = Path.Combine(CarpetaDatos, "servicios.csv");
    static readonly string RutaRespaldoBinario = Path.Combine(CarpetaDatos, "respaldo_vet.bin");

    static string ultimaOperacionPersistencia = "Ninguna operacion realizada en esta sesion.";

    // =========================================================
    // 1. GUARDAR DATOS EN CSV
    // =========================================================
    static void GuardarDatosCSV()
    {
        Console.WriteLine();
        Console.WriteLine("========================================");
        Console.WriteLine("          GUARDAR DATOS EN CSV");
        Console.WriteLine("========================================");

        if (!ValidarIntegridadListasPropietarios() ||
            !ValidarIntegridadListasMascotas() ||
            !ValidarIntegridadListasAtenciones() ||
            !ValidarIntegridadListasServicios())
        {
            Console.WriteLine("Error: alguna entidad tiene listas paralelas desincronizadas. No se puede guardar.");
            return;
        }

        if (codigosPropietarios.Count == 0 &&
            codigosMascotas.Count == 0 &&
            codigosAtenciones.Count == 0 &&
            codigosServicios.Count == 0)
        {
            Console.WriteLine("No hay registros en memoria para guardar.");
            return;
        }

        try
        {
            Directory.CreateDirectory(CarpetaDatos);

            // 1. Propietarios (6 columnas)
            List<string> lineasProp = new List<string>();
            lineasProp.Add("Codigo,Nombre,Telefono,DNI,Direccion,Correo");
            for (int i = 0; i < codigosPropietarios.Count; i++)
            {
                lineasProp.Add(string.Join(",", new string[]
                {
                    codigosPropietarios[i].ToString(CultureInfo.InvariantCulture),
                    EscaparCSV(propietarios[i]),
                    EscaparCSV(telefonos[i]),
                    EscaparCSV(dnisPropietarios[i]),
                    EscaparCSV(direcciones[i]),
                    EscaparCSV(correos[i])
                }));
            }
            File.WriteAllLines(RutaCsvPropietarios, lineasProp, Encoding.UTF8);

            // 2. Mascotas (7 columnas)
            List<string> lineasMasc = new List<string>();
            lineasMasc.Add("Codigo,Nombre,Especie,TipoAnimal,Sexo,FechaNacimiento,CodigoPropietario");
            for (int i = 0; i < codigosMascotas.Count; i++)
            {
                lineasMasc.Add(string.Join(",", new string[]
                {
                    codigosMascotas[i].ToString(CultureInfo.InvariantCulture),
                    EscaparCSV(mascotas[i]),
                    EscaparCSV(especies[i]),
                    EscaparCSV(tiposAnimales[i]),
                    EscaparCSV(sexos[i]),
                    fechasNacimiento[i].ToString("yyyy-MM-dd", CultureInfo.InvariantCulture),
                    codigosPropietariosMascotas[i].ToString(CultureInfo.InvariantCulture)
                }));
            }
            File.WriteAllLines(RutaCsvMascotas, lineasMasc, Encoding.UTF8);

            // 3. Atenciones (12 columnas)
            List<string> lineasAten = new List<string>();
            lineasAten.Add("Codigo,CodigoMascota,Especialidad,Veterinario,Fecha,Hora,Motivo,Costo,Diagnostico,Tratamiento,Estado,Situacion");
            for (int i = 0; i < codigosAtenciones.Count; i++)
            {
                lineasAten.Add(string.Join(",", new string[]
                {
                    codigosAtenciones[i].ToString(CultureInfo.InvariantCulture),
                    codigosMascotasAtenciones[i].ToString(CultureInfo.InvariantCulture),
                    EscaparCSV(especialidadesAtenciones[i]),
                    EscaparCSV(veterinariosAtenciones[i]),
                    fechasAtenciones[i].ToString("yyyy-MM-dd", CultureInfo.InvariantCulture),
                    EscaparCSV(horasAtenciones[i]),
                    EscaparCSV(motivosAtenciones[i]),
                    costosAtenciones[i].ToString(CultureInfo.InvariantCulture),
                    EscaparCSV(diagnosticosAtenciones[i]),
                    EscaparCSV(tratamientosAtenciones[i]),
                    EscaparCSV(estadosAtenciones[i]),
                    EscaparCSV(situacionesAtenciones[i])
                }));
            }
            File.WriteAllLines(RutaCsvAtenciones, lineasAten, Encoding.UTF8);

            // 4. Servicios (9 columnas)
            List<string> lineasServ = new List<string>();
            lineasServ.Add("Codigo,CodigoMascota,Nombre,Responsable,Fecha,Hora,Costo,Observaciones,Estado");
            for (int i = 0; i < codigosServicios.Count; i++)
            {
                lineasServ.Add(string.Join(",", new string[]
                {
                    codigosServicios[i].ToString(CultureInfo.InvariantCulture),
                    codigosMascotasServicios[i].ToString(CultureInfo.InvariantCulture),
                    EscaparCSV(nombresServicios[i]),
                    EscaparCSV(responsablesServicios[i]),
                    fechasServicios[i].ToString("yyyy-MM-dd", CultureInfo.InvariantCulture),
                    EscaparCSV(horasServicios[i]),
                    costosServicios[i].ToString(CultureInfo.InvariantCulture),
                    EscaparCSV(observacionesServicios[i]),
                    EscaparCSV(estadosServicios[i])
                }));
            }
            File.WriteAllLines(RutaCsvServicios, lineasServ, Encoding.UTF8);

            ultimaOperacionPersistencia = $"CSV guardado ({DateTime.Now:dd/MM/yyyy HH:mm:ss}): {codigosPropietarios.Count} prop., {codigosMascotas.Count} masc., {codigosAtenciones.Count} aten., {codigosServicios.Count} serv.";
            Console.WriteLine("Archivos CSV guardados correctamente en la carpeta 'Datos':");
            Console.WriteLine($" - propietarios.csv : {codigosPropietarios.Count} registros");
            Console.WriteLine($" - mascotas.csv     : {codigosMascotas.Count} registros");
            Console.WriteLine($" - atenciones.csv   : {codigosAtenciones.Count} registros");
            Console.WriteLine($" - servicios.csv    : {codigosServicios.Count} registros");
            Console.WriteLine("Nota: Productos y Ventas aun no se guardan (estructuras pendientes de Maximo).");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al guardar los archivos CSV: {ex.Message}");
        }
    }

    // =========================================================
    // 2. CARGAR DATOS DESDE CSV (Validacion atomica + reemplazo)
    // =========================================================
    static void CargarDatosCSV()
    {
        Console.WriteLine();
        Console.WriteLine("========================================");
        Console.WriteLine("          CARGAR DATOS DESDE CSV");
        Console.WriteLine("========================================");

        string[] archivosRequeridos = { RutaCsvPropietarios, RutaCsvMascotas, RutaCsvAtenciones, RutaCsvServicios };
        for (int i = 0; i < archivosRequeridos.Length; i++)
        {
            if (!File.Exists(archivosRequeridos[i]))
            {
                Console.WriteLine($"Error: No existe el archivo '{archivosRequeridos[i]}'. Carga cancelada.");
                return;
            }
        }

        try
        {
            string[] lineasProp = File.ReadAllLines(RutaCsvPropietarios, Encoding.UTF8);
            string[] lineasMasc = File.ReadAllLines(RutaCsvMascotas, Encoding.UTF8);
            string[] lineasAten = File.ReadAllLines(RutaCsvAtenciones, Encoding.UTF8);
            string[] lineasServ = File.ReadAllLines(RutaCsvServicios, Encoding.UTF8);

            if (lineasProp.Length == 0 || lineasMasc.Length == 0 || lineasAten.Length == 0 || lineasServ.Length == 0)
            {
                Console.WriteLine("Error: Uno o mas archivos CSV estan completamente vacios.");
                return;
            }

            // --- VALIDACION TEMPORAL: PROPIETARIOS ---
            List<int> tempCodProp = new List<int>();
            List<string> tempNomProp = new List<string>();
            List<string> tempTelProp = new List<string>();
            List<string> tempDniProp = new List<string>();
            List<string> tempDirProp = new List<string>();
            List<string> tempCorProp = new List<string>();
            HashSet<int> setCodProp = new HashSet<int>();

            List<string> cabProp = ParsearLineaCSV(lineasProp[0]);
            if (cabProp == null || cabProp.Count != 6)
            {
                Console.WriteLine("Error: Cabecera invalida en propietarios.csv.");
                return;
            }

            for (int i = 1; i < lineasProp.Length; i++)
            {
                if (string.IsNullOrWhiteSpace(lineasProp[i])) continue;

                List<string> c = ParsearLineaCSV(lineasProp[i]);
                if (c == null || c.Count != 6)
                {
                    Console.WriteLine($"Error en propietarios.csv (linea {i + 1}): formato o columnas invalidas.");
                    return;
                }
                if (!int.TryParse(c[0].Trim(), NumberStyles.Integer, CultureInfo.InvariantCulture, out int cod) || cod < 2001)
                {
                    Console.WriteLine($"Error en propietarios.csv (linea {i + 1}): codigo invalido '{c[0]}'.");
                    return;
                }
                if (!setCodProp.Add(cod))
                {
                    Console.WriteLine($"Error en propietarios.csv (linea {i + 1}): codigo duplicado ({cod}).");
                    return;
                }
                if (string.IsNullOrWhiteSpace(c[1]))
                {
                    Console.WriteLine($"Error en propietarios.csv (linea {i + 1}): nombre vacio.");
                    return;
                }

                tempCodProp.Add(cod);
                tempNomProp.Add(c[1]);
                tempTelProp.Add(c[2]);
                tempDniProp.Add(c[3]);
                tempDirProp.Add(c[4]);
                tempCorProp.Add(c[5]);
            }

            // --- VALIDACION TEMPORAL: MASCOTAS ---
            List<int> tempCodMasc = new List<int>();
            List<string> tempNomMasc = new List<string>();
            List<string> tempEspMasc = new List<string>();
            List<string> tempTipMasc = new List<string>();
            List<string> tempSexMasc = new List<string>();
            List<DateTime> tempFecMasc = new List<DateTime>();
            List<int> tempCodPropMasc = new List<int>();
            HashSet<int> setCodMasc = new HashSet<int>();

            List<string> cabMasc = ParsearLineaCSV(lineasMasc[0]);
            if (cabMasc == null || cabMasc.Count != 7)
            {
                Console.WriteLine("Error: Cabecera invalida en mascotas.csv.");
                return;
            }

            for (int i = 1; i < lineasMasc.Length; i++)
            {
                if (string.IsNullOrWhiteSpace(lineasMasc[i])) continue;

                List<string> c = ParsearLineaCSV(lineasMasc[i]);
                if (c == null || c.Count != 7)
                {
                    Console.WriteLine($"Error en mascotas.csv (linea {i + 1}): formato o columnas invalidas.");
                    return;
                }
                if (!int.TryParse(c[0].Trim(), NumberStyles.Integer, CultureInfo.InvariantCulture, out int codM) || codM < 1001)
                {
                    Console.WriteLine($"Error en mascotas.csv (linea {i + 1}): codigo invalido '{c[0]}'.");
                    return;
                }
                if (!setCodMasc.Add(codM))
                {
                    Console.WriteLine($"Error en mascotas.csv (linea {i + 1}): codigo duplicado ({codM}).");
                    return;
                }
                if (string.IsNullOrWhiteSpace(c[1]))
                {
                    Console.WriteLine($"Error en mascotas.csv (linea {i + 1}): nombre de mascota vacio.");
                    return;
                }
                if (!DateTime.TryParseExact(c[5].Trim(), "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime fechaNac))
                {
                    Console.WriteLine($"Error en mascotas.csv (linea {i + 1}): fecha invalida '{c[5]}'.");
                    return;
                }
                if (!int.TryParse(c[6].Trim(), NumberStyles.Integer, CultureInfo.InvariantCulture, out int codDueno) || !setCodProp.Contains(codDueno))
                {
                    Console.WriteLine($"Error en mascotas.csv (linea {i + 1}): propietario '{c[6]}' no existe.");
                    return;
                }

                tempCodMasc.Add(codM);
                tempNomMasc.Add(c[1]);
                tempEspMasc.Add(c[2]);
                tempTipMasc.Add(c[3]);
                tempSexMasc.Add(c[4]);
                tempFecMasc.Add(fechaNac);
                tempCodPropMasc.Add(codDueno);
            }

            // --- VALIDACION TEMPORAL: ATENCIONES ---
            List<int> tempCodAten = new List<int>();
            List<int> tempCodMascAten = new List<int>();
            List<string> tempEspAten = new List<string>();
            List<string> tempVetAten = new List<string>();
            List<DateTime> tempFecAten = new List<DateTime>();
            List<string> tempHorAten = new List<string>();
            List<string> tempMotAten = new List<string>();
            List<decimal> tempCosAten = new List<decimal>();
            List<string> tempDiaAten = new List<string>();
            List<string> tempTraAten = new List<string>();
            List<string> tempEstAten = new List<string>();
            List<string> tempSitAten = new List<string>();
            HashSet<int> setCodAten = new HashSet<int>();

            List<string> cabAten = ParsearLineaCSV(lineasAten[0]);
            if (cabAten == null || cabAten.Count != 12)
            {
                Console.WriteLine("Error: Cabecera invalida en atenciones.csv.");
                return;
            }

            for (int i = 1; i < lineasAten.Length; i++)
            {
                if (string.IsNullOrWhiteSpace(lineasAten[i])) continue;

                List<string> c = ParsearLineaCSV(lineasAten[i]);
                if (c == null || c.Count != 12)
                {
                    Console.WriteLine($"Error en atenciones.csv (linea {i + 1}): formato o columnas invalidas.");
                    return;
                }
                if (!int.TryParse(c[0].Trim(), NumberStyles.Integer, CultureInfo.InvariantCulture, out int codA) || codA <= 0)
                {
                    Console.WriteLine($"Error en atenciones.csv (linea {i + 1}): codigo de atencion invalido '{c[0]}'.");
                    return;
                }
                if (!setCodAten.Add(codA))
                {
                    Console.WriteLine($"Error en atenciones.csv (linea {i + 1}): codigo duplicado ({codA}).");
                    return;
                }
                if (!int.TryParse(c[1].Trim(), NumberStyles.Integer, CultureInfo.InvariantCulture, out int codMascRef) || !setCodMasc.Contains(codMascRef))
                {
                    Console.WriteLine($"Error en atenciones.csv (linea {i + 1}): mascota '{c[1]}' no existe.");
                    return;
                }
                if (!DateTime.TryParseExact(c[4].Trim(), "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime fechaAten))
                {
                    Console.WriteLine($"Error en atenciones.csv (linea {i + 1}): fecha invalida '{c[4]}'.");
                    return;
                }
                if (!decimal.TryParse(c[7].Trim(), NumberStyles.Number, CultureInfo.InvariantCulture, out decimal costoAten) || costoAten < 0)
                {
                    Console.WriteLine($"Error en atenciones.csv (linea {i + 1}): costo invalido '{c[7]}'.");
                    return;
                }
                if (string.IsNullOrWhiteSpace(c[10]) || string.IsNullOrWhiteSpace(c[11]))
                {
                    Console.WriteLine($"Error en atenciones.csv (linea {i + 1}): estado o situacion vacios.");
                    return;
                }

                tempCodAten.Add(codA);
                tempCodMascAten.Add(codMascRef);
                tempEspAten.Add(c[2]);
                tempVetAten.Add(c[3]);
                tempFecAten.Add(fechaAten);
                tempHorAten.Add(c[5]);
                tempMotAten.Add(c[6]);
                tempCosAten.Add(costoAten);
                tempDiaAten.Add(c[8]);
                tempTraAten.Add(c[9]);
                tempEstAten.Add(c[10]);
                tempSitAten.Add(c[11]);
            }

            // --- VALIDACION TEMPORAL: SERVICIOS ---
            List<int> tempCodServ = new List<int>();
            List<int> tempCodMascServ = new List<int>();
            List<string> tempNomServ = new List<string>();
            List<string> tempResServ = new List<string>();
            List<DateTime> tempFecServ = new List<DateTime>();
            List<string> tempHorServ = new List<string>();
            List<decimal> tempCosServ = new List<decimal>();
            List<string> tempObsServ = new List<string>();
            List<string> tempEstServ = new List<string>();
            HashSet<int> setCodServ = new HashSet<int>();

            List<string> cabServ = ParsearLineaCSV(lineasServ[0]);
            if (cabServ == null || cabServ.Count != 9)
            {
                Console.WriteLine("Error: Cabecera invalida en servicios.csv.");
                return;
            }

            for (int i = 1; i < lineasServ.Length; i++)
            {
                if (string.IsNullOrWhiteSpace(lineasServ[i])) continue;

                List<string> c = ParsearLineaCSV(lineasServ[i]);
                if (c == null || c.Count != 9)
                {
                    Console.WriteLine($"Error en servicios.csv (linea {i + 1}): formato o columnas invalidas.");
                    return;
                }
                if (!int.TryParse(c[0].Trim(), NumberStyles.Integer, CultureInfo.InvariantCulture, out int codS) || codS <= 0)
                {
                    Console.WriteLine($"Error en servicios.csv (linea {i + 1}): codigo de servicio invalido '{c[0]}'.");
                    return;
                }
                if (!setCodServ.Add(codS))
                {
                    Console.WriteLine($"Error en servicios.csv (linea {i + 1}): codigo duplicado ({codS}).");
                    return;
                }
                if (!int.TryParse(c[1].Trim(), NumberStyles.Integer, CultureInfo.InvariantCulture, out int codMascRef) || !setCodMasc.Contains(codMascRef))
                {
                    Console.WriteLine($"Error en servicios.csv (linea {i + 1}): mascota '{c[1]}' no existe.");
                    return;
                }
                if (!DateTime.TryParseExact(c[4].Trim(), "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime fechaServ))
                {
                    Console.WriteLine($"Error en servicios.csv (linea {i + 1}): fecha invalida '{c[4]}'.");
                    return;
                }
                if (!decimal.TryParse(c[6].Trim(), NumberStyles.Number, CultureInfo.InvariantCulture, out decimal costoServ) || costoServ < 0)
                {
                    Console.WriteLine($"Error en servicios.csv (linea {i + 1}): costo invalido '{c[6]}'.");
                    return;
                }
                if (string.IsNullOrWhiteSpace(c[8]))
                {
                    Console.WriteLine($"Error en servicios.csv (linea {i + 1}): estado vacio.");
                    return;
                }

                tempCodServ.Add(codS);
                tempCodMascServ.Add(codMascRef);
                tempNomServ.Add(c[2]);
                tempResServ.Add(c[3]);
                tempFecServ.Add(fechaServ);
                tempHorServ.Add(c[5]);
                tempCosServ.Add(costoServ);
                tempObsServ.Add(c[7]);
                tempEstServ.Add(c[8]);
            }

            if (tempCodProp.Count == 0 && tempCodMasc.Count == 0 && tempCodAten.Count == 0 && tempCodServ.Count == 0)
            {
                Console.WriteLine("Error: Los archivos CSV no contienen registros.");
                return;
            }

            // Confirmacion antes de reemplazar memoria
            if (codigosPropietarios.Count > 0 || codigosMascotas.Count > 0 || codigosAtenciones.Count > 0 || codigosServicios.Count > 0)
            {
                Console.Write($"Se reemplazaran los datos actuales por {tempCodProp.Count} prop., {tempCodMasc.Count} masc., {tempCodAten.Count} aten. y {tempCodServ.Count} serv. ¿Confirmar? (S/N): ");
                string resp = (Console.ReadLine() ?? "").Trim().ToUpperInvariant();
                if (resp != "S")
                {
                    Console.WriteLine("Carga cancelada por el usuario. Los datos actuales no fueron modificados.");
                    return;
                }
            }

            // Reemplazo atomico de las 4 entidades
            ReemplazarListasEnMemoria(
                tempCodProp, tempNomProp, tempTelProp, tempDniProp, tempDirProp, tempCorProp,
                tempCodMasc, tempNomMasc, tempEspMasc, tempTipMasc, tempSexMasc, tempFecMasc, tempCodPropMasc,
                tempCodAten, tempCodMascAten, tempEspAten, tempVetAten, tempFecAten, tempHorAten, tempMotAten, tempCosAten, tempDiaAten, tempTraAten, tempEstAten, tempSitAten,
                tempCodServ, tempCodMascServ, tempNomServ, tempResServ, tempFecServ, tempHorServ, tempCosServ, tempObsServ, tempEstServ
            );

            ultimaOperacionPersistencia = $"CSV cargado ({DateTime.Now:dd/MM/yyyy HH:mm:ss}): {codigosPropietarios.Count} prop., {codigosMascotas.Count} masc., {codigosAtenciones.Count} aten., {codigosServicios.Count} serv.";
            Console.WriteLine("Carga desde CSV completada exitosamente:");
            Console.WriteLine($" - Propietarios : {codigosPropietarios.Count} (proximo codigo: {codigoPropietario})");
            Console.WriteLine($" - Mascotas     : {codigosMascotas.Count} (proximo codigo: {codigoMascota})");
            Console.WriteLine($" - Atenciones   : {codigosAtenciones.Count}");
            Console.WriteLine($" - Servicios    : {codigosServicios.Count}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error inesperado al cargar CSV (los datos en memoria se conservaron): {ex.Message}");
        }
    }

    // =========================================================
    // 3. CREAR RESPALDO BINARIO (BinaryWriter)
    // =========================================================
    static void CrearRespaldoBinario()
    {
        Console.WriteLine();
        Console.WriteLine("========================================");
        Console.WriteLine("        CREAR RESPALDO BINARIO");
        Console.WriteLine("========================================");

        if (!ValidarIntegridadListasPropietarios() ||
            !ValidarIntegridadListasMascotas() ||
            !ValidarIntegridadListasAtenciones() ||
            !ValidarIntegridadListasServicios())
        {
            Console.WriteLine("Error: alguna entidad tiene listas paralelas desincronizadas.");
            return;
        }

        if (codigosPropietarios.Count == 0 &&
            codigosMascotas.Count == 0 &&
            codigosAtenciones.Count == 0 &&
            codigosServicios.Count == 0)
        {
            Console.WriteLine("No hay registros en memoria para respaldar.");
            return;
        }

        try
        {
            Directory.CreateDirectory(CarpetaDatos);

            using (FileStream fs = new FileStream(RutaRespaldoBinario, FileMode.Create, FileAccess.Write))
            using (BinaryWriter writer = new BinaryWriter(fs, Encoding.UTF8))
            {
                // Identificador y version (v2 incluye Propietarios, Mascotas, Atenciones y Servicios)
                writer.Write("VET_EP_ARIAS_BIN");
                writer.Write(2);

                // Bloque 1: Propietarios
                writer.Write(codigosPropietarios.Count);
                for (int i = 0; i < codigosPropietarios.Count; i++)
                {
                    writer.Write(codigosPropietarios[i]);
                    writer.Write(propietarios[i] ?? "");
                    writer.Write(telefonos[i] ?? "");
                    writer.Write(dnisPropietarios[i] ?? "");
                    writer.Write(direcciones[i] ?? "");
                    writer.Write(correos[i] ?? "");
                }

                // Bloque 2: Mascotas
                writer.Write(codigosMascotas.Count);
                for (int i = 0; i < codigosMascotas.Count; i++)
                {
                    writer.Write(codigosMascotas[i]);
                    writer.Write(mascotas[i] ?? "");
                    writer.Write(especies[i] ?? "");
                    writer.Write(tiposAnimales[i] ?? "");
                    writer.Write(sexos[i] ?? "");
                    writer.Write(fechasNacimiento[i].ToString("yyyy-MM-dd", CultureInfo.InvariantCulture));
                    writer.Write(codigosPropietariosMascotas[i]);
                }

                // Bloque 3: Atenciones
                writer.Write(codigosAtenciones.Count);
                for (int i = 0; i < codigosAtenciones.Count; i++)
                {
                    writer.Write(codigosAtenciones[i]);
                    writer.Write(codigosMascotasAtenciones[i]);
                    writer.Write(especialidadesAtenciones[i] ?? "");
                    writer.Write(veterinariosAtenciones[i] ?? "");
                    writer.Write(fechasAtenciones[i].ToString("yyyy-MM-dd", CultureInfo.InvariantCulture));
                    writer.Write(horasAtenciones[i] ?? "");
                    writer.Write(motivosAtenciones[i] ?? "");
                    writer.Write(costosAtenciones[i]);
                    writer.Write(diagnosticosAtenciones[i] ?? "");
                    writer.Write(tratamientosAtenciones[i] ?? "");
                    writer.Write(estadosAtenciones[i] ?? "");
                    writer.Write(situacionesAtenciones[i] ?? "");
                }

                // Bloque 4: Servicios
                writer.Write(codigosServicios.Count);
                for (int i = 0; i < codigosServicios.Count; i++)
                {
                    writer.Write(codigosServicios[i]);
                    writer.Write(codigosMascotasServicios[i]);
                    writer.Write(nombresServicios[i] ?? "");
                    writer.Write(responsablesServicios[i] ?? "");
                    writer.Write(fechasServicios[i].ToString("yyyy-MM-dd", CultureInfo.InvariantCulture));
                    writer.Write(horasServicios[i] ?? "");
                    writer.Write(costosServicios[i]);
                    writer.Write(observacionesServicios[i] ?? "");
                    writer.Write(estadosServicios[i] ?? "");
                }
            }

            ultimaOperacionPersistencia = $"Respaldo binario creado ({DateTime.Now:dd/MM/yyyy HH:mm:ss}): {codigosPropietarios.Count} prop., {codigosMascotas.Count} masc., {codigosAtenciones.Count} aten., {codigosServicios.Count} serv.";
            Console.WriteLine($"Respaldo binario creado exitosamente en: {Path.GetFullPath(RutaRespaldoBinario)}");
            Console.WriteLine("Registros incluidos (v2): Propietarios, Mascotas, Atenciones y Servicios.");
            Console.WriteLine("Pendientes: Productos y Ventas (esperando estructuras de Maximo).");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al crear el respaldo binario: {ex.Message}");
        }
    }

    // =========================================================
    // 4. RESTAURAR RESPALDO BINARIO (BinaryReader)
    // =========================================================
    static void RestaurarRespaldoBinario()
    {
        Console.WriteLine();
        Console.WriteLine("========================================");
        Console.WriteLine("      RESTAURAR RESPALDO BINARIO");
        Console.WriteLine("========================================");

        if (!File.Exists(RutaRespaldoBinario))
        {
            Console.WriteLine($"Error: No existe el archivo de respaldo '{RutaRespaldoBinario}'.");
            return;
        }

        try
        {
            List<int> tempCodProp = new List<int>();
            List<string> tempNomProp = new List<string>();
            List<string> tempTelProp = new List<string>();
            List<string> tempDniProp = new List<string>();
            List<string> tempDirProp = new List<string>();
            List<string> tempCorProp = new List<string>();
            HashSet<int> setCodProp = new HashSet<int>();

            List<int> tempCodMasc = new List<int>();
            List<string> tempNomMasc = new List<string>();
            List<string> tempEspMasc = new List<string>();
            List<string> tempTipMasc = new List<string>();
            List<string> tempSexMasc = new List<string>();
            List<DateTime> tempFecMasc = new List<DateTime>();
            List<int> tempCodPropMasc = new List<int>();
            HashSet<int> setCodMasc = new HashSet<int>();

            List<int> tempCodAten = new List<int>();
            List<int> tempCodMascAten = new List<int>();
            List<string> tempEspAten = new List<string>();
            List<string> tempVetAten = new List<string>();
            List<DateTime> tempFecAten = new List<DateTime>();
            List<string> tempHorAten = new List<string>();
            List<string> tempMotAten = new List<string>();
            List<decimal> tempCosAten = new List<decimal>();
            List<string> tempDiaAten = new List<string>();
            List<string> tempTraAten = new List<string>();
            List<string> tempEstAten = new List<string>();
            List<string> tempSitAten = new List<string>();
            HashSet<int> setCodAten = new HashSet<int>();

            List<int> tempCodServ = new List<int>();
            List<int> tempCodMascServ = new List<int>();
            List<string> tempNomServ = new List<string>();
            List<string> tempResServ = new List<string>();
            List<DateTime> tempFecServ = new List<DateTime>();
            List<string> tempHorServ = new List<string>();
            List<decimal> tempCosServ = new List<decimal>();
            List<string> tempObsServ = new List<string>();
            List<string> tempEstServ = new List<string>();
            HashSet<int> setCodServ = new HashSet<int>();

            using (FileStream fs = new FileStream(RutaRespaldoBinario, FileMode.Open, FileAccess.Read))
            using (BinaryReader reader = new BinaryReader(fs, Encoding.UTF8))
            {
                if (fs.Length == 0)
                {
                    Console.WriteLine("Error: El archivo binario esta vacio.");
                    return;
                }

                string firma = reader.ReadString();
                int version = reader.ReadInt32();

                if (firma != "VET_EP_ARIAS_BIN" || version != 2)
                {
                    Console.WriteLine("Error: Firma o version del archivo binario no compatible (se esperaba v2).");
                    return;
                }

                // Leer Propietarios
                int totalProp = reader.ReadInt32();
                if (totalProp < 0)
                {
                    Console.WriteLine("Error: Cantidad de propietarios corrupta.");
                    return;
                }
                for (int i = 0; i < totalProp; i++)
                {
                    int cod = reader.ReadInt32();
                    string nom = reader.ReadString();
                    string tel = reader.ReadString();
                    string dni = reader.ReadString();
                    string dir = reader.ReadString();
                    string cor = reader.ReadString();

                    if (cod < 2001 || !setCodProp.Add(cod))
                    {
                        Console.WriteLine($"Error: Codigo de propietario invalido o duplicado ({cod}).");
                        return;
                    }

                    tempCodProp.Add(cod);
                    tempNomProp.Add(nom);
                    tempTelProp.Add(tel);
                    tempDniProp.Add(dni);
                    tempDirProp.Add(dir);
                    tempCorProp.Add(cor);
                }

                // Leer Mascotas
                int totalMasc = reader.ReadInt32();
                if (totalMasc < 0)
                {
                    Console.WriteLine("Error: Cantidad de mascotas corrupta.");
                    return;
                }
                for (int i = 0; i < totalMasc; i++)
                {
                    int codM = reader.ReadInt32();
                    string nomM = reader.ReadString();
                    string esp = reader.ReadString();
                    string tip = reader.ReadString();
                    string sex = reader.ReadString();
                    string fecStr = reader.ReadString();
                    int codDueno = reader.ReadInt32();

                    if (codM < 1001 || !setCodMasc.Add(codM))
                    {
                        Console.WriteLine($"Error: Codigo de mascota invalido o duplicado ({codM}).");
                        return;
                    }
                    if (!DateTime.TryParseExact(fecStr, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime fechaNac))
                    {
                        Console.WriteLine($"Error: Fecha corrupta en mascota {codM}.");
                        return;
                    }
                    if (!setCodProp.Contains(codDueno))
                    {
                        Console.WriteLine($"Error: Mascota {codM} referencia al propietario inexistente {codDueno}.");
                        return;
                    }

                    tempCodMasc.Add(codM);
                    tempNomMasc.Add(nomM);
                    tempEspMasc.Add(esp);
                    tempTipMasc.Add(tip);
                    tempSexMasc.Add(sex);
                    tempFecMasc.Add(fechaNac);
                    tempCodPropMasc.Add(codDueno);
                }

                // Leer Atenciones
                int totalAten = reader.ReadInt32();
                if (totalAten < 0)
                {
                    Console.WriteLine("Error: Cantidad de atenciones corrupta.");
                    return;
                }
                for (int i = 0; i < totalAten; i++)
                {
                    int codA = reader.ReadInt32();
                    int codMascRef = reader.ReadInt32();
                    string espA = reader.ReadString();
                    string vetA = reader.ReadString();
                    string fecAStr = reader.ReadString();
                    string horA = reader.ReadString();
                    string motA = reader.ReadString();
                    decimal cosA = reader.ReadDecimal();
                    string diaA = reader.ReadString();
                    string traA = reader.ReadString();
                    string estA = reader.ReadString();
                    string sitA = reader.ReadString();

                    if (codA <= 0 || !setCodAten.Add(codA))
                    {
                        Console.WriteLine($"Error: Codigo de atencion invalido o duplicado ({codA}).");
                        return;
                    }
                    if (!setCodMasc.Contains(codMascRef))
                    {
                        Console.WriteLine($"Error: Atencion {codA} referencia a mascota inexistente {codMascRef}.");
                        return;
                    }
                    if (!DateTime.TryParseExact(fecAStr, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime fechaAten) || cosA < 0)
                    {
                        Console.WriteLine($"Error: Fecha o costo corrupto en atencion {codA}.");
                        return;
                    }

                    tempCodAten.Add(codA);
                    tempCodMascAten.Add(codMascRef);
                    tempEspAten.Add(espA);
                    tempVetAten.Add(vetA);
                    tempFecAten.Add(fechaAten);
                    tempHorAten.Add(horA);
                    tempMotAten.Add(motA);
                    tempCosAten.Add(cosA);
                    tempDiaAten.Add(diaA);
                    tempTraAten.Add(traA);
                    tempEstAten.Add(estA);
                    tempSitAten.Add(sitA);
                }

                // Leer Servicios
                int totalServ = reader.ReadInt32();
                if (totalServ < 0)
                {
                    Console.WriteLine("Error: Cantidad de servicios corrupta.");
                    return;
                }
                for (int i = 0; i < totalServ; i++)
                {
                    int codS = reader.ReadInt32();
                    int codMascRef = reader.ReadInt32();
                    string nomS = reader.ReadString();
                    string resS = reader.ReadString();
                    string fecSStr = reader.ReadString();
                    string horS = reader.ReadString();
                    decimal cosS = reader.ReadDecimal();
                    string obsS = reader.ReadString();
                    string estS = reader.ReadString();

                    if (codS <= 0 || !setCodServ.Add(codS))
                    {
                        Console.WriteLine($"Error: Codigo de servicio invalido o duplicado ({codS}).");
                        return;
                    }
                    if (!setCodMasc.Contains(codMascRef))
                    {
                        Console.WriteLine($"Error: Servicio {codS} referencia a mascota inexistente {codMascRef}.");
                        return;
                    }
                    if (!DateTime.TryParseExact(fecSStr, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime fechaServ) || cosS < 0)
                    {
                        Console.WriteLine($"Error: Fecha o costo corrupto en servicio {codS}.");
                        return;
                    }

                    tempCodServ.Add(codS);
                    tempCodMascServ.Add(codMascRef);
                    tempNomServ.Add(nomS);
                    tempResServ.Add(resS);
                    tempFecServ.Add(fechaServ);
                    tempHorServ.Add(horS);
                    tempCosServ.Add(cosS);
                    tempObsServ.Add(obsS);
                    tempEstServ.Add(estS);
                }

                if (fs.Position != fs.Length)
                {
                    Console.WriteLine("Error: El archivo binario contiene bytes residuales o esta corrupto.");
                    return;
                }
            }

            if (codigosPropietarios.Count > 0 || codigosMascotas.Count > 0 || codigosAtenciones.Count > 0 || codigosServicios.Count > 0)
            {
                Console.Write($"Se reemplazaran los datos en memoria por {tempCodProp.Count} prop., {tempCodMasc.Count} masc., {tempCodAten.Count} aten. y {tempCodServ.Count} serv. del respaldo. ¿Confirmar? (S/N): ");
                string resp = (Console.ReadLine() ?? "").Trim().ToUpperInvariant();
                if (resp != "S")
                {
                    Console.WriteLine("Restauracion cancelada. Los datos actuales se conservaron.");
                    return;
                }
            }

            ReemplazarListasEnMemoria(
                tempCodProp, tempNomProp, tempTelProp, tempDniProp, tempDirProp, tempCorProp,
                tempCodMasc, tempNomMasc, tempEspMasc, tempTipMasc, tempSexMasc, tempFecMasc, tempCodPropMasc,
                tempCodAten, tempCodMascAten, tempEspAten, tempVetAten, tempFecAten, tempHorAten, tempMotAten, tempCosAten, tempDiaAten, tempTraAten, tempEstAten, tempSitAten,
                tempCodServ, tempCodMascServ, tempNomServ, tempResServ, tempFecServ, tempHorServ, tempCosServ, tempObsServ, tempEstServ
            );

            ultimaOperacionPersistencia = $"Respaldo binario restaurado ({DateTime.Now:dd/MM/yyyy HH:mm:ss}): {codigosPropietarios.Count} prop., {codigosMascotas.Count} masc., {codigosAtenciones.Count} aten., {codigosServicios.Count} serv.";
            Console.WriteLine("Respaldo binario restaurado correctamente:");
            Console.WriteLine($" - Propietarios : {codigosPropietarios.Count} (proximo codigo: {codigoPropietario})");
            Console.WriteLine($" - Mascotas     : {codigosMascotas.Count} (proximo codigo: {codigoMascota})");
            Console.WriteLine($" - Atenciones   : {codigosAtenciones.Count}");
            Console.WriteLine($" - Servicios    : {codigosServicios.Count}");
        }
        catch (EndOfStreamException)
        {
            Console.WriteLine("Error: El archivo binario esta incompleto o truncado. Se conservaron los datos actuales.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al restaurar respaldo binario (se conservaron los datos actuales): {ex.Message}");
        }
    }

    // =========================================================
    // 5. CONSULTAR ESTADO DE CARGA
    // =========================================================
    static void ConsultarEstadoCarga()
    {
        Console.WriteLine();
        Console.WriteLine("========================================");
        Console.WriteLine("       ESTADO DE CARGA Y REGISTROS");
        Console.WriteLine("========================================");
        Console.WriteLine($"Propietarios en memoria : {codigosPropietarios.Count} (proximo codigo: {codigoPropietario}) | Integridad: {(ValidarIntegridadListasPropietarios() ? "OK" : "ERROR")}");
        Console.WriteLine($"Mascotas en memoria     : {codigosMascotas.Count} (proximo codigo: {codigoMascota}) | Integridad: {(ValidarIntegridadListasMascotas() ? "OK" : "ERROR")}");
        Console.WriteLine($"Atenciones en memoria   : {codigosAtenciones.Count} | Integridad: {(ValidarIntegridadListasAtenciones() ? "OK" : "ERROR")}");
        Console.WriteLine($"Servicios en memoria    : {codigosServicios.Count} | Integridad: {(ValidarIntegridadListasServicios() ? "OK" : "ERROR")}");
        Console.WriteLine("----------------------------------------");
        Console.WriteLine($"CSV Propietarios        : {(File.Exists(RutaCsvPropietarios) ? $"Disponible ({File.GetLastWriteTime(RutaCsvPropietarios):dd/MM/yyyy HH:mm:ss})" : "No generado")}");
        Console.WriteLine($"CSV Mascotas            : {(File.Exists(RutaCsvMascotas) ? $"Disponible ({File.GetLastWriteTime(RutaCsvMascotas):dd/MM/yyyy HH:mm:ss})" : "No generado")}");
        Console.WriteLine($"CSV Atenciones          : {(File.Exists(RutaCsvAtenciones) ? $"Disponible ({File.GetLastWriteTime(RutaCsvAtenciones):dd/MM/yyyy HH:mm:ss})" : "No generado")}");
        Console.WriteLine($"CSV Servicios           : {(File.Exists(RutaCsvServicios) ? $"Disponible ({File.GetLastWriteTime(RutaCsvServicios):dd/MM/yyyy HH:mm:ss})" : "No generado")}");
        Console.WriteLine($"Respaldo Binario (v2)   : {(File.Exists(RutaRespaldoBinario) ? $"Disponible ({File.GetLastWriteTime(RutaRespaldoBinario):dd/MM/yyyy HH:mm:ss})" : "No generado")}");
        Console.WriteLine("----------------------------------------");
        Console.WriteLine($"Ultima operacion        : {ultimaOperacionPersistencia}");
        Console.WriteLine("Entidades integradas    : Propietarios, Mascotas, Atenciones, Servicios.");
        Console.WriteLine("Entidades pendientes    : Productos y Ventas (esperando estructuras de Maximo).");
    }

    // =========================================================
    // AUXILIARES DE REEMPLAZO ATOMICO, CODIGOS Y CSV
    // =========================================================
    static void ReemplazarListasEnMemoria(
        List<int> tempCodProp, List<string> tempNomProp, List<string> tempTelProp, List<string> tempDniProp, List<string> tempDirProp, List<string> tempCorProp,
        List<int> tempCodMasc, List<string> tempNomMasc, List<string> tempEspMasc, List<string> tempTipMasc, List<string> tempSexMasc, List<DateTime> tempFecMasc, List<int> tempCodPropMasc,
        List<int> tempCodAten, List<int> tempCodMascAten, List<string> tempEspAten, List<string> tempVetAten, List<DateTime> tempFecAten, List<string> tempHorAten, List<string> tempMotAten, List<decimal> tempCosAten, List<string> tempDiaAten, List<string> tempTraAten, List<string> tempEstAten, List<string> tempSitAten,
        List<int> tempCodServ, List<int> tempCodMascServ, List<string> tempNomServ, List<string> tempResServ, List<DateTime> tempFecServ, List<string> tempHorServ, List<decimal> tempCosServ, List<string> tempObsServ, List<string> tempEstServ)
    {
        codigosPropietarios.Clear(); propietarios.Clear(); telefonos.Clear(); dnisPropietarios.Clear(); direcciones.Clear(); correos.Clear();
        codigosPropietarios.AddRange(tempCodProp);
        propietarios.AddRange(tempNomProp);
        telefonos.AddRange(tempTelProp);
        dnisPropietarios.AddRange(tempDniProp);
        direcciones.AddRange(tempDirProp);
        correos.AddRange(tempCorProp);

        codigosMascotas.Clear(); mascotas.Clear(); especies.Clear(); tiposAnimales.Clear(); sexos.Clear(); fechasNacimiento.Clear(); codigosPropietariosMascotas.Clear();
        codigosMascotas.AddRange(tempCodMasc);
        mascotas.AddRange(tempNomMasc);
        especies.AddRange(tempEspMasc);
        tiposAnimales.AddRange(tempTipMasc);
        sexos.AddRange(tempSexMasc);
        fechasNacimiento.AddRange(tempFecMasc);
        codigosPropietariosMascotas.AddRange(tempCodPropMasc);

        codigosAtenciones.Clear(); codigosMascotasAtenciones.Clear(); especialidadesAtenciones.Clear(); veterinariosAtenciones.Clear();
        fechasAtenciones.Clear(); horasAtenciones.Clear(); motivosAtenciones.Clear(); costosAtenciones.Clear();
        diagnosticosAtenciones.Clear(); tratamientosAtenciones.Clear(); estadosAtenciones.Clear(); situacionesAtenciones.Clear();

        codigosAtenciones.AddRange(tempCodAten);
        codigosMascotasAtenciones.AddRange(tempCodMascAten);
        especialidadesAtenciones.AddRange(tempEspAten);
        veterinariosAtenciones.AddRange(tempVetAten);
        fechasAtenciones.AddRange(tempFecAten);
        horasAtenciones.AddRange(tempHorAten);
        motivosAtenciones.AddRange(tempMotAten);
        costosAtenciones.AddRange(tempCosAten);
        diagnosticosAtenciones.AddRange(tempDiaAten);
        tratamientosAtenciones.AddRange(tempTraAten);
        estadosAtenciones.AddRange(tempEstAten);
        situacionesAtenciones.AddRange(tempSitAten);

        codigosServicios.Clear(); codigosMascotasServicios.Clear(); nombresServicios.Clear(); responsablesServicios.Clear();
        fechasServicios.Clear(); horasServicios.Clear(); costosServicios.Clear(); observacionesServicios.Clear(); estadosServicios.Clear();

        codigosServicios.AddRange(tempCodServ);
        codigosMascotasServicios.AddRange(tempCodMascServ);
        nombresServicios.AddRange(tempNomServ);
        responsablesServicios.AddRange(tempResServ);
        fechasServicios.AddRange(tempFecServ);
        horasServicios.AddRange(tempHorServ);
        costosServicios.AddRange(tempCosServ);
        observacionesServicios.AddRange(tempObsServ);
        estadosServicios.AddRange(tempEstServ);

        RecalcularSiguientesCodigos();
    }

    static void RecalcularSiguientesCodigos()
    {
        int maxProp = 2000;
        for (int i = 0; i < codigosPropietarios.Count; i++)
        {
            if (codigosPropietarios[i] > maxProp) maxProp = codigosPropietarios[i];
        }
        codigoPropietario = maxProp + 1;

        int maxMasc = 1000;
        for (int i = 0; i < codigosMascotas.Count; i++)
        {
            if (codigosMascotas[i] > maxMasc) maxMasc = codigosMascotas[i];
        }
        codigoMascota = maxMasc + 1;
    }

    static string EscaparCSV(string valor)
    {
        if (string.IsNullOrEmpty(valor)) return "";

        if (valor.Contains(",") || valor.Contains("\"") || valor.Contains("\n") || valor.Contains("\r"))
        {
            return "\"" + valor.Replace("\"", "\"\"") + "\"";
        }

        return valor;
    }

    static List<string> ParsearLineaCSV(string linea)
    {
        List<string> resultado = new List<string>();
        StringBuilder campoActual = new StringBuilder();
        bool enComillas = false;

        for (int i = 0; i < linea.Length; i++)
        {
            char c = linea[i];

            if (enComillas)
            {
                if (c == '"')
                {
                    if (i + 1 < linea.Length && linea[i + 1] == '"')
                    {
                        campoActual.Append('"');
                        i++;
                    }
                    else
                    {
                        enComillas = false;
                    }
                }
                else
                {
                    campoActual.Append(c);
                }
            }
            else
            {
                if (c == '"')
                {
                    enComillas = true;
                }
                else if (c == ',')
                {
                    resultado.Add(campoActual.ToString());
                    campoActual.Clear();
                }
                else
                {
                    campoActual.Append(c);
                }
            }
        }

        if (enComillas) return null;

        resultado.Add(campoActual.ToString());
        return resultado;
    }
}