# Entre Patas – Arias · Integración

Versión integrada de la aplicación de consola veterinaria. El menú reúne los módulos previstos y conecta las operaciones de propietarios y mascotas.

## Funcionalidades disponibles

| Módulo | Operaciones conectadas |
| --- | --- |
| Propietarios | Registrar, buscar por código, modificar, eliminar y consultar mascotas asociadas |
| Mascotas | Registrar para un propietario existente, buscar por código o nombre, modificar y eliminar |

Los registros se mantienen en listas durante la ejecución. Se asignan códigos automáticos y se validan campos como DNI, teléfono, fecha y asociación con propietarios. El programa impide eliminar un propietario que tenga mascotas asociadas.

## Opciones pendientes

El menú también permite navegar por atenciones médicas, servicios veterinarios, Pet Shop, historial clínico, listados y administración de registros. Las operaciones de estas áreas muestran un aviso de funcionalidad no implementada. En Pet Shop se puede abrir el submenú de productos o ventas, pero no registrar productos, stock ni ventas. Tampoco se guardan CSV ni se crean o restauran respaldos binarios desde este programa.

Los archivos `Atenciones.cs`, `Servicios.cs`, `Productos.cs`, `Listados.cs` y `Persistencia.cs` existen como espacios para esos módulos, pero están vacíos.

## Organización

```text
PSeInt/
└── SistemaVeterinaria_EP.psc
VetEntrePatasArias/
├── Modulos/
│   ├── Atenciones.cs
│   ├── Listados.cs
│   ├── Mascotas.cs
│   ├── Persistencia.cs
│   ├── Productos.cs
│   ├── Propietarios.cs
│   └── Servicios.cs
├── Program.cs
└── VetEntrePatasArias.csproj
README.md
```

`Program.cs` gestiona la navegación; `Propietarios.cs` y `Mascotas.cs` contienen las operaciones conectadas.

## Ejecución

Requiere el SDK de .NET 10. Desde la raíz del repositorio:

```bash
dotnet run --project VetEntrePatasArias/VetEntrePatasArias.csproj
```

## Próximos pasos

Implementar los módulos pendientes y conectar listados, archivos CSV y respaldos binarios antes de presentarlos como disponibles.
