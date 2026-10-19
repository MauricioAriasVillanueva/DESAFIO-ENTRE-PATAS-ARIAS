SubProceso BuscarMascota(cantidad, codigosPropietarios, propietarios, telefonos, codigosMascotas, mascotas, especies, tiposAnimales, sexos, fechasNacimiento)
    Definir codigoBuscado, posicion, i Como Entero
    Definir entrada, caracter Como Cadena
    Definir j Como Entero
    Definir entradaValida Como Logico

    Si cantidad = 0 Entonces
        Escribir "No hay mascotas registradas."
    SiNo
        Escribir "Ingrese el codigo de la mascota:"
        Leer entrada

        entradaValida <- Verdadero
        Si Longitud(entrada) = 0 Entonces
            entradaValida <- Falso
        FinSi
        Para j <- 0 Hasta Longitud(entrada) - 1 Hacer
            caracter <- Subcadena(entrada, j, j)
            Si caracter <> "0" Y caracter <> "1" Y caracter <> "2" Y caracter <> "3" Y caracter <> "4" Y caracter <> "5" Y caracter <> "6" Y caracter <> "7" Y caracter <> "8" Y caracter <> "9" Entonces
                entradaValida <- Falso
            FinSi
        FinPara

        Si NO entradaValida Entonces
            Escribir "Error: ingrese un codigo entero positivo, solo digitos."
        SiNo
                codigoBuscado <- ConvertirANumero(entrada)
                posicion <- -1
                i <- 1
                Mientras i <= cantidad Y posicion = -1 Hacer
                    Si codigosMascotas[i] = codigoBuscado Entonces
                        posicion <- i
                    FinSi
                    i <- i + 1
                FinMientras

                Si posicion = -1 Entonces
                    Escribir "Mascota no encontrada."
                SiNo
                    Escribir "========== MASCOTA ENCONTRADA =========="
                    Escribir "Codigo del propietario: ", codigosPropietarios[posicion]
                    Escribir "Propietario: ", propietarios[posicion]
                    Escribir "Telefono: ", telefonos[posicion]
                    Escribir "Codigo de mascota: ", codigosMascotas[posicion]
                    Escribir "Mascota: ", mascotas[posicion]
                    Escribir "Especie: ", especies[posicion]
                    Escribir "Tipo de animal: ", tiposAnimales[posicion]
                    Escribir "Sexo: ", sexos[posicion]
                    Escribir "Fecha de nacimiento: ", fechasNacimiento[posicion]
                FinSi
        FinSi
    FinSi
FinSubProceso

Algoritmo SistemaVeterinaria_EP

    // Variables numericas
    Definir cantidad Como Entero
    Definir correlativoPropietario Como Entero
    Definir correlativoMascota Como Entero
    Definir opcion Como Entero

    // Codigos
    Definir codigosPropietarios Como Entero
    Definir codigosMascotas Como Entero

    // Datos de propietarios y mascotas
    Definir propietarios, telefonos Como Cadena
    Definir mascotas, especies Como Cadena
    Definir tiposAnimales, sexos, fechasNacimiento Como Cadena

    // Arreglos compartidos
    Dimension codigosPropietarios[101]
    Dimension codigosMascotas[101]
    Dimension propietarios[101]
    Dimension telefonos[101]
    Dimension mascotas[101]
    Dimension especies[101]
    Dimension tiposAnimales[101]
    Dimension sexos[101]
    Dimension fechasNacimiento[101]

    // Valores iniciales
    cantidad <- 0
    correlativoPropietario <- 0
    correlativoMascota <- 0

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

    Si opcion = 2 Entonces
        BuscarMascota(cantidad, codigosPropietarios, propietarios, telefonos, codigosMascotas, mascotas, especies, tiposAnimales, sexos, fechasNacimiento)
    FinSi

    Hasta Que opcion = 7

FinAlgoritmo