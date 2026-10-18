# 🐾 Entre Patas – Arias | Sistema de Gestión Veterinaria

![Estado del Proyecto](https://img.shields.io/badge/Estado-Integración_Parcial-blue.svg)
![CSharp](https://img.shields.io/badge/Lenguaje-C%23-purple.svg)
![PSeInt](https://img.shields.io/badge/Pseudocódigo-PSeInt-orange.svg)
![Equipo](https://img.shields.io/badge/Equipo-4_Integrantes-green.svg)

## 📌 Descripción del proyecto

Este repositorio contiene el desarrollo del **Sistema de Gestión Veterinaria Entre Patas – Arias**, correspondiente a la Evaluación Parcial del curso **Fundamentos de Programación**.

El sistema tiene como propósito facilitar el registro y la administración de información de propietarios y mascotas mediante una aplicación de consola desarrollada en **C#**, acompañada de su representación algorítmica en **PSeInt**.

La rama `ep/integracion` está destinada a reunir los módulos desarrollados por los integrantes del equipo y comprobar su funcionamiento conjunto antes de incorporarlos a la versión principal del proyecto.

---

## 🎯 Objetivo de la integración

Unificar las funcionalidades del sistema veterinario utilizando estructuras de datos compartidas, ciclos, condicionales, funciones y procedimientos.

La integración busca garantizar que:

- Los registros de propietarios y mascotas se almacenen correctamente.
- Los módulos trabajen con los mismos nueve campos de información.
- El listado permita visualizar los registros existentes.
- El ordenamiento alfabético conserve la relación entre cada mascota y su propietario.
- Las funcionalidades puedan ejecutarse desde un menú principal.

---

## ✅ Funcionalidades integradas

| Opción | Funcionalidad | Estado |
|---|---|---|
| 1 | Registrar propietario y mascota | ✅ Implementada |
| 2 | Buscar mascota | 🚧 Pendiente |
| 3 | Modificar registro | 🚧 Pendiente |
| 4 | Eliminar registro | 🚧 Pendiente |
| 5 | Mostrar registros | ✅ Implementada |
| 6 | Ordenar registros alfabéticamente | ✅ Implementada |
| 7 | Salir del sistema | ✅ Implementada |

Las funcionalidades implementadas se encuentran conectadas en los programas de C# y PSeInt.

---

## 🧩 Módulos desarrollados

### 👨‍💻 Integrante 1 — Mauricio Arias

**Responsabilidad: listado y ordenamiento de registros.**

Funcionalidades desarrolladas:

- Implementación del recorrido de listas y arreglos.
- Visualización de información de propietarios y mascotas.
- Implementación del algoritmo de ordenamiento burbuja.
- Comparación alfabética de nombres de mascotas.
- Intercambio de los nueve campos asociados a cada registro.
- Desarrollo de las funcionalidades correspondientes en C# y PSeInt.

**Métodos y subprocesos:**

- C#: `MostrarRegistros()` y `Ordenar()`.
- PSeInt: `MostrarRegistros()` y `OrdenarRegistros()`.

### 👨‍💻 Integrante 2 — Joaquín Sánchez

**Responsabilidad: registro de propietarios y mascotas.**

Funcionalidades desarrolladas:

- Ingreso de información del propietario.
- Ingreso de información de la mascota.
- Validación de teléfonos de nueve dígitos.
- Validación de tipo de animal y sexo.
- Validación de fechas de nacimiento.
- Generación de códigos consecutivos.
- Almacenamiento de los nueve campos compartidos.
- Implementación del registro en C# y PSeInt.

**Métodos, funciones y subprocesos:**

- C#: `Registrar()`.
- PSeInt: `Registrar()`, `SoloDigitos()` y `FechaValida()`.

### 👨‍💻 Integrantes 3 y 4 — Piero Álvarez y Máximo Arriagada

**Módulos pendientes de incorporación.**

Se espera integrar las funcionalidades restantes de búsqueda, modificación y eliminación de registros, una vez que se encuentren disponibles y hayan sido comprobadas.

---

## 📊 Estructuras de datos compartidas

Para mantener la compatibilidad entre los módulos, se utilizan nueve campos:

| Campo | Tipo de dato en C# |
|---|---|
| `codigosPropietarios` | `List<int>` |
| `propietarios` | `List<string>` |
| `telefonos` | `List<string>` |
| `codigosMascotas` | `List<int>` |
| `mascotas` | `List<string>` |
| `tiposAnimales` | `List<string>` |
| `especies` | `List<string>` |
| `sexos` | `List<string>` |
| `fechasNacimiento` | `List<DateTime>` |

En PSeInt se utilizan arreglos equivalentes de tipo `Entero` y `Cadena`.

Cada registro comparte una misma posición en los nueve arreglos o listas, lo que permite mantener asociados todos los datos durante el ordenamiento.

---

## 💻 Archivos principales

| Archivo | Descripción |
|---|---|
| `VetEntrePatasArias/Program.cs` | Menú principal e integración de las funcionalidades en C#. |
| `VetEntrePatasArias/VetEntrePatasArias.csproj` | Configuración del proyecto de consola .NET. |
| `PSeInt/SistemaVeterinaria_EP.psc` | Programa integrado en PSeInt con registro, listado y ordenamiento. |
| `README.md` | Documentación de la rama de integración. |

---

## 🧪 Pruebas de integración

Se realizaron pruebas manuales de las funcionalidades implementadas.

**Registro de propietarios y mascotas:**
- Ingreso de datos de propietarios y mascotas.
- Validación de teléfonos y fechas.
- Generación de códigos consecutivos.
- Almacenamiento de registros.

**Listado de registros:**
- Recorrido y visualización de los campos registrados.
- Comprobación de los datos asociados a cada mascota.

**Ordenamiento burbuja:**
- Registro de mascotas con nombres desordenados.
- Ordenamiento alfabético de los nombres.
- Comprobación de que cada mascota mantenga la información de su propietario y los demás campos.

**Resultado:** las opciones 1, 5 y 6 fueron probadas manualmente durante la integración de PSeInt y se confirmó su funcionamiento.

Estas comprobaciones corresponden a los módulos implementados. No representan una validación completa de las funcionalidades todavía pendientes.

---

## 🛠️ Tecnologías utilizadas

- **Lenguaje de programación:** C#.
- **Pseudocódigo:** PSeInt.
- **Entorno de desarrollo:** Visual Studio Code.
- **Plataforma:** .NET.
- **Control de versiones:** Git y GitHub.
- **Paradigmas y técnicas:** programación estructurada, listas, arreglos, ciclos, condicionales, funciones y subprocesos.

---

## 🌿 Organización de ramas

| Rama | Propósito |
|---|---|
| `main` | Rama principal del repositorio. |
| `Integrante-1` | Trabajo individual de listado y ordenamiento. |
| `Integrante-2` | Trabajo individual de registro. |
| `ep/base-colaborativa` | Estructura inicial compartida. |
| `ep/integracion` | Integración y pruebas de los módulos del equipo. |

El desarrollo individual de cada integrante se mantiene separado para facilitar la identificación de sus contribuciones.

---

## 🚧 Estado actual del proyecto

La rama `ep/integracion` contiene una **integración parcial funcional** del sistema veterinario.

Hasta el momento se han incorporado:

- Registro de propietarios y mascotas.
- Listado de registros.
- Ordenamiento burbuja alfabético.
- Menú principal y salida del programa.

**Próximos pasos:**

- Incorporar los módulos de búsqueda, modificación y eliminación.
- Comprobar su compatibilidad con los nueve campos compartidos.
- Ejecutar pruebas integrales del sistema.
- Revisar la documentación y preparar la versión correspondiente a la entrega académica.

---

## 👥 Equipo de desarrollo

Proyecto desarrollado colaborativamente por:

- **Mauricio Arias** — Integrante 1.
- **Joaquín Sánchez** — Integrante 2.
- **Piero Álvarez** — Integrante 3.
- **Máximo Arriagada** — Integrante 4.

**Proyecto académico — Fundamentos de Programación | Evaluación Parcial.**
