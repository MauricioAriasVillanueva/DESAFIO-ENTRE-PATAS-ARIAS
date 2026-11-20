# Entre Patas – Arias · Búsquedas

Rama individual para consultar mascotas y propietarios por código.

## Funcionalidades conectadas

| Opción | Comportamiento |
| --- | --- |
| Buscar mascota | Recorre los códigos y muestra los datos asociados cuando encuentra una coincidencia |
| Buscar propietario | Busca por código y muestra nombre y teléfono |

El programa informa cuando no hay registros, cuando el código es inválido o cuando no existe coincidencia. **Esta rama no carga ni registra datos al iniciar**, por lo que las listas están vacías en una ejecución independiente. Las demás opciones impresas en el menú no ejecutan sus operaciones aquí. La navegación termina con la opción 8.

## Archivos

```text
BusquedaMascotas_Integrante4.psc
Program.cs
README.md
```

`Program.cs` necesita incorporarse a un proyecto de consola C# para ejecutarse con .NET, ya que esta rama no contiene `.csproj`.
