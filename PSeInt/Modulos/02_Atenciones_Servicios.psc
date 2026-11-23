// Joaquin - Atenciones medicas y servicios veterinarios.
// Perfil Flexible: indices de arreglos y cadenas en base 1.
// Archivo independiente con datos ficticios; no importa otros .psc.
Funcion limpio <- Recortar(texto)
    Definir limpio Como Cadena
    Definir inicio, final Como Entero
    Definir seguir Como Logico
    inicio <- 1
    final <- Longitud(texto)
    seguir <- Verdadero
    Mientras inicio <= final Y seguir Hacer
        Si SubCadena(texto, inicio, inicio) = " " Entonces
            inicio <- inicio + 1
        SiNo
            seguir <- Falso
        FinSi
    FinMientras
    seguir <- Verdadero
    Mientras final >= inicio Y seguir Hacer
        Si SubCadena(texto, final, final) = " " Entonces
            final <- final - 1
        SiNo
            seguir <- Falso
        FinSi
    FinMientras
    limpio <- ""
    Si inicio <= final Entonces
        limpio <- SubCadena(texto, inicio, final)
    FinSi
FinFuncion

Funcion texto <- LeerTexto(mensaje)
    Definir texto Como Cadena
    Escribir Sin Saltar mensaje
    Leer texto
    texto <- Recortar(texto)
FinFuncion

Funcion valido <- SoloDigitos(texto)
    Definir valido Como Logico
    Definir i Como Entero
    Definir letra Como Cadena
    valido <- Longitud(texto) > 0
    Si Longitud(texto) > 0 Entonces
        Para i <- 1 Hasta Longitud(texto) Hacer
            letra <- SubCadena(texto, i, i)
            Si letra < "0" O letra > "9" Entonces
                valido <- Falso
            FinSi
        FinPara
    FinSi
FinFuncion

Funcion numero <- LeerEntero(mensaje)
    Definir numero Como Entero
    Definir texto Como Cadena
    numero <- -1
    texto <- LeerTexto(mensaje)
    Si SoloDigitos(texto) Y Longitud(texto) <= 10 Entonces
        Si ConvertirANumero(texto) <= 2147483647 Entonces
            numero <- ConvertirANumero(texto)
        FinSi
    FinSi
FinFuncion

Funcion costo <- LeerCosto(mensaje)
    Definir costo Como Real
    Definir texto, letra Como Cadena
    Definir i, puntos, digitos Como Entero
    Definir valido Como Logico
    texto <- LeerTexto(mensaje)
    costo <- -1
    puntos <- 0
    digitos <- 0
    valido <- Longitud(texto) > 0
    Si Longitud(texto) > 0 Entonces
        Para i <- 1 Hasta Longitud(texto) Hacer
            letra <- SubCadena(texto, i, i)
            Si letra = "." Entonces
                puntos <- puntos + 1
            SiNo
                Si letra >= "0" Y letra <= "9" Entonces
                    digitos <- digitos + 1
                SiNo
                    valido <- Falso
                FinSi
            FinSi
        FinPara
    FinSi
    Si valido Y puntos <= 1 Y digitos > 0 Entonces
        costo <- ConvertirANumero(texto)
    FinSi
FinFuncion

Funcion confirmado <- Confirmar(mensaje)
    Definir confirmado Como Logico
    Definir respuesta Como Cadena
    respuesta <- Mayusculas(LeerTexto(mensaje))
    confirmado <- respuesta = "S" O respuesta = "SI"
FinFuncion

Funcion posicion <- BuscarPosicion(codigos, cantidad, codigo)
    Definir posicion, i Como Entero
    posicion <- -1
    Si cantidad > 0 Entonces
        Para i <- 1 Hasta cantidad Hacer
            Si codigos[i] = codigo Entonces
                posicion <- i
            FinSi
        FinPara
    FinSi
FinFuncion

Funcion codigo <- SiguienteCodigo(codigos, cantidad, base)
    Definir codigo, mayor, i Como Entero
    mayor <- base
    Si cantidad > 0 Entonces
        Para i <- 1 Hasta cantidad Hacer
            Si codigos[i] > mayor Entonces
                mayor <- codigos[i]
            FinSi
        FinPara
    FinSi
    codigo <- -1
    Si mayor < 2147483647 Entonces
        codigo <- mayor + 1
    FinSi
