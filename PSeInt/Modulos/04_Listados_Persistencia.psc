Algoritmo EF_Listados_Persistencia
    Escribir "Modulo de listados y persistencia: pendiente de implementar."
    // ============================================================================
// PROYECTO: VETERINARIA "ENTRE PATAS ARIAS"
// INTEGRANTE 4 (PIERO) - MODULOS 7 Y 8: LISTADOS, ORDENAMIENTO Y PERSISTENCIA
// ARCHIVO: 04_Listados_Persistencia.psc
// ============================================================================

Algoritmo ListadosYPersistenciaVeterinaria
    Definir CAP Como Entero
    CAP <- 50

    // --- ARREGLOS PARALELOS EN MEMORIA PRINCIPAL ---
    // 1. Propietarios
    Dimension codProp[50], nomProp[50], dniProp[50], telProp[50]
    Definir codProp, totalProp, sigCodProp Como Entero
    Definir nomProp, dniProp, telProp Como Caracter

    // 2. Mascotas (codPropMasc enlaza con codProp)
    Dimension codMasc[50], nomMasc[50], espMasc[50], sexoMasc[50], codPropMasc[50]
    Definir codMasc, codPropMasc, totalMasc, sigCodMasc Como Entero
    Definir nomMasc, espMasc, sexoMasc Como Caracter

    // 3. Atenciones medicas (codMascAten enlaza con codMasc)
    Dimension codAten[50], codMascAten[50], motivoAten[50], costoAten[50]
    Definir codAten, codMascAten, totalAten, sigCodAten Como Entero
    Definir motivoAten Como Caracter
    Definir costoAten Como Real

    // 4. Servicios veterinarios (codMascServ enlaza con codMasc)
    Dimension codServ[50], codMascServ[50], nomServ[50], costoServ[50]
    Definir codServ, codMascServ, totalServ, sigCodServ Como Entero
    Definir nomServ Como Caracter
    Definir costoServ Como Real

    // --- ARREGLOS DE RESPALDO (SIMULACION PERSISTENCIA CSV / BINARIO) ---
    Dimension bakCodProp[50], bakNomProp[50], bakDniProp[50], bakTelProp[50]
    Definir bakCodProp, bakTotalProp Como Entero
    Definir bakNomProp, bakDniProp, bakTelProp Como Caracter

    Dimension bakCodMasc[50], bakNomMasc[50], bakEspMasc[50], bakSexoMasc[50], bakCodPropMasc[50]
    Definir bakCodMasc, bakCodPropMasc, bakTotalMasc Como Entero
    Definir bakNomMasc, bakEspMasc, bakSexoMasc Como Caracter

    Dimension bakCodAten[50], bakCodMascAten[50], bakMotivoAten[50], bakCostoAten[50]
    Definir bakCodAten, bakCodMascAten, bakTotalAten Como Entero
    Definir bakMotivoAten Como Caracter
    Definir bakCostoAten Como Real

    Dimension bakCodServ[50], bakCodMascServ[50], bakNomServ[50], bakCostoServ[50]
    Definir bakCodServ, bakCodMascServ, bakTotalServ Como Entero
    Definir bakNomServ Como Caracter
    Definir bakCostoServ Como Real

    Definir existeRespaldo Como Logico
    Definir ultimaOperacion, pausa, nuevoNomMasc, nuevaEsp, nuevoSexo, confirmar Como Caracter
    Definir opcion, i, duenoSel, maxProp, maxMasc Como Entero

    existeRespaldo <- Falso
    bakTotalProp <- 0
    bakTotalMasc <- 0
    bakTotalAten <- 0
    bakTotalServ <- 0
    ultimaOperacion <- "Sin operaciones de persistencia en esta sesion."

    // --- CARGA INICIAL DE DATOS DE PRUEBA ---
    totalProp <- 2
    codProp[1] <- 2001
    nomProp[1] <- "Ana Torres"
    dniProp[1] <- "71234561"
    telProp[1] <- "987654321"

    codProp[2] <- 2002
    nomProp[2] <- "Luis Vega"
    dniProp[2] <- "72345672"
    telProp[2] <- "912345678"
    sigCodProp <- 2003

    // Mascotas cargadas desordenadas alfabeticamente para probar ordenamiento
    totalMasc <- 3
    codMasc[1] <- 1001
    nomMasc[1] <- "Zorro"
    espMasc[1] <- "Canino"
    sexoMasc[1] <- "Macho"
    codPropMasc[1] <- 2001

    codMasc[2] <- 1002
    nomMasc[2] <- "Boby"
    espMasc[2] <- "Canino"
    sexoMasc[2] <- "Macho"
    codPropMasc[2] <- 2002

    codMasc[3] <- 1003
    nomMasc[3] <- "Max"
    espMasc[3] <- "Felino"
    sexoMasc[3] <- "Macho"
    codPropMasc[3] <- 2001
    sigCodMasc <- 1004

    // Atencion vinculada a mascota 1002 (Boby)
    totalAten <- 1
    codAten[1] <- 1
    codMascAten[1] <- 1002
    motivoAten[1] <- "Control de vacunacion anual"
    costoAten[1] <- 65.00
    sigCodAten <- 2

    // Servicio vinculado a mascota 1002 (Boby)
    totalServ <- 1
    codServ[1] <- 1
    codMascServ[1] <- 1002
    nomServ[1] <- "Bano medicado y corte"
    costoServ[1] <- 45.00
    sigCodServ <- 2

    Repetir
        Escribir ""
        Escribir "======================================================="
        Escribir "   VETERINARIA ENTRE PATAS ARIAS - MODULOS 7 Y 8"
        Escribir "======================================================="
        Escribir " [MODULO 7: LISTADOS Y ORDENAMIENTOS]"
        Escribir "  1. Listar propietarios"
        Escribir "  2. Listar mascotas (con propietario vinculado)"
        Escribir "  3. Listar atenciones medicas"
        Escribir "  4. Listar servicios veterinarios"
        Escribir "  5. Ordenar mascotas por codigo (Ascendente)"
        Escribir "  6. Ordenar mascotas por nombre (Alfabetico)"
        Escribir "-------------------------------------------------------"
        Escribir " [MODULO 8: PERSISTENCIA Y RESPALDOS (SIMULACION)]"
        Escribir "  7. Guardar respaldo de datos (Exportar CSV/Binario)"
        Escribir "  8. Restaurar datos desde respaldo"
        Escribir "  9. Consultar estado de memoria y respaldo"
        Escribir " 10. Prueba rapida: Vaciar memoria / Registrar mascota"
        Escribir "  0. Salir"
        Escribir "======================================================="
        Escribir Sin Saltar "Seleccione una opcion: "
        Leer opcion

        Segun opcion Hacer
            1:
                ListarPropietarios(codProp, nomProp, dniProp, telProp, totalProp)
            2:
                ListarMascotas(codMasc, nomMasc, espMasc, sexoMasc, codPropMasc, totalMasc, codProp, nomProp, totalProp)
            3:
                ListarAtenciones(codAten, codMascAten, motivoAten, costoAten, totalAten, codMasc, nomMasc, totalMasc)
            4:
                ListarServicios(codServ, codMascServ, nomServ, costoServ, totalServ, codMasc, nomMasc, totalMasc)
            5:
                OrdenarMascotasPorCodigo(codMasc, nomMasc, espMasc, sexoMasc, codPropMasc, totalMasc)
            6:
                OrdenarMascotasPorNombre(codMasc, nomMasc, espMasc, sexoMasc, codPropMasc, totalMasc)
            7:
                // Guardar respaldo atomico en arreglos paralelos de respaldo
                Si totalProp = 0 Y totalMasc = 0 Y totalAten = 0 Y totalServ = 0 Entonces
                    Escribir "Error: No hay registros en memoria para guardar."
                SiNo
                    bakTotalProp <- totalProp
                    Para i <- 1 Hasta totalProp Con Paso 1 Hacer
                        bakCodProp[i] <- codProp[i]
                        bakNomProp[i] <- nomProp[i]
                        bakDniProp[i] <- dniProp[i]
                        bakTelProp[i] <- telProp[i]
                    FinPara

                    bakTotalMasc <- totalMasc
                    Para i <- 1 Hasta totalMasc Con Paso 1 Hacer
                        bakCodMasc[i] <- codMasc[i]
                        bakNomMasc[i] <- nomMasc[i]
                        bakEspMasc[i] <- espMasc[i]
                        bakSexoMasc[i] <- sexoMasc[i]
                        bakCodPropMasc[i] <- codPropMasc[i]
                    FinPara

                    bakTotalAten <- totalAten
                    Para i <- 1 Hasta totalAten Con Paso 1 Hacer
                        bakCodAten[i] <- codAten[i]
                        bakCodMascAten[i] <- codMascAten[i]
                        bakMotivoAten[i] <- motivoAten[i]
                        bakCostoAten[i] <- costoAten[i]
                    FinPara

                    bakTotalServ <- totalServ
                    Para i <- 1 Hasta totalServ Con Paso 1 Hacer
                        bakCodServ[i] <- codServ[i]
                        bakCodMascServ[i] <- codMascServ[i]
                        bakNomServ[i] <- nomServ[i]
                        bakCostoServ[i] <- costoServ[i]
                    FinPara

                    existeRespaldo <- Verdadero
                    ultimaOperacion <- "Respaldo guardado correctamente (" + ConvertirATexto(totalProp) + " prop., " + ConvertirATexto(totalMasc) + " masc., " + ConvertirATexto(totalAten) + " aten., " + ConvertirATexto(totalServ) + " serv.)."
                    Escribir ""
                    Escribir "Exito: Datos guardados en el respaldo persistente."
                FinSi
            8:
                // Restaurar desde respaldo con validacion previa
                Si No existeRespaldo Entonces
                    Escribir ""
                    Escribir "Error: No existe ningun respaldo guardado. Memoria sin cambios."
                SiNo
                    Escribir Sin Saltar "Se reemplazaran los datos actuales en memoria. Confirmar (S/N)? "
                    Leer confirmar
                    Si Mayusculas(confirmar) = "S" Entonces
                        totalProp <- bakTotalProp
                        maxProp <- 2000
                        Para i <- 1 Hasta totalProp Con Paso 1 Hacer
                            codProp[i] <- bakCodProp[i]
                            nomProp[i] <- bakNomProp[i]
                            dniProp[i] <- bakDniProp[i]
                            telProp[i] <- bakTelProp[i]
                            Si codProp[i] > maxProp Entonces
                                maxProp <- codProp[i]
                            FinSi
                        FinPara
                        sigCodProp <- maxProp + 1

                        totalMasc <- bakTotalMasc
                        maxMasc <- 1000
                        Para i <- 1 Hasta totalMasc Con Paso 1 Hacer
                            codMasc[i] <- bakCodMasc[i]
                            nomMasc[i] <- bakNomMasc[i]
                            espMasc[i] <- bakEspMasc[i]
                            sexoMasc[i] <- bakSexoMasc[i]
                            codPropMasc[i] <- bakCodPropMasc[i]
                            Si codMasc[i] > maxMasc Entonces
                                maxMasc <- codMasc[i]
                            FinSi
                        FinPara
                        sigCodMasc <- maxMasc + 1

                        totalAten <- bakTotalAten
                        Para i <- 1 Hasta totalAten Con Paso 1 Hacer
                            codAten[i] <- bakCodAten[i]
                            codMascAten[i] <- bakCodMascAten[i]
                            motivoAten[i] <- bakMotivoAten[i]
                            costoAten[i] <- bakCostoAten[i]
                        FinPara

                        totalServ <- bakTotalServ
                        Para i <- 1 Hasta totalServ Con Paso 1 Hacer
                            codServ[i] <- bakCodServ[i]
                            codMascServ[i] <- bakCodMascServ[i]
                            nomServ[i] <- bakNomServ[i]
                            costoServ[i] <- bakCostoServ[i]
                        FinPara

                        ultimaOperacion <- "Respaldo restaurado (" + ConvertirATexto(totalProp) + " prop., " + ConvertirATexto(totalMasc) + " masc.)."
                        Escribir "Exito: Datos restaurados. Proximo cod. propietario: ", sigCodProp, " | Proximo cod. mascota: ", sigCodMasc
                    SiNo
                        Escribir "Restauracion cancelada. Los datos actuales se conservaron."
                    FinSi
                FinSi
            9:
                Escribir ""
                Escribir "======================================================="
                Escribir "          ESTADO DE MEMORIA Y PERSISTENCIA"
                Escribir "======================================================="
                Escribir "Propietarios en memoria : ", totalProp, " (proximo codigo: ", sigCodProp, ")"
                Escribir "Mascotas en memoria     : ", totalMasc, " (proximo codigo: ", sigCodMasc, ")"
                Escribir "Atenciones en memoria   : ", totalAten
                Escribir "Servicios en memoria    : ", totalServ
                Escribir "-------------------------------------------------------"
                Si existeRespaldo Entonces
                    Escribir "Estado de respaldo      : DISPONIBLE (", bakTotalProp, " prop., ", bakTotalMasc, " masc., ", bakTotalAten, " aten., ", bakTotalServ, " serv.)"
                SiNo
                    Escribir "Estado de respaldo      : NO GENERADO"
                FinSi
                Escribir "Ultima operacion        : ", ultimaOperacion
            10:
                Escribir ""
                Escribir "1. Simular cierre de sesion (vaciar memoria actual)"
                Escribir "2. Registrar mascota nueva (probar continuidad de codigo)"
                Escribir Sin Saltar "Elija subopcion: "
                Leer i
                Si i = 1 Entonces
                    totalProp <- 0
                    totalMasc <- 0
                    totalAten <- 0
                    totalServ <- 0
                    sigCodProp <- 2001
                    sigCodMasc <- 1001
                    Escribir "Memoria vaciada (simula reinicio del sistema)."
                SiNo
                    Si i = 2 Entonces
                        Si totalProp = 0 Entonces
                            Escribir "Error: Debe existir al menos un propietario para registrar mascota."
                        SiNo
                            Si totalMasc >= CAP Entonces
                                Escribir "Error: Capacidad maxima alcanzada."
                            SiNo
                                Escribir Sin Saltar "Nombre de la mascota: "
                                Leer nuevoNomMasc
                                Escribir Sin Saltar "Especie (Canino/Felino): "
                                Leer nuevaEsp
                                Escribir Sin Saltar "Sexo (Macho/Hembra): "
                                Leer nuevoSexo
                                duenoSel <- codProp[1]
                                totalMasc <- totalMasc + 1
                                codMasc[totalMasc] <- sigCodMasc
                                nomMasc[totalMasc] <- nuevoNomMasc
                                espMasc[totalMasc] <- nuevaEsp
                                sexoMasc[totalMasc] <- nuevoSexo
                                codPropMasc[totalMasc] <- duenoSel
                                Escribir "Mascota registrada con codigo automatico: ", sigCodMasc, " (Propietario: ", duenoSel, ")"
                                sigCodMasc <- sigCodMasc + 1
                            FinSi
                        FinSi
                    SiNo
                        Escribir "Subopcion no valida."
                    FinSi
                FinSi
            0:
                Escribir "Saliendo de los modulos de Listados y Persistencia..."
            De Otro Modo:
                Escribir "Opcion no valida. Intente nuevamente."
        FinSegun

        Si opcion <> 0 Entonces
            Escribir ""
            Escribir Sin Saltar "Presione ENTER para continuar..."
            Leer pausa
        FinSi
    Hasta Que opcion = 0
