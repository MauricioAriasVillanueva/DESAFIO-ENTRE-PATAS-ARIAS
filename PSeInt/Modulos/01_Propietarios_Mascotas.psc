Funcion valido <- ValidarDigitos(texto, cantidad)
    Definir i Como Entero
    Definir caracter Como Cadena

    valido <- Longitud(texto) = cantidad

    Si valido Entonces
        Para i <- 1 Hasta cantidad Hacer
            caracter <- SubCadena(texto, i, i)

            Si caracter <> "0" Y caracter <> "1" Y caracter <> "2" Y caracter <> "3" Y caracter <> "4" Y caracter <> "5" Y caracter <> "6" Y caracter <> "7" Y caracter <> "8" Y caracter <> "9" Entonces
                valido <- Falso
            FinSi
        FinPara
    FinSi
FinFuncion


Funcion valido <- ValidarCorreo(correo)
    Definir i, arrobas, posicionArroba, posicionPunto Como Entero
    Definir caracter Como Cadena
    Definir tieneEspacio Como Logico

    arrobas <- 0
    posicionArroba <- 0
    posicionPunto <- 0
    tieneEspacio <- Falso

    Si Longitud(correo) > 0 Entonces
        Para i <- 1 Hasta Longitud(correo) Hacer
            caracter <- SubCadena(correo, i, i)

            Si caracter = "@" Entonces
                arrobas <- arrobas + 1
                posicionArroba <- i
            FinSi

            Si caracter = "." Entonces
                posicionPunto <- i
            FinSi

            Si caracter = " " Entonces
                tieneEspacio <- Verdadero
            FinSi
        FinPara
    FinSi

    valido <- arrobas = 1 Y posicionArroba > 1 Y posicionPunto > posicionArroba + 1 Y posicionPunto < Longitud(correo) Y tieneEspacio = Falso
FinFuncion

// RECHAZAR CADENAS VACIAS O FORMADAS SOLO POR ESPACIOS
Funcion vacio <- TextoVacio(texto)
    Definir i Como Entero
    vacio <- Verdadero
    Si Longitud(texto) > 0 Entonces
        Para i <- 1 Hasta Longitud(texto) Hacer
            Si SubCadena(texto, i, i) <> " " Entonces
                vacio <- Falso
            FinSi
        FinPara
    FinSi
FinFuncion

// FECHAS REALES, AÑOS BISIESTOS Y FECHAS NO FUTURAS
Funcion valido <- ValidarFechaNacimiento(fecha)
    Definir dia, mes, anio, diasMes, fechaNumero Como Entero
    valido <- Falso
    Si Longitud(fecha) = 10 Entonces
        Si SubCadena(fecha, 3, 3) = "/" Y SubCadena(fecha, 6, 6) = "/" Entonces
            Si ValidarDigitos(SubCadena(fecha, 1, 2), 2) Y ValidarDigitos(SubCadena(fecha, 4, 5), 2) Y ValidarDigitos(SubCadena(fecha, 7, 10), 4) Entonces
                dia <- ConvertirANumero(SubCadena(fecha, 1, 2))
                mes <- ConvertirANumero(SubCadena(fecha, 4, 5))
                anio <- ConvertirANumero(SubCadena(fecha, 7, 10))
                Si anio >= 1 Y mes >= 1 Y mes <= 12 Entonces
                    diasMes <- 31
                    Si mes = 4 O mes = 6 O mes = 9 O mes = 11 Entonces
                        diasMes <- 30
                    FinSi
                    Si mes = 2 Entonces
                        diasMes <- 28
                        Si (anio MOD 4 = 0 Y anio MOD 100 <> 0) O anio MOD 400 = 0 Entonces
                            diasMes <- 29
                        FinSi
                    FinSi
                    fechaNumero <- anio * 10000 + mes * 100 + dia
                    Si dia >= 1 Y dia <= diasMes Y fechaNumero <= FechaActual() Entonces
                        valido <- Verdadero
                    FinSi
                FinSi
            FinSi
        FinSi
    FinSi
FinFuncion

Funcion tipo <- ObtenerTipoAnimal(entrada)
    tipo <- ""
    Si Mayusculas(entrada) = "CANINO" Entonces
        tipo <- "Canino"
    SiNo
        Si Mayusculas(entrada) = "FELINO" Entonces
            tipo <- "Felino"
        FinSi
    FinSi
