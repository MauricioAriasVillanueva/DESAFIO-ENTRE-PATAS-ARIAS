
Algoritmo PetShop_Historial_Integrante4

    // =====================================================
    // DATOS DEL PET SHOP
    // =====================================================

    Definir cantidadProductos, cantidadVentas Como Entero
    Definir siguienteProducto, siguienteVenta Como Entero
    Definir opcionPrincipal, opcionPetShop Como Entero
    Definir opcionProductos, opcionVentas Como Entero

    Dimension codigosProductos[100]
    Dimension nombresProductos[100]
    Dimension preciosProductos[100]
    Dimension stocksProductos[100]

    Dimension codigosVentas[100]
    Dimension codigosProductosVendidos[100]
    Dimension nombresProductosVendidos[100]
    Dimension cantidadesVendidas[100]
    Dimension preciosUnitariosVentas[100]
    Dimension totalesVentas[100]

    cantidadProductos <- 0
    cantidadVentas <- 0
    siguienteProducto <- 4001
    siguienteVenta <- 5001

    // =====================================================
    // DATOS DEL HISTORIAL CLINICO
    // =====================================================

    Definir cantidadMascotas, cantidadAtenciones, cantidadServicios Como Entero

    Dimension codigosMascotas[100]
    Dimension nombresMascotas[100]
    Dimension especiesMascotas[100]

    Dimension codigosMascotasAtenciones[100]
    Dimension codigosAtenciones[100]
    Dimension motivosAtenciones[100]
    Dimension diagnosticosAtenciones[100]
    Dimension tratamientosAtenciones[100]

    Dimension codigosMascotasServicios[100]
    Dimension codigosServicios[100]
    Dimension nombresServicios[100]

    cantidadMascotas <- 0
    cantidadAtenciones <- 0
    cantidadServicios <- 0

    // =====================================================
    // MENU PRINCIPAL
    // =====================================================

    Repetir
        Escribir ""
        Escribir "=========================================="
        Escribir "       VETERINARIA ENTRE PATAS - ARIAS"
        Escribir "=========================================="
        Escribir "1. Pet Shop"
        Escribir "2. Historial clinico"
        Escribir "0. Salir"
        Escribir "Seleccione una opcion:"
        Leer opcionPrincipal

        Segun opcionPrincipal Hacer
            1:
                Repetir
                    Escribir ""
                    Escribir "========== PET SHOP =========="
                    Escribir "1. Gestion de productos"
                    Escribir "2. Gestion de ventas"
                    Escribir "0. Volver al menu principal"
                    Leer opcionPetShop

                    Segun opcionPetShop Hacer
                        1:
                            Repetir
                                Escribir ""
                                Escribir "===== GESTION DE PRODUCTOS ====="
                                Escribir "1. Registrar producto"
                                Escribir "2. Buscar producto"
                                Escribir "3. Modificar producto"
                                Escribir "4. Eliminar producto"
                                Escribir "5. Movimiento de stock"
                                Escribir "0. Volver"
                                Leer opcionProductos

                                Segun opcionProductos Hacer
                                    1:
                                        RegistrarProducto(codigosProductos, nombresProductos, preciosProductos, stocksProductos, cantidadProductos, siguienteProducto)
                                    2:
                                        BuscarProducto(codigosProductos, nombresProductos, preciosProductos, stocksProductos, cantidadProductos)
                                    3:
                                        ModificarProducto(codigosProductos, nombresProductos, preciosProductos, stocksProductos, cantidadProductos)
                                    4:
                                        EliminarProducto(codigosProductos, nombresProductos, preciosProductos, stocksProductos, codigosProductosVendidos, cantidadProductos, cantidadVentas)
                                    5:
                                        MovimientoStock(codigosProductos, nombresProductos, stocksProductos, cantidadProductos)
                                    0:
                                        Escribir "Volviendo al menu de Pet Shop..."
                                    De Otro Modo:
                                        Escribir "Opcion no valida."
                                FinSegun
                            Hasta Que opcionProductos = 0

                        2:
                            Repetir
                                Escribir ""
                                Escribir "===== GESTION DE VENTAS ====="
                                Escribir "1. Registrar venta"
                                Escribir "2. Consultar venta por codigo"
                                Escribir "0. Volver"
                                Leer opcionVentas

                                Segun opcionVentas Hacer
                                    1:
                                        RegistrarVenta(codigosProductos, nombresProductos, preciosProductos, stocksProductos, cantidadProductos, codigosVentas, codigosProductosVendidos, nombresProductosVendidos, cantidadesVendidas, preciosUnitariosVentas, totalesVentas, cantidadVentas, siguienteVenta)
                                    2:
                                        ConsultarVenta(codigosVentas, codigosProductosVendidos, nombresProductosVendidos, cantidadesVendidas, preciosUnitariosVentas, totalesVentas, cantidadVentas)
                                    0:
                                        Escribir "Volviendo al menu de Pet Shop..."
                                    De Otro Modo:
                                        Escribir "Opcion no valida."
                                FinSegun
                            Hasta Que opcionVentas = 0

                        0:
                            Escribir "Volviendo al menu principal..."
                        De Otro Modo:
                            Escribir "Opcion no valida."
                    FinSegun
                Hasta Que opcionPetShop = 0

            2:
                ConsultarHistorial(codigosMascotas, nombresMascotas, especiesMascotas, cantidadMascotas, codigosMascotasAtenciones, codigosAtenciones, motivosAtenciones, diagnosticosAtenciones, tratamientosAtenciones, cantidadAtenciones, codigosMascotasServicios, codigosServicios, nombresServicios, cantidadServicios)

            0:
                Escribir "Finalizando el programa..."
            De Otro Modo:
                Escribir "Opcion no valida."
        FinSegun
    Hasta Que opcionPrincipal = 0

    Escribir "Programa finalizado."

