// PRUEBA INDEPENDIENTE DEL MODULO DE PIERO - ANALOGO A
// PSeInt/Modulos/01_Ordenamiento.psc DE INTEGRANTE-1.
// Abrir este archivo por separado en PSeInt, no pegarlo junto con el archivo principal.
// Nueve arreglos compartidos. Funciones/subprocesos primero; algoritmo principal abajo.

// FUNCIONES DE APOYO COMPARTIDAS DEL SISTEMA (VALIDACION).
// Se incluyen para probar los subprocesos de Piero de forma independiente.
Funcion correcto <- SoloDigitos(texto)
    Definir i Como Entero
    Definir letra Como Cadena

    correcto <- Verdadero
    Si Longitud(texto) = 0 Entonces
        correcto <- Falso
    FinSi

    Para i <- 1 Hasta Longitud(texto) Hacer
        letra <- Subcadena(texto, i, i)
        Si letra < "0" O letra > "9" Entonces
            correcto <- Falso
        FinSi
    FinPara
FinFuncion

// Verifica DD/MM/AAAA, dia real del mes y fecha no posterior a la referencia.
Funcion correcto <- FechaValida(fecha, referencia)
    Definir dia, mes, anio, diasMes Como Entero
    Definir diaRef, mesRef, anioRef Como Entero
    Definir numeros Como Cadena

    correcto <- Falso
    Si Longitud(fecha) = 10 Y Longitud(referencia) = 10 Entonces
        Si Subcadena(fecha, 3, 3) = "/" Y Subcadena(fecha, 6, 6) = "/" Entonces
            numeros <- Concatenar(Subcadena(fecha, 1, 2), Subcadena(fecha, 4, 5))
            numeros <- Concatenar(numeros, Subcadena(fecha, 7, 10))
            Si SoloDigitos(numeros) Entonces
                dia <- ConvertirANumero(Subcadena(fecha, 1, 2))
                mes <- ConvertirANumero(Subcadena(fecha, 4, 5))
                anio <- ConvertirANumero(Subcadena(fecha, 7, 10))

                diaRef <- ConvertirANumero(Subcadena(referencia, 1, 2))
                mesRef <- ConvertirANumero(Subcadena(referencia, 4, 5))
                anioRef <- ConvertirANumero(Subcadena(referencia, 7, 10))

                Si anio >= 1 Y mes >= 1 Y mes <= 12 Y dia >= 1 Entonces
                    diasMes <- 31
                    Si mes = 4 O mes = 6 O mes = 9 O mes = 11 Entonces
                        diasMes <- 30
                    FinSi
                    Si mes = 2 Entonces
                        diasMes <- 28
                        Si anio MOD 400 = 0 O (anio MOD 4 = 0 Y anio MOD 100 <> 0) Entonces
                            diasMes <- 29
                        FinSi
                    FinSi
                    Si dia <= diasMes Entonces
                        Si anio < anioRef O (anio = anioRef Y mes < mesRef) O (anio = anioRef Y mes = mesRef Y dia <= diaRef) Entonces
                            correcto <- Verdadero
                        FinSi
                    FinSi
                FinSi
            FinSi
        FinSi
    FinSi
FinFuncion