FinFuncion

Funcion sexoCorrecto <- ObtenerSexo(entrada)
    sexoCorrecto <- ""
    Si Mayusculas(entrada) = "MACHO" Entonces
        sexoCorrecto <- "Macho"
    SiNo
        Si Mayusculas(entrada) = "HEMBRA" Entonces
            sexoCorrecto <- "Hembra"
        FinSi
    FinSi
FinFuncion

Funcion posicion <- BuscarPosicionMascota(codigosMascotas, cantidadMascotas, codigoBuscado)
    Definir i Como Entero
    posicion <- -1
    Si cantidadMascotas > 0 Entonces
        Para i <- 1 Hasta cantidadMascotas Hacer
            Si codigosMascotas[i] = codigoBuscado Entonces
                posicion <- i
            FinSi
        FinPara
    FinSi
FinFuncion

SubProceso MostrarDatosMascota(codigosMascotas, mascotas, tiposAnimales, especies, sexos, fechasNacimiento, codigosPropietariosMascotas, codigosPropietarios, propietarios, cantidadPropietarios, posicion)
    Definir codigoDueno, posicionDueno Como Entero
    Escribir "------------------------------"
    Escribir "Codigo mascota: ", codigosMascotas[posicion]
    Escribir "Nombre: ", mascotas[posicion]
    Escribir "Tipo: ", tiposAnimales[posicion]
    Escribir "Especie o raza: ", especies[posicion]
    Escribir "Sexo: ", sexos[posicion]
    Escribir "Nacimiento: ", fechasNacimiento[posicion]
    codigoDueno <- codigosPropietariosMascotas[posicion]
    posicionDueno <- BuscarPosicionPropietario(codigosPropietarios, cantidadPropietarios, codigoDueno)
    Escribir "Codigo propietario: ", codigoDueno
    Si posicionDueno <> -1 Entonces
        Escribir "Propietario: ", propietarios[posicionDueno]
    FinSi
FinSubProceso

// EL PROPIETARIO PUEDE REGISTRAR VARIAS MASCOTAS
SubProceso RegistrarMascota(codigosPropietarios, propietarios, cantidadPropietarios, codigosMascotas, mascotas, tiposAnimales, especies, sexos, fechasNacimiento, codigosPropietariosMascotas, cantidadMascotas Por Referencia, codigoMascota Por Referencia)
    Definir codigoBuscado, posicionPropietario Como Entero
    Definir nombre, tipo, especie, sexo, fecha Como Cadena
    Definir valido Como Logico
    Escribir ""
    Escribir "=== REGISTRAR MASCOTA ==="
    Si cantidadPropietarios = 0 Entonces
        Escribir "Primero debe registrar un propietario."
    SiNo
        Si cantidadMascotas >= 100 Entonces
            Escribir "No hay espacio para mas mascotas."
        SiNo
            Escribir "Codigo del propietario:"
            Leer codigoBuscado
            posicionPropietario <- BuscarPosicionPropietario(codigosPropietarios, cantidadPropietarios, codigoBuscado)
            Si posicionPropietario = -1 Entonces
                Escribir "Error: el propietario no existe."
            SiNo
                Escribir "Propietario: ", propietarios[posicionPropietario]
                valido <- Verdadero
                Escribir "Nombre de la mascota:"
                Leer nombre
                Si TextoVacio(nombre) Entonces
                    Escribir "Error: el nombre es obligatorio."
                    valido <- Falso
                FinSi
                Si valido Entonces
                    Escribir "Tipo (Canino/Felino):"
                    Leer tipo
                    tipo <- ObtenerTipoAnimal(tipo)
                    Si tipo = "" Entonces
                        Escribir "Error: ingrese Canino o Felino."
                        valido <- Falso
                    FinSi
                FinSi
                Si valido Entonces
                    Escribir "Especie o raza:"
                    Leer especie
                    Si TextoVacio(especie) Entonces
                        Escribir "Error: la especie o raza es obligatoria."
                        valido <- Falso
                    FinSi
                FinSi
                Si valido Entonces
                    Escribir "Sexo (Macho/Hembra):"
                    Leer sexo
                    sexo <- ObtenerSexo(sexo)
                    Si sexo = "" Entonces
                        Escribir "Error: ingrese Macho o Hembra."
                        valido <- Falso
                    FinSi
                FinSi
                Si valido Entonces
                    Escribir "Fecha de nacimiento (dd/MM/yyyy):"
                    Leer fecha
                    Si ValidarFechaNacimiento(fecha) = Falso Entonces
                        Escribir "Error: fecha invalida o futura."
                        valido <- Falso
                    FinSi
                FinSi
                Si valido Entonces
                    cantidadMascotas <- cantidadMascotas + 1
                    codigosMascotas[cantidadMascotas] <- codigoMascota
                    mascotas[cantidadMascotas] <- nombre
                    tiposAnimales[cantidadMascotas] <- tipo
                    especies[cantidadMascotas] <- especie
                    sexos[cantidadMascotas] <- sexo
                    fechasNacimiento[cantidadMascotas] <- fecha
                    codigosPropietariosMascotas[cantidadMascotas] <- codigoBuscado
                    Escribir "Mascota registrada correctamente. Codigo: ", codigoMascota
                    Escribir "Propietario: ", propietarios[posicionPropietario]
                    codigoMascota <- codigoMascota + 1
                FinSi
            FinSi
        FinSi
    FinSi
