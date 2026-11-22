# Entre Patas – Arias | Integrante 1

Esta rama reúne mis aportes individuales al examen final (EF) de Fundamentos de Programación: **gestión de propietarios y gestión de mascotas**. El proyecto grupal se integra en C#, mientras que cada integrante conserva su propio archivo PSeInt para mostrar y explicar su trabajo sin reunir miles de líneas en un único pseudocódigo.

## Mi aporte en C#

Los archivos principales son [`Propietarios.cs`](VetEntrePatasArias/Modulos/Propietarios.cs) y [`Mascotas.cs`](VetEntrePatasArias/Modulos/Mascotas.cs).

### Propietarios
- Registrar propietarios con código automático desde 2001.
- Buscar propietarios por código y mostrar sus datos.
- Modificar nombre, DNI, teléfono, dirección o correo.
- Eliminar propietarios con confirmación, siempre que no tengan mascotas asociadas.
- Consultar las mascotas vinculadas a un propietario.
- Validar datos obligatorios, DNI, teléfono y correo electrónico.

### Mascotas
- Registrar mascotas para propietarios existentes, con código automático desde 1001.
- Buscar mascotas por código o nombre.
- Modificar nombre, tipo, especie o raza, sexo, fecha de nacimiento y propietario.
- Eliminar mascotas con confirmación.
- Validar tipo de animal, sexo y fecha de nacimiento.

La implementación C# usa **listas paralelas**. La lista `codigosPropietariosMascotas` almacena el **código del dueño** de cada mascota; no se asume que propietarios y mascotas estén en la misma posición. Un propietario puede tener varias mascotas o ninguna.

[`VetEntrePatasArias/Program.cs`](VetEntrePatasArias/Program.cs) conserva la navegación general del proyecto. **Solo están conectadas en esta rama las opciones de Propietarios y Mascotas**; las demás corresponden al trabajo de otros integrantes y se integran en `ep/integracion`.

## Mi aporte en PSeInt

**Archivo completo:** [`PSeInt/Modulos/01_Propietarios_Mascotas.psc`](PSeInt/Modulos/01_Propietarios_Mascotas.psc).

Contiene **831 líneas** y un menú individual con nueve operaciones:
1. Registrar propietario.
2. Buscar propietario.
3. Modificar propietario.
4. Eliminar propietario.
5. Ver mascotas asociadas.
6. Registrar mascota.
7. Buscar mascota por código o nombre.
8. Modificar mascota.
9. Eliminar mascota.

Se reutilizan funciones de validación, búsqueda por posición y presentación de datos. Se usan **arreglos paralelos** con referencias por código, validaciones y controles al eliminar registros. Este archivo está pensado para abrirse y ejecutarse **de manera independiente en PSeInt**; no requiere copiar los otros módulos al mismo `.psc`.

La versión de este PSeInt también está subida a [`ep/integracion`](https://github.com/MauricioAriasVillanueva/DESAFIO-ENTRE-PATAS-ARIAS/tree/ep/integracion/PSeInt/Modulos), donde se mantiene separada por integrante. Las pruebas manuales de mi módulo se realizaron durante su desarrollo; el repositorio no incorpora un sistema automatizado de pruebas PSeInt.

## Ejercicios anteriores (EP)

En [`Ejercicios/EP_Listados_Persistencia/`](Ejercicios/EP_Listados_Persistencia/) conservo, **separados del EF**, mis ejercicios anteriores de:
- Listados y visualización de registros.
- Ordenamiento por burbuja sobre datos paralelos.
- Persistencia y lectura de CSV, con datos de prueba.

Estos ejercicios pueden servir de referencia a Piero, pero **no deben incorporarse sin adaptar**: los ejercicios antiguos y el EF no utilizan necesariamente las mismas relaciones entre propietarios y mascotas.

## Estructura

```text
README.md
VetEntrePatasArias/
├── Program.cs
├── VetEntrePatasArias.csproj
└── Modulos/
    ├── Propietarios.cs
    └── Mascotas.cs
PSeInt/
└── Modulos/
    └── 01_Propietarios_Mascotas.psc
Ejercicios/
└── EP_Listados_Persistencia/
    ├── README.md
    ├── PSeInt/
    └── VetEntrePatasArias/
```

## Ejecución

**C#:** requiere el SDK de .NET 10. Desde la raíz del repositorio:

```powershell
dotnet run --project VetEntrePatasArias/VetEntrePatasArias.csproj
```

**PSeInt:** abrir `PSeInt/Modulos/01_Propietarios_Mascotas.psc` en PSeInt y ejecutar el algoritmo (F9). Se desarrolló con arreglos e índices configurados desde 1.

## Diferencia entre ramas

- **`Integrante-1`:** mi código C#, mi PSeInt individual y ejercicios propios anteriores.
- **`ep/integracion`:** punto de integración del C# de todos los integrantes y almacenamiento de los PSeInt individuales por separado.

**Decisión de organización del equipo:** el C# se integra en una aplicación; el PSeInt se entrega **por módulos individuales**, para que el profesor distinga el aporte de cada integrante. `SistemaVeterinaria_EF.psc` es una referencia de menús y **no** reemplaza esos archivos independientes.
