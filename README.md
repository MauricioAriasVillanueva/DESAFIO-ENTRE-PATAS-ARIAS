# Entre Patas – Arias · Base colaborativa

Estructura de navegación para la aplicación de consola veterinaria. Esta rama define menús y datos compartidos para desarrollar los módulos.

## Estado funcional

El programa muestra ocho áreas de trabajo y la opción de salida. Acepta opciones numéricas, permite entrar y volver de los submenús y despliega submenús adicionales para productos y ventas de Pet Shop. Al seleccionar operaciones como registrar, buscar o guardar, informa que la funcionalidad está pendiente: aquí aún no se ejecutan esas operaciones.

| Área visible | Estado |
| --- | --- |
| Propietarios y mascotas | Submenús de navegación; sin operaciones conectadas |
| Atenciones, servicios e historial clínico | Opciones visibles; sin operaciones conectadas |
| Pet Shop | Navegación a productos y ventas; sin gestión de datos |
| Listados y administración | Opciones visibles; sin listado ni archivos funcionales |

## Archivos

```text
PSeInt/
├── SistemaVeterinaria_EF.psc
└── SistemaVeterinaria_EP.psc
VetEntrePatasArias/
├── Program.cs
└── VetEntrePatasArias.csproj
README.md
```

Los archivos PSeInt acompañan la propuesta; el menú ejecutable está en `Program.cs`.

## Ejecución

Requiere el SDK de .NET 10. Desde la raíz del repositorio:

```bash
dotnet run --project VetEntrePatasArias/VetEntrePatasArias.csproj
```

## Siguiente paso

Conectar las opciones de navegación con las operaciones de cada módulo y comprobar los recorridos completos.
