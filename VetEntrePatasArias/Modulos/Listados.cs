using System;
using System.Globalization;

partial class Program
{
    // =========================================================
    // 1. LISTAR PROPIETARIOS
    // =========================================================
    static void ListarPropietarios()
    {
        Console.WriteLine();
        Console.WriteLine("========================================");
        Console.WriteLine("          LISTADO DE PROPIETARIOS");
        Console.WriteLine("========================================");

        if (codigosPropietarios.Count == 0)
        {
            Console.WriteLine("No hay propietarios registrados.");
            return;
        }

        if (!ValidarIntegridadListasPropietarios())
        {
            Console.WriteLine("Error interno: las listas paralelas de propietarios tienen distinta longitud.");
            return;
        }

        for (int i = 0; i < codigosPropietarios.Count; i++)
        {
            Console.WriteLine(
                $"Codigo: {codigosPropietarios[i]} | " +
                $"Nombre: {propietarios[i]} | " +
                $"DNI: {dnisPropietarios[i]} | " +
                $"Tel: {telefonos[i]} | " +
                $"Dir: {direcciones[i]} | " +
                $"Correo: {correos[i]}"
            );
        }

        Console.WriteLine("----------------------------------------");
        Console.WriteLine($"Total de propietarios: {codigosPropietarios.Count}");
    }

    // =========================================================
    // 2. LISTAR MASCOTAS (Relacion por codigo, nunca por indice)
    // =========================================================
    static void ListarMascotas()
    {
        Console.WriteLine();
        Console.WriteLine("========================================");
        Console.WriteLine("           LISTADO DE MASCOTAS");
        Console.WriteLine("========================================");

        if (codigosMascotas.Count == 0)
        {
            Console.WriteLine("No hay mascotas registradas.");
            return;
        }

        if (!ValidarIntegridadListasMascotas())
        {
            Console.WriteLine("Error interno: las listas paralelas de mascotas tienen distinta longitud.");
            return;
        }

        for (int i = 0; i < codigosMascotas.Count; i++)
        {
            int codProp = codigosPropietariosMascotas[i];
            int idxProp = BuscarIndicePropietarioPorCodigo(codProp);
            string nombreProp = (idxProp != -1) ? propietarios[idxProp] : "PROPIETARIO NO ENCONTRADO";

            Console.WriteLine(
                $"Codigo: {codigosMascotas[i]} | " +
                $"Nombre: {mascotas[i]} | " +
                $"Especie: {especies[i]} | " +
                $"Tipo: {tiposAnimales[i]} | " +
                $"Sexo: {sexos[i]} | " +
                $"Nacimiento: {fechasNacimiento[i]:dd/MM/yyyy} | " +
                $"Propietario: {codProp} ({nombreProp})"
            );
        }

        Console.WriteLine("----------------------------------------");
        Console.WriteLine($"Total de mascotas: {codigosMascotas.Count}");
    }

    // =========================================================
    // 3. LISTAR ATENCIONES MEDICAS (Identificando su mascota)
    // =========================================================
    static void ListarAtenciones()
    {
        Console.WriteLine();
        Console.WriteLine("========================================");
        Console.WriteLine("      LISTADO DE ATENCIONES MEDICAS");
        Console.WriteLine("========================================");

        if (codigosAtenciones.Count == 0)
        {
            Console.WriteLine("No hay atenciones medicas registradas.");
            return;
        }

        if (!ValidarIntegridadListasAtenciones())
        {
            Console.WriteLine("Error interno: las listas paralelas de atenciones tienen distinta longitud.");
            return;
        }

        for (int i = 0; i < codigosAtenciones.Count; i++)
        {
            int codMasc = codigosMascotasAtenciones[i];
            int idxMasc = BuscarIndiceMascotaPorCodigo(codMasc);
            string nombreMasc = (idxMasc != -1) ? mascotas[idxMasc] : "MASCOTA NO ENCONTRADA";

            Console.WriteLine(
                $"Cod. Atencion: {codigosAtenciones[i]} | " +
                $"Mascota: {codMasc} ({nombreMasc}) | " +
                $"Especialidad: {especialidadesAtenciones[i]} | " +
                $"Veterinario: {veterinariosAtenciones[i]} | " +
                $"Fecha: {fechasAtenciones[i]:dd/MM/yyyy} {horasAtenciones[i]} | " +
                $"Motivo: {motivosAtenciones[i]} | " +
                $"Costo: S/ {costosAtenciones[i].ToString("F2", CultureInfo.InvariantCulture)} | " +
                $"Diagnostico: {diagnosticosAtenciones[i]} | " +
                $"Tratamiento: {tratamientosAtenciones[i]} | " +
                $"Estado: {estadosAtenciones[i]} | " +
                $"Situacion: {situacionesAtenciones[i]}"
            );
        }

        Console.WriteLine("----------------------------------------");
        Console.WriteLine($"Total de atenciones: {codigosAtenciones.Count}");
    }

