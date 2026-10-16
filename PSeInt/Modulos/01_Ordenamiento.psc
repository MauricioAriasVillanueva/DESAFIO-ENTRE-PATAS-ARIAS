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

	MostrarRegistros(mascotas, propietarios, telefonos, especies, codigosMascotas, codigosPropietarios, tiposAnimales, sexos, fechasNacimiento, cantidad)

FinAlgoritmo