FinSubProceso

// BUSCAR POR CODIGO O POR NOMBRE
SubProceso BuscarMascota(codigosMascotas, mascotas, tiposAnimales, especies, sexos, fechasNacimiento, codigosPropietariosMascotas, cantidadMascotas, codigosPropietarios, propietarios, cantidadPropietarios)
    Definir codigoBuscado, posicion, i, encontrados Como Entero
    Definir busqueda Como Cadena
    Escribir ""
    Escribir "=== BUSCAR MASCOTA ==="
    Si cantidadMascotas = 0 Entonces
        Escribir "No hay mascotas registradas."
    SiNo
        Escribir "Ingrese el codigo o nombre de la mascota:"
        Leer busqueda
        Si TextoVacio(busqueda) Entonces
            Escribir "Error: ingrese un codigo o nombre."
        SiNo
            Si ValidarDigitos(busqueda, Longitud(busqueda)) Entonces
                codigoBuscado <- ConvertirANumero(busqueda)
                posicion <- BuscarPosicionMascota(codigosMascotas, cantidadMascotas, codigoBuscado)
                Si posicion = -1 Entonces
                    Escribir "Mascota no encontrada."
                SiNo
                    MostrarDatosMascota(codigosMascotas, mascotas, tiposAnimales, especies, sexos, fechasNacimiento, codigosPropietariosMascotas, codigosPropietarios, propietarios, cantidadPropietarios, posicion)
                FinSi
            SiNo
                encontrados <- 0
                Para i <- 1 Hasta cantidadMascotas Hacer
                    Si Mayusculas(mascotas[i]) = Mayusculas(busqueda) Entonces
                        MostrarDatosMascota(codigosMascotas, mascotas, tiposAnimales, especies, sexos, fechasNacimiento, codigosPropietariosMascotas, codigosPropietarios, propietarios, cantidadPropietarios, i)
                        encontrados <- encontrados + 1
                    FinSi
                FinPara
                Si encontrados = 0 Entonces
                    Escribir "No se encontraron mascotas con ese nombre."
                SiNo
                    Escribir "Total encontrado: ", encontrados
                FinSi
            FinSi
        FinSi
    FinSi
FinSubProceso