// INTEGRANTE 3 - MODIFICAR
// Se modifica un dato por vez, igual que en C#.
SubProceso ModificarMascota(mascotas Por Referencia, propietarios Por Referencia, telefonos Por Referencia, especies Por Referencia, codigosMascotas Por Referencia, codigosPropietarios Por Referencia, tiposAnimales Por Referencia, sexos Por Referencia, fechasNacimiento Por Referencia, cantidad, fechaReferencia)
    Definir codigoTexto, dato Como Cadena
    Definir codigo, posicion, i, campo Como Entero
    Definir valido Como Logico

    Si cantidad = 0 Entonces
        Escribir "No hay mascotas registradas."
    SiNo
        Escribir "Codigo de mascota:"
        Leer codigoTexto
        Si SoloDigitos(codigoTexto) Entonces
            codigo <- ConvertirANumero(codigoTexto)
            posicion <- 0
            Para i <- 1 Hasta cantidad Con Paso 1 Hacer
                Si codigosMascotas[i] = codigo Entonces
                    posicion <- i
                FinSi
            FinPara

            Si posicion = 0 Entonces
                Escribir "No se encontro la mascota."
            SiNo
                Escribir "Mascota encontrada: ", mascotas[posicion]
                Escribir "1. Nombre mascota"
                Escribir "2. Nombre propietario"
                Escribir "3. Telefono"
                Escribir "4. Tipo de animal"
                Escribir "5. Especie o raza"
                Escribir "6. Sexo"
                Escribir "7. Fecha de nacimiento"
                Escribir "Que dato desea modificar?:"
                Leer campo

                Si campo < 1 O campo > 7 Entonces
                    Escribir "Opcion invalida."
                SiNo
                    Escribir "Ingrese el nuevo dato:"
                    Leer dato
                    valido <- Longitud(dato) > 0

                    // Validamos solamente el campo seleccionado.
                    Si campo = 3 Entonces
                        Si Longitud(dato) <> 9 Entonces
                            valido <- Falso
                        SiNo
                            Si NO SoloDigitos(dato) Entonces
                                valido <- Falso
                            FinSi
                        FinSi
                    FinSi
                    Si campo = 4 Entonces
                        dato <- Mayusculas(dato)
                        Si dato <> "CANINO" Y dato <> "FELINO" Entonces
                            valido <- Falso
                        FinSi
                    FinSi
                    Si campo = 6 Entonces
                        dato <- Mayusculas(dato)
                        Si dato <> "MACHO" Y dato <> "HEMBRA" Entonces
                            valido <- Falso
                        FinSi
                    FinSi
                    Si campo = 7 Entonces
                        Si NO FechaValida(dato, fechaReferencia) Entonces
                            valido <- Falso
                        FinSi
                    FinSi

                    Si valido Entonces
                        Segun campo Hacer
                            1:
                                mascotas[posicion] <- dato
                            2:
                                propietarios[posicion] <- dato
                            3:
                                telefonos[posicion] <- dato
                            4:
                                tiposAnimales[posicion] <- dato
                            5:
                                especies[posicion] <- dato
                            6:
                                sexos[posicion] <- dato
                            7:
                                fechasNacimiento[posicion] <- dato
                        FinSegun
                        Escribir "Dato modificado correctamente."
                    SiNo
                        Escribir "Dato invalido. No se modifico el registro."
                    FinSi
                FinSi
            FinSi
        SiNo
            Escribir "Ingrese un codigo numerico."
        FinSi
    FinSi
FinSubProceso

// INTEGRANTE 3 - ELIMINAR
SubProceso EliminarMascota(mascotas Por Referencia, propietarios Por Referencia, telefonos Por Referencia, especies Por Referencia, codigosMascotas Por Referencia, codigosPropietarios Por Referencia, tiposAnimales Por Referencia, sexos Por Referencia, fechasNacimiento Por Referencia, cantidad Por Referencia)
    Definir codigoTexto, respuesta Como Cadena
    Definir codigo, posicion, i, j Como Entero

    Si cantidad = 0 Entonces
        Escribir "No hay mascotas registradas."
    SiNo
        Escribir "Codigo de mascota a eliminar:"
        Leer codigoTexto
        Si SoloDigitos(codigoTexto) Entonces
            codigo <- ConvertirANumero(codigoTexto)
            posicion <- 0
            Para i <- 1 Hasta cantidad Con Paso 1 Hacer
                Si codigosMascotas[i] = codigo Entonces
                    posicion <- i
                FinSi
            FinPara

            Si posicion = 0 Entonces
                Escribir "No se encontro la mascota."
            SiNo
                Escribir "Mascota: ", mascotas[posicion]
                Escribir "Seguro que desea eliminarla? (S/N):"
                Leer respuesta
                Si Mayusculas(respuesta) = "S" O Mayusculas(respuesta) = "SI" Entonces
                    // Pasamos las nueve listas una posicion hacia la izquierda.
                    Para j <- posicion Hasta cantidad - 1 Con Paso 1 Hacer
                        mascotas[j] <- mascotas[j + 1]
                        propietarios[j] <- propietarios[j + 1]
                        telefonos[j] <- telefonos[j + 1]
                        especies[j] <- especies[j + 1]
                        codigosMascotas[j] <- codigosMascotas[j + 1]
                        codigosPropietarios[j] <- codigosPropietarios[j + 1]
                        tiposAnimales[j] <- tiposAnimales[j + 1]
                        sexos[j] <- sexos[j + 1]
                        fechasNacimiento[j] <- fechasNacimiento[j + 1]
                    FinPara
                    cantidad <- cantidad - 1
                    Escribir "Mascota eliminada correctamente."
                SiNo
                    Escribir "Eliminacion cancelada."
                FinSi
            FinSi
        SiNo
            Escribir "Ingrese un codigo numerico."
        FinSi
    FinSi
FinSubProceso