FinAlgoritmo


// =====================================================
// 1. REGISTRAR PRODUCTO
// =====================================================

SubProceso RegistrarProducto(codigos, nombres, precios, stocks, cantidad Por Referencia, siguiente Por Referencia)

    Definir nombre Como Cadena
    Definir precio Como Real
    Definir stock Como Entero

    Si cantidad >= 100 Entonces
        Escribir "No hay espacio para registrar mas productos."
    SiNo
        Escribir "Ingrese el nombre del producto:"
        Leer nombre

        Si nombre = "" Entonces
            Escribir "El nombre es obligatorio."
        SiNo
            Escribir "Ingrese el precio:"
            Leer precio
            Escribir "Ingrese el stock inicial:"
            Leer stock

            Si precio <= 0 O stock < 0 Entonces
                Escribir "Precio o stock invalido."
            SiNo
                codigos[cantidad] <- siguiente
                nombres[cantidad] <- nombre
                precios[cantidad] <- precio
                stocks[cantidad] <- stock

                Escribir "Producto registrado correctamente."
                Escribir "Codigo asignado: ", siguiente

                cantidad <- cantidad + 1
                siguiente <- siguiente + 1
            FinSi
        FinSi
    FinSi

FinSubProceso


// =====================================================
// 2. BUSCAR PRODUCTO POR CODIGO O NOMBRE
// =====================================================

SubProceso BuscarProducto(codigos, nombres, precios, stocks, cantidad)

    Definir opcion, codigo, i Como Entero
    Definir nombreBuscado Como Cadena
    Definir encontrado Como Logico

    encontrado <- Falso

    Escribir "1. Buscar por codigo"
    Escribir "2. Buscar por nombre"
    Escribir "Seleccione una opcion:"
    Leer opcion

    Segun opcion Hacer
        1:
            Escribir "Ingrese el codigo del producto:"
            Leer codigo

            Para i <- 0 Hasta cantidad - 1 Hacer
                Si codigos[i] = codigo Entonces
                    MostrarProducto(codigos[i], nombres[i], precios[i], stocks[i])
                    encontrado <- Verdadero
                FinSi
            FinPara

        2:
            Escribir "Ingrese el nombre del producto:"
            Leer nombreBuscado

            Para i <- 0 Hasta cantidad - 1 Hacer
                Si nombres[i] = nombreBuscado Entonces
                    MostrarProducto(codigos[i], nombres[i], precios[i], stocks[i])
                    encontrado <- Verdadero
                FinSi
            FinPara

        De Otro Modo:
            Escribir "Opcion no valida."
    FinSegun

    Si encontrado = Falso Entonces
        Escribir "No se encontro el producto."
    FinSi