    // =========================================================
    // 4. LISTAR SERVICIOS VETERINARIOS (Identificando su mascota)
    // =========================================================
    static void ListarServicios()
    {
        Console.WriteLine();
        Console.WriteLine("========================================");
        Console.WriteLine("    LISTADO DE SERVICIOS VETERINARIOS");
        Console.WriteLine("========================================");

        if (codigosServicios.Count == 0)
        {
            Console.WriteLine("No hay servicios veterinarios registrados.");
            return;
        }

        if (!ValidarIntegridadListasServicios())
        {
            Console.WriteLine("Error interno: las listas paralelas de servicios tienen distinta longitud.");
            return;
        }

        for (int i = 0; i < codigosServicios.Count; i++)
        {
            int codMasc = codigosMascotasServicios[i];
            int idxMasc = BuscarIndiceMascotaPorCodigo(codMasc);
            string nombreMasc = (idxMasc != -1) ? mascotas[idxMasc] : "MASCOTA NO ENCONTRADA";

            Console.WriteLine(
                $"Cod. Servicio: {codigosServicios[i]} | " +
                $"Mascota: {codMasc} ({nombreMasc}) | " +
                $"Servicio: {nombresServicios[i]} | " +
                $"Responsable: {responsablesServicios[i]} | " +
                $"Fecha: {fechasServicios[i]:dd/MM/yyyy} {horasServicios[i]} | " +
                $"Costo: S/ {costosServicios[i].ToString("F2", CultureInfo.InvariantCulture)} | " +
                $"Obs: {observacionesServicios[i]} | " +
                $"Estado: {estadosServicios[i]}"
            );
        }

        Console.WriteLine("----------------------------------------");
        Console.WriteLine($"Total de servicios: {codigosServicios.Count}");
    }

    // 5. LISTAR PRODUCTOS DEL PET SHOP
    static void ListarProductos()
    {
        Console.WriteLine();
        Console.WriteLine("========== LISTADO DE PRODUCTOS DEL PET SHOP ==========");

        if (codigosProductos.Count == 0)
        {
            Console.WriteLine("No hay productos registrados.");
            return;
        }

        if (!ValidarIntegridadListasProductos())
        {
            Console.WriteLine("Error: las listas de productos no estan integras.");
            return;
        }

        for (int i = 0; i < codigosProductos.Count; i++)
        {
            Console.WriteLine("----------------------------------------");
            Console.WriteLine("Codigo: " + codigosProductos[i]);
            Console.WriteLine("Nombre: " + nombresProductos[i]);
            Console.WriteLine("Precio: S/ " +
                preciosProductos[i].ToString("0.00"));
            Console.WriteLine("Stock: " + stocksProductos[i]);
        }

        Console.WriteLine("----------------------------------------");
        Console.WriteLine("Total de productos: " + codigosProductos.Count);
    }
    
    // 6. LISTAR VENTAS DEL PET SHOP
    
    static void ListarVentas()
    {
        Console.WriteLine();
        Console.WriteLine("========== LISTADO DE VENTAS DEL PET SHOP ==========");

        if (codigosVentas.Count == 0)
        {
            Console.WriteLine("No hay ventas registradas.");
            return;
        }

        if (!ValidarIntegridadListasVentas())
        {
            Console.WriteLine("Error: las listas de ventas no estan integras.");
            return;
        }

        for (int i = 0; i < codigosVentas.Count; i++)
        {
            Console.WriteLine("----------------------------------------");
            Console.WriteLine("Codigo de venta: " + codigosVentas[i]);
            Console.WriteLine("Codigo de producto: " + codigosProductosVendidos[i]);
            Console.WriteLine("Producto: " + nombresProductosVendidos[i]);
            Console.WriteLine("Cantidad: " + cantidadesVendidas[i]);
            Console.WriteLine("Precio unitario: S/ " +
                preciosUnitariosVentas[i].ToString("0.00"));
            Console.WriteLine("Total: S/ " +
                totalesVentas[i].ToString("0.00"));
            Console.WriteLine("Fecha: " + fechasVentas[i]);

            if (codigosPropietariosVentas[i] == 0)
            {
                Console.WriteLine("Tipo: Venta libre");
            }
            else
            {
                Console.WriteLine("Codigo de propietario: " +
                    codigosPropietariosVentas[i]);

                if (codigosMascotasVentas[i] != 0)
                {
                    Console.WriteLine("Codigo de mascota: " +
                        codigosMascotasVentas[i]);
                }
                else
                {
                    Console.WriteLine("Mascota: Sin asociar");
                }
            }
        }

        Console.WriteLine("----------------------------------------");
        Console.WriteLine("Total de ventas: " + codigosVentas.Count);
    }


    // =========================================================
    // 7. ORDENAR MASCOTAS POR CODIGO (BURBUJA MEJORADA)
    // =========================================================
    static void OrdenarMascotasPorCodigo()
    {
        if (codigosMascotas.Count == 0)
        {
            Console.WriteLine("No hay mascotas registradas para ordenar.");
            return;
        }

        if (!ValidarIntegridadListasMascotas())
        {
            Console.WriteLine("Error interno: las listas paralelas de mascotas tienen distinta longitud.");
            return;
        }

        int n = codigosMascotas.Count;
        bool huboIntercambio;

        for (int i = 0; i < n - 1; i++)
        {
            huboIntercambio = false;

            for (int j = 0; j < n - 1 - i; j++)
            {
                if (codigosMascotas[j] > codigosMascotas[j + 1])
                {
                    IntercambiarMascotas(j, j + 1);
                    huboIntercambio = true;
                }
            }

            if (!huboIntercambio)
            {
                break;
            }
        }

        Console.WriteLine("Mascotas ordenadas ascendentemente por codigo.");
        ListarMascotas();
    }

