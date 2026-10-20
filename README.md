# 🐾 Entre Patas – Arias | Prototipo Inicial del Sistema Veterinario

![Estado del Proyecto](https://img.shields.io/badge/Estado-Prototipo_Inicial-yellow.svg)
![Lenguaje](https://img.shields.io/badge/Lenguaje-C%23-purple.svg)
![Versión](https://img.shields.io/badge/Versión-Experimental-orange.svg)
![Equipo](https://img.shields.io/badge/Equipo-4_Integrantes-green.svg)

## 📌 Descripción del proyecto

Este repositorio contiene el **prototipo inicial del Sistema de Gestión Veterinaria Entre Patas – Arias**, desarrollado como parte de la Evaluación Parcial del curso **Fundamentos de Programación**.

El prototipo fue creado para explorar la estructura básica de un sistema de gestión de propietarios y mascotas mediante una aplicación de consola desarrollada en **C#**.

Su principal propósito fue experimentar con los métodos, estructuras de datos y algoritmos necesarios para implementar las funcionalidades principales del sistema antes de comenzar el desarrollo colaborativo.

**Importante:** esta rama corresponde a una versión experimental y no representa el proyecto final ni la versión integrada del equipo.

---

## 🎯 Objetivo del prototipo

Desarrollar una primera versión del sistema veterinario utilizando conceptos fundamentales de programación.

Los principales objetivos fueron:

- Diseñar un menú interactivo mediante `switch`.
- Implementar métodos independientes para las funcionalidades del sistema.
- Almacenar información de propietarios y mascotas mediante listas.
- Generar códigos numéricos para identificar registros.
- Implementar operaciones de búsqueda, modificación y eliminación.
- Recorrer listas para visualizar registros.
- Aplicar el algoritmo de ordenamiento burbuja.
- Comprender cómo organizar un programa antes de trabajar en equipo.

---

## ✅ Funcionalidades desarrolladas

El prototipo contiene siete opciones en el menú principal:

| Opción | Funcionalidad | Método |
|---|---|---|
| 1 | Registrar propietario y mascota | `Registrar()` |
| 2 | Buscar mascota | `Buscar()` |
| 3 | Modificar registro | `Modificar()` |
| 4 | Eliminar registro | `Eliminar()` |
| 5 | Mostrar registros | `MostrarRegistros()` |
| 6 | Ordenar registros | `Ordenar()` |
| 7 | Salir del sistema | `Main()` |

Estas funcionalidades fueron desarrolladas como una primera aproximación al comportamiento del sistema veterinario.

Su implementación es experimental y puede presentar diferencias respecto a los módulos de la versión integrada.

---

## 🧩 Estructura del código

El archivo principal del prototipo es:

`VetEntrePatasArias/Program.cs`

El programa utiliza una clase principal `Program`, dentro de la cual se encuentran las listas compartidas, el menú y los métodos del sistema.

### 1. Menú principal

Se utiliza `Main()` para iniciar el programa y mostrar las opciones disponibles.

El menú emplea:

- `do-while` para repetir las opciones.
- `switch` para seleccionar una funcionalidad.
- `break` para finalizar cada caso.

### 2. Registro de información

El método `Registrar()` permite almacenar información básica de propietarios y mascotas.

Los registros utilizan códigos numéricos para identificar a cada propietario y mascota.

### 3. Búsqueda de registros

El método `Buscar()` utiliza un recorrido de listas para localizar mascotas mediante su código.

### 4. Modificación y eliminación

Los métodos `Modificar()` y `Eliminar()` permiten experimentar con la actualización y eliminación de información almacenada en listas.

### 5. Listado de registros

El método `MostrarRegistros()` recorre las listas mediante un ciclo `for` para visualizar los datos.

### 6. Ordenamiento burbuja

El método `Ordenar()` utiliza ciclos `for` anidados y realiza intercambios entre elementos de las listas.

En este prototipo, el ordenamiento se realiza por **código numérico de mascota**.

En el desarrollo posterior del proyecto se implementó un ordenamiento alfabético por nombre de mascota.

---

## 📊 Estructuras de datos utilizadas

El prototipo utiliza seis listas principales:

| Lista | Tipo de dato |
|---|---|
| `codigosPropietarios` | `List<int>` |
| `propietarios` | `List<string>` |
| `telefonos` | `List<string>` |
| `codigosMascotas` | `List<int>` |
| `mascotas` | `List<string>` |
| `especies` | `List<string>` |

Estas listas permiten comprender el almacenamiento y procesamiento de registros mediante posiciones relacionadas.

En las versiones posteriores se amplió la estructura a nueve campos.

---

## 🔄 Evolución del desarrollo

La rama `Features-codes` representa una de las primeras etapas de desarrollo del sistema.

Durante esta etapa se implementaron progresivamente:

1. Estructura inicial del prototipo.
2. Menú principal.
3. Listas para propietarios y mascotas.
4. Registro y generación de códigos.
5. Búsqueda lineal.
6. Modificación de registros.
7. Eliminación de registros.
8. Recorrido y listado.
9. Ordenamiento por burbuja.

Este proceso permitió identificar los componentes necesarios para construir una versión más organizada y compatible con el trabajo de los integrantes.

---

## ⚠️ Limitaciones del prototipo

Esta versión presenta las siguientes características y limitaciones:

- Está desarrollada únicamente en C#.
- Utiliza seis listas de datos.
- No incluye la implementación equivalente en PSeInt.
- Almacena los registros temporalmente en memoria.
- No utiliza una base de datos ni persistencia de información.
- El ordenamiento utiliza códigos numéricos en lugar de nombres de mascotas.
- No incorpora todavía las mejoras de validación y organización desarrolladas posteriormente por el equipo.

Por estas razones, **no debe utilizarse como la versión definitiva del proyecto**.

---

## 🌿 Relación con las demás ramas

| Rama | Propósito |
|---|---|
| `Features-codes` | Prototipo inicial y experimental |
| `ep/base-colaborativa` | Base compartida para el desarrollo en equipo |
| `Integrante-1` | Listado y ordenamiento |
| `Integrante-2` | Registro de propietarios y mascotas |
| `Integrante-3` | Modificación y eliminación |
| `Integrante-4` | Búsqueda de mascotas |
| `ep/integracion` | Integración de los módulos del equipo |
| `main` | Rama principal del repositorio |

La rama `Features-codes` se conserva como **referencia histórica del desarrollo inicial**, mientras que `ep/integracion` reúne el trabajo colaborativo posterior.

---

## 🛠️ Tecnologías y conceptos utilizados

- **Lenguaje de programación:** C#.
- **Entorno de desarrollo:** Visual Studio Code.
- **Control de versiones:** Git y GitHub.
- **Interfaz:** consola.
- **Estructuras de datos:** listas.
- **Estructuras de control:** condicionales, ciclos y `switch`.
- **Algoritmos:** búsqueda lineal y ordenamiento burbuja.
- **Organización del código:** métodos y variables compartidas.

---

## 👥 Equipo de desarrollo

Proyecto académico desarrollado por cuatro integrantes:

- **Mauricio Arias** — Integrante 1: listado y ordenamiento.
- **Joaquín Sánchez** — Integrante 2: registro de propietarios y mascotas.
- **Piero Álvarez** — Integrante 3: modificación y eliminación.
- **Máximo Arriagada** — Integrante 4: búsqueda de mascotas.

El prototipo de esta rama corresponde al trabajo preparatorio realizado antes de la integración de los módulos individuales.

---

## 📌 Estado de la rama

**Estado: PROTOTIPO INICIAL — REFERENCIA HISTÓRICA**

Esta rama documenta la primera implementación de las funcionalidades del sistema y permite observar su evolución.

Los nuevos desarrollos, correcciones e integraciones deben realizarse en las ramas correspondientes del equipo, utilizando `ep/integracion` como referencia para la versión conjunta.

**Proyecto académico — Fundamentos de Programación | Evaluación Parcial.**