FinFuncion

// Devuelve aaaammdd; -1 indica fecha invalida, incluyendo bisiestos.
Funcion valor <- ValorFecha(texto)
    Definir valor, dia, mes, anio, diasMes Como Entero
    valor <- -1
    Si Longitud(texto) = 10 Entonces
        Si SubCadena(texto, 3, 3) = "/" Y SubCadena(texto, 6, 6) = "/" Entonces
            Si SoloDigitos(SubCadena(texto, 1, 2)) Y SoloDigitos(SubCadena(texto, 4, 5)) Y SoloDigitos(SubCadena(texto, 7, 10)) Entonces
                dia <- ConvertirANumero(SubCadena(texto, 1, 2))
                mes <- ConvertirANumero(SubCadena(texto, 4, 5))
                anio <- ConvertirANumero(SubCadena(texto, 7, 10))
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
                    Si dia >= 1 Y dia <= diasMes Entonces
                        valor <- anio * 10000 + mes * 100 + dia
                    FinSi
                FinSi
            FinSi
        FinSi
    FinSi
FinFuncion

// Devuelve minutos desde medianoche; -1 indica hora invalida.
Funcion valor <- ValorHora(texto)
    Definir valor, hora, minuto Como Entero
    valor <- -1
    Si Longitud(texto) = 5 Entonces
        Si SubCadena(texto, 3, 3) = ":" Entonces
            Si SoloDigitos(SubCadena(texto, 1, 2)) Y SoloDigitos(SubCadena(texto, 4, 5)) Entonces
                hora <- ConvertirANumero(SubCadena(texto, 1, 2))
                minuto <- ConvertirANumero(SubCadena(texto, 4, 5))
                Si hora <= 23 Y minuto <= 59 Entonces
                    valor <- hora * 60 + minuto
                FinSi
            FinSi
        FinSi
    FinSi
FinFuncion

Funcion valido <- SituacionFechaValida(situacion, fecha, hora)
    Definir valido Como Logico
    Definir fechaNumero, minutos, hoy, ahora Como Entero
    fechaNumero <- ValorFecha(fecha)
    minutos <- ValorHora(hora)
    hoy <- FechaActual()
    ahora <- HoraActual()
    valido <- Falso
    Si fechaNumero <> -1 Y minutos <> -1 Entonces
        Si situacion = "Realizada" Entonces
            valido <- fechaNumero < hoy O (fechaNumero = hoy Y (Trunc(minutos / 60) * 10000 + (minutos MOD 60) * 100) <= ahora)
        SiNo
            // HoraActual devuelve HHMMSS. Comparar segundos evita aceptar
            // como futura una hora cuyo minuto ya comenzo, como DateTime.Now.
            valido <- fechaNumero > hoy O (fechaNumero = hoy Y (Trunc(minutos / 60) * 10000 + (minutos MOD 60) * 100) >= ahora)
        FinSi
    FinSi
FinFuncion

SubProceso MostrarPaciente(codigo, codMasc, mascotas, codDuenoMasc, cantMasc, codProp, propietarios, cantProp)
    Definir m, p Como Entero
    m <- BuscarPosicion(codMasc, cantMasc, codigo)
    Escribir "Codigo de mascota: ", codigo
    Si m <> -1 Entonces
        Escribir "Mascota: ", mascotas[m]
        p <- BuscarPosicion(codProp, cantProp, codDuenoMasc[m])
        Si p <> -1 Entonces
            Escribir "Propietario: ", propietarios[p]
        SiNo
            Escribir "Propietario no encontrado."
        FinSi
    SiNo
        Escribir "Mascota no encontrada."
    FinSi
FinSubProceso

