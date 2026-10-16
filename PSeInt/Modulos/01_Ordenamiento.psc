SubProceso MostrarRegistros(mascotas, propietarios, telefonos, especies, codigosMascotas, codigosPropietarios, cantidad)
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

	Dimension telefonos[101]
	Dimension especies[101]
	Dimension mascotas[101]
	Dimension propietarios[101]
	Dimension codigosMascotas[101]
	Dimension codigosPropietarios[101]

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
	especies[1] <- "Perro"
	especies[2] <- "Gato"
	especies[3] <- "Pajaro"

	//CODIGO MASCOTAS
	codigosMascotas[1] <- 1001
	codigosMascotas[2] <- 1002
	codigosMascotas[3] <- 1003

	//CODIGO PROPIETARIOS
	codigosPropietarios[1] <- 2001
	codigosPropietarios[2] <- 2002
	codigosPropietarios[3] <- 2003

	MostrarRegistros(mascotas, propietarios, telefonos, especies, codigosMascotas, codigosPropietarios, cantidad)

FinAlgoritmo