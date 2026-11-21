# Entre Patas – Arias | Integrante 1

Esta rama documenta **mis aportes individuales** al examen final (EF): **gestión de propietarios y gestión de mascotas**. También conservo, por separado, mis ejercicios anteriores de **listados, ordenamiento por burbuja y persistencia CSV**, que pueden servir como referencia para el trabajo de Piero.

## Lo que desarrollé para el EF

### Propietarios

En [`Propietarios.cs`](VetEntrePatasArias/Modulos/Propietarios.cs) implementé:
- Registrar un propietario, asignando un código automático.
- Buscar propietarios por código.
- Modificar los datos del propietario.
- Eliminar un propietario únicamente si no tiene mascotas asociadas.
- Consultar las mascotas vinculadas a un propietario.
- Validar nombre, DNI, teléfono, dirección y correo electrónico.

### Mascotas

En [`Mascotas.cs`](VetEntrePatasArias/Modulos/Mascotas.cs) implementé:
- Registrar una mascota para un propietario existente.
- Buscar una mascota por código o nombre.
- Modificar datos, incluida la asociación con el propietario.
- Eliminar una mascota y sus datos asociados en listas paralelas.
- Validar tipo de animal, sexo y fecha de nacimiento.

Los datos se manejan con **listas paralelas** y métodos de búsqueda lineal. La lista `codigosPropietariosMascotas` asocia cada mascota al **código de su propietario**, lo que permite tener varias mascotas para una misma persona, o propietarios sin mascotas.

El archivo [`Program.cs`](VetEntrePatasArias/Program.cs) conserva **el menú y la navegación de nueve opciones** de [`ep/integracion`](https://github.com/MauricioAriasVillanueva/DESAFIO-ENTRE-PATAS-ARIAS/tree/ep/integracion). En mi rama personal están conectadas únicamente las operaciones de **Propietarios y Mascotas**. Los otros submenús se mantienen visibles como opciones pendientes, sin depender de los archivos de mis compañeros.

### PSeInt

El archivo [`01_Propietarios_Mascotas.psc`](PSeInt/Modulos/01_Propietarios_Mascotas.psc) está reservado para representar mis algoritmos del EF en pseudocódigo. **Aún es una plantilla**, no contiene la implementación final.

## Ejercicios anteriores: listados, burbuja y CSV

En [`Ejercicios/EP_Listados_Persistencia/`](Ejercicios/EP_Listados_Persistencia/) guardé el código y los datos de prueba que desarrollé anteriormente. Estos archivos se conservaron como referencia y están separados de mi implementación actual del EF.

| Ejercicio propio | Qué desarrollé | Referencia |
| --- | --- | --- |
| Listados | Recorrido y visualización de registros | `PSeInt/Modulos/01_Ordenamiento.psc` y `VetEntrePatasArias/Program.cs` de `Ejercicios/` |
| Burbuja | Comparación e intercambio de datos de las listas paralelas | `PSeInt/Modulos/01_Ordenamiento.psc` y `VetEntrePatasArias/Program.cs` de `Ejercicios/` |
| Persistencia CSV | Guardado, lectura, validación de datos y actualización de códigos | `PSeInt/Modulos/02_Persistencia.psc` y `VetEntrePatasArias/Program.cs` de `Ejercicios/` |
| Pruebas CSV | Datos de propietarios y mascotas para el ejercicio | `VetEntrePatasArias/Datos/` de `Ejercicios/` |

**Referencia para Piero:** puede estudiar estos ejercicios para desarrollar los listados, el ordenamiento y la persistencia del EF, pero **no debe trasladarlos directamente**. El ejercicio anterior emparejaba propietarios y mascotas por posición, mientras que el EF relaciona cada mascota con un código de propietario. El ordenamiento definitivo deberá usar **burbuja mejorada**, por **código y por nombre**, conservando todas las posiciones asociadas. La persistencia final también deberá adaptar el CSV a la nueva estructura e incorporar respaldos binarios.

## Estructura de esta rama

```text
VetEntrePatasArias/
├── Program.cs                      # Menú compartido; solo opciones 1 y 2 funcionales
├── VetEntrePatasArias.csproj
└── Modulos/
    ├── Propietarios.cs              # Mi aporte al EF
    └── Mascotas.cs                  # Mi aporte al EF

PSeInt/
└── Modulos/
    └── 01_Propietarios_Mascotas.psc # Plantilla pendiente

Ejercicios/
└── EP_Listados_Persistencia/
    ├── PSeInt/
    └── VetEntrePatasArias/
        ├── Program.cs
        ├── VetEntrePatasArias.csproj
        └── Datos/
```

## Ejecución

Se requiere el SDK .NET 10. Desde la raíz del repositorio:

```powershell
dotnet run --project VetEntrePatasArias/VetEntrePatasArias.csproj
```

Esta rama **no sustituye al EF integrado**. Mi código de propietarios y mascotas también está incorporado en `ep/integracion`, donde todos los integrantes conectan sus módulos.
