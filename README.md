# Entre Patas – Arias · Modificación y eliminación

Rama individual para trabajar con registros de mascotas y propietarios mediante listas paralelas.

## Funcionalidades disponibles

| Opción | Alcance |
| --- | --- |
| Modificar | Busca por código de mascota y permite cambiar un campo del registro |
| Eliminar | Busca por código, solicita confirmación y retira las posiciones asociadas de las listas |
| Mostrar | Presenta los registros precargados como apoyo para probar el módulo |

Al arrancar se cargan datos de prueba. El menú también anuncia registrar, buscar y ordenar, pero esas opciones no están conectadas. Los cambios solo viven durante la ejecución; no hay archivos de persistencia.

## Archivos

```text
03_Modificar_Eliminar.psc
Program.cs
SistemaVeterinaria_EP.psc
VetEntrePatasArias.csproj
README.md
```

## Ejecución

Requiere el SDK de .NET 10. Desde la raíz de esta rama:

```bash
dotnet run --project VetEntrePatasArias.csproj
```