// MOSTRAR PARA PRUEBAS SOLAMENTE. No es el modulo oficial de listado del integrante 1.
SubProceso MostrarDatosPrueba(mascotas, propietarios, telefonos, especies, codigosMascotas, codigosPropietarios, tiposAnimales, sexos, fechasNacimiento, cantidad)
    Definir i Como Entero
    Si cantidad = 0 Entonces
        Escribir "No hay registros."
    SiNo
        Para i <- 1 Hasta cantidad Con Paso 1 Hacer
            Escribir "------------------------------"
            Escribir "Mascota: ", mascotas[i]
            Escribir "Codigo mascota: ", codigosMascotas[i]
            Escribir "Propietario: ", propietarios[i]
            Escribir "Codigo propietario: ", codigosPropietarios[i]
            Escribir "Telefono: ", telefonos[i]
            Escribir "Especie o raza: ", especies[i]
            Escribir "Tipo de animal: ", tiposAnimales[i]
            Escribir "Sexo: ", sexos[i]
            Escribir "Fecha: ", fechasNacimiento[i]
        FinPara
    FinSi
FinSubProceso

Algoritmo PruebaModificarEliminar
    // Variables numericas de la base colaborativa
    Definir cantidad, correlativoPropietario, correlativoMascota, opcion Como Entero
    Definir codigosPropietarios, codigosMascotas Como Entero

    // Datos de propietarios y mascotas de la base colaborativa
    Definir propietarios, telefonos, mascotas, especies Como Cadena
    Definir tiposAnimales, sexos, fechasNacimiento Como Cadena
    Definir fechaReferencia Como Cadena
    Definir fechaCorrecta Como Logico

    // Los nueve arreglos usados por todo el equipo
    Dimension codigosPropietarios[101]
    Dimension codigosMascotas[101]
    Dimension propietarios[101]
    Dimension telefonos[101]
    Dimension mascotas[101]
    Dimension especies[101]
    Dimension tiposAnimales[101]
    Dimension sexos[101]
    Dimension fechasNacimiento[101]

    cantidad <- 0
    correlativoPropietario <- 2001
    correlativoMascota <- 1001

    Repetir
        Escribir "Ingrese la fecha actual (DD/MM/AAAA):"
        Leer fechaReferencia
        fechaCorrecta <- FechaValida(fechaReferencia, "31/12/9999")
        Si NO fechaCorrecta Entonces
            Escribir "Fecha invalida. Intente nuevamente."
        FinSi
    Hasta Que fechaCorrecta

    // DATOS DE PRUEBA. Corresponden al ejemplo del integrante 1.
    // Quitar este bloque cuando se integre el modulo Registrar del equipo.
    cantidad <- 3
    mascotas[1] <- "Zeus"
    mascotas[2] <- "Firulais"
    mascotas[3] <- "Bobby"
    propietarios[1] <- "Juan"
    propietarios[2] <- "Maria"
    propietarios[3] <- "Pedro"
    telefonos[1] <- "999111222"
    telefonos[2] <- "999333444"
    telefonos[3] <- "999555666"
    especies[1] <- "Labrador"
    especies[2] <- "Siames"
    especies[3] <- "Beagle"
    codigosMascotas[1] <- 1001
    codigosMascotas[2] <- 1002
    codigosMascotas[3] <- 1003
    codigosPropietarios[1] <- 2001
    codigosPropietarios[2] <- 2002
    codigosPropietarios[3] <- 2003
    tiposAnimales[1] <- "Canino"
    tiposAnimales[2] <- "Felino"
    tiposAnimales[3] <- "Canino"
    sexos[1] <- "Macho"
    sexos[2] <- "Hembra"
    sexos[3] <- "Macho"
    fechasNacimiento[1] <- "15/03/2024"
    fechasNacimiento[2] <- "10/06/2023"
    fechasNacimiento[3] <- "20/01/2025"
    // Fin de los datos de prueba.

    Repetir
        Escribir "================================"
        Escribir " VETERINARIA ENTRE PATAS - ARIAS"
        Escribir "================================"
        Escribir "1. Registrar propietario y mascota"
        Escribir "2. Buscar mascota"
        Escribir "3. Modificar registro"
        Escribir "4. Eliminar registro"
        Escribir "5. Mostrar registros"
        Escribir "6. Ordenar registros"
        Escribir "7. Salir"
        Escribir "Seleccione una opcion:"
        Leer opcion

        Segun opcion Hacer
            3:
                ModificarMascota(mascotas, propietarios, telefonos, especies, codigosMascotas, codigosPropietarios, tiposAnimales, sexos, fechasNacimiento, cantidad, fechaReferencia)
            4:
                EliminarMascota(mascotas, propietarios, telefonos, especies, codigosMascotas, codigosPropietarios, tiposAnimales, sexos, fechasNacimiento, cantidad)
            5:
                // SOLO PARA PROBAR; listado oficial corresponde a Integrante-1.
                MostrarDatosPrueba(mascotas, propietarios, telefonos, especies, codigosMascotas, codigosPropietarios, tiposAnimales, sexos, fechasNacimiento, cantidad)
            7:
                Escribir "Saliendo del sistema..."
            De Otro Modo:
                Escribir "Opcion pendiente de integrar o invalida."
        FinSegun
    Hasta Que opcion = 7
FinAlgoritmo