FinAlgoritmo

// ============================================================================
// SUBPROCESOS DEL MODULO 7: LISTADOS Y ORDENAMIENTOS
// ============================================================================

SubProceso ListarPropietarios(codProp, nomProp, dniProp, telProp, totalProp)
    Definir i Como Entero
    Escribir ""
    Escribir "======================================================="
    Escribir "               LISTADO DE PROPIETARIOS"
    Escribir "======================================================="
    Si totalProp = 0 Entonces
        Escribir "No hay propietarios registrados en memoria."
    SiNo
        Escribir "COD  | NOMBRE               | DNI      | TELEFONO"
        Escribir "-------------------------------------------------------"
        Para i <- 1 Hasta totalProp Con Paso 1 Hacer
            Escribir codProp[i], " | ", nomProp[i], " | ", dniProp[i], " | ", telProp[i]
        FinPara
        Escribir "-------------------------------------------------------"
        Escribir "Total de propietarios: ", totalProp
    FinSi
FinSubProceso

SubProceso ListarMascotas(codMasc, nomMasc, espMasc, sexoMasc, codPropMasc, totalMasc, codProp, nomProp, totalProp)
    Definir i, j Como Entero
    Definir nombreDueno Como Caracter
    Escribir ""
    Escribir "======================================================="
    Escribir "                 LISTADO DE MASCOTAS"
    Escribir "======================================================="
    Si totalMasc = 0 Entonces
        Escribir "No hay mascotas registradas en memoria."
    SiNo
        Escribir "COD  | MASCOTA     | ESPECIE | SEXO   | PROPIETARIO"
        Escribir "-------------------------------------------------------"
        Para i <- 1 Hasta totalMasc Con Paso 1 Hacer
            nombreDueno <- "No encontrado"
            Para j <- 1 Hasta totalProp Con Paso 1 Hacer
                Si codProp[j] = codPropMasc[i] Entonces
                    nombreDueno <- nomProp[j] + " (" + ConvertirATexto(codProp[j]) + ")"
                FinSi
            FinPara
            Escribir codMasc[i], " | ", nomMasc[i], " | ", espMasc[i], " | ", sexoMasc[i], " | ", nombreDueno
        FinPara
        Escribir "-------------------------------------------------------"
        Escribir "Total de mascotas: ", totalMasc
    FinSi