SubProceso MostrarDatosAtencion(i, codigosAtenciones, codigosMascotasAtenciones, especialidadesAtenciones, veterinariosAtenciones, fechasAtenciones, horasAtenciones, motivosAtenciones, costosAtenciones, diagnosticosAtenciones, tratamientosAtenciones, estadosAtenciones, situacionesAtenciones, codigosMascotas, mascotas, codigosPropietariosMascotas, cantidadMascotas, codigosPropietarios, propietarios, cantidadPropietarios)
    Escribir "======================================"
    Escribir "DATOS DE ATENCION"
    Escribir "Codigo: ", codigosAtenciones[i]
    MostrarPaciente(codigosMascotasAtenciones[i], codigosMascotas, mascotas, codigosPropietariosMascotas, cantidadMascotas, codigosPropietarios, propietarios, cantidadPropietarios)
    Escribir "Especialidad: ", especialidadesAtenciones[i]
    Escribir "Veterinario: ", veterinariosAtenciones[i]
    Escribir "Fecha: ", fechasAtenciones[i]
    Escribir "Hora: ", horasAtenciones[i]
    Escribir "Motivo: ", motivosAtenciones[i]
    Escribir "Costo: ", costosAtenciones[i]
    Si diagnosticosAtenciones[i] = "" Entonces
        Escribir "Diagnostico: Pendiente"
    SiNo
        Escribir "Diagnostico: ", diagnosticosAtenciones[i]
    FinSi
    Si tratamientosAtenciones[i] = "" Entonces
        Escribir "Tratamiento: Pendiente"
    SiNo
        Escribir "Tratamiento: ", tratamientosAtenciones[i]
    FinSi
    Escribir "Estado: ", estadosAtenciones[i]
    Escribir "Situacion: ", situacionesAtenciones[i]
FinSubProceso

SubProceso MostrarDatosServicio(i, codigosServicios, codigosMascotasServicios, nombresServicios, responsablesServicios, fechasServicios, horasServicios, costosServicios, observacionesServicios, estadosServicios, codigosMascotas, mascotas, codigosPropietariosMascotas, cantidadMascotas, codigosPropietarios, propietarios, cantidadPropietarios)
    Escribir "======================================"
    Escribir "DATOS DE SERVICIO"
    Escribir "Codigo: ", codigosServicios[i]
    MostrarPaciente(codigosMascotasServicios[i], codigosMascotas, mascotas, codigosPropietariosMascotas, cantidadMascotas, codigosPropietarios, propietarios, cantidadPropietarios)
    Escribir "Servicio: ", nombresServicios[i]
    Escribir "Responsable: ", responsablesServicios[i]
    Escribir "Fecha: ", fechasServicios[i]
    Escribir "Hora: ", horasServicios[i]
    Escribir "Costo: ", costosServicios[i]
    Escribir "Observaciones: ", observacionesServicios[i]
    Escribir "Estado: ", estadosServicios[i]
FinSubProceso

