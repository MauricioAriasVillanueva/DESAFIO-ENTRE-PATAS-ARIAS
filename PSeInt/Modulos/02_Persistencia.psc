
SubProceso MostrarMascotasCSV(codigosMascotas, codigosPropietarios, mascotas, especies, tiposAnimales, sexos, fechasNacimiento, cantidadMascotas)
	Definir i Como Entero

	//MOSTRAR MASCOTAS EN FORMATO CSV
	Escribir "----Mascota----"
	Escribir "CodigoMascota,CodigoPropietario,NombreMascota,Especie,TipoAnimal,Sexo,FechaDeNacimiento"
		Para i <- 1 Hasta cantidadMascotas Con Paso 1 Hacer
			Escribir codigosMascotas[i],",",codigosPropietarios[i],",",mascotas[i],",",especies[i],",",tiposAnimales[i],",",sexos[i],",",fechasNacimiento[i]
		FinPara
FinSubProceso

SubProceso MostrarPropietariosCSV(codigosPropietarios, propietarios, telefonos, cantidadPropietarios)
	Definir j Como Entero

	//MOSTRAR PROPIETARIOS EN FORMATO CSV
	Escribir "----Propietario----"
	Escribir "CodigoPropietario,Nombre,Telefono"
		Para j <- 1 Hasta cantidadPropietarios Con Paso 1 Hacer
			Escribir codigosPropietarios[j],",",propietarios[j],",",telefonos[j]
		FinPara
FinSubProceso

Algoritmo PruebaPersistencia
	Definir cantidadMascotas Como Entero
	Definir cantidadPropietarios Como Entero
	Definir codigosPropietarios Como Entero
	Definir propietarios, telefonos Como Cadena
	Definir mascotas, especies, tiposAnimales, sexos, fechasNacimiento Como Cadena
	Definir codigosMascotas Como Entero
	Definir datosValidos como Logico
	Definir i Como Entero
	Definir j Como Entero
	Definir codigoMascota, codigoPropietario Como Entero

	Dimension codigosPropietarios[101];
	Dimension propietarios[101];
	Dimension telefonos[101];
	Dimension mascotas[101];
	Dimension especies[101];
	Dimension tiposAnimales[101];
	Dimension sexos[101];
	Dimension fechasNacimiento[101];
	Dimension codigosMascotas[101];

	CantidadMascotas <- 3

	//MASCOTAS
	mascotas[1] <- "Zeus"
    mascotas[2] <- "Firulais"
    mascotas[3] <- "Scott"

	//ESPECIES
	especies[1] <- "Labrador"
	especies[2] <- "Siames"
	especies[3] <- "Beagle"

	//CODIGO MASCOTAS
	codigosMascotas[1] <- 1001
	codigosMascotas[2] <- 1002
	codigosMascotas[3] <- 1003

	// TIPO DE ANIMAL
	tiposAnimales[1] <- "Canino"
	tiposAnimales[2] <- "Felino"
	tiposAnimales[3] <- "Canino"

	// SEXO
	sexos[1] <- "Macho"
	sexos[2] <- "Hembra"
	sexos[3] <- "Macho"

	// FECHA DE NACIMIENTO
	fechasNacimiento[1] <- "2024-03-15"
	fechasNacimiento[2] <- "2023-06-10"
	fechasNacimiento[3] <- "2025-01-20"

	cantidadPropietarios <- 3

	//CODIGO PROPIETARIOS
	codigosPropietarios[1] <- 2001
	codigosPropietarios[2] <- 2002
	codigosPropietarios[3] <- 2003

	//PROPIETARIOS
	propietarios[1] <- "Juan"
	propietarios[2] <- "Maria"
	propietarios[3] <- "Pedro"

	//TELEFONOS
	telefonos[1] <- "999111222"
	telefonos[2] <- "999333444"
	telefonos[3] <- "999555666"

	//INICIALIZAR CODIGOS Y ESTADO DE VALIDACION
	codigoMascota <- 1001
	codigoPropietario <- 2001
	datosValidos <- Verdadero

	//VALIDAR CANTIDAD DE REGISTROS
	Si (cantidadMascotas <> cantidadPropietarios) Entonces
		datosValidos <- Falso
		Escribir "ERROR: La cantidad de mascotas y propietarios no coincide."
	FinSi

	//VALIDAR CAMPOS VACIOS DE MASCOTAS
	Para i <- 1 Hasta cantidadMascotas Con Paso 1 Hacer
		Si mascotas[i] = "" O especies[i] = "" O tiposAnimales[i] = "" O sexos[i] = "" O fechasNacimiento[i] = "" Entonces
			datosValidos <- Falso
			Escribir "ERROR: Los datos de la mascota en la posicion ", i, " no son validos."
		FinSi

		//VALIDAR FORMATO DE FECHA
		Si Longitud(fechasNacimiento[i]) <> 10 Entonces
			datosValidos <- Falso
			Escribir "ERROR: La fecha de nacimiento de la mascota en la posicion ", i, " no tiene el formato correcto (YYYY-MM-DD)."
		SiNo
			Si Subcadena(fechasNacimiento[i], 5, 5) <> "-" O Subcadena(fechasNacimiento[i], 8, 8) <> "-" Entonces
				datosValidos <- Falso
				Escribir "ERROR: La fecha de nacimiento de la mascota en la posicion ", i, " no tiene el formato correcto (YYYY-MM-DD)."
				FinSi
		FinSi
	FinPara

	//VALIDAR CAMPOS VACIOS DE PROPIETARIOS
	Para j <- 1 Hasta cantidadPropietarios Con Paso 1 Hacer
		Si propietarios[j] = "" O telefonos[j] = "" Entonces
			datosValidos <- Falso
			Escribir "ERROR: Los datos del propietario en la posicion ", j, " no son validos."
		FinSi
	FinPara

	//COMPROBAR SI LOS DATOS SON VALIDOS
	Si (datosValidos) Entonces
		Escribir "Los datos son validos."

		//ACTUALIZAR CODIGO AUTOMATICO DE MASCOTA
		Para i <- 1 Hasta cantidadMascotas Con Paso 1 Hacer
			Si codigosMascotas[i] >= codigoMascota Entonces
				codigoMascota <- codigosMascotas[i] + 1
			FinSi
		FinPara

		Escribir "Siguiente codigo de mascota: ", codigoMascota

		//ACTUALIZAR CODIGO AUTOMATICO DE PROPIETARIO
		Para j <- 1 Hasta cantidadPropietarios Con Paso 1 Hacer
			Si codigosPropietarios[j] >= codigoPropietario Entonces
				codigoPropietario <- codigosPropietarios[j] + 1
			FinSi
		FinPara

		Escribir "Siguiente codigo de propietario: ", codigoPropietario

		// Mostrar los datos en formato CSV
		MostrarPropietariosCSV(codigosPropietarios, propietarios, telefonos, cantidadPropietarios)
		MostrarMascotasCSV(codigosMascotas, codigosPropietarios, mascotas, especies, tiposAnimales, sexos, fechasNacimiento, cantidadMascotas)
	SiNo
		Escribir "Los datos no son validos."
	FinSi

FinAlgoritmo