FinSubProceso


// =====================================================
// MOSTRAR DATOS DE UN PRODUCTO
// =====================================================

SubProceso MostrarProducto(codigo, nombre, precio, stock)

    Escribir "---------- PRODUCTO ----------"
    Escribir "Codigo: ", codigo
    Escribir "Nombre: ", nombre
    Escribir "Precio: S/ ", precio
    Escribir "Stock: ", stock

FinSubProceso


// =====================================================
// 3. MODIFICAR PRODUCTO
// =====================================================

SubProceso ModificarProducto(codigos, nombres, precios, stocks, cantidad)

    Definir codigo, posicion, i, nuevoStock Como Entero
    Definir nuevoNombre Como Cadena
    Definir nuevoPrecio Como Real

    posicion <- -1

    Escribir "Ingrese el codigo del producto:"
    Leer codigo

    Para i <- 0 Hasta cantidad - 1 Hacer
        Si codigos[i] = codigo Entonces
            posicion <- i
        FinSi
    FinPara

    Si posicion = -1 Entonces
        Escribir "Producto no encontrado."
    SiNo
        MostrarProducto(codigos[posicion], nombres[posicion], precios[posicion], stocks[posicion])

        Escribir "Ingrese el nuevo nombre:"
        Leer nuevoNombre
        Escribir "Ingrese el nuevo precio:"
        Leer nuevoPrecio
        Escribir "Ingrese el nuevo stock:"
        Leer nuevoStock

        Si nuevoNombre = "" O nuevoPrecio <= 0 O nuevoStock < 0 Entonces
            Escribir "Datos invalidos. No se modifico el producto."
        SiNo
            nombres[posicion] <- nuevoNombre
            precios[posicion] <- nuevoPrecio
            stocks[posicion] <- nuevoStock
            Escribir "Producto modificado correctamente."
        FinSi
    FinSi

FinSubProceso


// =====================================================
// 4. ELIMINAR PRODUCTO SIN VENTAS ASOCIADAS
// =====================================================

SubProceso EliminarProducto(codigos, nombres, precios, stocks, codigosVendidos, cantidad Por Referencia, cantidadVentas)

    Definir codigo, posicion, i Como Entero
    Definir tieneVentas Como Logico

    posicion <- -1
    tieneVentas <- Falso

    Escribir "Ingrese el codigo del producto:"
    Leer codigo

    Para i <- 0 Hasta cantidad - 1 Hacer
        Si codigos[i] = codigo Entonces
            posicion <- i
        FinSi
    FinPara

    Si posicion = -1 Entonces
        Escribir "Producto no encontrado."
    SiNo
        Para i <- 0 Hasta cantidadVentas - 1 Hacer
            Si codigosVendidos[i] = codigo Entonces
                tieneVentas <- Verdadero
            FinSi
        FinPara

        Si tieneVentas Entonces
            Escribir "No se puede eliminar: el producto tiene ventas asociadas."
        SiNo
            Para i <- posicion Hasta cantidad - 2 Hacer
                codigos[i] <- codigos[i + 1]
                nombres[i] <- nombres[i + 1]
                precios[i] <- precios[i + 1]
                stocks[i] <- stocks[i + 1]
            FinPara

            cantidad <- cantidad - 1
            Escribir "Producto eliminado correctamente."
        FinSi
    FinSi

FinSubProceso


// =====================================================
// 5. REGISTRAR ENTRADA O SALIDA DE STOCK
// =====================================================

