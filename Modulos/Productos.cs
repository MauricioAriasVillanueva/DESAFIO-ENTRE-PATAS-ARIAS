using System.Collections.Generic;

partial class Program
{
    // LISTAS PARA LA GESTION DE PRODUCTOS
    static List<int> codigosProductos = new List<int>();
    static List<string> nombresProductos = new List<string>();
    static List<decimal> preciosProductos = new List<decimal>();
    static List<int> stocksProductos = new List<int>();

    // BUSCAR POSICION DEL PRODUCTO POR CODIGO
    static int BuscarPosicionProducto(int codigo)
    {
        for (int i = 0; i < codigosProductos.Count; i++)
        {
            if (codigosProductos[i] == codigo)
            {
                return i;
            }
        }

        return -1;
    }
}
