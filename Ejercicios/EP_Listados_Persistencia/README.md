# Ejercicios individuales anteriores: listados, burbuja y CSV

Aquí conservé **sin modificar su contenido** los archivos con los que practiqué listado de registros, ordenamiento por burbuja, validaciones y persistencia en CSV.

## Material incluido

- `PSeInt/Modulos/01_Ordenamiento.psc`: recorrido y ordenamiento de registros.
- `PSeInt/Modulos/02_Persistencia.psc`: representación de la carga, validación y presentación de datos CSV.
- `PSeInt/SistemaVeterinaria_EP.psc`: base antigua de PSeInt.
- `VetEntrePatasArias/Program.cs`: versión anterior de mi código C# con ejercicios de listado, ordenamiento y CSV.
- `VetEntrePatasArias/VetEntrePatasArias.csproj`: proyecto C# de esa versión.
- `VetEntrePatasArias/Datos/`: archivos CSV usados para probarla.

## Uso como referencia para Piero

Este código puede servirle a Piero para estudiar mis implementaciones de **ordenamiento por burbuja** y **persistencia CSV** antes de crear las versiones finales.

**No copiar directamente al EF:** en este ejercicio propietario y mascota se relacionaban por la misma posición; en el EF real las mascotas guardan una referencia al **código del propietario** y un propietario puede tener varias mascotas.

Para el EF hay que adaptar el ordenamiento para comparar **código o nombre** mediante **burbuja mejorada**, intercambiando *todos* los datos de cada mascota sin romper sus asociaciones. La persistencia también debe cubrir la estructura nueva, más las operaciones de respaldo binario acordadas para el proyecto.

Este directorio es histórico y está separado del código actual en la raíz de la rama.
