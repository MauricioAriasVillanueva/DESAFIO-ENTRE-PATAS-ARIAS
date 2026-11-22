
using System;
using System.Collections.Generic;

partial class Program
{
    // LISTAS PARA LA GESTION DE VENTAS
    static List<int> codigosVentas = new List<int>();
    static List<int> codigosProductosVendidos = new List<int>();
    static List<int> cantidadesVendidas = new List<int>();
    static List<decimal> totalesVentas = new List<decimal>();

    // GENERAR CODIGO AUTOMATICO DE VENTA
    static int SiguienteCodigoVenta()
    {
        int mayor = 5000;

        for (int i = 0; i < codigosVentas.Count; i++)
        {
            if (codigosVentas[i] > mayor)
            {
                mayor = codigosVentas[i];
            }
        }

        return mayor == int.MaxValue ? -1 : mayor + 1;
    }
}
