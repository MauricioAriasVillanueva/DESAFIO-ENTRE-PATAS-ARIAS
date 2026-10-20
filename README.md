# 🐾 Entre Patas – Arias | Base Colaborativa del Sistema Veterinario

![Estado](https://img.shields.io/badge/Estado-Base_Colaborativa-blue.svg)
![Lenguaje](https://img.shields.io/badge/Lenguaje-C%23-purple.svg)
![PSeInt](https://img.shields.io/badge/Pseudoc%C3%B3digo-PSeInt-orange.svg)
![Equipo](https://img.shields.io/badge/Equipo-4_Integrantes-green.svg)

## 📌 Descripción del proyecto

Esta rama contiene la **estructura base colaborativa del Sistema de Gestión Veterinaria Entre Patas – Arias**, desarrollado para la Evaluación Parcial del curso **Fundamentos de Programación**.

Su propósito es establecer una organización común que permita a los cuatro integrantes desarrollar sus módulos de manera independiente, manteniendo la compatibilidad entre las estructuras de datos, los nombres de las variables y las funcionalidades del sistema.

La base fue preparada utilizando **C# y PSeInt**, de acuerdo con los conocimientos adquiridos durante el curso.

**Importante:** esta rama representa el punto de partida compartido del equipo. No es una versión funcional completa ni la entrega final.

---

## 🎯 Objetivos de la base colaborativa

Establecer una estructura inicial que permita:

- Utilizar los mismos nueve campos de información en todos los módulos.
- Definir los datos necesarios para registrar propietarios y mascotas.
- Preparar un menú principal con siete opciones.
- Establecer códigos numéricos para identificar propietarios y mascotas.
- Facilitar la incorporación posterior de métodos y subprocesos.
- Evitar incompatibilidades entre los desarrollos individuales.
- Organizar el trabajo colaborativo mediante ramas de GitHub.

---

## 🧩 Estructura del programa

### 1. Archivo principal en C#

`VetEntrePatasArias/Program.cs`

Contiene:

- Clase principal `Program`.
- Nueve listas compartidas.
- Variables para generar códigos.
- Método `Main()`.
- Menú de siete opciones.
- Ciclo `do-while` para mantener el menú activo.
- Validación básica de la opción ingresada mediante `int.TryParse()`.

Los métodos correspondientes a las funcionalidades del sistema se incorporan posteriormente en las ramas de desarrollo e integración.

### 2. Archivo principal en PSeInt

`PSeInt/SistemaVeterinaria_EP.psc`

Contiene:

- Algoritmo principal `SistemaVeterinaria_EP`.
- Declaración de variables.
- Nueve arreglos compartidos.
- Variables de control y correlativos.
- Menú principal.
- Ciclo `Repetir...Hasta Que` para permitir la navegación.

Esta estructura facilita que los integrantes desarrollen sus propios subprocesos conservando los nombres de los arreglos.

---

## 📊 Estructuras de datos compartidas

La base colaborativa define nueve campos:

| Campo | C# | PSeInt |
|---|---|---|
| `codigosPropietarios` | `List<int>` | Arreglo Entero |
| `propietarios` | `List<string>` | Arreglo Cadena |
| `telefonos` | `List<string>` | Arreglo Cadena |
| `codigosMascotas` | `List<int>` | Arreglo Entero |
| `mascotas` | `List<string>` | Arreglo Cadena |
| `especies` | `List<string>` | Arreglo Cadena |
| `tiposAnimales` | `List<string>` | Arreglo Cadena |
| `sexos` | `List<string>` | Arreglo Cadena |
| `fechasNacimiento` | `List<DateTime>` | Arreglo Cadena |

### Relación entre los datos

Todos los campos de una mascota y su propietario deben mantenerse en la misma posición de las listas o arreglos.

Por ejemplo, si una mascota ocupa la posición `0` en C#, su propietario, teléfono, códigos y demás datos corresponden a esa misma posición.

En PSeInt, los módulos del equipo trabajan con posiciones desde `1`.

Esta relación resulta fundamental para que los métodos de búsqueda, modificación, eliminación y ordenamiento funcionen correctamente.

---

## 🔢 Identificación de los registros

En C# se prepararon los siguientes valores iniciales:

| Identificador | Valor inicial |
|---|---|
| Código de propietario | `2001` |
| Código de mascota | `1001` |

Estos códigos permiten diferenciar los registros y localizar la información correspondiente.

En esta versión base, PSeInt mantiene sus correlativos inicializados en `0`; la asignación de códigos desde `2001` y `1001` se realiza en la versión de integración.

---

## 📋 Menú principal establecido

| Opción | Funcionalidad | Estado en esta rama |
|---|---|---|
| 1 | Registrar propietario y mascota | Preparada en menú |
| 2 | Buscar mascota | Preparada en menú |
| 3 | Modificar registro | Preparada en menú |
| 4 | Eliminar registro | Preparada en menú |
| 5 | Mostrar registros | Preparada en menú |
| 6 | Ordenar registros | Preparada en menú |
| 7 | Salir del sistema | Control de salida implementado |

**Nota:** las opciones representan la estructura prevista del sistema. En esta rama no están conectados todavía los métodos funcionales correspondientes a las opciones 1 a 6.

---

## 👥 Organización del trabajo

El sistema fue dividido entre cuatro integrantes, cada uno con responsabilidades específicas.

### Integrante 1 — Mauricio Arias

**Responsabilidad:** listado y ordenamiento.

- Mostrar los registros existentes.
- Recorrer listas y arreglos.
- Implementar el ordenamiento burbuja.
- Conservar la relación de los nueve campos durante los intercambios.

### Integrante 2 — Joaquín Sánchez

**Responsabilidad:** registro de propietarios y mascotas.

- Solicitar información del propietario y de la mascota.
- Validar los datos ingresados.
- Generar códigos de identificación.
- Almacenar información en las nueve estructuras compartidas.

### Integrante 3 — Piero Álvarez

**Responsabilidad:** modificación y eliminación.

- Localizar registros mediante códigos.
- Permitir la modificación de información.
- Eliminar registros conservando la organización de los datos.

### Integrante 4 — Máximo Arriagada

**Responsabilidad:** búsqueda de mascotas.

- Implementar búsqueda lineal por código.
- Identificar la posición del registro.
- Mostrar los datos asociados a la mascota encontrada.

Los módulos funcionales se desarrollan en sus respectivas ramas personales y posteriormente se incorporan a `ep/integracion`.

---

## 🌿 Organización de ramas

| Rama | Propósito |
|---|---|
| `main` | Rama principal del repositorio |
| `Features-codes` | Prototipo inicial del sistema |
| `ep/base-colaborativa` | Estructura compartida para el equipo |
| `Integrante-1` | Listado y ordenamiento |
| `Integrante-2` | Registro |
| `Integrante-3` | Modificación y eliminación |
| `Integrante-4` | Búsqueda |
| `ep/integracion` | Incorporación y pruebas de los módulos |

La rama `ep/base-colaborativa` establece una estructura común, mientras que `ep/integracion` incorpora el desarrollo funcional de los integrantes.

---

## 💻 Archivos principales

| Archivo | Descripción |
|---|---|
| `VetEntrePatasArias/Program.cs` | Estructura base del programa en C# |
| `VetEntrePatasArias/VetEntrePatasArias.csproj` | Configuración del proyecto .NET |
| `PSeInt/SistemaVeterinaria_EP.psc` | Estructura base del algoritmo en PSeInt |
| `README.md` | Documentación de la base colaborativa |

---

## 🛠️ Tecnologías y conceptos utilizados

- **Lenguaje de programación:** C#.
- **Pseudocódigo:** PSeInt.
- **Entorno de desarrollo:** Visual Studio Code.
- **Plataforma:** .NET.
- **Control de versiones:** Git y GitHub.
- **Conceptos:** variables, listas, arreglos, ciclos, condicionales, métodos y algoritmos.

---

## 📌 Estado de la rama

**Estado: BASE COLABORATIVA — ESTRUCTURA INICIAL**

Esta rama conserva la organización inicial utilizada para distribuir y desarrollar los módulos del sistema.

No representa una versión completa del programa.

Los métodos y subprocesos funcionales se incorporan y comprueban en la rama `ep/integracion`.

**Proyecto académico — Fundamentos de Programación | Evaluación Parcial.**
