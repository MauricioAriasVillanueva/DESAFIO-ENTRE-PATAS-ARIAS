# Entre Patas – Arias | Integrante 1

Esta rama conserva mis **ejercicios anteriores de programación** y presenta mi **aporte al examen final (EF)** dentro de la aplicación veterinaria integrada. Mi trabajo para el EF se concentra en **Propietarios y Mascotas**.

## Mi aporte al EF

### Gestión de propietarios — [Propietarios.cs](VetEntrePatasArias/Modulos/Propietarios.cs)

Implementé las operaciones para registrar propietarios, buscarlos por código, modificar su información, eliminarlos y consultar las mascotas que tienen asociadas. El registro contempla nombre, DNI, teléfono, dirección y correo, además de validaciones para evitar datos incorrectos o duplicados cuando corresponde.

La eliminación verifica si el propietario todavía tiene mascotas vinculadas; si es así, impide borrar el registro para no perder la relación.

### Gestión de mascotas — [Mascotas.cs](VetEntrePatasArias/Modulos/Mascotas.cs)

Implementé el registro de mascotas para propietarios existentes; la búsqueda por **código o nombre**; la modificación de datos y la eliminación. Se validan datos como nombre, tipo de animal, sexo y fecha de nacimiento. Las búsquedas y modificaciones trabajan con las listas ya definidas en el sistema.

### Decisiones de programación que apliqué

- Usé **listas paralelas** para administrar los registros y mantener alineados sus datos.
- Utilicé **códigos automáticos** como identificadores de propietarios y mascotas.
- Relacioné cada mascota con su propietario mediante `codigosPropietariosMascotas`, usando el **código del propietario**, no una coincidencia de posiciones. Un propietario puede tener varias mascotas.
- Organicé la lógica en métodos y mantuve la navegación en el `Program.cs` compartido.
- Utilicé condicionales, ciclos, búsquedas lineales, `switch`, validación de entradas y confirmaciones.

**Archivos de mi aporte al EF:** `VetEntrePatasArias/Modulos/Propietarios.cs` y `VetEntrePatasArias/Modulos/Mascotas.cs`.

El archivo [`PSeInt/Modulos/01_Propietarios_Mascotas.psc`](PSeInt/Modulos/01_Propietarios_Mascotas.psc) está reservado para traducir estos algoritmos a PSeInt; **por ahora es una plantilla pendiente**, no una implementación terminada.

## Ejercicios anteriores que realicé

Conservé los ejercicios que desarrollé previamente en la carpeta [`Ejercicios/EP_Listados_Persistencia/`](Ejercicios/EP_Listados_Persistencia/). Son prácticas individuales y **no forman parte del código actual integrado del EF**.

| Ejercicio | Qué trabajé | Archivo |
| --- | --- | --- |
| Listado de registros | Recorrer y mostrar información de propietarios y mascotas | `PSeInt/Modulos/01_Ordenamiento.psc` y `VetEntrePatasArias/Program.cs` del respaldo |
| Ordenamiento por burbuja | Comparar e intercambiar datos conservando las listas paralelas | Los mismos archivos de ordenamiento |
| Persistencia CSV | Guardar y recuperar registros, verificar datos cargados y actualizar correlativos | `PSeInt/Modulos/02_Persistencia.psc` y `VetEntrePatasArias/Program.cs` del respaldo |
| Datos de prueba | Archivos CSV de propietarios y mascotas | `VetEntrePatasArias/Datos/` del respaldo |

### Referencia para Piero

Piero puede utilizar mis ejercicios de **burbuja, listado y CSV** como punto de partida para sus módulos de ordenamiento y persistencia. No debe copiarlos sin adaptarlos: el ejercicio anterior emparejaba propietarios y mascotas por posición y exigía cantidades equivalentes; el EF utiliza **códigos de propietario asociados a cada mascota** y permite múltiples mascotas por propietario.

Para el EF, el ordenamiento previsto es **burbuja mejorada por código y por nombre**, manteniendo juntos todos los campos de cada mascota, incluido el código del propietario asociado. La persistencia final deberá trabajar con los datos actuales y contemplar también los respaldos binarios, que **no son parte de estos ejercicios anteriores**.

## Cómo está organizada esta rama

```text
VetEntrePatasArias/
├── Program.cs                   # Menú compartido del EF
├── VetEntrePatasArias.csproj
└── Modulos/
    ├── Propietarios.cs          # Mi módulo del EF
    ├── Mascotas.cs              # Mi módulo del EF
    └── ...                      # Módulos compartidos de otros integrantes

PSeInt/
├── SistemaVeterinaria_EP.psc
├── SistemaVeterinaria_EF.psc
└── Modulos/
    └── 01_Propietarios_Mascotas.psc  # Mi PSeInt, pendiente

Ejercicios/
└── EP_Listados_Persistencia/
    ├── PSeInt/
    └── VetEntrePatasArias/
        ├── Program.cs
        ├── VetEntrePatasArias.csproj
        └── Datos/
```

Los demás módulos de C# y PSeInt que aparecen en la raíz se conservan como **base compartida del EF**. No los presento como desarrollos propios; sus responsables están trabajando en otras partes del sistema.

## Ejecutar el EF

Con el SDK de .NET 10, desde la raíz del repositorio:

```powershell
dotnet run --project VetEntrePatasArias/VetEntrePatasArias.csproj
```

La rama [`ep/integracion`](../ep/integracion) es la versión colaborativa utilizada por el equipo. Esta rama, `Integrante-1`, está organizada para mostrar mi trabajo individual y conservar mis ejercicios como referencia sin sobrescribir la integración.