SubProceso MovimientoStock(codigos, nombres, stocks, cantidad)

    Definir codigo, posicion, i, unidades, opcion Como Entero

    posicion <- -1

    Escribir "Ingrese el codigo del producto:"
    Leer codigo

    Para i <- 0 Hasta cantidad - 1 Hacer
        Si codigos[i] = codigo Entonces
            posicion <- i
        FinSi
    FinPara

    Si posicion = -1 Entonces
        Escribir "Producto no encontrado."
    SiNo
        Escribir "Producto: ", nombres[posicion]
        Escribir "Stock actual: ", stocks[posicion]
        Escribir "1. Entrada de stock"
        Escribir "2. Salida de stock"
        Leer opcion

        Si opcion = 1 O opcion = 2 Entonces
            Escribir "Ingrese la cantidad:"
            Leer unidades

            Si unidades <= 0 Entonces
                Escribir "La cantidad debe ser mayor que cero."
            SiNo
                Si opcion = 1 Entonces
                    stocks[posicion] <- stocks[posicion] + unidades
                    Escribir "Entrada de stock registrada."
                SiNo
                    Si unidades > stocks[posicion] Entonces
                        Escribir "Stock insuficiente."
                    SiNo
                        stocks[posicion] <- stocks[posicion] - unidades
                        Escribir "Salida de stock registrada."
                    FinSi
                FinSi

                Escribir "Stock actual: ", stocks[posicion]
            FinSi
        SiNo
            Escribir "Opcion no valida."
        FinSi
    FinSi

FinSubProceso


// =====================================================
// 6. REGISTRAR VENTA Y DESCONTAR STOCK
// =====================================================

SubProceso RegistrarVenta(codigos, nombres, precios, stocks, cantidadProductos, codVentas, codVendidos, nombresVendidos, cantidades, preciosVentas, totales, cantidadVentas Por Referencia, siguiente Por Referencia)

    Definir codigo, posicion, unidades, i Como Entero
    Definir total Como Real
    Definir respuesta Como Cadena

    posicion <- -1

    Si cantidadVentas >= 100 Entonces
        Escribir "No hay espacio para registrar mas ventas."
    SiNo
        Si cantidadProductos = 0 Entonces
            Escribir "No hay productos registrados."
        SiNo
            Escribir "Ingrese el codigo del producto:"
            Leer codigo

            Para i <- 0 Hasta cantidadProductos - 1 Hacer
                Si codigos[i] = codigo Entonces
                    posicion <- i
                FinSi
            FinPara

            Si posicion = -1 Entonces
                Escribir "Producto no encontrado."
            SiNo
                MostrarProducto(codigos[posicion], nombres[posicion], precios[posicion], stocks[posicion])

                Escribir "Ingrese la cantidad a vender:"
                Leer unidades

                Si unidades <= 0 Entonces
                    Escribir "La cantidad debe ser mayor que cero."
                SiNo
                    Si unidades > stocks[posicion] Entonces
                        Escribir "Stock insuficiente."
                    SiNo
                        total <- precios[posicion] * unidades

                        Escribir "========== RESUMEN DE VENTA =========="
                        Escribir "Producto: ", nombres[posicion]
                        Escribir "Cantidad: ", unidades
                        Escribir "Precio unitario: S/ ", precios[posicion]
                        Escribir "Total: S/ ", total
                        Escribir "Confirmar venta? (S/N):"
                        Leer respuesta

                        Si respuesta = "S" O respuesta = "s" Entonces
                            codVentas[cantidadVentas] <- siguiente
                            codVendidos[cantidadVentas] <- codigo
                            nombresVendidos[cantidadVentas] <- nombres[posicion]
                            cantidades[cantidadVentas] <- unidades
                            preciosVentas[cantidadVentas] <- precios[posicion]
                            totales[cantidadVentas] <- total

                            cantidadVentas <- cantidadVentas + 1
                            siguiente <- siguiente + 1
                            stocks[posicion] <- stocks[posicion] - unidades

                            Escribir "Venta registrada correctamente."
                            Escribir "Codigo de venta: ", siguiente - 1
                            Escribir "Stock restante: ", stocks[posicion]
                        SiNo
                            Escribir "Venta cancelada."
                        FinSi
                    FinSi
                FinSi
            FinSi
        FinSi
    FinSi