SubProceso ModificarMascota(codigosMascotas, mascotas, tiposAnimales, especies, sexos, fechasNacimiento, codigosPropietariosMascotas, cantidadMascotas, codigosPropietarios, propietarios, cantidadPropietarios)
    Definir codigoBuscado, posicion, campo, nuevoPropietario Como Entero
    Definir dato, tipo, sexo Como Cadena
    Definir valido Como Logico
    Escribir ""
    Escribir "=== MODIFICAR MASCOTA ==="
    Si cantidadMascotas = 0 Entonces
        Escribir "No hay mascotas registradas."
    SiNo
        Escribir "Codigo de la mascota:"
        Leer codigoBuscado
        posicion <- BuscarPosicionMascota(codigosMascotas, cantidadMascotas, codigoBuscado)
        Si posicion = -1 Entonces
            Escribir "Mascota no encontrada."
        SiNo
            MostrarDatosMascota(codigosMascotas, mascotas, tiposAnimales, especies, sexos, fechasNacimiento, codigosPropietariosMascotas, codigosPropietarios, propietarios, cantidadPropietarios, posicion)
            Escribir "1. Nombre"
            Escribir "2. Tipo de animal"
            Escribir "3. Especie o raza"
            Escribir "4. Sexo"
            Escribir "5. Fecha de nacimiento"
            Escribir "6. Cambiar propietario"
            Escribir "0. Cancelar"
            Escribir "Seleccione una opcion:"
            Leer campo
            Si campo = 0 Entonces
                Escribir "Modificacion cancelada."
            SiNo
                Si campo < 1 O campo > 6 Entonces
                    Escribir "Opcion no valida."
                SiNo
                    Escribir "Ingrese el nuevo dato:"
                    Leer dato
                    valido <- Verdadero
                    Si TextoVacio(dato) Entonces
                        Escribir "Error: el dato es obligatorio."
                        valido <- Falso
                    FinSi
                    Si valido Entonces
                        Segun campo Hacer
                            1:
                                mascotas[posicion] <- dato
                            2:
                                tipo <- ObtenerTipoAnimal(dato)
                                Si tipo = "" Entonces
                                    valido <- Falso
                                    Escribir "Error: ingrese Canino o Felino."
                                SiNo
                                    tiposAnimales[posicion] <- tipo
                                FinSi
                            3:
                                especies[posicion] <- dato
                            4:
                                sexo <- ObtenerSexo(dato)
                                Si sexo = "" Entonces
                                    valido <- Falso
                                    Escribir "Error: ingrese Macho o Hembra."
                                SiNo
                                    sexos[posicion] <- sexo
                                FinSi
                            5:
                                Si ValidarFechaNacimiento(dato) Entonces
                                    fechasNacimiento[posicion] <- dato
                                SiNo
                                    valido <- Falso
                                    Escribir "Error: fecha invalida o futura."
                                FinSi
                            6:
                                Si ValidarDigitos(dato, Longitud(dato)) Entonces
                                    nuevoPropietario <- ConvertirANumero(dato)
                                    Si BuscarPosicionPropietario(codigosPropietarios, cantidadPropietarios, nuevoPropietario) <> -1 Entonces
                                        codigosPropietariosMascotas[posicion] <- nuevoPropietario
                                    SiNo
                                        Escribir "Error: el propietario no existe."
                                        valido <- Falso
                                    FinSi
                                SiNo
                                    Escribir "Error: codigo de propietario invalido."
                                    valido <- Falso
                                FinSi
                        FinSegun
                    FinSi
                    Si valido Entonces
                        Escribir "Mascota modificada correctamente."
                        MostrarDatosMascota(codigosMascotas, mascotas, tiposAnimales, especies, sexos, fechasNacimiento, codigosPropietariosMascotas, codigosPropietarios, propietarios, cantidadPropietarios, posicion)
                    FinSi
                FinSi
            FinSi
        FinSi
    FinSi
FinSubProceso

SubProceso EliminarMascota(codigosMascotas, mascotas, tiposAnimales, especies, sexos, fechasNacimiento, codigosPropietariosMascotas, cantidadMascotas Por Referencia, codigosPropietarios, propietarios, cantidadPropietarios)
    Definir codigoBuscado, posicion, i Como Entero
    Definir respuesta Como Cadena
    Escribir ""
    Escribir "=== ELIMINAR MASCOTA ==="
    Si cantidadMascotas = 0 Entonces
        Escribir "No hay mascotas registradas."
    SiNo
        Escribir "Codigo de la mascota:"
        Leer codigoBuscado
        posicion <- BuscarPosicionMascota(codigosMascotas, cantidadMascotas, codigoBuscado)
        Si posicion = -1 Entonces
            Escribir "Mascota no encontrada."
        SiNo
            MostrarDatosMascota(codigosMascotas, mascotas, tiposAnimales, especies, sexos, fechasNacimiento, codigosPropietariosMascotas, codigosPropietarios, propietarios, cantidadPropietarios, posicion)
            Escribir "Confirmar eliminacion? (S/N):"
            Leer respuesta
            respuesta <- Mayusculas(respuesta)
            Si respuesta = "S" O respuesta = "SI" Entonces
                Si posicion < cantidadMascotas Entonces
                    Para i <- posicion Hasta cantidadMascotas - 1 Hacer
                        codigosMascotas[i] <- codigosMascotas[i + 1]
                        mascotas[i] <- mascotas[i + 1]
                        tiposAnimales[i] <- tiposAnimales[i + 1]
                        especies[i] <- especies[i + 1]
                        sexos[i] <- sexos[i + 1]
                        fechasNacimiento[i] <- fechasNacimiento[i + 1]
                        codigosPropietariosMascotas[i] <- codigosPropietariosMascotas[i + 1]
                    FinPara
                FinSi
                cantidadMascotas <- cantidadMascotas - 1
                Escribir "Mascota eliminada correctamente."
            SiNo
                Escribir "Eliminacion cancelada."
            FinSi
        FinSi
    FinSi
