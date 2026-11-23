# Entre Patas – Arias | Integración del EF

Sistema de gestión veterinaria de consola en C# para Fundamentos de Programación.
La rama `ep/integracion` reúne los módulos del equipo mediante `partial class Program`
y listas paralelas compartidas. Las relaciones usan códigos, no posiciones de otras listas.

## Responsabilidades y estado

| Integrante | Archivos C# integrados | PSeInt individual |
| --- | --- | --- |
| Mauricio | `Propietarios.cs`, `Mascotas.cs` | `01_Propietarios_Mascotas.psc`, desarrollado |
| Joaquín | `Atenciones.cs`, `Servicios.cs` | `02_Atenciones_Servicios.psc`, desarrollado |
| Máximo | `Productos.cs`, `Ventas.cs`, `Historial.cs` | `03_PetShop_Historial.psc`, plantilla pendiente |
| Piero | `Listados.cs`, `Persistencia.cs` | `04_Listados_Persistencia.psc`, código presente; requiere corregir sus dos declaraciones de algoritmo y verificar ejecución |

Los nueve módulos C# están conectados desde `VetEntrePatasArias/Program.cs`.
Los `.psc` son demostraciones independientes: no se importan ni comparten datos
automáticamente. `PSeInt/SistemaVeterinaria_EF.psc` conserva el esquema de navegación.

## Ejecutar el sistema

Requiere **.NET SDK 10**. Desde la raíz del repositorio:

```powershell
dotnet build .\VetEntrePatasArias\VetEntrePatasArias.csproj
dotnet run --project .\VetEntrePatasArias\VetEntrePatasArias.csproj
```

Si esta carpeta es un clon de Git, actualizar primero con:

```powershell
git switch ep/integracion
git pull --ff-only origin ep/integracion
```

Reemplazar los archivos originales al actualizar módulos. Guardar los respaldos
fuera de `VetEntrePatasArias`: cualquier copia `.cs` dentro del proyecto también
se compila y puede producir los errores de duplicación CS0102/CS0111.

## Funcionalidades integradas

- **Propietarios y mascotas:** registro, búsqueda, modificación y eliminación;
  varias mascotas por propietario; validaciones de DNI, teléfono, correo y nacimiento.
- **Atenciones:** registro, búsqueda, modificación y anulación; situación programada
  o realizada, fecha y hora coherentes; diagnóstico y tratamiento para realizadas.
- **Servicios:** registro, búsqueda, modificación y anulación, asociados a mascotas.
- **Productos:** registro desde el código 4001, búsqueda por código o nombre,
  modificación, eliminación y entradas/salidas de stock. Precio con punto decimal.
- **Ventas:** códigos desde 5001, control de stock, confirmación y consulta.
  Venta libre con propietario/mascota `0`, o asociación opcional a un propietario
  existente y a una mascota que le pertenezca. Conserva precio y nombre de la venta.
- **Historial:** atenciones y servicios por código de mascota, incluidos los anulados.
- **Listados:** las seis entidades; ordenamiento burbuja por código o nombre de mascota,
  intercambiando todos sus datos paralelos juntos.
- **Persistencia:** guardar/cargar CSV, crear/restaurar respaldo binario y consultar
  el estado de las seis entidades.

Se impide borrar productos con ventas, propietarios con mascotas o ventas y mascotas
con atenciones, servicios o ventas. Una venta libre no bloquea registros ajenos.
Cancelar una venta conserva el stock y las listas. Se rechazan cantidades insuficientes
y desbordamientos en el importe o las entradas de stock.

## Guardado y recuperación

La carpeta `Datos` se crea en el directorio desde el cual se ejecuta el programa.
Mantener ese mismo directorio al guardar y cargar. La carga es manual desde el menú 8;
no existe guardado automático al salir.

| Formato | Contenido | Recuperación |
| --- | --- | --- |
| CSV | Los seis archivos: `propietarios.csv`, `mascotas.csv`, `atenciones.csv`, `servicios.csv`, `productos.csv`, `ventas.csv` | Requiere el conjunto completo; valida registros y relaciones antes de reemplazar memoria |
| Binario v3 | Las seis entidades y stock en `respaldo_vet.bin` | Valida el respaldo completo; reconstruye productos vendidos y siguientes códigos |
| Binario v2 antiguo | Propietarios, mascotas, atenciones y servicios | Compatible solo si no hay productos ni ventas en memoria, para evitar perder Pet Shop |

La restauración solicita confirmación si hay datos en memoria. Los archivos truncados,
con bytes residuales o referencias inválidas se rechazan conservando la memoria.
Los binarios v3 requieren esta versión del sistema: las versiones anteriores solo leen v2.

Limitaciones actuales: no se guarda un conjunto totalmente vacío; los seis CSV se
reemplazan secuencialmente, por lo que una interrupción durante ese reemplazo puede
dejar archivos de distintas versiones. Para recuperar el conjunto en un solo archivo,
usar el respaldo binario. No se incluye recuperación frente a cualquier fallo de disco.

## Verificación de esta integración

Compilación realizada con .NET SDK 10.0.100: **0 errores y 0 advertencias**.
Pruebas reproducibles, sin paquetes adicionales y con datos ficticios en un directorio temporal:

```powershell
dotnet run --project .\tests\Integracion\Integracion.csproj
```

36 comprobaciones: primer producto, precio con cultura es-ES, validaciones y cancelaciones,
stock, ventas libres/asociadas, mascota de otro propietario, eliminación protegida,
registro clínico, ordenamiento y correspondencia entre listas, historial con anulados,
seis listados, CSV con comas/comillas, recuperación completa binaria, rechazo de corrupción,
confirmación y compatibilidad con respaldos v2. No representan cobertura de todas las entradas posibles.

## Estructura

- `VetEntrePatasArias/Program.cs`: menú y listas de propietarios/mascotas.
- `VetEntrePatasArias/Modulos/`: nueve módulos C# del equipo.
- `PSeInt/Modulos/`: pseudocódigos individuales.
- `tests/Integracion/`: comprobaciones automáticas del C# integrado.

Las incorporaciones a `ep/integracion` deben revisar los archivos correspondientes,
evitando merges generales que arrastren ejercicios o versiones antiguas.
