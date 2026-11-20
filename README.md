# Entre Patas – Arias · Listados y archivos

Rama individual centrada en mostrar registros, ordenarlos y recuperar información desde archivos CSV.

## Funcionalidades disponibles

| Opción conectada | Comportamiento |
| --- | --- |
| Mostrar registros | Presenta datos de mascotas y propietarios cargados en listas paralelas |
| Ordenar registros | Aplica ordenamiento por nombre de mascota y luego muestra el resultado |
| Guardar datos en CSV | Escribe `propietarios.csv` y `mascotas.csv` si la carga inicial fue exitosa |

Al iniciar, el programa intenta cargar ambos CSV desde `Datos/`, valida la cantidad y el formato de sus líneas y actualiza los siguientes códigos a partir de los registros recuperados. Los menús de registro, búsqueda, modificación y eliminación aparecen, pero no están conectados en esta versión.

**Alcance del formato:** la carga exige el mismo número de filas en ambos archivos y empareja cada propietario y mascota por posición. El guardado separa campos con comas sin escapar valores; conviene evitar comas dentro de los datos. Si no se cargan correctamente los archivos iniciales, la opción de guardado queda bloqueada.

## Archivos

```text
PSeInt/
├── Modulos/
│   ├── 01_Ordenamiento.psc
│   └── 02_Persistencia.psc
└── SistemaVeterinaria_EP.psc
VetEntrePatasArias/
├── Datos/
│   ├── mascotas.csv
│   └── propietarios.csv
├── Program.cs
└── VetEntrePatasArias.csproj
README.md
```

## Ejecución

Requiere el SDK de .NET 10. Para cargar los CSV incluidos, entra en la carpeta del proyecto:

```bash
cd VetEntrePatasArias
dotnet run --project VetEntrePatasArias.csproj
```

Ejecuta el comando desde `VetEntrePatasArias/` si quieres utilizar directamente los CSV incluidos en esa carpeta: `dotnet run --project VetEntrePatasArias.csproj`. Las rutas `Datos/` son relativas al directorio desde el que se inicia el programa.