FinSubProceso

SubProceso VerMascotasAsociadas(codigosPropietarios, propietarios, cantidadPropietarios, codigosMascotas, mascotas, tiposAnimales, especies, sexos, fechasNacimiento, codigosPropietariosMascotas, cantidadMascotas)
    Definir codigoBuscado, posicion, i, encontrados Como Entero
    Escribir ""
    Escribir "=== MASCOTAS ASOCIADAS ==="
    Si cantidadPropietarios = 0 Entonces
        Escribir "No hay propietarios registrados."
    SiNo
        Escribir "Codigo del propietario:"
        Leer codigoBuscado
        posicion <- BuscarPosicionPropietario(codigosPropietarios, cantidadPropietarios, codigoBuscado)
        Si posicion = -1 Entonces
            Escribir "Propietario no encontrado."
        SiNo
            Escribir "Propietario: ", propietarios[posicion]
            encontrados <- 0
            Si cantidadMascotas > 0 Entonces
                Para i <- 1 Hasta cantidadMascotas Hacer
                    Si codigosPropietariosMascotas[i] = codigoBuscado Entonces
                        MostrarDatosMascota(codigosMascotas, mascotas, tiposAnimales, especies, sexos, fechasNacimiento, codigosPropietariosMascotas, codigosPropietarios, propietarios, cantidadPropietarios, i)
                        encontrados <- encontrados + 1
                    FinSi
                FinPara
            FinSi
            Si encontrados = 0 Entonces
                Escribir "Este propietario no tiene mascotas asociadas."
            SiNo
                Escribir "Total de mascotas: ", encontrados
            FinSi
        FinSi
    FinSi
FinSubProceso

Funcion posicion <- BuscarPosicionPropietario(codigosPropietarios, cantidadPropietarios, codigoBuscado)
    Definir i Como Entero

    posicion <- -1

    Si cantidadPropietarios > 0 Entonces
        Para i <- 1 Hasta cantidadPropietarios Con Paso 1 Hacer
            Si codigosPropietarios[i] = codigoBuscado Entonces
                posicion <- i
            FinSi
        FinPara
    FinSi
FinFuncion


SubProceso MostrarDatosPropietario(codigosPropietarios, propietarios, dnisPropietarios, telefonos, direcciones, correos, posicion)

    Escribir "Codigo: ", codigosPropietarios[posicion]
    Escribir "Nombre: ", propietarios[posicion]
    Escribir "DNI: ", dnisPropietarios[posicion]
    Escribir "Telefono: ", telefonos[posicion]
    Escribir "Direccion: ", direcciones[posicion]
    Escribir "Correo: ", correos[posicion]

FinSubProceso

