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
    // 1. GUARDAR DATOS EN CSV (Escritura atomica con .tmp)
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

        string tmpProp = RutaCsvPropietarios + ".tmp";
        string tmpMasc = RutaCsvMascotas + ".tmp";
        string tmpAten = RutaCsvAtenciones + ".tmp";
        string tmpServ = RutaCsvServicios + ".tmp";

        try
        {
            Directory.CreateDirectory(CarpetaDatos);

            // 1. Preparar y escribir Propietarios en temporal (6 columnas)
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
            File.WriteAllLines(tmpProp, lineasProp, Encoding.UTF8);

            // 2. Preparar y escribir Mascotas en temporal (7 columnas)
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
            File.WriteAllLines(tmpMasc, lineasMasc, Encoding.UTF8);

            // 3. Preparar y escribir Atenciones en temporal (12 columnas)
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
            File.WriteAllLines(tmpAten, lineasAten, Encoding.UTF8);

            // 4. Preparar y escribir Servicios en temporal (9 columnas)
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
            File.WriteAllLines(tmpServ, lineasServ, Encoding.UTF8);

            // 5. Commit atomico: solo si los 4 temporales se escribieron sin error, reemplazan los definitivos
            File.Move(tmpProp, RutaCsvPropietarios, true);
            File.Move(tmpMasc, RutaCsvMascotas, true);
            File.Move(tmpAten, RutaCsvAtenciones, true);
            File.Move(tmpServ, RutaCsvServicios, true);

            ultimaOperacionPersistencia = $"CSV guardado ({DateTime.Now:dd/MM/yyyy HH:mm:ss}): {codigosPropietarios.Count} prop., {codigosMascotas.Count} masc., {codigosAtenciones.Count} aten., {codigosServicios.Count} serv.";
            Console.WriteLine("Archivos CSV guardados de forma atomica en la carpeta 'Datos':");
            Console.WriteLine($" - propietarios.csv : {codigosPropietarios.Count} registros");
            Console.WriteLine($" - mascotas.csv     : {codigosMascotas.Count} registros");
            Console.WriteLine($" - atenciones.csv   : {codigosAtenciones.Count} registros");
            Console.WriteLine($" - servicios.csv    : {codigosServicios.Count} registros");
            Console.WriteLine("Nota: Productos y Ventas aun no se guardan (estructuras pendientes de Maximo).");
        }
        catch (Exception ex)
        {
            LimpiarArchivosTemporales(tmpProp, tmpMasc, tmpAten, tmpServ);
            Console.WriteLine($"Error al guardar los archivos CSV (no se alteraron archivos previos): {ex.Message}");
        }
    }

    // =========================================================
    // 2. CARGAR DATOS DESDE CSV (Parser multilinea + reglas)
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
            List<RegistroCSV> registrosProp = ParsearArchivoCSV(File.ReadAllText(RutaCsvPropietarios, Encoding.UTF8));
            List<RegistroCSV> registrosMasc = ParsearArchivoCSV(File.ReadAllText(RutaCsvMascotas, Encoding.UTF8));
            List<RegistroCSV> registrosAten = ParsearArchivoCSV(File.ReadAllText(RutaCsvAtenciones, Encoding.UTF8));
            List<RegistroCSV> registrosServ = ParsearArchivoCSV(File.ReadAllText(RutaCsvServicios, Encoding.UTF8));

            if (registrosProp == null || registrosMasc == null || registrosAten == null || registrosServ == null)
            {
                Console.WriteLine("Error: Comillas sin cerrar detectadas en uno de los archivos CSV.");
                return;
            }

            if (registrosProp.Count == 0 || registrosMasc.Count == 0 || registrosAten.Count == 0 || registrosServ.Count == 0)
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
            HashSet<string> setDniProp = new HashSet<string>();

            if (registrosProp[0].Campos.Count != 6)
            {
                Console.WriteLine("Error: Cabecera invalida en propietarios.csv.");
                return;
            }

            for (int i = 1; i < registrosProp.Count; i++)
            {
                RegistroCSV reg = registrosProp[i];
                List<string> c = reg.Campos;

                if (c.Count != 6)
                {
                    Console.WriteLine($"Error en propietarios.csv (linea {reg.LineaInicio}): columnas invalidas.");
                    return;
                }
                if (!int.TryParse(c[0].Trim(), NumberStyles.Integer, CultureInfo.InvariantCulture, out int cod) || cod < 2001)
                {
                    Console.WriteLine($"Error en propietarios.csv (linea {reg.LineaInicio}): codigo invalido '{c[0]}'.");
                    return;
                }
                if (!setCodProp.Add(cod))
                {
                    Console.WriteLine($"Error en propietarios.csv (linea {reg.LineaInicio}): codigo duplicado ({cod}).");
                    return;
                }

                string nom = c[1].Trim();
                string tel = c[2].Trim();
                string dni = c[3].Trim();
                string dir = c[4].Trim();
                string cor = c[5].Trim();

                if (!ValidarReglasPropietario(nom, tel, dni, dir, cor, out string errorProp))
                {
                    Console.WriteLine($"Error en propietarios.csv (linea {reg.LineaInicio}): {errorProp}");
                    return;
                }
                if (!setDniProp.Add(dni))
                {
                    Console.WriteLine($"Error en propietarios.csv (linea {reg.LineaInicio}): DNI duplicado ({dni}).");
                    return;
                }

                tempCodProp.Add(cod);
                tempNomProp.Add(nom);
                tempTelProp.Add(tel);
                tempDniProp.Add(dni);
                tempDirProp.Add(dir);
                tempCorProp.Add(cor);
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

            if (registrosMasc[0].Campos.Count != 7)
            {
                Console.WriteLine("Error: Cabecera invalida en mascotas.csv.");
                return;
            }

            for (int i = 1; i < registrosMasc.Count; i++)
            {
                RegistroCSV reg = registrosMasc[i];
                List<string> c = reg.Campos;

                if (c.Count != 7)
                {
                    Console.WriteLine($"Error en mascotas.csv (linea {reg.LineaInicio}): columnas invalidas.");
                    return;
                }
                if (!int.TryParse(c[0].Trim(), NumberStyles.Integer, CultureInfo.InvariantCulture, out int codM) || codM < 1001)
                {
                    Console.WriteLine($"Error en mascotas.csv (linea {reg.LineaInicio}): codigo invalido '{c[0]}'.");
                    return;
                }
                if (!setCodMasc.Add(codM))
                {
                    Console.WriteLine($"Error en mascotas.csv (linea {reg.LineaInicio}): codigo duplicado ({codM}).");
                    return;
                }
                if (!DateTime.TryParseExact(c[5].Trim(), "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime fechaNac))
                {
                    Console.WriteLine($"Error en mascotas.csv (linea {reg.LineaInicio}): fecha invalida '{c[5]}'.");
                    return;
                }
                if (!int.TryParse(c[6].Trim(), NumberStyles.Integer, CultureInfo.InvariantCulture, out int codDueno) || !setCodProp.Contains(codDueno))
                {
                    Console.WriteLine($"Error en mascotas.csv (linea {reg.LineaInicio}): propietario '{c[6]}' no existe.");
                    return;
                }

                string nomM = c[1].Trim();
                string espM = c[2].Trim();
                string tipM = c[3].Trim();
                string sexM = c[4].Trim();

                if (!ValidarReglasMascota(nomM, espM, tipM, sexM, fechaNac, out string errorMasc))
                {
                    Console.WriteLine($"Error en mascotas.csv (linea {reg.LineaInicio}): {errorMasc}");
                    return;
                }

                tempCodMasc.Add(codM);
                tempNomMasc.Add(nomM);
                tempEspMasc.Add(espM);
                tempTipMasc.Add(tipM);
                tempSexMasc.Add(sexM);
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

            if (registrosAten[0].Campos.Count != 12)
            {
                Console.WriteLine("Error: Cabecera invalida en atenciones.csv.");
                return;
            }

            for (int i = 1; i < registrosAten.Count; i++)
            {
                RegistroCSV reg = registrosAten[i];
                List<string> c = reg.Campos;

                if (c.Count != 12)
                {
                    Console.WriteLine($"Error en atenciones.csv (linea {reg.LineaInicio}): columnas invalidas.");
                    return;
                }
                if (!int.TryParse(c[0].Trim(), NumberStyles.Integer, CultureInfo.InvariantCulture, out int codA) || codA <= 0)
                {
                    Console.WriteLine($"Error en atenciones.csv (linea {reg.LineaInicio}): codigo de atencion invalido '{c[0]}'.");
                    return;
                }
                if (!setCodAten.Add(codA))
                {
                    Console.WriteLine($"Error en atenciones.csv (linea {reg.LineaInicio}): codigo duplicado ({codA}).");
                    return;
                }
                if (!int.TryParse(c[1].Trim(), NumberStyles.Integer, CultureInfo.InvariantCulture, out int codMascRef) || !setCodMasc.Contains(codMascRef))
                {
                    Console.WriteLine($"Error en atenciones.csv (linea {reg.LineaInicio}): mascota '{c[1]}' no existe.");
                    return;
                }
                if (!DateTime.TryParseExact(c[4].Trim(), "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime fechaAten))
                {
                    Console.WriteLine($"Error en atenciones.csv (linea {reg.LineaInicio}): fecha invalida '{c[4]}'.");
                    return;
                }
                if (!decimal.TryParse(c[7].Trim(), NumberStyles.Number, CultureInfo.InvariantCulture, out decimal costoAten))
                {
                    Console.WriteLine($"Error en atenciones.csv (linea {reg.LineaInicio}): costo invalido '{c[7]}'.");
                    return;
                }

                string espA = c[2].Trim();
                string vetA = c[3].Trim();
                string horA = c[5].Trim();
                string motA = c[6].Trim();
                string diaA = c[8].Trim();
                string traA = c[9].Trim();
                string estA = c[10].Trim();
                string sitA = c[11].Trim();

                if (!ValidarReglasAtencion(espA, vetA, horA, motA, costoAten, estA, sitA, out string errorAten))
                {
                    Console.WriteLine($"Error en atenciones.csv (linea {reg.LineaInicio}): {errorAten}");
                    return;
                }

                tempCodAten.Add(codA);
                tempCodMascAten.Add(codMascRef);
                tempEspAten.Add(espA);
                tempVetAten.Add(vetA);
                tempFecAten.Add(fechaAten);
                tempHorAten.Add(horA);
                tempMotAten.Add(motA);
                tempCosAten.Add(costoAten);
                tempDiaAten.Add(diaA);
                tempTraAten.Add(traA);
                tempEstAten.Add(estA);
                tempSitAten.Add(sitA);
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

            if (registrosServ[0].Campos.Count != 9)
            {
                Console.WriteLine("Error: Cabecera invalida en servicios.csv.");
                return;
            }

            for (int i = 1; i < registrosServ.Count; i++)
            {
                RegistroCSV reg = registrosServ[i];
                List<string> c = reg.Campos;

                if (c.Count != 9)
                {
                    Console.WriteLine($"Error en servicios.csv (linea {reg.LineaInicio}): columnas invalidas.");
                    return;
                }
                if (!int.TryParse(c[0].Trim(), NumberStyles.Integer, CultureInfo.InvariantCulture, out int codS) || codS <= 0)
                {
                    Console.WriteLine($"Error en servicios.csv (linea {reg.LineaInicio}): codigo de servicio invalido '{c[0]}'.");
                    return;
                }
                if (!setCodServ.Add(codS))
                {
                    Console.WriteLine($"Error en servicios.csv (linea {reg.LineaInicio}): codigo duplicado ({codS}).");
                    return;
                }
                if (!int.TryParse(c[1].Trim(), NumberStyles.Integer, CultureInfo.InvariantCulture, out int codMascRef) || !setCodMasc.Contains(codMascRef))
                {
                    Console.WriteLine($"Error en servicios.csv (linea {reg.LineaInicio}): mascota '{c[1]}' no existe.");
                    return;
                }
                if (!DateTime.TryParseExact(c[4].Trim(), "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime fechaServ))
                {
                    Console.WriteLine($"Error en servicios.csv (linea {reg.LineaInicio}): fecha invalida '{c[4]}'.");
                    return;
                }
                if (!decimal.TryParse(c[6].Trim(), NumberStyles.Number, CultureInfo.InvariantCulture, out decimal costoServ))
                {
                    Console.WriteLine($"Error en servicios.csv (linea {reg.LineaInicio}): costo invalido '{c[6]}'.");
                    return;
                }

                string nomS = c[2].Trim();
                string resS = c[3].Trim();
                string horS = c[5].Trim();
                string obsS = c[7].Trim();
                string estS = c[8].Trim();

                if (!ValidarReglasServicio(nomS, resS, horS, costoServ, estS, out string errorServ))
                {
                    Console.WriteLine($"Error en servicios.csv (linea {reg.LineaInicio}): {errorServ}");
                    return;
                }

                tempCodServ.Add(codS);
                tempCodMascServ.Add(codMascRef);
                tempNomServ.Add(nomS);
                tempResServ.Add(resS);
                tempFecServ.Add(fechaServ);
                tempHorServ.Add(horS);
                tempCosServ.Add(costoServ);
                tempObsServ.Add(obsS);
                tempEstServ.Add(estS);
            }

            if (tempCodProp.Count == 0 && tempCodMasc.Count == 0 && tempCodAten.Count == 0 && tempCodServ.Count == 0)
            {
                Console.WriteLine("Error: Los archivos CSV no contienen registros.");
                return;
            }

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
    // 3. CREAR RESPALDO BINARIO (Escritura atomica con .tmp)
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

        string tmpBin = RutaRespaldoBinario + ".tmp";

        try
        {
            Directory.CreateDirectory(CarpetaDatos);

            using (FileStream fs = new FileStream(tmpBin, FileMode.Create, FileAccess.Write))
            using (BinaryWriter writer = new BinaryWriter(fs, Encoding.UTF8))
            {
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

            File.Move(tmpBin, RutaRespaldoBinario, true);

            ultimaOperacionPersistencia = $"Respaldo binario creado ({DateTime.Now:dd/MM/yyyy HH:mm:ss}): {codigosPropietarios.Count} prop., {codigosMascotas.Count} masc., {codigosAtenciones.Count} aten., {codigosServicios.Count} serv.";
            Console.WriteLine($"Respaldo binario creado exitosamente en: {Path.GetFullPath(RutaRespaldoBinario)}");
            Console.WriteLine("Registros incluidos (v2): Propietarios, Mascotas, Atenciones y Servicios.");
            Console.WriteLine("Pendientes: Productos y Ventas (esperando estructuras de Maximo).");
        }
        catch (Exception ex)
        {
            LimpiarArchivosTemporales(tmpBin);
            Console.WriteLine($"Error al crear el respaldo binario: {ex.Message}");
        }
    }

    // =========================================================
    // 4. RESTAURAR RESPALDO BINARIO (BinaryReader + reglas)
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
            HashSet<string> setDniProp = new HashSet<string>();

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
                    string nom = reader.ReadString().Trim();
                    string tel = reader.ReadString().Trim();
                    string dni = reader.ReadString().Trim();
                    string dir = reader.ReadString().Trim();
                    string cor = reader.ReadString().Trim();

                    if (cod < 2001 || !setCodProp.Add(cod))
                    {
                        Console.WriteLine($"Error: Codigo de propietario invalido o duplicado ({cod}).");
                        return;
                    }
                    if (!ValidarReglasPropietario(nom, tel, dni, dir, cor, out string errorProp) || !setDniProp.Add(dni))
                    {
                        Console.WriteLine($"Error en propietario {cod} del respaldo: {errorProp}");
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
                    string nomM = reader.ReadString().Trim();
                    string esp = reader.ReadString().Trim();
                    string tip = reader.ReadString().Trim();
                    string sex = reader.ReadString().Trim();
                    string fecStr = reader.ReadString().Trim();
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
                    if (!ValidarReglasMascota(nomM, esp, tip, sex, fechaNac, out string errorMasc))
                    {
                        Console.WriteLine($"Error en mascota {codM} del respaldo: {errorMasc}");
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
                    string espA = reader.ReadString().Trim();
                    string vetA = reader.ReadString().Trim();
                    string fecAStr = reader.ReadString().Trim();
                    string horA = reader.ReadString().Trim();
                    string motA = reader.ReadString().Trim();
                    decimal cosA = reader.ReadDecimal();
                    string diaA = reader.ReadString().Trim();
                    string traA = reader.ReadString().Trim();
                    string estA = reader.ReadString().Trim();
                    string sitA = reader.ReadString().Trim();

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
                    if (!DateTime.TryParseExact(fecAStr, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime fechaAten))
                    {
                        Console.WriteLine($"Error: Fecha corrupta en atencion {codA}.");
                        return;
                    }
                    if (!ValidarReglasAtencion(espA, vetA, horA, motA, cosA, estA, sitA, out string errorAten))
                    {
                        Console.WriteLine($"Error en atencion {codA} del respaldo: {errorAten}");
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
                    string nomS = reader.ReadString().Trim();
                    string resS = reader.ReadString().Trim();
                    string fecSStr = reader.ReadString().Trim();
                    string horS = reader.ReadString().Trim();
                    decimal cosS = reader.ReadDecimal();
                    string obsS = reader.ReadString().Trim();
                    string estS = reader.ReadString().Trim();

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
                    if (!DateTime.TryParseExact(fecSStr, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime fechaServ))
                    {
                        Console.WriteLine($"Error: Fecha corrupta en servicio {codS}.");
                        return;
                    }
                    if (!ValidarReglasServicio(nomS, resS, horS, cosS, estS, out string errorServ))
                    {
                        Console.WriteLine($"Error en servicio {codS} del respaldo: {errorServ}");
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
    // VALIDACIONES DE REGLAS DE NEGOCIO POR ENTIDAD
    // =========================================================
    static bool ValidarReglasPropietario(string nombre, string telefono, string dni, string direccion, string correo, out string error)
    {
        if (string.IsNullOrWhiteSpace(nombre))
        {
            error = "nombre del propietario vacio.";
            return false;
        }
        if (string.IsNullOrWhiteSpace(telefono) || telefono.Length < 7 || telefono.Length > 15 || !EsSoloDigitos(telefono))
        {
            error = $"telefono invalido '{telefono}' (debe tener entre 7 y 15 digitos).";
            return false;
        }
        if (string.IsNullOrWhiteSpace(dni) || dni.Length != 8 || !EsSoloDigitos(dni))
        {
            error = $"DNI invalido '{dni}' (debe tener 8 digitos).";
            return false;
        }
        if (string.IsNullOrWhiteSpace(direccion))
        {
            error = "direccion vacia.";
            return false;
        }
        int arroba = correo.IndexOf('@');
        if (string.IsNullOrWhiteSpace(correo) || arroba <= 0 || correo.IndexOf('.', arroba) <= arroba + 1 || correo.EndsWith("."))
        {
            error = $"correo invalido '{correo}'.";
            return false;
        }

        error = "";
        return true;
    }

    static bool ValidarReglasMascota(string nombre, string especie, string tipoAnimal, string sexo, DateTime fechaNacimiento, out string error)
    {
        if (string.IsNullOrWhiteSpace(nombre))
        {
            error = "nombre de mascota vacio.";
            return false;
        }
        if (string.IsNullOrWhiteSpace(especie))
        {
            error = "especie vacia.";
            return false;
        }
        if (string.IsNullOrWhiteSpace(tipoAnimal))
        {
            error = "tipo de animal vacio.";
            return false;
        }
        string sexNorm = sexo.ToUpperInvariant();
        if (sexNorm != "MACHO" && sexNorm != "HEMBRA" && sexNorm != "M" && sexNorm != "H")
        {
            error = $"sexo invalido '{sexo}'.";
            return false;
        }
        if (fechaNacimiento.Date > DateTime.Today || fechaNacimiento.Year < 1980)
        {
            error = $"fecha de nacimiento fuera de rango ({fechaNacimiento:yyyy-MM-dd}).";
            return false;
        }

        error = "";
        return true;
    }

    static bool ValidarReglasAtencion(string especialidad, string veterinario, string hora, string motivo, decimal costo, string estado, string situacion, out string error)
    {
        if (string.IsNullOrWhiteSpace(especialidad) || string.IsNullOrWhiteSpace(veterinario) || string.IsNullOrWhiteSpace(motivo))
        {
            error = "especialidad, veterinario o motivo vacios.";
            return false;
        }
        if (!TimeSpan.TryParseExact(hora, new string[] { @"hh\:mm", @"h\:mm" }, CultureInfo.InvariantCulture, out _))
        {
            error = $"hora invalida '{hora}' (se esperaba formato HH:mm).";
            return false;
        }
        if (costo < 0)
        {
            error = $"costo negativo ({costo}).";
            return false;
        }
        string estNorm = estado.ToUpperInvariant();
        if (estNorm != "ACTIVA" && estNorm != "ANULADA" && estNorm != "ACTIVO" && estNorm != "ANULADO")
        {
            error = $"estado invalido '{estado}'.";
            return false;
        }
        if (string.IsNullOrWhiteSpace(situacion))
        {
            error = "situacion vacia.";
            return false;
        }

        error = "";
        return true;
    }

    static bool ValidarReglasServicio(string nombreServicio, string responsable, string hora, decimal costo, string estado, out string error)
    {
        if (string.IsNullOrWhiteSpace(nombreServicio) || string.IsNullOrWhiteSpace(responsable))
        {
            error = "nombre del servicio o responsable vacios.";
            return false;
        }
        if (!TimeSpan.TryParseExact(hora, new string[] { @"hh\:mm", @"h\:mm" }, CultureInfo.InvariantCulture, out _))
        {
            error = $"hora invalida '{hora}' (se esperaba formato HH:mm).";
            return false;
        }
        if (costo < 0)
        {
            error = $"costo negativo ({costo}).";
            return false;
        }
        string estNorm = estado.ToUpperInvariant();
        if (estNorm != "ACTIVO" && estNorm != "ANULADO" && estNorm != "ACTIVA" && estNorm != "ANULADA")
        {
            error = $"estado invalido '{estado}'.";
            return false;
        }

        error = "";
        return true;
    }

    static bool EsSoloDigitos(string texto)
    {
        for (int i = 0; i < texto.Length; i++)
        {
            if (texto[i] < '0' || texto[i] > '9') return false;
        }
        return true;
    }

    // =========================================================
    // AUXILIARES DE REEMPLAZO ATOMICO, CODIGOS Y CSV MULTILINEA
    // =========================================================
    static void LimpiarArchivosTemporales(params string[] rutas)
    {
        for (int i = 0; i < rutas.Length; i++)
        {
            try
            {
                if (File.Exists(rutas[i])) File.Delete(rutas[i]);
            }
            catch
            {
                // Ignorar error secundario al limpiar temporales
            }
        }
    }

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

    class RegistroCSV
    {
        public int LineaInicio;
        public List<string> Campos = new List<string>();
    }

    static List<RegistroCSV> ParsearArchivoCSV(string contenido)
    {
        List<RegistroCSV> registros = new List<RegistroCSV>();
        if (string.IsNullOrEmpty(contenido)) return registros;

        if (contenido[0] == '\uFEFF')
        {
            contenido = contenido.Substring(1);
        }

        List<string> camposActuales = new List<string>();
        StringBuilder campoActual = new StringBuilder();
        bool enComillas = false;
        int lineaActual = 1;
        int lineaInicioRegistro = 1;

        for (int i = 0; i < contenido.Length; i++)
        {
            char c = contenido[i];

            if (enComillas)
            {
                if (c == '"')
                {
                    if (i + 1 < contenido.Length && contenido[i + 1] == '"')
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
                    if (c == '\r')
                    {
                        if (i + 1 < contenido.Length && contenido[i + 1] == '\n') i++;
                        campoActual.Append('\n');
                        lineaActual++;
                    }
                    else if (c == '\n')
                    {
                        campoActual.Append('\n');
                        lineaActual++;
                    }
                    else
                    {
                        campoActual.Append(c);
                    }
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
                    camposActuales.Add(campoActual.ToString());
                    campoActual.Clear();
                }
                else if (c == '\r' || c == '\n')
                {
                    if (c == '\r' && i + 1 < contenido.Length && contenido[i + 1] == '\n') i++;

                    camposActuales.Add(campoActual.ToString());
                    campoActual.Clear();

                    if (!EsRegistroCSVVacio(camposActuales))
                    {
                        registros.Add(new RegistroCSV
                        {
                            LineaInicio = lineaInicioRegistro,
                            Campos = camposActuales
                        });
                    }

                    camposActuales = new List<string>();
                    lineaActual++;
                    lineaInicioRegistro = lineaActual;
                }
                else
                {
                    campoActual.Append(c);
                }
            }
        }

        if (enComillas) return null;

        if (campoActual.Length > 0 || camposActuales.Count > 0)
        {
            camposActuales.Add(campoActual.ToString());
            if (!EsRegistroCSVVacio(camposActuales))
            {
                registros.Add(new RegistroCSV
                {
                    LineaInicio = lineaInicioRegistro,
                    Campos = camposActuales
                });
            }
        }

        return registros;
    }

    static bool EsRegistroCSVVacio(List<string> campos)
    {
        for (int i = 0; i < campos.Count; i++)
        {
            if (!string.IsNullOrWhiteSpace(campos[i])) return false;
        }
        return true;
    }
}