#  Entre Patas – Arias | Integrante 4

Rama individual correspondiente al desarrollo de funcionalidades del sistema de gestión veterinaria **Entre Patas – Arias**.

Este apartado documenta las funcionalidades implementadas por el Integrante 4, relacionadas con la búsqueda de mascotas y propietarios, la administración del Pet Shop y la consulta del historial clínico.

## Funcionalidades desarrolladas

###  1. Módulo Pet Shop

| Funcionalidad | Comportamiento |
|---|---|
| Registrar productos | Incorpora nuevos productos al inventario. |
| Buscar productos | Consulta productos registrados. |
| Modificar productos | Actualiza la información de los productos. |
| Eliminar productos | Controla que no existan ventas vinculadas antes de eliminarlos. |
| Gestionar stock | Permite registrar movimientos de inventario. |
| Registrar ventas | Registra ventas y actualiza las cantidades disponibles. |
| Consultar ventas | Permite consultar la información de una venta registrada. |

El módulo incorpora validaciones para controlar precios, cantidades, disponibilidad de stock y relaciones entre productos, ventas, propietarios y mascotas.

###  2. Historial clínico

| Funcionalidad | Comportamiento |
|---|---|
| Buscar historial | Permite consultar el historial mediante el código de una mascota. |
| Datos de la mascota | Muestra la información general de la mascota seleccionada. |
| Atenciones médicas | Consulta las atenciones asociadas a la mascota. |
| Servicios veterinarios | Consulta los servicios registrados para la mascota. |
| Validación de registros | Comprueba la integridad de las listas antes de mostrar la información. |

La consulta reúne la información clínica relacionada con una mascota y permite identificar si existen atenciones médicas o servicios registrados.

## Organización de archivos

```text
Integrante-4/
│
├── Modulos/
│   ├── Productos.cs
│   ├── Ventas.cs
│   ├── Historial.cs
│   ├── Mascotas.cs
│   └── Propietarios.cs
│
├── BusquedaMascotas_Integrante4.psc
├── PetShop_Historial_Integrante4.psc
├── Program.cs
└── README.md
```

### Descripción de archivos

| Archivo | Descripción |
|---|---|
| `Productos.cs` | Funciones de administración de productos e inventario. |
| `Ventas.cs` | Registro y consulta de ventas del Pet Shop. |
| `Historial.cs` | Consulta de atenciones médicas y servicios veterinarios. |
| `Mascotas.cs` | Registro, búsqueda y administración de mascotas. |
| `Propietarios.cs` | Funciones relacionadas con los propietarios. |
| `BusquedaMascotas_Integrante4.psc` | Pseudocódigo del módulo de búsquedas. |
| `PetShop_Historial_Integrante4.psc` | Pseudocódigo de Pet Shop e historial clínico. |
| `Program.cs` | Código principal y organización de las operaciones. |

## Tecnologías y conceptos aplicados

- **C#:** implementación de las funcionalidades.
- **PSeInt:** elaboración de pseudocódigos.
- **Git y GitHub:** control de versiones y trabajo colaborativo.
- **Listas:** almacenamiento y administración de información.
- **Programación modular:** organización mediante métodos.
- **Validaciones:** control de entradas y consistencia de datos.
- **Búsquedas:** localización de registros mediante criterios específicos.

## Integración del proyecto

El desarrollo individual se realizó en la rama `Integrante-4`.

Los avances correspondientes a esta rama fueron incorporados a `ep/integracion`, donde se reúne el trabajo desarrollado por los integrantes del equipo.

## Responsable

**Máximo Arriagada – Integrante 4**

Áreas de responsabilidad:

- Módulo Pet Shop.
- Historial clínico.

---

**Proyecto académico:** Sistema de Gestión de la Veterinaria Entre Patas – Arias.

**Curso:** Fundamentos de Programación.

**Repositorio:** [DESAFIO-ENTRE-PATAS-ARIAS](https://github.com/MauricioAriasVillanueva/DESAFIO-ENTRE-PATAS-ARIAS)
