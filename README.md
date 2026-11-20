# Entre Patas – Arias · Versión de registro

Rama de trabajo dedicada al alta conjunta de propietarios y mascotas. Conserva una variante del módulo de registro para revisarla por separado.

## Alcance

La única opción operativa del menú es **registrar propietario y mascota**: recoge los datos, comprueba que el teléfono tenga nueve dígitos, asigna códigos y muestra un resumen. Las opciones de búsqueda, modificación, eliminación, listado y ordenamiento figuran en pantalla, pero no tienen implementación conectada. Los datos permanecen en memoria mientras se ejecuta el programa.

## Archivos

```text
Program.cs
Registro_Propietarios_Mascotas.psc
README.md
```

El código C# está en `Program.cs`. Esta rama no contiene `.csproj`; para ejecutarlo con .NET hay que agregar ese archivo a un proyecto de consola local.