SubProceso RegistrarPropietario(codigosPropietarios, propietarios, dnisPropietarios, telefonos, direcciones, correos, cantidadPropietarios Por Referencia, codigoPropietario Por Referencia)

    Definir nombre, dni, telefono, direccion, correo Como Cadena
    Definir i Como Entero
    Definir valido, duplicado Como Logico

    Escribir ""
    Escribir "=== REGISTRAR PROPIETARIO ==="

    Si cantidadPropietarios >= 100 Entonces
        Escribir "No hay espacio para mas propietarios."
    SiNo
        valido <- Verdadero

        Escribir "Nombre del propietario:"
        Leer nombre

        Si TextoVacio(nombre) Entonces
            Escribir "Error: el nombre es obligatorio."
            valido <- Falso
        FinSi

        Si valido Entonces
            Escribir "DNI (8 digitos):"
            Leer dni

            Si ValidarDigitos(dni, 8) = Falso Entonces
                Escribir "Error: DNI invalido."
                valido <- Falso
            SiNo
                duplicado <- Falso

                Si cantidadPropietarios > 0 Entonces
                    Para i <- 1 Hasta cantidadPropietarios Hacer
                        Si dnisPropietarios[i] = dni Entonces
                            duplicado <- Verdadero
                        FinSi
                    FinPara
                FinSi

                Si duplicado Entonces
                    Escribir "Error: el DNI ya existe."
                    valido <- Falso
                FinSi
            FinSi
        FinSi

        Si valido Entonces
            Escribir "Telefono (9 digitos):"
            Leer telefono

            Si ValidarDigitos(telefono, 9) = Falso Entonces
                Escribir "Error: telefono invalido."
                valido <- Falso
            FinSi
        FinSi

        Si valido Entonces
            Escribir "Direccion:"
            Leer direccion

            Si TextoVacio(direccion) Entonces
                Escribir "Error: la direccion es obligatoria."
                valido <- Falso
            FinSi
        FinSi

        Si valido Entonces
            Escribir "Correo electronico:"
            Leer correo

            Si ValidarCorreo(correo) = Falso Entonces
                Escribir "Error: correo invalido."
                valido <- Falso
            FinSi
        FinSi

        Si valido Entonces
            cantidadPropietarios <- cantidadPropietarios + 1

            codigosPropietarios[cantidadPropietarios] <- codigoPropietario
            propietarios[cantidadPropietarios] <- nombre
            dnisPropietarios[cantidadPropietarios] <- dni
            telefonos[cantidadPropietarios] <- telefono
            direcciones[cantidadPropietarios] <- direccion
            correos[cantidadPropietarios] <- correo

            Escribir ""
            Escribir "=== REGISTRO EXITOSO ==="
            Escribir "Codigo: ", codigoPropietario
            Escribir "Nombre: ", nombre
            Escribir "DNI: ", dni

            codigoPropietario <- codigoPropietario + 1
        FinSi
    FinSi
FinSubProceso


SubProceso BuscarPropietario(codigosPropietarios, propietarios, dnisPropietarios, telefonos, direcciones, correos, cantidadPropietarios)

    Definir codigoBuscado, posicion Como Entero

    Escribir ""
    Escribir "=== BUSCAR PROPIETARIO ==="

    Si cantidadPropietarios = 0 Entonces
        Escribir "No hay propietarios registrados."
    SiNo
        Escribir "Ingrese el codigo del propietario:"
        Leer codigoBuscado

        posicion <- BuscarPosicionPropietario(codigosPropietarios, cantidadPropietarios, codigoBuscado)

        Si posicion = -1 Entonces
            Escribir "Propietario no encontrado."
        SiNo
            MostrarDatosPropietario(codigosPropietarios, propietarios, dnisPropietarios, telefonos, direcciones, correos, posicion)
        FinSi
    FinSi
FinSubProceso