SubProceso GestionarAtenciones(codigosAtenciones, codigosMascotasAtenciones, especialidadesAtenciones, veterinariosAtenciones, fechasAtenciones, horasAtenciones, motivosAtenciones, costosAtenciones, diagnosticosAtenciones, tratamientosAtenciones, estadosAtenciones, situacionesAtenciones, cantidadAtenciones Por Referencia, codigosMascotas, mascotas, codigosPropietariosMascotas, cantidadMascotas, codigosPropietarios, propietarios, cantidadPropietarios)
    Definir eleccion, codigo, posicion, m, p, campo, nuevoCodigo, tipo Como Entero
    Definir especialidad, veterinario, motivo, fecha, hora, diagnostico, tratamiento Como Cadena
    Definir nombre, responsable, observacion, situacion, dato Como Cadena
    Definir costo Como Real
    Definir valido Como Logico
    Repetir
        Escribir "======================================"
        Escribir "GESTION DE ATENCIONES"
        Escribir "1. Registrar  2. Buscar  3. Modificar  4. Anular  0. Volver"
        eleccion <- LeerEntero("Seleccione una opcion: ")
        Si eleccion = 1 Entonces
            valido <- cantidadAtenciones < 100 Y cantidadMascotas > 0
            Si NO valido Entonces
                Escribir "No hay mascotas o se alcanzo el limite de 100 registros."
            FinSi
            Si valido Entonces
                codigo <- LeerEntero("Codigo de mascota (0 para cancelar): ")
                m <- BuscarPosicion(codigosMascotas, cantidadMascotas, codigo)
                valido <- codigo > 0 Y m <> -1
                Si codigo = 0 Entonces
                    Escribir "Registro cancelado."
                SiNo
                    Si NO valido Entonces
                        Escribir "Codigo invalido o mascota no registrada."
                    FinSi
                FinSi
            FinSi
            Si valido Entonces
                p <- BuscarPosicion(codigosPropietarios, cantidadPropietarios, codigosPropietariosMascotas[m])
                valido <- p <> -1
                Si NO valido Entonces
                    Escribir "La mascota no tiene un propietario valido."
                FinSi
            FinSi
            Si valido Entonces
                MostrarPaciente(codigo, codigosMascotas, mascotas, codigosPropietariosMascotas, cantidadMascotas, codigosPropietarios, propietarios, cantidadPropietarios)
                especialidad <- LeerTexto("Especialidad: ")
                veterinario <- LeerTexto("Veterinario: ")
                motivo <- LeerTexto("Motivo: ")
                valido <- especialidad <> "" Y veterinario <> "" Y motivo <> ""
                Si NO valido Entonces
                    Escribir "Especialidad, veterinario y motivo son obligatorios."
                FinSi
            FinSi
            Si valido Entonces
                tipo <- LeerEntero("1. Programada / 2. Realizada: ")
                valido <- tipo = 1 O tipo = 2
                situacion <- "Programada"
                Si tipo = 2 Entonces
                    situacion <- "Realizada"
                FinSi
                Si NO valido Entonces
                    Escribir "Situacion invalida."
                FinSi
            FinSi
            Si valido Entonces
                fecha <- LeerTexto("Fecha (dd/MM/yyyy): ")
                valido <- ValorFecha(fecha) <> -1
                Si NO valido Entonces
                    Escribir "Fecha invalida."
                FinSi
            FinSi
            Si valido Entonces
                hora <- LeerTexto("Hora (HH:mm): ")
                valido <- ValorHora(hora) <> -1
                valido <- valido Y SituacionFechaValida(situacion, fecha, hora)
                Si NO valido Entonces
                    Escribir "Hora invalida o fecha incompatible con la situacion."
                FinSi
            FinSi
            Si valido Entonces
                costo <- LeerCosto("Costo (use punto decimal): ")
                valido <- costo >= 0
                Si NO valido Entonces
                    Escribir "Costo invalido."
                FinSi
            FinSi
            diagnostico <- ""
            tratamiento <- ""
            Si valido Entonces
                Si situacion = "Realizada" Entonces
                    diagnostico <- LeerTexto("Diagnostico: ")
                    tratamiento <- LeerTexto("Tratamiento: ")
                    valido <- diagnostico <> "" Y tratamiento <> ""
                    Si NO valido Entonces
                        Escribir "Diagnostico y tratamiento son obligatorios."
                    FinSi
                FinSi
            FinSi
            Si valido Entonces
                nuevoCodigo <- SiguienteCodigo(codigosAtenciones, cantidadAtenciones, 1000)
                Si nuevoCodigo = -1 Entonces
                    Escribir "No hay mas codigos disponibles."
                SiNo
                    Si Confirmar("Guardar atencion? (S/N): ") Entonces
                        posicion <- cantidadAtenciones + 1
                        codigosAtenciones[posicion] <- nuevoCodigo
                        codigosMascotasAtenciones[posicion] <- codigo
                        especialidadesAtenciones[posicion] <- especialidad
                        veterinariosAtenciones[posicion] <- veterinario
                        fechasAtenciones[posicion] <- fecha
                        horasAtenciones[posicion] <- hora
                        motivosAtenciones[posicion] <- motivo
                        costosAtenciones[posicion] <- costo
                        diagnosticosAtenciones[posicion] <- diagnostico
                        tratamientosAtenciones[posicion] <- tratamiento
                        estadosAtenciones[posicion] <- "Activa"
                        situacionesAtenciones[posicion] <- situacion
                        cantidadAtenciones <- cantidadAtenciones + 1
                        Escribir "Registro guardado. Codigo: ", nuevoCodigo
                    SiNo
                        Escribir "Registro cancelado."
                    FinSi
                FinSi
            FinSi
        SiNo
            Si eleccion >= 2 Y eleccion <= 4 Entonces
                codigo <- LeerEntero("Codigo de atencion: ")
                posicion <- BuscarPosicion(codigosAtenciones, cantidadAtenciones, codigo)
                Si codigo <= 0 O posicion = -1 Entonces
                    Escribir "Codigo invalido o registro no encontrado."
                SiNo
                    MostrarDatosAtencion(posicion, codigosAtenciones, codigosMascotasAtenciones, especialidadesAtenciones, veterinariosAtenciones, fechasAtenciones, horasAtenciones, motivosAtenciones, costosAtenciones, diagnosticosAtenciones, tratamientosAtenciones, estadosAtenciones, situacionesAtenciones, codigosMascotas, mascotas, codigosPropietariosMascotas, cantidadMascotas, codigosPropietarios, propietarios, cantidadPropietarios)
                    Si eleccion = 4 Entonces
                        Si estadosAtenciones[posicion] = "Anulada" Entonces
                            Escribir "El registro ya esta anulado."
                        SiNo
                            Si Confirmar("Confirmar anulacion? (S/N): ") Entonces
                                estadosAtenciones[posicion] <- "Anulada"
                                Escribir "Registro anulado."
                            SiNo
                                Escribir "Anulacion cancelada."
                            FinSi
                        FinSi
                    FinSi
                    Si eleccion = 3 Entonces
                        Si estadosAtenciones[posicion] = "Anulada" Entonces
                            Escribir "No se puede modificar un registro anulado."
                        SiNo
                            Escribir "1. Especialidad  2. Veterinario  3. Fecha  4. Hora"
                            Escribir "5. Motivo  6. Costo  7. Diagnostico  8. Tratamiento"
                            Escribir "9. Marcar como realizada  0. Cancelar"
                            campo <- LeerEntero("Seleccione un campo: ")
                            valido <- campo >= 1 Y campo <= 9
                            Si campo = 0 Entonces
                                Escribir "Modificacion cancelada."
                            SiNo
                                Si NO valido Entonces
                                    Escribir "Opcion invalida."
                                FinSi
                            FinSi
                            Si valido Y campo = 9 Entonces
                                valido <- Falso
                                Si situacionesAtenciones[posicion] = "Realizada" Entonces
                                    Escribir "Ya esta realizada."
                                SiNo
                                    Si SituacionFechaValida("Realizada", fechasAtenciones[posicion], horasAtenciones[posicion]) Entonces
                                        diagnostico <- LeerTexto("Diagnostico: ")
                                        tratamiento <- LeerTexto("Tratamiento: ")
                                        Si diagnostico <> "" Y tratamiento <> "" Entonces
                                            diagnosticosAtenciones[posicion] <- diagnostico
                                            tratamientosAtenciones[posicion] <- tratamiento
                                            situacionesAtenciones[posicion] <- "Realizada"
                                            Escribir "Atencion marcada como realizada."
                                        SiNo
                                            Escribir "Ambos datos son obligatorios."
                                        FinSi
                                    SiNo
                                        Escribir "Una atencion futura no puede marcarse realizada."
                                    FinSi
                                FinSi
                            FinSi
                            Si valido Y (campo = 7 O campo = 8) Entonces
                                valido <- situacionesAtenciones[posicion] = "Realizada"
                                Si NO valido Entonces
                                    Escribir "Primero marque la atencion como realizada."
                                FinSi
                            FinSi
                            Si valido Entonces
                                Si campo = 6 Entonces
                                    costo <- LeerCosto("Nuevo costo (punto decimal): ")
                                    valido <- costo >= 0
                                    Si valido Entonces
                                        costosAtenciones[posicion] <- costo
                                    FinSi
                                SiNo
                                    dato <- LeerTexto("Nuevo dato: ")
                                    valido <- dato <> ""
                                    Si valido Entonces
                                        Segun campo Hacer
                                            1:
                                                especialidadesAtenciones[posicion] <- dato
                                            2:
                                                veterinariosAtenciones[posicion] <- dato
                                            3:
                                                valido <- ValorFecha(dato) <> -1 Y SituacionFechaValida(situacionesAtenciones[posicion], dato, horasAtenciones[posicion])
                                                Si valido Entonces
                                                    fechasAtenciones[posicion] <- dato
                                                FinSi
                                            4:
                                                valido <- ValorHora(dato) <> -1 Y SituacionFechaValida(situacionesAtenciones[posicion], fechasAtenciones[posicion], dato)
                                                Si valido Entonces
                                                    horasAtenciones[posicion] <- dato
                                                FinSi
                                            5:
                                                motivosAtenciones[posicion] <- dato
                                            7:
                                                diagnosticosAtenciones[posicion] <- dato
                                            8:
                                                tratamientosAtenciones[posicion] <- dato
                                        FinSegun
                                    FinSi
                                FinSi
                                Si valido Entonces
                                    Escribir "Registro modificado correctamente."
                                SiNo
                                    Escribir "Dato invalido, obligatorio o incompatible con la situacion."
                                FinSi
                            FinSi
                        FinSi
                    FinSi
                FinSi
            SiNo
                Si eleccion <> 0 Entonces
                    Escribir "Opcion invalida."
                FinSi
            FinSi
        FinSi
    Hasta Que eleccion = 0
