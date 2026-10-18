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

// Verifica DD/MM/AAAA, los dias reales del mes y que fecha <= referencia.
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

// ORDEN DE PARAMETROS IGUAL AL DE MOSTRARREGISTROS Y ORDENARREGISTROS.
// Los primeros nueve argumentos son los mismos arreglos del equipo.
SubProceso Registrar(mascotas Por Referencia, propietarios Por Referencia, telefonos Por Referencia, especies Por Referencia, codigosMascotas Por Referencia, codigosPropietarios Por Referencia, tiposAnimales Por Referencia, sexos Por Referencia, fechasNacimiento Por Referencia, cantidad Por Referencia, codigoPropietario Por Referencia, codigoMascota Por Referencia, fechaReferencia)
    Definir nombrePropietario, telefono, nombreMascota Como Cadena
    Definir tipoAnimal, especie, sexo, fechaNacimiento Como Cadena
    Definir valido Como Logico

    Si cantidad >= 100 Entonces
        Escribir "Limite de 100 registros alcanzado."
    SiNo
        Escribir "=== REGISTRO DE PROPIETARIO Y MASCOTA ==="
        Escribir "Nombre del propietario:"
        Leer nombrePropietario

        // Como en C#: el telefono incorrecto se vuelve a solicitar.
        Repetir
            Escribir "Telefono (9 digitos):"
            Leer telefono
            valido <- Longitud(telefono) = 9
            Si valido Entonces
                valido <- SoloDigitos(telefono)
            FinSi
            Si NO valido Entonces
                Escribir "Error: telefono de 9 digitos numericos."
            FinSi
        Hasta Que valido

        Escribir "Nombre de la mascota:"
        Leer nombreMascota
        Escribir "Tipo de animal (Canino/Felino):"
        Leer tipoAnimal
        tipoAnimal <- Mayusculas(tipoAnimal)
        valido <- tipoAnimal = "CANINO" O tipoAnimal = "FELINO"
        Si NO valido Entonces
            Escribir "Error: solo Canino o Felino. Registro cancelado."
        FinSi

        Si valido Entonces
            Escribir "Especie o raza:"
            Leer especie
            Escribir "Sexo (Macho/Hembra):"
            Leer sexo
            sexo <- Mayusculas(sexo)
            valido <- sexo = "MACHO" O sexo = "HEMBRA"
            Si NO valido Entonces
                Escribir "Error: solo Macho o Hembra. Registro cancelado."
            FinSi
        FinSi

        Si valido Entonces
            Escribir "Fecha de nacimiento (DD/MM/AAAA):"
            Leer fechaNacimiento
            valido <- FechaValida(fechaNacimiento, fechaReferencia)
            Si NO valido Entonces
                Escribir "Error: fecha invalida o futura. Registro cancelado."
            FinSi
        FinSi

        // Como en C#: si el dato obligatorio esta vacio, no se guarda.
        Si valido Entonces
            valido <- Longitud(nombrePropietario) > 0 Y Longitud(nombreMascota) > 0 Y Longitud(especie) > 0
            Si NO valido Entonces
                Escribir "Error: todos los campos son obligatorios."
            FinSi
        FinSi

        Si valido Entonces
            // Todos los datos del registro utilizan la MISMA posicion.
            cantidad <- cantidad + 1
            codigosPropietarios[cantidad] <- codigoPropietario
            propietarios[cantidad] <- nombrePropietario
            telefonos[cantidad] <- telefono
            codigosMascotas[cantidad] <- codigoMascota
            mascotas[cantidad] <- nombreMascota
            tiposAnimales[cantidad] <- tipoAnimal
            especies[cantidad] <- especie
            sexos[cantidad] <- sexo
            fechasNacimiento[cantidad] <- fechaNacimiento

            Escribir "REGISTRO EXITOSO"
            Escribir "Codigo propietario: ", codigoPropietario
            Escribir "Propietario: ", nombrePropietario
            Escribir "Telefono: ", telefono
            Escribir "Codigo mascota: ", codigoMascota
            Escribir "Mascota: ", nombreMascota
            Escribir "Tipo: ", tipoAnimal
            Escribir "Especie o raza: ", especie
            Escribir "Sexo: ", sexo
            Escribir "Nacimiento: ", fechaNacimiento

            codigoPropietario <- codigoPropietario + 1
            codigoMascota <- codigoMascota + 1
        FinSi
    FinSi
FinSubProceso

