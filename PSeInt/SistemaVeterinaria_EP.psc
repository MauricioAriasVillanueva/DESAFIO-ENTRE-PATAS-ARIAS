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

    Hasta Que opcion = 7

FinAlgoritmo