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

Algoritmo PruebaListado

	Definir cantidad como Entero
	Definir mascotas como Cadena
	Definir propietarios como Cadena
	Definir telefonos, especies Como Cadena
	Definir codigosMascotas, codigosPropietarios Como Entero
	Definir tiposAnimales, sexos, fechasNacimiento Como Cadena

	Dimension telefonos[101]
	Dimension especies[101]
	Dimension mascotas[101]
	Dimension propietarios[101]
	Dimension codigosMascotas[101]
	Dimension codigosPropietarios[101]
	Dimension tiposAnimales[101]
	Dimension sexos[101]
	Dimension fechasNacimiento[101]

	cantidad <- 3

	//MASCOTAS
	mascotas[1] <- "Zeus"
    mascotas[2] <- "Firulais"
    mascotas[3] <- "Bobby"

	//PROPIETARIOS
	propietarios[1] <- "Juan"
	propietarios[2] <- "Maria"
	propietarios[3] <- "Pedro"

	//TELEFONOS
	telefonos[1] <- "999111222"
	telefonos[2] <- "999333444"
	telefonos[3] <- "999555666"

	//ESPECIES
especies[1] <- "Labrador"
especies[2] <- "Siames"
especies[3] <- "Beagle"

	//CODIGO MASCOTAS
	codigosMascotas[1] <- 1001
	codigosMascotas[2] <- 1002
	codigosMascotas[3] <- 1003

	//CODIGO PROPIETARIOS
	codigosPropietarios[1] <- 2001
	codigosPropietarios[2] <- 2002
	codigosPropietarios[3] <- 2003

	// TIPO DE ANIMAL
	tiposAnimales[1] <- "Canino"
	tiposAnimales[2] <- "Felino"
	tiposAnimales[3] <- "Canino"

	// SEXO
	sexos[1] <- "Macho"
	sexos[2] <- "Hembra"
	sexos[3] <- "Macho"

	// FECHA DE NACIMIENTO
	fechasNacimiento[1] <- "15/03/2024"
	fechasNacimiento[2] <- "10/06/2023"
	fechasNacimiento[3] <- "20/01/2025"

	Escribir "=== ANTES DE ORDENAR ==="
	MostrarRegistros(mascotas, propietarios, telefonos, especies, codigosMascotas, codigosPropietarios, tiposAnimales, sexos, fechasNacimiento, cantidad)

	OrdenarRegistros(mascotas, propietarios, telefonos, especies, codigosMascotas, codigosPropietarios, tiposAnimales, sexos, fechasNacimiento, cantidad)

	Escribir "=== DESPUES DE ORDENAR ==="
	MostrarRegistros(mascotas, propietarios, telefonos, especies, codigosMascotas, codigosPropietarios, tiposAnimales, sexos, fechasNacimiento, cantidad)

FinAlgoritmo