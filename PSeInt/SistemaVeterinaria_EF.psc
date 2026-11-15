
SubProceso MostrarMenuPrincipal
    Escribir ""
    Escribir "========================================"
    Escribir "      VETERINARIA ENTRE PATAS - ARIAS"
    Escribir "========================================"
    Escribir "1. Gestionar propietarios"
    Escribir "2. Gestionar mascotas"
    Escribir "3. Atenciones medicas"
    Escribir "4. Servicios veterinarios"
    Escribir "5. Historial de mascotas"
    Escribir "6. Gestion de productos"
    Escribir "7. Listados y ordenamientos"
    Escribir "8. Administracion de registros"
    Escribir "9. Salir"
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
                    Escribir "2. Modificar atencion"
                    Escribir "3. Anular atencion"
                    cantidadOpciones <- 3

                SiNo
                    Si opcionPrincipal = 4 Entonces
                        Escribir "          SERVICIOS VETERINARIOS"
                        Escribir "========================================"
                        Escribir "1. Registrar servicio"
                        Escribir "2. Modificar servicio"
                        Escribir "3. Anular servicio"
                        cantidadOpciones <- 3

                    SiNo
                        Si opcionPrincipal = 5 Entonces
                            Escribir "          HISTORIAL DE MASCOTAS"
                            Escribir "========================================"
                            Escribir "1. Consultar historial completo por mascota"
                            cantidadOpciones <- 1

                        SiNo
                            Si opcionPrincipal = 6 Entonces
                                Escribir "          GESTION DE PRODUCTOS"
                                Escribir "========================================"
                                Escribir "1. Registrar producto"
                                Escribir "2. Buscar producto"
                                Escribir "3. Modificar producto"
                                Escribir "4. Eliminar producto"
                                Escribir "5. Registrar movimiento de stock"
                                cantidadOpciones <- 5

                            SiNo
                                Si opcionPrincipal = 7 Entonces
                                    Escribir "          LISTADOS Y ORDENAMIENTOS"
                                    Escribir "========================================"
                                    Escribir "1. Listar propietarios"
                                    Escribir "2. Listar mascotas"
                                    Escribir "3. Listar atenciones medicas"
                                    Escribir "4. Listar servicios veterinarios"
                                    Escribir "5. Listar productos"
                                    Escribir "6. Ordenar mascotas por codigo"
                                    Escribir "7. Ordenar mascotas por nombre"
                                    cantidadOpciones <- 7

                                SiNo
                                    Escribir "          ADMINISTRACION DE REGISTROS"
                                    Escribir "========================================"
                                    Escribir "1. Guardar datos en CSV"
                                    Escribir "2. Crear respaldo binario"
                                    Escribir "3. Restaurar respaldo binario"
                                    Escribir "4. Consultar estado de carga"
                                    cantidadOpciones <- 4
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
            Escribir "Funcionalidad pendiente de implementar."
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
