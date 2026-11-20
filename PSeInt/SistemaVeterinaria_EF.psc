
SubProceso MostrarMenuPrincipal
    Escribir ""
    Escribir "========================================"
    Escribir "      VETERINARIA ENTRE PATAS - ARIAS"
    Escribir "========================================"
    Escribir "1. Gestionar propietarios"
    Escribir "2. Gestionar mascotas"
    Escribir "3. Atenciones medicas"
    Escribir "4. Servicios veterinarios"
    Escribir "5. Pet Shop"
    Escribir "6. Historial clinico de mascotas"
    Escribir "7. Listados y ordenamientos"
    Escribir "8. Administracion de registros"
    Escribir "9. Salir"
FinSubProceso

// SUBMENUS INTERNOS: GESTION DE PRODUCTOS Y VENTAS
SubProceso MostrarSubmenuPetShop(opcionPetShop)
    Definir opcionInterna, cantidadOpciones Como Entero

    Repetir
        Escribir ""
        Escribir "========================================"
        Si opcionPetShop = 1 Entonces
            Escribir "          GESTION DE PRODUCTOS"
            Escribir "========================================"
            Escribir "1. Registrar producto"
            Escribir "2. Buscar producto por codigo o nombre"
            Escribir "3. Modificar producto"
            Escribir "4. Eliminar producto"
            Escribir "5. Registrar movimiento de stock"
            cantidadOpciones <- 5
        SiNo
            Escribir "          GESTION DE VENTAS"
            Escribir "========================================"
            Escribir "1. Registrar venta"
            Escribir "2. Consultar venta por codigo"
            cantidadOpciones <- 2
        FinSi

        Escribir "0. Volver a Pet Shop"
        Escribir "Seleccione una opcion:"
        Leer opcionInterna

        Si opcionInterna >= 1 Y opcionInterna <= cantidadOpciones Entonces
            Escribir "Funcionalidad pendiente de implementar."
        SiNo
            Si opcionInterna <> 0 Entonces
                Escribir "Opcion no valida."
            FinSi
        FinSi
    Hasta Que opcionInterna = 0
FinSubProceso