    // =========================================================
    // 8. ORDENAR MASCOTAS POR NOMBRE (BURBUJA MEJORADA)
    // =========================================================
    static void OrdenarMascotasPorNombre()
    {
        if (codigosMascotas.Count == 0)
        {
            Console.WriteLine("No hay mascotas registradas para ordenar.");
            return;
        }

        if (!ValidarIntegridadListasMascotas())
        {
            Console.WriteLine("Error interno: las listas paralelas de mascotas tienen distinta longitud.");
            return;
        }

        int n = codigosMascotas.Count;
        bool huboIntercambio;

        for (int i = 0; i < n - 1; i++)
        {
            huboIntercambio = false;

            for (int j = 0; j < n - 1 - i; j++)
            {
                if (string.Compare(mascotas[j], mascotas[j + 1], StringComparison.OrdinalIgnoreCase) > 0)
                {
                    IntercambiarMascotas(j, j + 1);
                    huboIntercambio = true;
                }
            }

            if (!huboIntercambio)
            {
                break;
            }
        }

        Console.WriteLine("Mascotas ordenadas alfabeticamente por nombre.");
        ListarMascotas();
    }

    // =========================================================
    // AUXILIARES DE INTERCAMBIO, BUSQUEDA E INTEGRIDAD
    // =========================================================
    static void IntercambiarMascotas(int a, int b)
    {
        int tempCodigo = codigosMascotas[a];
        codigosMascotas[a] = codigosMascotas[b];
        codigosMascotas[b] = tempCodigo;

        string tempNombre = mascotas[a];
        mascotas[a] = mascotas[b];
        mascotas[b] = tempNombre;

        string tempEspecie = especies[a];
        especies[a] = especies[b];
        especies[b] = tempEspecie;

        string tempTipo = tiposAnimales[a];
        tiposAnimales[a] = tiposAnimales[b];
        tiposAnimales[b] = tempTipo;

        string tempSexo = sexos[a];
        sexos[a] = sexos[b];
        sexos[b] = tempSexo;

        DateTime tempFecha = fechasNacimiento[a];
        fechasNacimiento[a] = fechasNacimiento[b];
        fechasNacimiento[b] = tempFecha;

        int tempCodProp = codigosPropietariosMascotas[a];
        codigosPropietariosMascotas[a] = codigosPropietariosMascotas[b];
        codigosPropietariosMascotas[b] = tempCodProp;
    }

    static int BuscarIndicePropietarioPorCodigo(int codigoBuscado)
    {
        for (int i = 0; i < codigosPropietarios.Count; i++)
        {
            if (codigosPropietarios[i] == codigoBuscado)
            {
                return i;
            }
        }
        return -1;
    }

    static int BuscarIndiceMascotaPorCodigo(int codigoBuscado)
    {
        for (int i = 0; i < codigosMascotas.Count; i++)
        {
            if (codigosMascotas[i] == codigoBuscado)
            {
                return i;
            }
        }
        return -1;
    }

    static bool ValidarIntegridadListasPropietarios()
    {
        int total = codigosPropietarios.Count;
        return propietarios.Count == total &&
               telefonos.Count == total &&
               dnisPropietarios.Count == total &&
               direcciones.Count == total &&
               correos.Count == total;
    }

    static bool ValidarIntegridadListasMascotas()
    {
        int total = codigosMascotas.Count;
        return mascotas.Count == total &&
               especies.Count == total &&
               tiposAnimales.Count == total &&
               sexos.Count == total &&
               fechasNacimiento.Count == total &&
               codigosPropietariosMascotas.Count == total;
    }

    static bool ValidarIntegridadListasAtenciones()
    {
        int total = codigosAtenciones.Count;
        return codigosMascotasAtenciones.Count == total &&
               especialidadesAtenciones.Count == total &&
               veterinariosAtenciones.Count == total &&
               fechasAtenciones.Count == total &&
               horasAtenciones.Count == total &&
               motivosAtenciones.Count == total &&
               costosAtenciones.Count == total &&
               diagnosticosAtenciones.Count == total &&
               tratamientosAtenciones.Count == total &&
               estadosAtenciones.Count == total &&
               situacionesAtenciones.Count == total;
    }

    static bool ValidarIntegridadListasServicios()
    {
        int total = codigosServicios.Count;
        return codigosMascotasServicios.Count == total &&
               nombresServicios.Count == total &&
               responsablesServicios.Count == total &&
               fechasServicios.Count == total &&
               horasServicios.Count == total &&
               costosServicios.Count == total &&
               observacionesServicios.Count == total &&
               estadosServicios.Count == total;
    }
}
