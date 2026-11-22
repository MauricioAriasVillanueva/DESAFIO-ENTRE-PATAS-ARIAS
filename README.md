# Entre Patas – Arias | Integración del EF

Proyecto grupal de **Fundamentos de Programación**: sistema de gestión veterinaria desarrollado en **C# (aplicación de consola)** y documentado en **PSeInt por módulos independientes**.

## Criterio de organización

- **C#:** los integrantes aportan sus archivos `.cs` y las operaciones se conectan desde `VetEntrePatasArias/Program.cs`, usando datos compartidos.
- **PSeInt:** cada integrante mantiene su propio archivo `.psc` ejecutable de forma independiente, para facilitar la evaluación, las pruebas y la exposición individual. **No se fusionarán todos los PSeInt en un único archivo gigante.**
- [`PSeInt/SistemaVeterinaria_EF.psc`](PSeInt/SistemaVeterinaria_EF.psc) conserva un **esquema de navegación y submenús**, pero **no ejecuta las operaciones de los módulos individuales** ni los importa automáticamente.

La integración funcional del sistema grupal corresponde al **C#**. Los archivos PSeInt reflejan, por separado, el desarrollo algorítmico de cada responsable.

## Distribución del trabajo y estado verificado en esta rama

| Integrante | Responsabilidad | C# en `ep/integracion` | PSeInt en `ep/integracion` |
| --- | --- | --- | --- |
| 1 — Mauricio | Propietarios y Mascotas | Implementado y conectado en el menú | **Completo**, `01_Propietarios_Mascotas.psc` (831 líneas) |
| 2 — Joaquín | Atenciones Médicas y Servicios | Implementado y conectado en el menú | **Pendiente de subir**: `02_Atenciones_Servicios.psc` es todavía una plantilla |
| 3 — Máximo | Pet Shop e Historial | Pendiente en los archivos de esta rama | **Plantilla**: `03_PetShop_Historial.psc` |
| 4 — Piero | Listados y Persistencia | Pendiente en los archivos de esta rama | **Plantilla**: `04_Listados_Persistencia.psc` |

Este estado describe los **archivos presentes en la rama**, no el trabajo que los compañeros puedan tener guardado localmente.

## Funcionalidades C# implementadas

### Propietarios
- Registrar, buscar por código, modificar y eliminar con confirmación.
- Consultar mascotas asociadas.
- Validaciones de DNI, teléfono, correo y relación con mascotas.
- Restricción: no eliminar un propietario que tenga mascotas asociadas.

### Mascotas
- Registrar para un propietario existente, buscar por código o nombre, modificar y eliminar.
- Validar tipo, sexo y fecha de nacimiento.
- Asociar por código de propietario, permitiendo varias mascotas para una misma persona.

### Atenciones médicas
- Registrar, buscar, modificar y **anular** atenciones.
- Asociación con mascotas registradas.
- Fecha, hora, costo, especialidad, veterinario y motivo.
- Situación programada/realizada, diagnóstico y tratamiento según corresponda.

### Servicios veterinarios
- Registrar, buscar, modificar y **anular** servicios.
- Asociación con mascotas registradas.
- Responsable, fecha, hora, costo, observaciones y estado.

Las operaciones de los cuatro módulos anteriores están **enlazadas desde `Program.cs`**. Esto no constituye por sí solo una verificación de compilación o pruebas automatizadas.

## Áreas aún pendientes en el C# integrado

Los submenús de **Pet Shop, Historial, Listados y Persistencia** figuran en la navegación, pero sus operaciones todavía no están conectadas. En la revisión actual, `Productos.cs`, `Listados.cs` y `Persistencia.cs` siguen vacíos. No se debe presentar como implementado el registro de productos/ventas, la persistencia CSV ni los respaldos binarios.

## Estructura del proyecto

```text
README.md
VetEntrePatasArias/
├── Program.cs
├── VetEntrePatasArias.csproj
└── Modulos/
    ├── Propietarios.cs
    ├── Mascotas.cs
    ├── Atenciones.cs
    ├── Servicios.cs
    ├── Productos.cs
    ├── Listados.cs
    └── Persistencia.cs
PSeInt/
├── SistemaVeterinaria_EF.psc
└── Modulos/
    ├── 01_Propietarios_Mascotas.psc
    ├── 02_Atenciones_Servicios.psc
    ├── 03_PetShop_Historial.psc
    └── 04_Listados_Persistencia.psc
```

## Cómo ejecutar

**Aplicación C#:** requiere .NET SDK 10. Desde la raíz del repositorio:

```powershell
dotnet run --project VetEntrePatasArias/VetEntrePatasArias.csproj
```

**PSeInt:** abrir por separado el archivo de cada integrante dentro de `PSeInt/Modulos/` y ejecutarlo desde PSeInt (F9). Actualmente el módulo 01 está desarrollado; los módulos 02, 03 y 04 aún figuran como plantillas en esta rama.

> Los archivos `.psc` son demostraciones individuales: **no comparten datos automáticamente entre procesos**. Las relaciones con propietarios y mascotas deben prepararse como datos de prueba dentro de cada módulo cuando se ejecuta por separado.

## Estado del examen final

El avance confirmado es: **Propietarios/Mascotas y Atenciones/Servicios implementados en C#**, y **PSeInt individual de Propietarios/Mascotas subido**. Faltan los demás módulos individuales de PSeInt y las funcionalidades de C# aún sin implementar, además de la revisión final del conjunto.

Cada integrante mantiene su propia rama; las incorporaciones a `ep/integracion` deben limitarse a los archivos de su responsabilidad, sin hacer merges generales que arrastren ejercicios antiguos u otros cambios de las ramas personales.