FinSubProceso

SubProceso ListarAtenciones(codAten, codMascAten, motivoAten, costoAten, totalAten, codMasc, nomMasc, totalMasc)
    Definir i, j Como Entero
    Definir nombreMascota Como Caracter
    Escribir ""
    Escribir "======================================================="
    Escribir "             LISTADO DE ATENCIONES MEDICAS"
    Escribir "======================================================="
    Si totalAten = 0 Entonces
        Escribir "No hay atenciones medicas registradas."
    SiNo
        Escribir "ID | MASCOTA          | MOTIVO                      | COSTO"
        Escribir "-------------------------------------------------------"
        Para i <- 1 Hasta totalAten Con Paso 1 Hacer
            nombreMascota <- "Desconocida"
            Para j <- 1 Hasta totalMasc Con Paso 1 Hacer
                Si codMasc[j] = codMascAten[i] Entonces
                    nombreMascota <- nomMasc[j] + " (" + ConvertirATexto(codMasc[j]) + ")"
                FinSi
            FinPara
            Escribir codAten[i], "  | ", nombreMascota, " | ", motivoAten[i], " | S/ ", costoAten[i]
        FinPara
        Escribir "-------------------------------------------------------"
        Escribir "Total de atenciones: ", totalAten
    FinSi
FinSubProceso

