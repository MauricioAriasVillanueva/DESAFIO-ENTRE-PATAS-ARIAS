# Entre Patas – Arias · Prototipo de registros

Versión temprana de la aplicación veterinaria con un menú de siete opciones y operaciones en memoria.

## Funcionalidades disponibles

| Operación | Alcance en esta versión |
| --- | --- |
| Registrar | Añade propietario y mascota con códigos automáticos |
| Buscar | Consulta una mascota por código y muestra datos asociados |
| Modificar y eliminar | Actualiza o quita el registro localizado |
| Mostrar y ordenar | Lista registros y los ordena por código de mascota |

Esta versión usa listas mientras el programa está abierto. No incluye archivos CSV, respaldos, módulos clínicos ni Pet Shop. La lectura del menú utiliza `Convert.ToInt32`, por lo que una entrada no numérica puede interrumpir la ejecución.

## Archivos

```text
VetEntrePatasArias/
└── Program.cs
README.md
```

Para ejecutar este código con .NET, incorpóralo a un proyecto de consola local: la rama no incluye archivo `.csproj`.