SubProceso ModificarPropietario(codigosPropietarios, propietarios, dnisPropietarios, telefonos, direcciones, correos, cantidadPropietarios)

    Definir codigoBuscado, posicion, i, campo Como Entero
    Definir nuevoDato Como Cadena
    Definir valido, duplicado Como Logico

    Escribir ""
    Escribir "=== MODIFICAR PROPIETARIO ==="

    Si cantidadPropietarios = 0 Entonces
        Escribir "No hay propietarios registrados."
    SiNo
        Escribir "Ingrese el codigo del propietario:"
        Leer codigoBuscado

        posicion <- BuscarPosicionPropietario(codigosPropietarios, cantidadPropietarios, codigoBuscado)

        Si posicion = -1 Entonces
            Escribir "No se encontro un propietario con ese codigo."
        SiNo
            // MOSTRAR DATOS ACTUALES
            Escribir ""
            MostrarDatosPropietario(codigosPropietarios, propietarios, dnisPropietarios, telefonos, direcciones, correos, posicion)

            Escribir ""
            Escribir "=== DATOS A MODIFICAR ==="
            Escribir "1. Nombre completo"
            Escribir "2. DNI"
            Escribir "3. Telefono"
            Escribir "4. Direccion"
            Escribir "5. Correo electronico"
            Escribir "0. Cancelar"
            Escribir "Seleccione una opcion:"
            Leer campo

            Si campo = 0 Entonces
                Escribir "Modificacion cancelada."
            SiNo
                Si campo < 1 O campo > 5 Entonces
                    Escribir "Opcion no valida."
                SiNo
                    Escribir "Ingrese el nuevo dato:"
                    Leer nuevoDato

                    valido <- Verdadero

                    Si TextoVacio(nuevoDato) Entonces
                        Escribir "Error: el campo no puede estar vacio."
                        valido <- Falso
                    FinSi

                    Si valido Entonces
                        Segun campo Hacer
                            1:
                                propietarios[posicion] <- nuevoDato

                            2:
                                Si ValidarDigitos(nuevoDato, 8) = Falso Entonces
                                    Escribir "Error: el DNI debe tener 8 digitos."
                                    valido <- Falso
                                SiNo
                                    duplicado <- Falso

                                    Para i <- 1 Hasta cantidadPropietarios Hacer
                                        Si i <> posicion Y dnisPropietarios[i] = nuevoDato Entonces
                                            duplicado <- Verdadero
                                        FinSi
                                    FinPara

                                    Si duplicado Entonces
                                        Escribir "Error: el DNI ya pertenece a otro propietario."
                                        valido <- Falso
                                    SiNo
                                        dnisPropietarios[posicion] <- nuevoDato
                                    FinSi
                                FinSi

                            3:
                                Si ValidarDigitos(nuevoDato, 9) Entonces
                                    telefonos[posicion] <- nuevoDato
                                SiNo
                                    Escribir "Error: el telefono debe tener 9 digitos."
                                    valido <- Falso
                                FinSi

                            4:
                                direcciones[posicion] <- nuevoDato

                            5:
                                Si ValidarCorreo(nuevoDato) Entonces
                                    correos[posicion] <- nuevoDato
                                SiNo
                                    Escribir "Error: ingrese un correo valido."
                                    valido <- Falso
                                FinSi

                        FinSegun
                    FinSi

                    Si valido Entonces
                        Escribir ""
                        Escribir "Propietario modificado correctamente."
                        Escribir "Codigo: ", codigosPropietarios[posicion]
                    FinSi
                FinSi
            FinSi
        FinSi
    FinSi

FinSubProceso


SubProceso EliminarPropietario(codigosPropietarios, propietarios, dnisPropietarios, telefonos, direcciones, correos, cantidadPropietarios Por Referencia, codigosPropietariosMascotas, cantidadMascotas)

    Definir codigoBuscado, posicion, i Como Entero
    Definir respuesta Como Cadena
    Definir tieneMascotas Como Logico

    Escribir ""
    Escribir "=== ELIMINAR PROPIETARIO ==="

    Si cantidadPropietarios = 0 Entonces
        Escribir "No hay propietarios registrados."
    SiNo
        Escribir "Ingrese el codigo del propietario:"
        Leer codigoBuscado

        posicion <- BuscarPosicionPropietario(codigosPropietarios, cantidadPropietarios, codigoBuscado)

        Si posicion = -1 Entonces
            Escribir "Propietario no encontrado."
        SiNo

            // VERIFICAR MASCOTAS ASOCIADAS
            tieneMascotas <- Falso

            Si cantidadMascotas > 0 Entonces
                Para i <- 1 Hasta cantidadMascotas Con Paso 1 Hacer
                    Si codigosPropietariosMascotas[i] = codigoBuscado Entonces
                        tieneMascotas <- Verdadero
                    FinSi
                FinPara
            FinSi

            Si tieneMascotas Entonces
                Escribir "No se puede eliminar: tiene mascotas asociadas."
            SiNo

                // MOSTRAR DATOS
                MostrarDatosPropietario(codigosPropietarios, propietarios, dnisPropietarios, telefonos, direcciones, correos, posicion)

                Escribir "Confirmar eliminacion? (S/N):"
                Leer respuesta
                respuesta <- Mayusculas(respuesta)

                Si respuesta = "S" O respuesta = "SI" Entonces

                    // DESPLAZAR LOS REGISTROS
                    Si posicion < cantidadPropietarios Entonces
                        Para i <- posicion Hasta cantidadPropietarios - 1 Con Paso 1 Hacer

                            codigosPropietarios[i] <- codigosPropietarios[i + 1]
                            propietarios[i] <- propietarios[i + 1]
                            dnisPropietarios[i] <- dnisPropietarios[i + 1]
                            telefonos[i] <- telefonos[i + 1]
                            direcciones[i] <- direcciones[i + 1]
                            correos[i] <- correos[i + 1]

                        FinPara
                    FinSi

                    cantidadPropietarios <- cantidadPropietarios - 1

                    Escribir "Propietario eliminado correctamente."

                SiNo
                    Escribir "Eliminacion cancelada."
                FinSi
            FinSi
        FinSi
    FinSi