SubProceso ListarServicios(codServ, codMascServ, nomServ, costoServ, totalServ, codMasc, nomMasc, totalMasc)
    Definir i, j Como Entero
    Definir nombreMascota Como Caracter
    Escribir ""
    Escribir "======================================================="
    Escribir "           LISTADO DE SERVICIOS VETERINARIOS"
    Escribir "======================================================="
    Si totalServ = 0 Entonces
        Escribir "No hay servicios veterinarios registrados."
    SiNo
        Escribir "ID | MASCOTA          | SERVICIO                    | COSTO"
        Escribir "-------------------------------------------------------"
        Para i <- 1 Hasta totalServ Con Paso 1 Hacer
            nombreMascota <- "Desconocida"
            Para j <- 1 Hasta totalMasc Con Paso 1 Hacer
                Si codMasc[j] = codMascServ[i] Entonces
                    nombreMascota <- nomMasc[j] + " (" + ConvertirATexto(codMasc[j]) + ")"
                FinSi
            FinPara
            Escribir codServ[i], "  | ", nombreMascota, " | ", nomServ[i], " | S/ ", costoServ[i]
        FinPara
        Escribir "-------------------------------------------------------"
        Escribir "Total de servicios: ", totalServ
    FinSi
FinSubProceso

SubProceso OrdenarMascotasPorCodigo(codMasc, nomMasc, espMasc, sexoMasc, codPropMasc, totalMasc)
    Definir i, j, auxCod, auxCodProp Como Entero
    Definir auxNom, auxEsp, auxSexo Como Caracter

    Si totalMasc < 2 Entonces
        Escribir "Se necesitan al menos 2 mascotas para ordenar."
    SiNo
        Para i <- 1 Hasta totalMasc - 1 Con Paso 1 Hacer
            Para j <- 1 Hasta totalMasc - i Con Paso 1 Hacer
                Si codMasc[j] > codMasc[j + 1] Entonces
                    auxCod <- codMasc[j]
                    codMasc[j] <- codMasc[j + 1]
                    codMasc[j + 1] <- auxCod

                    auxNom <- nomMasc[j]
                    nomMasc[j] <- nomMasc[j + 1]
                    nomMasc[j + 1] <- auxNom

                    auxEsp <- espMasc[j]
                    espMasc[j] <- espMasc[j + 1]
                    espMasc[j + 1] <- auxEsp

                    auxSexo <- sexoMasc[j]
                    sexoMasc[j] <- sexoMasc[j + 1]
                    sexoMasc[j + 1] <- auxSexo

                    auxCodProp <- codPropMasc[j]
                    codPropMasc[j] <- codPropMasc[j + 1]
                    codPropMasc[j + 1] <- auxCodProp
                FinSi
            FinPara
        FinPara
        Escribir "Mascotas ordenadas por codigo ascendente conservando sus propietarios."
    FinSi