FinSubProceso

SubProceso GestionarServicios(codigosServicios, codigosMascotasServicios, nombresServicios, responsablesServicios, fechasServicios, horasServicios, costosServicios, observacionesServicios, estadosServicios, cantidadServicios Por Referencia, codigosMascotas, mascotas, codigosPropietariosMascotas, cantidadMascotas, codigosPropietarios, propietarios, cantidadPropietarios)
    Definir eleccion, codigo, posicion, m, p, campo, nuevoCodigo, tipo Como Entero
    Definir especialidad, veterinario, motivo, fecha, hora, diagnostico, tratamiento Como Cadena
    Definir nombre, responsable, observacion, situacion, dato Como Cadena
    Definir costo Como Real
    Definir valido Como Logico
    Repetir
        Escribir "======================================"
        Escribir "GESTION DE SERVICIOES"
        Escribir "1. Registrar  2. Buscar  3. Modificar  4. Anular  0. Volver"
        eleccion <- LeerEntero("Seleccione una opcion: ")
        Si eleccion = 1 Entonces
            valido <- cantidadServicios < 100 Y cantidadMascotas > 0
            Si NO valido Entonces
                Escribir "No hay mascotas o se alcanzo el limite de 100 registros."
            FinSi
            Si valido Entonces
                codigo <- LeerEntero("Codigo de mascota (0 para cancelar): ")
                m <- BuscarPosicion(codigosMascotas, cantidadMascotas, codigo)
                valido <- codigo > 0 Y m <> -1
                Si codigo = 0 Entonces
                    Escribir "Registro cancelado."
                SiNo
                    Si NO valido Entonces
                        Escribir "Codigo invalido o mascota no registrada."
                    FinSi
                FinSi
            FinSi
            Si valido Entonces
                p <- BuscarPosicion(codigosPropietarios, cantidadPropietarios, codigosPropietariosMascotas[m])
                valido <- p <> -1
                Si NO valido Entonces
                    Escribir "La mascota no tiene un propietario valido."
                FinSi
            FinSi
            Si valido Entonces
                MostrarPaciente(codigo, codigosMascotas, mascotas, codigosPropietariosMascotas, cantidadMascotas, codigosPropietarios, propietarios, cantidadPropietarios)
                nombre <- LeerTexto("Nombre del servicio: ")
                responsable <- LeerTexto("Responsable: ")
                valido <- nombre <> "" Y responsable <> ""
                Si NO valido Entonces
                    Escribir "El servicio y el responsable son obligatorios."
                FinSi
            FinSi
            Si valido Entonces
                fecha <- LeerTexto("Fecha (dd/MM/yyyy): ")
                valido <- ValorFecha(fecha) <> -1
                Si NO valido Entonces
                    Escribir "Fecha invalida."
                FinSi
            FinSi
            Si valido Entonces
                hora <- LeerTexto("Hora (HH:mm): ")
                valido <- ValorHora(hora) <> -1
                Si NO valido Entonces
                    Escribir "Hora invalida o fecha incompatible con la situacion."
                FinSi
            FinSi
            Si valido Entonces
                costo <- LeerCosto("Costo (use punto decimal): ")
                valido <- costo >= 0
                Si NO valido Entonces
                    Escribir "Costo invalido."
                FinSi
            FinSi
            Si valido Entonces
                observacion <- LeerTexto("Observaciones (eleccional): ")
            FinSi
            Si valido Entonces
                nuevoCodigo <- SiguienteCodigo(codigosServicios, cantidadServicios, 3000)
                Si nuevoCodigo = -1 Entonces
                    Escribir "No hay mas codigos disponibles."
                SiNo
                    Si Confirmar("Guardar servicio? (S/N): ") Entonces
                        posicion <- cantidadServicios + 1
                        codigosServicios[posicion] <- nuevoCodigo
                        codigosMascotasServicios[posicion] <- codigo
                        nombresServicios[posicion] <- nombre
                        responsablesServicios[posicion] <- responsable
                        fechasServicios[posicion] <- fecha
                        horasServicios[posicion] <- hora
                        costosServicios[posicion] <- costo
                        observacionesServicios[posicion] <- observacion
                        estadosServicios[posicion] <- "Activo"
                        cantidadServicios <- cantidadServicios + 1
                        Escribir "Registro guardado. Codigo: ", nuevoCodigo
                    SiNo
                        Escribir "Registro cancelado."
                    FinSi
                FinSi
            FinSi
        SiNo
            Si eleccion >= 2 Y eleccion <= 4 Entonces
                codigo <- LeerEntero("Codigo de servicio: ")
                posicion <- BuscarPosicion(codigosServicios, cantidadServicios, codigo)
                Si codigo <= 0 O posicion = -1 Entonces
                    Escribir "Codigo invalido o registro no encontrado."
                SiNo
                    MostrarDatosServicio(posicion, codigosServicios, codigosMascotasServicios, nombresServicios, responsablesServicios, fechasServicios, horasServicios, costosServicios, observacionesServicios, estadosServicios, codigosMascotas, mascotas, codigosPropietariosMascotas, cantidadMascotas, codigosPropietarios, propietarios, cantidadPropietarios)
                    Si eleccion = 4 Entonces
                        Si estadosServicios[posicion] = "Anulado" Entonces
                            Escribir "El registro ya esta anulado."
                        SiNo
                            Si Confirmar("Confirmar anulacion? (S/N): ") Entonces
                                estadosServicios[posicion] <- "Anulado"
                                Escribir "Registro anulado."
                            SiNo
                                Escribir "Anulacion cancelada."
                            FinSi
                        FinSi
                    FinSi
                    Si eleccion = 3 Entonces
                        Si estadosServicios[posicion] = "Anulado" Entonces
                            Escribir "No se puede modificar un registro anulado."
                        SiNo
                            Escribir "1. Servicio  2. Responsable  3. Fecha  4. Hora"
                            Escribir "5. Costo  6. Observaciones  0. Cancelar"
                            campo <- LeerEntero("Seleccione un campo: ")
                            valido <- campo >= 1 Y campo <= 6
                            Si campo = 0 Entonces
                                Escribir "Modificacion cancelada."
                            SiNo
                                Si NO valido Entonces
                                    Escribir "Opcion invalida."
                                FinSi
                            FinSi
                            Si valido Entonces
                                Si campo = 5 Entonces
                                    costo <- LeerCosto("Nuevo costo (punto decimal): ")
                                    valido <- costo >= 0
                                    Si valido Entonces
                                        costosServicios[posicion] <- costo
                                    FinSi
                                SiNo
                                    dato <- LeerTexto("Nuevo dato: ")
                                    valido <- dato <> "" O campo = 6
                                    Si valido Entonces
                                        Segun campo Hacer
                                            1:
                                                nombresServicios[posicion] <- dato
                                            2:
                                                responsablesServicios[posicion] <- dato
                                            3:
                                                valido <- ValorFecha(dato) <> -1
                                                Si valido Entonces
                                                    fechasServicios[posicion] <- dato
                                                FinSi
                                            4:
                                                valido <- ValorHora(dato) <> -1
                                                Si valido Entonces
                                                    horasServicios[posicion] <- dato
                                                FinSi
                                            6:
                                                observacionesServicios[posicion] <- dato
                                        FinSegun
                                    FinSi
                                FinSi
                                Si valido Entonces
                                    Escribir "Registro modificado correctamente."
                                SiNo
                                    Escribir "Dato invalido, obligatorio o incompatible con la situacion."
                                FinSi
                            FinSi
                        FinSi
                    FinSi
                FinSi
            SiNo
                Si eleccion <> 0 Entonces
                    Escribir "Opcion invalida."
                FinSi
            FinSi
        FinSi
    Hasta Que eleccion = 0