SubProceso MostrarRegistros(mascotas, propietarios, telefonos, especies, codigosMascotas, codigosPropietarios, tiposAnimales, sexos, fechasNacimiento, cantidad)
	Definir i Como Entero

	si cantidad <= 0 Entonces
		Escribir "No hay registros para mostrar."
	sino
		Para i <- 1 Hasta cantidad Con Paso 1 Hacer
			Escribir "Mascota: ", mascotas[i]
			Escribir "Propietario: ", propietarios[i]
			Escribir "Telefono: ", telefonos[i]
			Escribir "Especie: ", especies[i]
			Escribir "Codigo Mascota: ", codigosMascotas[i]
			Escribir "Codigo Propietario: ", codigosPropietarios[i]
			Escribir "Tipo de Animal: ", tiposAnimales[i]
			Escribir "Sexo: ", sexos[i]
			Escribir "Fecha de Nacimiento: ", fechasNacimiento[i]
			Escribir "------------------------------"
		FinPara
	FinSi

FinSubProceso

SubProceso OrdenarRegistros(mascotas, propietarios, telefonos, especies, codigosMascotas, codigosPropietarios, tiposAnimales, sexos, fechasNacimiento, cantidad)
    Definir pasada, j Como Entero
	Definir temporal Como Cadena
	Definir temporalCodigo Como Entero
	Definir temporalCodigoPropietario Como Entero
	Definir temporalPropietario Como Cadena
	Definir temporalTelefono Como Cadena
	Definir temporalEspecie Como Cadena
	Definir temporalTipoAnimal Como Cadena
	Definir temporalSexo Como Cadena
	Definir temporalFechaNacimiento Como Cadena

    Para pasada <- 1 Hasta cantidad - 1 Con Paso 1 Hacer
		Para j <- 1 Hasta cantidad - pasada Con Paso 1 Hacer
			Si Mayusculas(mascotas[j]) > Mayusculas(mascotas[j + 1]) Entonces
				temporal <- mascotas[j]
				mascotas[j] <- mascotas[j + 1]
				mascotas[j + 1] <- temporal
				temporalCodigo <- codigosMascotas[j]
				codigosMascotas[j] <- codigosMascotas[j + 1]
				codigosMascotas[j + 1] <- temporalCodigo
				temporalCodigoPropietario <- codigosPropietarios[j]
				codigosPropietarios[j] <- codigosPropietarios[j + 1]
				codigosPropietarios[j + 1] <- temporalCodigoPropietario
				temporalPropietario <- propietarios[j]
				propietarios[j] <- propietarios[j + 1]
				propietarios[j + 1] <- temporalPropietario
				temporalTelefono <- telefonos[j]
				telefonos[j] <- telefonos[j + 1]
				telefonos[j + 1] <- temporalTelefono
				temporalEspecie <- especies[j]
				especies[j] <- especies[j + 1]
				especies[j + 1] <- temporalEspecie
				temporalTipoAnimal <- tiposAnimales[j]
				tiposAnimales[j] <- tiposAnimales[j + 1]
				tiposAnimales[j + 1] <- temporalTipoAnimal
				temporalSexo <- sexos[j]
				sexos[j] <- sexos[j + 1]
				sexos[j + 1] <- temporalSexo
				temporalFechaNacimiento <- fechasNacimiento[j]
				fechasNacimiento[j] <- fechasNacimiento[j + 1]
				fechasNacimiento[j + 1] <- temporalFechaNacimiento
			FinSi
		FinPara
    FinPara
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
    Definir fechaReferencia Como Cadena
    Definir fechaCorrecta Como Logico

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
    correlativoPropietario <- 2001
    correlativoMascota <- 1001

    Repetir
    Escribir "Ingrese la fecha actual (DD/MM/AAAA):"
    Leer fechaReferencia

    fechaCorrecta <- FechaValida(fechaReferencia, "31/12/9999")

        Si NO fechaCorrecta Entonces
                Escribir "Fecha incorrecta."
        FinSi
    Hasta Que fechaCorrecta

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
        1:
            Registrar(mascotas, propietarios, telefonos, especies, codigosMascotas, codigosPropietarios, tiposAnimales, sexos, fechasNacimiento, cantidad, correlativoPropietario, correlativoMascota, fechaReferencia)
        5:
            MostrarRegistros(mascotas, propietarios, telefonos, especies, codigosMascotas, codigosPropietarios, tiposAnimales, sexos, fechasNacimiento, cantidad)

        6:
            OrdenarRegistros(mascotas, propietarios, telefonos, especies, codigosMascotas, codigosPropietarios, tiposAnimales, sexos, fechasNacimiento, cantidad)

            Escribir "=== REGISTROS ORDENADOS ==="

            MostrarRegistros(mascotas, propietarios, telefonos, especies, codigosMascotas, codigosPropietarios, tiposAnimales, sexos, fechasNacimiento, cantidad)

        7:
            Escribir "Saliendo del sistema..."

        De Otro Modo:
            Escribir "Opcion pendiente de implementar."
    FinSegun

    Hasta Que opcion = 7

FinAlgoritmo