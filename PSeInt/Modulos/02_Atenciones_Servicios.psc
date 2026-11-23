Algoritmo Gestion_Veterinaria
	
    // =========================================
    // 1. DECLARACION DE VARIABLES
    // =========================================
	
    Definir codigosPropietarios, codigosMascotas, codigosPropietariosMascotas Como Entero
    Definir cantidadPropietarios, cantidadMascotas Como Entero
    Definir propietarios, telefonos, mascotas, especies Como Cadena
	
    Definir codigosAtenciones, codigosMascotasAtenciones Como Entero
    Definir especialidades, veterinarios Como Cadena
    Definir fechasAtenciones, horasAtenciones Como Cadena
    Definir motivos, diagnosticos, tratamientos Como Cadena
    Definir estadosAtenciones, situaciones Como Cadena
    Definir costosAtenciones Como Real
    Definir cantidadAtenciones Como Entero
	
    Definir codigosServicios, codigosMascotasServicios Como Entero
    Definir nombresServicios, responsablesServicios Como Cadena
    Definir fechasServicios, horasServicios Como Cadena
    Definir observacionesServicios, estadosServicios Como Cadena
    Definir costosServicios Como Real
    Definir cantidadServicios Como Entero
	
    Definir opcionPrincipal, opcionAtencion, opcionServicio Como Entero
    Definir i, j, k, codigo, codigoMascota, posicion Como Entero
    Definir nuevoCodigo, encontrado, opcionModificar Como Entero
	
    Definir respuesta, dato, dato2 Como Cadena
    Definir nuevoCosto Como Real
	
    // =========================================
    // 2. DIMENSIONES
    // =========================================
	
    Dimension codigosPropietarios[10]
    Dimension codigosMascotas[10]
    Dimension codigosPropietariosMascotas[10]
    Dimension propietarios[10]
    Dimension telefonos[10]
    Dimension mascotas[10]
    Dimension especies[10]
	
    Dimension codigosAtenciones[100]
    Dimension codigosMascotasAtenciones[100]
    Dimension especialidades[100]
    Dimension veterinarios[100]
    Dimension fechasAtenciones[100]
    Dimension horasAtenciones[100]
    Dimension motivos[100]
    Dimension diagnosticos[100]
    Dimension tratamientos[100]
    Dimension estadosAtenciones[100]
    Dimension situaciones[100]
    Dimension costosAtenciones[100]
	
    Dimension codigosServicios[100]
    Dimension codigosMascotasServicios[100]
    Dimension nombresServicios[100]
    Dimension responsablesServicios[100]
    Dimension fechasServicios[100]
    Dimension horasServicios[100]
    Dimension observacionesServicios[100]
    Dimension estadosServicios[100]
    Dimension costosServicios[100]
	
    // =========================================
    // 3. DATOS DE PRUEBA DE PROPIETARIOS
    // =========================================
	
    cantidadPropietarios <- 2
	
    codigosPropietarios[1] <- 2001
    propietarios[1] <- "Juan Perez"
    telefonos[1] <- "987654321"
	
    codigosPropietarios[2] <- 2002
    propietarios[2] <- "Maria Lopez"
    telefonos[2] <- "912345678"
	
    // =========================================
    // 4. DATOS DE PRUEBA DE MASCOTAS
    // =========================================
	
    cantidadMascotas <- 2
	
    codigosMascotas[1] <- 1001
    mascotas[1] <- "Max"
    especies[1] <- "Perro"
    codigosPropietariosMascotas[1] <- 2001
	
    codigosMascotas[2] <- 1002
    mascotas[2] <- "Luna"
    especies[2] <- "Gato"
    codigosPropietariosMascotas[2] <- 2002
	
    cantidadAtenciones <- 0
    cantidadServicios <- 0
	
    // =========================================
    // 5. MENU PRINCIPAL
    // =========================================
	
    Repetir
		
        Limpiar Pantalla
		
        Escribir "======================================"
        Escribir "      SISTEMA DE GESTION VETERINARIA"
        Escribir "======================================"
        Escribir "1. Gestion de Atenciones"
        Escribir "2. Gestion de Servicios"
        Escribir "0. Salir del sistema"
        Escribir "======================================"
        Escribir "Seleccione una opcion:"
        Leer opcionPrincipal
		
        Segun opcionPrincipal Hacer
			
				// =================================
				// MODULO 1: GESTION DE ATENCIONES
				// =================================
			
            1:
				
                Repetir
					
                    Limpiar Pantalla
					
                    Escribir "======================================"
                    Escribir "          GESTION DE ATENCIONES"
                    Escribir "======================================"
                    Escribir "1. Registrar atencion"
                    Escribir "2. Buscar atencion"
                    Escribir "3. Modificar atencion"
                    Escribir "4. Anular atencion"
                    Escribir "5. Mostrar atenciones"
                    Escribir "0. Regresar al menu principal"
                    Escribir "======================================"
                    Leer opcionAtencion
					
                    Segun opcionAtencion Hacer
						
							// =================================
							// 1. REGISTRAR ATENCION
							// =================================
						
                        1:
							
                            Si cantidadMascotas = 0 Entonces
								
                                Escribir "No existen mascotas registradas."
								
                            SiNo
								
                                Si cantidadAtenciones < 100 Entonces
									
                                    Escribir "Ingrese codigo de mascota:"
                                    Leer codigoMascota
									
                                    posicion <- 0
									
                                    Para i <- 1 Hasta cantidadMascotas Hacer
										
                                        Si codigosMascotas[i] = codigoMascota Entonces
                                            posicion <- i
                                        FinSi
										
                                    FinPara
									
                                    Si posicion = 0 Entonces
										
                                        Escribir "Mascota no encontrada."
										
                                    SiNo
										
                                        Escribir "Mascota: ", mascotas[posicion]
                                        Escribir "Especie: ", especies[posicion]
										
                                        encontrado <- 0
										
                                        Para i <- 1 Hasta cantidadPropietarios Hacer
											
                                            Si codigosPropietarios[i] = codigosPropietariosMascotas[posicion] Entonces
												
                                                Escribir "Propietario: ", propietarios[i]
                                                Escribir "Telefono: ", telefonos[i]
												
                                                encontrado <- 1
												
                                            FinSi
											
                                        FinPara
										
                                        Si encontrado = 0 Entonces
											
                                            Escribir "La mascota no tiene propietario valido."
											
                                        SiNo
											
                                            Escribir "Ingrese especialidad:"
                                            Leer especialidades[cantidadAtenciones + 1]
											
                                            Escribir "Ingrese veterinario:"
                                            Leer veterinarios[cantidadAtenciones + 1]
											
                                            Escribir "Ingrese motivo de consulta:"
                                            Leer motivos[cantidadAtenciones + 1]
											
                                            Escribir "Tipo de atencion:"
                                            Escribir "1. Programada"
                                            Escribir "2. Realizada"
                                            Leer j
											
                                            Si j = 1 O j = 2 Entonces
												
                                                Si j = 1 Entonces
                                                    situaciones[cantidadAtenciones + 1] <- "Programada"
                                                SiNo
                                                    situaciones[cantidadAtenciones + 1] <- "Realizada"
                                                FinSi
												
                                                Escribir "Ingrese fecha (dd/mm/aaaa):"
                                                Leer fechasAtenciones[cantidadAtenciones + 1]
												
                                                Escribir "Ingrese hora (HH:mm):"
                                                Leer horasAtenciones[cantidadAtenciones + 1]
												
                                                Escribir "Ingrese costo:"
                                                Leer costosAtenciones[cantidadAtenciones + 1]
												
                                                Si costosAtenciones[cantidadAtenciones + 1] >= 0 Entonces
													
                                                    Si j = 2 Entonces
														
                                                        Escribir "Ingrese diagnostico:"
                                                        Leer diagnosticos[cantidadAtenciones + 1]
														
                                                        Escribir "Ingrese tratamiento:"
                                                        Leer tratamientos[cantidadAtenciones + 1]
														
                                                    SiNo
														
                                                        diagnosticos[cantidadAtenciones + 1] <- ""
                                                        tratamientos[cantidadAtenciones + 1] <- ""
														
                                                    FinSi
													
                                                    Escribir "¿Confirma el registro? (S/N)"
                                                    Leer respuesta
													
                                                    Si Mayusculas(respuesta) = "S" O Mayusculas(respuesta) = "SI" Entonces
														
                                                        nuevoCodigo <- 1001
														
                                                        nuevoCodigo <- 1001
														
														Si cantidadAtenciones > 0 Entonces
															
															Para i <- 1 Hasta cantidadAtenciones Hacer
																
																Si codigosAtenciones[i] >= nuevoCodigo Entonces
																	nuevoCodigo <- codigosAtenciones[i] + 1
																FinSi
																
															FinPara
															
														FinSi
                                                        cantidadAtenciones <- cantidadAtenciones + 1
														
                                                        codigosAtenciones[cantidadAtenciones] <- nuevoCodigo
                                                        codigosMascotasAtenciones[cantidadAtenciones] <- codigoMascota
                                                        estadosAtenciones[cantidadAtenciones] <- "Activa"
														
                                                        Escribir "Atencion registrada correctamente."
                                                        Escribir "Codigo generado: ", nuevoCodigo
														
                                                    SiNo
														
                                                        Escribir "Registro cancelado."
														
                                                    FinSi
													
                                                SiNo
													
                                                    Escribir "El costo no puede ser negativo."
													
                                                FinSi
												
                                            SiNo
												
                                                Escribir "Tipo de atencion no valido."
												
                                            FinSi
											
                                        FinSi
										
                                    FinSi
									
                                SiNo
									
                                    Escribir "Se alcanzo el limite de atenciones."
									
                                FinSi
								
                            FinSi
							
                            Esperar Tecla
							
							// =================================
							// 2. BUSCAR ATENCION
							// =================================
							
                        2:
							
                            Escribir "Ingrese codigo de atencion:"
                            Leer codigo
							
                            posicion <- 0
							
                            Para i <- 1 Hasta cantidadAtenciones Hacer
								
                                Si codigosAtenciones[i] = codigo Entonces
                                    posicion <- i
                                FinSi
								
                            FinPara
							
                            Si posicion = 0 Entonces
								
                                Escribir "Atencion no encontrada."
								
                            SiNo
								
                                Escribir "======================================"
                                Escribir "          DATOS DE ATENCION"
                                Escribir "======================================"
								
                                Escribir "Codigo: ", codigosAtenciones[posicion]
                                Escribir "Codigo de mascota: ", codigosMascotasAtenciones[posicion]
								
                                Para i <- 1 Hasta cantidadMascotas Hacer
									
                                    Si codigosMascotas[i] = codigosMascotasAtenciones[posicion] Entonces
										
                                        Escribir "Mascota: ", mascotas[i]
                                        Escribir "Especie: ", especies[i]
										
                                        Para k <- 1 Hasta cantidadPropietarios Hacer
											
                                            Si codigosPropietarios[k] = codigosPropietariosMascotas[i] Entonces
												
                                                Escribir "Propietario: ", propietarios[k]
                                                Escribir "Telefono: ", telefonos[k]
												
                                            FinSi
											
                                        FinPara
										
                                    FinSi
									
                                FinPara
								
                                Escribir "Especialidad: ", especialidades[posicion]
                                Escribir "Veterinario: ", veterinarios[posicion]
                                Escribir "Fecha: ", fechasAtenciones[posicion]
                                Escribir "Hora: ", horasAtenciones[posicion]
                                Escribir "Motivo: ", motivos[posicion]
                                Escribir "Costo: ", costosAtenciones[posicion]
                                Escribir "Situacion: ", situaciones[posicion]
                                Escribir "Diagnostico: ", diagnosticos[posicion]
                                Escribir "Tratamiento: ", tratamientos[posicion]
                                Escribir "Estado: ", estadosAtenciones[posicion]
								
                            FinSi
							
                            Esperar Tecla
							
							// =================================
							// 3. MODIFICAR ATENCION
							// =================================
							
                        3:
							
                            Escribir "Ingrese codigo de atencion:"
                            Leer codigo
							
                            posicion <- 0
							
                            Para i <- 1 Hasta cantidadAtenciones Hacer
								
                                Si codigosAtenciones[i] = codigo Entonces
                                    posicion <- i
                                FinSi
								
                            FinPara
							
                            Si posicion = 0 Entonces
								
                                Escribir "Atencion no encontrada."
								
                            SiNo
								
                                Si estadosAtenciones[posicion] = "Anulada" Entonces
									
                                    Escribir "No se puede modificar una atencion anulada."
									
                                SiNo
									
                                    Escribir "======================================"
                                    Escribir "          MODIFICAR ATENCION"
                                    Escribir "======================================"
                                    Escribir "1. Especialidad"
                                    Escribir "2. Veterinario"
                                    Escribir "3. Fecha"
                                    Escribir "4. Hora"
                                    Escribir "5. Motivo"
                                    Escribir "6. Costo"
                                    Escribir "7. Diagnostico"
                                    Escribir "8. Tratamiento"
                                    Escribir "9. Marcar como realizada"
                                    Escribir "0. Cancelar"
                                    Leer opcionModificar
									
                                    dato <- ""
                                    dato2 <- ""
									
                                    Segun opcionModificar Hacer
										
                                        1:
                                            Escribir "Nueva especialidad:"
                                            Leer dato
											
                                        2:
                                            Escribir "Nuevo veterinario:"
                                            Leer dato
											
                                        3:
                                            Escribir "Nueva fecha:"
                                            Leer dato
											
                                        4:
                                            Escribir "Nueva hora:"
                                            Leer dato
											
                                        5:
                                            Escribir "Nuevo motivo:"
                                            Leer dato
											
                                        6:
                                            Escribir "Nuevo costo:"
                                            Leer nuevoCosto
											
                                        7:
                                            Si situaciones[posicion] = "Realizada" Entonces
                                                Escribir "Nuevo diagnostico:"
                                                Leer dato
                                            SiNo
                                                Escribir "La atencion aun no esta realizada."
                                            FinSi
											
                                        8:
                                            Si situaciones[posicion] = "Realizada" Entonces
                                                Escribir "Nuevo tratamiento:"
                                                Leer dato
                                            SiNo
                                                Escribir "La atencion aun no esta realizada."
                                            FinSi
											
                                        9:
                                            Si situaciones[posicion] = "Realizada" Entonces
												
                                                Escribir "La atencion ya esta realizada."
												
                                            SiNo
												
                                                Escribir "Ingrese diagnostico:"
                                                Leer dato
												
                                                Escribir "Ingrese tratamiento:"
                                                Leer dato2
												
                                            FinSi
											
                                        0:
                                            Escribir "Modificacion cancelada."
											
                                        De Otro Modo:
                                            Escribir "Opcion no valida."
											
                                    FinSegun
									
                                    // CONFIRMAR Y GUARDAR CAMBIOS
									
                                    Si opcionModificar >= 1 Y opcionModificar <= 9 Entonces
										
                                        Si opcionModificar = 6 Entonces
											
                                            Si nuevoCosto < 0 Entonces
												
                                                Escribir "El costo no puede ser negativo."
												
                                            SiNo
												
                                                Escribir "¿Confirma los cambios? (S/N)"
                                                Leer respuesta
												
                                                Si Mayusculas(respuesta) = "S" O Mayusculas(respuesta) = "SI" Entonces
													
                                                    costosAtenciones[posicion] <- nuevoCosto
                                                    Escribir "Costo actualizado correctamente."
													
                                                SiNo
													
                                                    Escribir "Modificacion cancelada."
													
                                                FinSi
												
                                            FinSi
											
                                        SiNo
											
                                            Si opcionModificar = 7 O opcionModificar = 8 Entonces
												
                                                Si situaciones[posicion] = "Realizada" Entonces
													
                                                    Escribir "¿Confirma los cambios? (S/N)"
                                                    Leer respuesta
													
                                                    Si Mayusculas(respuesta) = "S" O Mayusculas(respuesta) = "SI" Entonces
														
                                                        Si opcionModificar = 7 Entonces
                                                            diagnosticos[posicion] <- dato
                                                        SiNo
                                                            tratamientos[posicion] <- dato
                                                        FinSi
														
                                                        Escribir "Cambios guardados correctamente."
														
                                                    SiNo
														
                                                        Escribir "Modificacion cancelada."
														
                                                    FinSi
													
                                                FinSi
												
                                            SiNo
												
                                                Si opcionModificar = 9 Entonces
													
                                                    Si situaciones[posicion] = "Programada" Entonces
														
                                                        Escribir "¿Confirma los cambios? (S/N)"
                                                        Leer respuesta
														
                                                        Si Mayusculas(respuesta) = "S" O Mayusculas(respuesta) = "SI" Entonces
															
                                                            diagnosticos[posicion] <- dato
                                                            tratamientos[posicion] <- dato2
                                                            situaciones[posicion] <- "Realizada"
															
                                                            Escribir "Atencion marcada como realizada."
															
                                                        SiNo
															
                                                            Escribir "Modificacion cancelada."
															
                                                        FinSi
														
                                                    FinSi
													
                                                SiNo
													
                                                    Escribir "¿Confirma los cambios? (S/N)"
                                                    Leer respuesta
													
                                                    Si Mayusculas(respuesta) = "S" O Mayusculas(respuesta) = "SI" Entonces
														
                                                        Segun opcionModificar Hacer
															
                                                            1:
                                                                especialidades[posicion] <- dato
																
                                                            2:
                                                                veterinarios[posicion] <- dato
																
                                                            3:
                                                                fechasAtenciones[posicion] <- dato
																
                                                            4:
                                                                horasAtenciones[posicion] <- dato
																
                                                            5:
                                                                motivos[posicion] <- dato
																
                                                        FinSegun
														
                                                        Escribir "Cambios guardados correctamente."
														
                                                    SiNo
														
                                                        Escribir "Modificacion cancelada."
														
                                                    FinSi
													
                                                FinSi
												
                                            FinSi
											
                                        FinSi
										
                                    FinSi
									
                                FinSi
								
                            FinSi
							
                            Esperar Tecla
							
							// =================================
							// 4. ANULAR ATENCION
							// =================================
							
                        4:
							
                            Escribir "Ingrese codigo de atencion:"
                            Leer codigo
							
                            posicion <- 0
							
                            Para i <- 1 Hasta cantidadAtenciones Hacer
								
                                Si codigosAtenciones[i] = codigo Entonces
                                    posicion <- i
                                FinSi
								
                            FinPara
							
                            Si posicion = 0 Entonces
								
                                Escribir "Atencion no encontrada."
								
                            SiNo
								
                                Si estadosAtenciones[posicion] = "Anulada" Entonces
									
                                    Escribir "La atencion ya se encuentra anulada."
									
                                SiNo
									
                                    Escribir "¿Desea anular esta atencion? (S/N)"
                                    Leer respuesta
									
                                    Si Mayusculas(respuesta) = "S" O Mayusculas(respuesta) = "SI" Entonces
										
                                        estadosAtenciones[posicion] <- "Anulada"
                                        Escribir "Atencion anulada correctamente."
										
                                    SiNo
										
                                        Escribir "Operacion cancelada."
										
                                    FinSi
									
                                FinSi
								
                            FinSi
							
                            Esperar Tecla
							
							// =================================
							// 5. MOSTRAR ATENCIONES
							// =================================
							
                        5:
							
                            Si cantidadAtenciones = 0 Entonces
								
                                Escribir "No hay atenciones registradas."
								
                            SiNo
								
                                Escribir "======================================"
                                Escribir "          LISTA DE ATENCIONES"
                                Escribir "======================================"
								
                                Para i <- 1 Hasta cantidadAtenciones Hacer
									
                                    Escribir "--------------------------------------"
                                    Escribir "Codigo: ", codigosAtenciones[i]
                                    Escribir "Codigo de mascota: ", codigosMascotasAtenciones[i]
									
                                    Para j <- 1 Hasta cantidadMascotas Hacer
										
                                        Si codigosMascotas[j] = codigosMascotasAtenciones[i] Entonces
											
                                            Escribir "Mascota: ", mascotas[j]
                                            Escribir "Especie: ", especies[j]
											
                                            Para k <- 1 Hasta cantidadPropietarios Hacer
												
                                                Si codigosPropietarios[k] = codigosPropietariosMascotas[j] Entonces
                                                    Escribir "Propietario: ", propietarios[k]
                                                FinSi
												
                                            FinPara
											
                                        FinSi
										
                                    FinPara
									
                                    Escribir "Especialidad: ", especialidades[i]
                                    Escribir "Veterinario: ", veterinarios[i]
                                    Escribir "Fecha: ", fechasAtenciones[i]
                                    Escribir "Hora: ", horasAtenciones[i]
                                    Escribir "Motivo: ", motivos[i]
                                    Escribir "Costo: ", costosAtenciones[i]
                                    Escribir "Situacion: ", situaciones[i]
                                    Escribir "Diagnostico: ", diagnosticos[i]
                                    Escribir "Tratamiento: ", tratamientos[i]
                                    Escribir "Estado: ", estadosAtenciones[i]
									
                                FinPara
								
                            FinSi
							
                            Esperar Tecla
							
                        0:
							
                            Escribir "Regresando al menu principal..."
							
                        De Otro Modo:
							
                            Escribir "Opcion no valida."
                            Esperar Tecla
							
                    FinSegun
					
                Hasta Que opcionAtencion = 0
				
				// =================================
				// MODULO 2: GESTION DE SERVICIOS
				// =================================
				
            2:
				
                Repetir
					
                    Limpiar Pantalla
					
                    Escribir "======================================"
                    Escribir "           GESTION DE SERVICIOS"
                    Escribir "======================================"
                    Escribir "1. Registrar servicio"
                    Escribir "2. Buscar servicio"
                    Escribir "3. Modificar servicio"
                    Escribir "4. Anular servicio"
                    Escribir "5. Mostrar servicios"
                    Escribir "0. Regresar al menu principal"
                    Escribir "======================================"
                    Leer opcionServicio
					
                    Segun opcionServicio Hacer
						
							// =================================
							// 1. REGISTRAR SERVICIO
							// =================================
						
                        1:
							
                            Si cantidadMascotas = 0 Entonces
								
                                Escribir "Primero debe registrar una mascota."
								
                            SiNo
								
                                Si cantidadServicios < 100 Entonces
									
                                    Escribir "Ingrese codigo de mascota (0 para cancelar):"
                                    Leer codigoMascota
									
                                    Si codigoMascota = 0 Entonces
										
                                        Escribir "Registro cancelado."
										
                                    SiNo
										
                                        posicion <- 0
										
                                        Para i <- 1 Hasta cantidadMascotas Hacer
											
                                            Si codigosMascotas[i] = codigoMascota Entonces
                                                posicion <- i
                                            FinSi
											
                                        FinPara
										
                                        Si posicion = 0 Entonces
											
                                            Escribir "Mascota no encontrada."
											
                                        SiNo
											
                                            Escribir "Mascota: ", mascotas[posicion]
											
                                            encontrado <- 0
											
                                            Para i <- 1 Hasta cantidadPropietarios Hacer
												
                                                Si codigosPropietarios[i] = codigosPropietariosMascotas[posicion] Entonces
													
                                                    Escribir "Propietario: ", propietarios[i]
                                                    encontrado <- 1
													
                                                FinSi
												
                                            FinPara
											
                                            Si encontrado = 0 Entonces
												
                                                Escribir "La mascota no tiene un propietario valido."
												
                                            SiNo
												
                                                Escribir "Ingrese nombre del servicio:"
                                                Leer nombresServicios[cantidadServicios + 1]
												
                                                Escribir "Ingrese responsable:"
                                                Leer responsablesServicios[cantidadServicios + 1]
												
                                                Escribir "Ingrese fecha (dd/mm/aaaa):"
                                                Leer fechasServicios[cantidadServicios + 1]
												
                                                Escribir "Ingrese hora (HH:mm):"
                                                Leer horasServicios[cantidadServicios + 1]
												
                                                Escribir "Ingrese costo:"
                                                Leer costosServicios[cantidadServicios + 1]
												
                                                Si costosServicios[cantidadServicios + 1] < 0 Entonces
													
                                                    Escribir "El costo no puede ser negativo. Registro cancelado."
													
                                                SiNo
													
                                                    Escribir "Ingrese observaciones (puede dejar vacio):"
                                                    Leer observacionesServicios[cantidadServicios + 1]
													
                                                    nuevoCodigo <- 3001
													
                                                    Si cantidadServicios > 0 Entonces
														
                                                        Para i <- 1 Hasta cantidadServicios Hacer
															
                                                            Si codigosServicios[i] >= nuevoCodigo Entonces
                                                                nuevoCodigo <- codigosServicios[i] + 1
                                                            FinSi
															
                                                        FinPara
														
                                                    FinSi
													
                                                    Escribir "Codigo de servicio generado: ", nuevoCodigo
													
                                                    Escribir "¿Desea guardar el servicio? (S/N)"
                                                    Leer respuesta
													
                                                    Si Mayusculas(respuesta) = "S" O Mayusculas(respuesta) = "SI" Entonces
														
                                                        cantidadServicios <- cantidadServicios + 1
														
                                                        codigosServicios[cantidadServicios] <- nuevoCodigo
                                                        codigosMascotasServicios[cantidadServicios] <- codigoMascota
                                                        estadosServicios[cantidadServicios] <- "Activo"
														
                                                        Escribir "Servicio registrado correctamente."
														
                                                    SiNo
														
                                                        Escribir "Registro cancelado."
														
                                                    FinSi
													
                                                FinSi
												
                                            FinSi
											
                                        FinSi
										
                                    FinSi
									
                                SiNo
									
                                    Escribir "Se alcanzo el limite de servicios."
									
                                FinSi
								
                            FinSi
							
                            Esperar Tecla
							
							// =================================
							// 2. BUSCAR SERVICIO
							// =================================
							
                        2:
							
                            Escribir "Ingrese codigo de servicio:"
                            Leer codigo
							
                            posicion <- 0
							
                            Para i <- 1 Hasta cantidadServicios Hacer
								
                                Si codigosServicios[i] = codigo Entonces
                                    posicion <- i
                                FinSi
								
                            FinPara
							
                            Si posicion = 0 Entonces
								
                                Escribir "Servicio no encontrado."
								
                            SiNo
								
                                Escribir "======================================"
                                Escribir "         DATOS DEL SERVICIO"
                                Escribir "======================================"
								
                                Escribir "Codigo: ", codigosServicios[posicion]
                                Escribir "Codigo de mascota: ", codigosMascotasServicios[posicion]
								
                                encontrado <- 0
								
                                Para i <- 1 Hasta cantidadMascotas Hacer
									
                                    Si codigosMascotas[i] = codigosMascotasServicios[posicion] Entonces
										
                                        Escribir "Mascota: ", mascotas[i]
										
                                        Para k <- 1 Hasta cantidadPropietarios Hacer
											
                                            Si codigosPropietarios[k] = codigosPropietariosMascotas[i] Entonces
                                                Escribir "Propietario: ", propietarios[k]
                                            FinSi
											
                                        FinPara
										
                                        encontrado <- 1
										
                                    FinSi
									
                                FinPara
								
                                Si encontrado = 0 Entonces
                                    Escribir "Mascota no encontrada: referencia pendiente de revisar."
                                FinSi
								
                                Escribir "Servicio: ", nombresServicios[posicion]
                                Escribir "Responsable: ", responsablesServicios[posicion]
                                Escribir "Fecha: ", fechasServicios[posicion]
                                Escribir "Hora: ", horasServicios[posicion]
                                Escribir "Costo: ", costosServicios[posicion]
                                Escribir "Observaciones: ", observacionesServicios[posicion]
                                Escribir "Estado: ", estadosServicios[posicion]
								
                            FinSi
							
                            Esperar Tecla
							
							// =================================
							// 3. MODIFICAR SERVICIO
							// =================================
							
                        3:
							
                            Escribir "Ingrese codigo de servicio:"
                            Leer codigo
							
                            posicion <- 0
							
                            Para i <- 1 Hasta cantidadServicios Hacer
								
                                Si codigosServicios[i] = codigo Entonces
                                    posicion <- i
                                FinSi
								
                            FinPara
							
                            Si posicion = 0 Entonces
								
                                Escribir "Servicio no encontrado."
								
                            SiNo
								
                                Si estadosServicios[posicion] = "Anulado" Entonces
									
                                    Escribir "No se puede modificar un servicio anulado."
									
                                SiNo
									
                                    Escribir "======================================"
                                    Escribir "        MODIFICAR SERVICIO"
                                    Escribir "======================================"
                                    Escribir "1. Servicio"
                                    Escribir "2. Responsable"
                                    Escribir "3. Fecha"
                                    Escribir "4. Hora"
                                    Escribir "5. Costo"
                                    Escribir "6. Observaciones"
                                    Escribir "0. Cancelar"
                                    Escribir "Seleccione el dato que desea modificar:"
                                    Leer opcionModificar
									
                                    Segun opcionModificar Hacer
										
                                        1:
                                            Escribir "Ingrese nuevo nombre del servicio:"
                                            Leer dato
											
                                        2:
                                            Escribir "Ingrese nuevo responsable:"
                                            Leer dato
											
                                        3:
                                            Escribir "Ingrese nueva fecha:"
                                            Leer dato
											
                                        4:
                                            Escribir "Ingrese nueva hora:"
                                            Leer dato
											
                                        5:
                                            Escribir "Ingrese nuevo costo:"
                                            Leer nuevoCosto
											
                                        6:
                                            Escribir "Ingrese nuevas observaciones:"
                                            Leer dato
											
                                        0:
                                            Escribir "Modificacion cancelada."
											
                                        De Otro Modo:
                                            Escribir "Opcion no valida."
											
                                    FinSegun
									
                                    Si opcionModificar >= 1 Y opcionModificar <= 6 Entonces
										
                                        Si opcionModificar = 5 Entonces
											
                                            Escribir "¿Confirma los cambios? (S/N)"
                                            Leer respuesta
											
                                            Si Mayusculas(respuesta) = "S" O Mayusculas(respuesta) = "SI" Entonces
												
                                                Si nuevoCosto >= 0 Entonces
													
                                                    costosServicios[posicion] <- nuevoCosto
                                                    Escribir "Costo modificado correctamente."
													
                                                SiNo
													
                                                    Escribir "El costo no puede ser negativo."
													
                                                FinSi
												
                                            SiNo
												
                                                Escribir "Modificacion cancelada."
												
                                            FinSi
											
                                        SiNo
											
                                            Si dato = "" Y opcionModificar <> 6 Entonces
												
                                                Escribir "El dato no puede estar vacio."
												
                                            SiNo
												
                                                Escribir "¿Confirma los cambios? (S/N)"
                                                Leer respuesta
												
                                                Si Mayusculas(respuesta) = "S" O Mayusculas(respuesta) = "SI" Entonces
													
                                                    Segun opcionModificar Hacer
														
                                                        1:
                                                            nombresServicios[posicion] <- dato
															
                                                        2:
                                                            responsablesServicios[posicion] <- dato
															
                                                        3:
                                                            fechasServicios[posicion] <- dato
															
                                                        4:
                                                            horasServicios[posicion] <- dato
															
                                                        6:
                                                            observacionesServicios[posicion] <- dato
															
                                                    FinSegun
													
                                                    Escribir "Servicio modificado correctamente."
													
                                                SiNo
													
                                                    Escribir "Modificacion cancelada."
													
                                                FinSi
												
                                            FinSi
											
                                        FinSi
										
                                    FinSi
									
                                FinSi
								
                            FinSi
							
                            Esperar Tecla
							
							// =================================
							// 4. ANULAR SERVICIO
							// =================================
							
                        4:
							
                            Escribir "Ingrese codigo de servicio:"
                            Leer codigo
							
                            posicion <- 0
							
                            Para i <- 1 Hasta cantidadServicios Hacer
								
                                Si codigosServicios[i] = codigo Entonces
                                    posicion <- i
                                FinSi
								
                            FinPara
							
                            Si posicion = 0 Entonces
								
                                Escribir "Servicio no encontrado."
								
                            SiNo
								
                                Si estadosServicios[posicion] = "Anulado" Entonces
									
                                    Escribir "El servicio ya se encuentra anulado."
									
                                SiNo
									
                                    Escribir "Servicio: ", nombresServicios[posicion]
                                    Escribir "Responsable: ", responsablesServicios[posicion]
									
                                    Escribir "¿Desea anular este servicio? (S/N)"
                                    Leer respuesta
									
                                    Si Mayusculas(respuesta) = "S" O Mayusculas(respuesta) = "SI" Entonces
										
                                        estadosServicios[posicion] <- "Anulado"
                                        Escribir "Servicio anulado correctamente."
										
                                    SiNo
										
                                        Escribir "Operacion cancelada."
										
                                    FinSi
									
                                FinSi
								
                            FinSi
							
                            Esperar Tecla
							
							// =================================
							// 5. MOSTRAR SERVICIOS
							// =================================
							
                        5:
							
                            Si cantidadServicios = 0 Entonces
								
                                Escribir "No hay servicios registrados."
								
                            SiNo
								
                                Escribir "======================================"
                                Escribir "       LISTA DE SERVICIOS"
                                Escribir "======================================"
								
                                Para i <- 1 Hasta cantidadServicios Hacer
									
                                    Escribir "--------------------------------------"
                                    Escribir "Codigo: ", codigosServicios[i]
                                    Escribir "Codigo de mascota: ", codigosMascotasServicios[i]
                                    Escribir "Servicio: ", nombresServicios[i]
                                    Escribir "Responsable: ", responsablesServicios[i]
                                    Escribir "Fecha: ", fechasServicios[i]
                                    Escribir "Hora: ", horasServicios[i]
                                    Escribir "Costo: ", costosServicios[i]
                                    Escribir "Observaciones: ", observacionesServicios[i]
                                    Escribir "Estado: ", estadosServicios[i]
									
                                FinPara
								
                            FinSi
							
                            Esperar Tecla
							
                        0:
							
                            Escribir "Regresando al menu principal..."
							
                        De Otro Modo:
							
                            Escribir "Opcion no valida."
                            Esperar Tecla
							
                    FinSegun
					
                Hasta Que opcionServicio = 0
				
            0:
				
                Escribir "Saliendo del sistema..."
				
            De Otro Modo:
				
                Escribir "Opcion no valida."
                Esperar Tecla
				
        FinSegun
		
    Hasta Que opcionPrincipal = 0
	
FinAlgoritmo