FinSubProceso

Algoritmo Atenciones_Servicios_Entre_Patas_Arias
    Definir codigosPropietarios, codigosMascotas, codigosPropietariosMascotas Como Entero
    Definir cantidadPropietarios, cantidadMascotas, cantidadAtenciones, cantidadServicios, eleccion Como Entero
    Definir propietarios, mascotas Como Cadena
    Dimension codigosPropietarios[10], propietarios[10]
    Dimension codigosMascotas[10], mascotas[10], codigosPropietariosMascotas[10]
    Definir codigosAtenciones, codigosMascotasAtenciones Como Entero
    Definir costosAtenciones Como Real
    Definir especialidadesAtenciones, veterinariosAtenciones, fechasAtenciones, horasAtenciones, motivosAtenciones, diagnosticosAtenciones, tratamientosAtenciones, estadosAtenciones, situacionesAtenciones Como Cadena
    Dimension codigosAtenciones[100], codigosMascotasAtenciones[100], especialidadesAtenciones[100]
    Dimension veterinariosAtenciones[100], fechasAtenciones[100], horasAtenciones[100]
    Dimension motivosAtenciones[100], costosAtenciones[100], diagnosticosAtenciones[100]
    Dimension tratamientosAtenciones[100], estadosAtenciones[100], situacionesAtenciones[100]
    Definir codigosServicios, codigosMascotasServicios Como Entero
    Definir costosServicios Como Real
    Definir nombresServicios, responsablesServicios, fechasServicios, horasServicios, observacionesServicios, estadosServicios Como Cadena
    Dimension codigosServicios[100], codigosMascotasServicios[100], nombresServicios[100]
    Dimension responsablesServicios[100], fechasServicios[100], horasServicios[100]
    Dimension costosServicios[100], observacionesServicios[100], estadosServicios[100]
    cantidadPropietarios <- 2
    cantidadMascotas <- 2
    codigosPropietarios[1] <- 2001
    propietarios[1] <- "Juan Perez"
    codigosPropietarios[2] <- 2002
    propietarios[2] <- "Maria Lopez"
    codigosMascotas[1] <- 1001
    mascotas[1] <- "Max"
    codigosPropietariosMascotas[1] <- 2001
    codigosMascotas[2] <- 1002
    mascotas[2] <- "Luna"
    codigosPropietariosMascotas[2] <- 2002
    cantidadAtenciones <- 0
    cantidadServicios <- 0
    Repetir
        Escribir "======================================"
        Escribir "VETERINARIA ENTRE PATAS - ARIAS"
        Escribir "1. Atenciones medicas  2. Servicios veterinarios  0. Salir"
        eleccion <- LeerEntero("Seleccione una opcion: ")
        Segun eleccion Hacer
            1:
                GestionarAtenciones(codigosAtenciones, codigosMascotasAtenciones, especialidadesAtenciones, veterinariosAtenciones, fechasAtenciones, horasAtenciones, motivosAtenciones, costosAtenciones, diagnosticosAtenciones, tratamientosAtenciones, estadosAtenciones, situacionesAtenciones, cantidadAtenciones, codigosMascotas, mascotas, codigosPropietariosMascotas, cantidadMascotas, codigosPropietarios, propietarios, cantidadPropietarios)
            2:
                GestionarServicios(codigosServicios, codigosMascotasServicios, nombresServicios, responsablesServicios, fechasServicios, horasServicios, costosServicios, observacionesServicios, estadosServicios, cantidadServicios, codigosMascotas, mascotas, codigosPropietariosMascotas, cantidadMascotas, codigosPropietarios, propietarios, cantidadPropietarios)
            0:
                Escribir "Programa finalizado."
            De Otro Modo:
                Escribir "Opcion invalida."
        FinSegun
    Hasta Que eleccion = 0
FinAlgoritmo