SubProceso MostrarSubmenu(opcionPrincipal)
    Definir opcionSubmenu, cantidadOpciones Como Entero

    Repetir
        Escribir ""
        Escribir "========================================"

        Si opcionPrincipal = 1 Entonces
            Escribir "          GESTIONAR PROPIETARIOS"
            Escribir "========================================"
            Escribir "1. Registrar propietario"
            Escribir "2. Buscar propietario por codigo"
            Escribir "3. Modificar propietario"
            Escribir "4. Eliminar propietario"
            Escribir "5. Ver mascotas asociadas"
            cantidadOpciones <- 5

        SiNo
            Si opcionPrincipal = 2 Entonces
                Escribir "          GESTIONAR MASCOTAS"
                Escribir "========================================"
                Escribir "1. Registrar mascota"
                Escribir "2. Buscar mascota por codigo o nombre"
                Escribir "3. Modificar mascota"
                Escribir "4. Eliminar mascota"
                cantidadOpciones <- 4

            SiNo
                Si opcionPrincipal = 3 Entonces
                    Escribir "          ATENCIONES MEDICAS"
                    Escribir "========================================"
                    Escribir "1. Registrar atencion"
                    Escribir "2. Buscar atencion por codigo"
                    Escribir "3. Modificar atencion"
                    Escribir "4. Anular atencion"
                    cantidadOpciones <- 4

                SiNo
                    Si opcionPrincipal = 4 Entonces
                        Escribir "          SERVICIOS VETERINARIOS"
                        Escribir "========================================"
                        Escribir "1. Registrar servicio"
                        Escribir "2. Buscar servicio por codigo"
                        Escribir "3. Modificar servicio"
                        Escribir "4. Anular servicio"
                        cantidadOpciones <- 4

                    SiNo
                        Si opcionPrincipal = 5 Entonces
                            Escribir "          PET SHOP"
                            Escribir "========================================"
                            Escribir "1. Gestion de productos"
                            Escribir "2. Gestion de ventas"
                            cantidadOpciones <- 2

                        SiNo
                            Si opcionPrincipal = 6 Entonces
                                Escribir "          HISTORIAL CLINICO DE MASCOTAS"
                                Escribir "========================================"
                                Escribir "1. Consultar historial completo por mascota"
                                cantidadOpciones <- 1

                            SiNo
                                Si opcionPrincipal = 7 Entonces
                                    Escribir "          LISTADOS Y ORDENAMIENTOS"
                                    Escribir "========================================"
                                    Escribir "1. Listar propietarios"
                                    Escribir "2. Listar mascotas"
                                    Escribir "3. Listar atenciones medicas"
                                    Escribir "4. Listar servicios veterinarios"
                                    Escribir "5. Listar productos del Pet Shop"
                                    Escribir "6. Listar ventas del Pet Shop"
                                    Escribir "7. Ordenar mascotas por codigo"
                                    Escribir "8. Ordenar mascotas por nombre"
                                    cantidadOpciones <- 8

                                SiNo
                                    Escribir "          ADMINISTRACION DE REGISTROS"
                                    Escribir "========================================"
                                    Escribir "1. Guardar datos en CSV"
                                    Escribir "2. Cargar datos desde CSV"
                                    Escribir "3. Crear respaldo binario"
                                    Escribir "4. Restaurar respaldo binario"
                                    Escribir "5. Consultar estado de carga"
                                    cantidadOpciones <- 5
                                FinSi
                            FinSi
                        FinSi
                    FinSi
                FinSi
            FinSi
        FinSi

        Escribir "0. Volver al menu principal"
        Escribir "Seleccione una opcion:"
        Leer opcionSubmenu

        Si opcionSubmenu >= 1 Y opcionSubmenu <= cantidadOpciones Entonces
            Si opcionPrincipal = 5 Entonces
                MostrarSubmenuPetShop(opcionSubmenu)
            SiNo
                Escribir "Funcionalidad pendiente de implementar."
            FinSi
        SiNo
            Si opcionSubmenu <> 0 Entonces
                Escribir "Opcion no valida."
            FinSi
        FinSi

    Hasta Que opcionSubmenu = 0
FinSubProceso

Algoritmo SistemaVeterinaria_EF

    //VARIABLES NUMERICAS
    Definir cantidad, correlativoPropietario, correlativoMascota Como Entero
    Definir opcion Como Entero

    //CODIGOS
    Definir codigosPropietarios, codigosMascotas Como Entero

    //DATOS DE PROPIETARIOS Y MASCOTAS
    Definir propietarios, telefonos Como Cadena
    Definir mascotas, especies Como Cadena
    Definir tiposAnimales, sexos, fechasNacimiento Como Cadena

    //ARREGLOS COMPARTIDOS
    Dimension codigosPropietarios[101]
    Dimension codigosMascotas[101]
    Dimension propietarios[101]
    Dimension telefonos[101]
    Dimension mascotas[101]
    Dimension especies[101]
    Dimension tiposAnimales[101]
    Dimension sexos[101]
    Dimension fechasNacimiento[101]

    //VALORES INICIALES
    cantidad <- 0
    correlativoPropietario <- 0
    correlativoMascota <- 0

    //NAVEGACION PRINCIPAL
    Repetir
        MostrarMenuPrincipal

        Escribir "Seleccione una opcion:"
        Leer opcion

        Si opcion >= 1 Y opcion <= 8 Entonces
            MostrarSubmenu(opcion)
        SiNo
            Si opcion = 9 Entonces
                Escribir "Saliendo del programa..."
            SiNo
                Escribir "Opcion no valida."
            FinSi
        FinSi

    Hasta Que opcion = 9

    Escribir "Programa finalizado."

FinAlgoritmo