FinSubProceso


// =====================================================
// 7. CONSULTAR VENTA POR CODIGO
// =====================================================

SubProceso ConsultarVenta(codVentas, codVendidos, nombresVendidos, cantidades, precios, totales, cantidadVentas)

    Definir codigo, posicion, i Como Entero

    posicion <- -1

    Si cantidadVentas = 0 Entonces
        Escribir "No hay ventas registradas."
    SiNo
        Escribir "Ingrese el codigo de venta:"
        Leer codigo

        Para i <- 0 Hasta cantidadVentas - 1 Hacer
            Si codVentas[i] = codigo Entonces
                posicion <- i
            FinSi
        FinPara

        Si posicion = -1 Entonces
            Escribir "Venta no encontrada."
        SiNo
            Escribir "========== DETALLE DE VENTA =========="
            Escribir "Codigo de venta: ", codVentas[posicion]
            Escribir "Codigo de producto: ", codVendidos[posicion]
            Escribir "Producto: ", nombresVendidos[posicion]
            Escribir "Cantidad: ", cantidades[posicion]
            Escribir "Precio unitario: S/ ", precios[posicion]
            Escribir "Total: S/ ", totales[posicion]
        FinSi
    FinSi

FinSubProceso


// =====================================================
// 8. CONSULTAR HISTORIAL CLINICO DE UNA MASCOTA
// =====================================================

SubProceso ConsultarHistorial(codMascotas, nombresMascotas, especies, cantidadMascotas, codMascAtenciones, codAtenciones, motivos, diagnosticos, tratamientos, cantidadAtenciones, codMascServicios, codServicios, nombresServicios, cantidadServicios)

    Definir codigo, posicion, i Como Entero
    Definir totalAtenciones, totalServicios Como Entero

    posicion <- -1
    totalAtenciones <- 0
    totalServicios <- 0

    Si cantidadMascotas = 0 Entonces
        Escribir "No hay mascotas registradas."
    SiNo
        Escribir "Ingrese el codigo de la mascota:"
        Leer codigo

        Para i <- 0 Hasta cantidadMascotas - 1 Hacer
            Si codMascotas[i] = codigo Entonces
                posicion <- i
            FinSi
        FinPara

        Si posicion = -1 Entonces
            Escribir "Mascota no encontrada."
        SiNo
            Escribir "========== DATOS DE LA MASCOTA =========="
            Escribir "Codigo: ", codMascotas[posicion]
            Escribir "Nombre: ", nombresMascotas[posicion]
            Escribir "Especie: ", especies[posicion]

            Escribir "========== ATENCIONES MEDICAS =========="

            Para i <- 0 Hasta cantidadAtenciones - 1 Hacer
                Si codMascAtenciones[i] = codigo Entonces
                    Escribir "Codigo de atencion: ", codAtenciones[i]
                    Escribir "Motivo: ", motivos[i]
                    Escribir "Diagnostico: ", diagnosticos[i]
                    Escribir "Tratamiento: ", tratamientos[i]
                    totalAtenciones <- totalAtenciones + 1
                FinSi
            FinPara

            Si totalAtenciones = 0 Entonces
                Escribir "No tiene atenciones medicas registradas."
            SiNo
                Escribir "Total de atenciones: ", totalAtenciones
            FinSi

            Escribir "========== SERVICIOS VETERINARIOS =========="

            Para i <- 0 Hasta cantidadServicios - 1 Hacer
                Si codMascServicios[i] = codigo Entonces
                    Escribir "Codigo de servicio: ", codServicios[i]
                    Escribir "Servicio: ", nombresServicios[i]
                    totalServicios <- totalServicios + 1
                FinSi
            FinPara

            Si totalServicios = 0 Entonces
                Escribir "No tiene servicios registrados."
            SiNo
                Escribir "Total de servicios: ", totalServicios
            FinSi

            Escribir "========== FIN DEL HISTORIAL CLINICO =========="
        FinSi
    FinSi

FinSubProceso