FinSubProceso

// DEMOSTRACION INDIVIDUAL: LOS 2 MODULOS DEL INTEGRANTE 1
Algoritmo EF_Propietarios_Mascotas
    Definir codigosPropietarios, codigosMascotas, codigosPropietariosMascotas Como Entero
    Definir propietarios, dnisPropietarios, telefonos, direcciones, correos Como Cadena
    Definir mascotas, tiposAnimales, especies, sexos, fechasNacimiento Como Cadena
    Definir cantidadPropietarios, cantidadMascotas, codigoPropietario, codigoMascota, opcion Como Entero

    Dimension codigosPropietarios[101]
    Dimension propietarios[101]
    Dimension dnisPropietarios[101]
    Dimension telefonos[101]
    Dimension direcciones[101]
    Dimension correos[101]
    Dimension codigosMascotas[101]
    Dimension mascotas[101]
    Dimension tiposAnimales[101]
    Dimension especies[101]
    Dimension sexos[101]
    Dimension fechasNacimiento[101]
    Dimension codigosPropietariosMascotas[101]

    cantidadPropietarios <- 0
    cantidadMascotas <- 0
    codigoPropietario <- 2001
    codigoMascota <- 1001

    Repetir
        Escribir ""
        Escribir "=== ENTRE PATAS ARIAS - INTEGRANTE 1 ==="
        Escribir "PROPIETARIOS"
        Escribir "1. Registrar propietario"
        Escribir "2. Buscar propietario"
        Escribir "3. Modificar propietario"
        Escribir "4. Eliminar propietario"
        Escribir "5. Ver mascotas asociadas"
        Escribir "MASCOTAS"
        Escribir "6. Registrar mascota"
        Escribir "7. Buscar mascota (codigo o nombre)"
        Escribir "8. Modificar mascota"
        Escribir "9. Eliminar mascota"
        Escribir "0. Salir"
        Escribir "Seleccione una opcion:"
        Leer opcion

        Segun opcion Hacer
            1:
                RegistrarPropietario(codigosPropietarios, propietarios, dnisPropietarios, telefonos, direcciones, correos, cantidadPropietarios, codigoPropietario)
            2:
                BuscarPropietario(codigosPropietarios, propietarios, dnisPropietarios, telefonos, direcciones, correos, cantidadPropietarios)
            3:
                ModificarPropietario(codigosPropietarios, propietarios, dnisPropietarios, telefonos, direcciones, correos, cantidadPropietarios)
            4:
                EliminarPropietario(codigosPropietarios, propietarios, dnisPropietarios, telefonos, direcciones, correos, cantidadPropietarios, codigosPropietariosMascotas, cantidadMascotas)
            5:
                VerMascotasAsociadas(codigosPropietarios, propietarios, cantidadPropietarios, codigosMascotas, mascotas, tiposAnimales, especies, sexos, fechasNacimiento, codigosPropietariosMascotas, cantidadMascotas)
            6:
                RegistrarMascota(codigosPropietarios, propietarios, cantidadPropietarios, codigosMascotas, mascotas, tiposAnimales, especies, sexos, fechasNacimiento, codigosPropietariosMascotas, cantidadMascotas, codigoMascota)
            7:
                BuscarMascota(codigosMascotas, mascotas, tiposAnimales, especies, sexos, fechasNacimiento, codigosPropietariosMascotas, cantidadMascotas, codigosPropietarios, propietarios, cantidadPropietarios)
            8:
                ModificarMascota(codigosMascotas, mascotas, tiposAnimales, especies, sexos, fechasNacimiento, codigosPropietariosMascotas, cantidadMascotas, codigosPropietarios, propietarios, cantidadPropietarios)
            9:
                EliminarMascota(codigosMascotas, mascotas, tiposAnimales, especies, sexos, fechasNacimiento, codigosPropietariosMascotas, cantidadMascotas, codigosPropietarios, propietarios, cantidadPropietarios)
            0:
                Escribir "Programa finalizado."
            De Otro Modo:
                Escribir "Opcion no valida."
        FinSegun
    Hasta Que opcion = 0
FinAlgoritmo