FinSubProceso

SubProceso OrdenarMascotasPorNombre(codMasc, nomMasc, espMasc, sexoMasc, codPropMasc, totalMasc)
    Definir i, j, auxCod, auxCodProp Como Entero
    Definir auxNom, auxEsp, auxSexo Como Caracter

    Si totalMasc < 2 Entonces
        Escribir "Se necesitan al menos 2 mascotas para ordenar."
    SiNo
        Para i <- 1 Hasta totalMasc - 1 Con Paso 1 Hacer
            Para j <- 1 Hasta totalMasc - i Con Paso 1 Hacer
                Si Mayusculas(nomMasc[j]) > Mayusculas(nomMasc[j + 1]) Entonces
                    auxCod <- codMasc[j]
                    codMasc[j] <- codMasc[j + 1]
                    codMasc[j + 1] <- auxCod

                    auxNom <- nomMasc[j]
                    nomMasc[j] <- nomMasc[j + 1]
                    nomMasc[j + 1] <- auxNom

                    auxEsp <- espMasc[j]
                    espMasc[j] <- espMasc[j + 1]
                    espMasc[j + 1] <- auxEsp

                    auxSexo <- sexoMasc[j]
                    sexoMasc[j] <- sexoMasc[j + 1]
                    sexoMasc[j + 1] <- auxSexo

                    auxCodProp <- codPropMasc[j]
                    codPropMasc[j] <- codPropMasc[j + 1]
                    codPropMasc[j + 1] <- auxCodProp
                FinSi
            FinPara
        FinPara
        Escribir "Mascotas ordenadas alfabeticamente por nombre conservando sus propietarios."
    FinSi
FinSubProceso
FinAlgoritmo
