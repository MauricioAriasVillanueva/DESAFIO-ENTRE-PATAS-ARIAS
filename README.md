# 🐾 Sistema de Gestión Veterinaria – Entre Patas Arias

## 📌 Descripción del proyecto

**Entre Patas Arias** es un sistema de gestión veterinaria desarrollado como parte del curso de **Fundamentos de Programación** de la Universidad Privada del Norte (UPN).

El proyecto está implementado en **C# (aplicación de consola)** y **PSeInt (pseudocódigo)**. Permite registrar, buscar, modificar, eliminar, mostrar y ordenar información de propietarios y mascotas.

La rama `ep/integracion` reúne los módulos desarrollados por los cuatro integrantes del equipo en una misma versión funcional.

## 🎯 Objetivo

Desarrollar un sistema básico de gestión veterinaria mediante estructuras de control, arreglos, listas, funciones, subprocesos y algoritmos de búsqueda y ordenamiento, aplicando los conocimientos adquiridos en el curso.

## ⚙️ Funcionalidades implementadas

| Opción | Funcionalidad | Descripción |
|---|---|---|
| 1 | Registrar | Registra los datos del propietario y su mascota. |
| 2 | Buscar | Busca una mascota mediante su código único. |
| 3 | Modificar | Permite actualizar los datos de un registro existente. |
| 4 | Eliminar | Elimina un registro previa confirmación. |
| 5 | Mostrar registros | Presenta los datos de todas las mascotas registradas. |
| 6 | Ordenar registros | Ordena alfabéticamente los registros por nombre de mascota. |
| 7 | Salir | Finaliza la ejecución del programa. |

**Estado:** Los módulos están integrados en C# y PSeInt. Se realizaron pruebas funcionales durante la integración.

## 👥 Integrantes y responsabilidades

| Integrante | Responsabilidad |
|---|---|
| **Mauricio Arias Villanueva – Integrante 1** | Mostrar registros, ordenamiento burbuja e integración del proyecto. |
| **Joaquín Sánchez – Integrante 2** | Registro de propietarios y mascotas, generación de códigos y validaciones. |
| **Piero Álvarez – Integrante 3** | Modificación y eliminación de registros. |
| **Máximo Arriagada – Integrante 4** | Búsqueda lineal de mascotas por código. |

Cada integrante desarrolló su módulo de acuerdo con las responsabilidades asignadas. Posteriormente, las funcionalidades se adaptaron a la estructura común de datos.

## 🗂️ Datos del sistema

El sistema administra nueve campos mediante listas en C# y arreglos en PSeInt.

**Propietario:**
- Código del propietario.
- Nombre del propietario.
- Teléfono de contacto.

**Mascota:**
- Código de la mascota.
- Nombre de la mascota.
- Tipo de animal (Canino o Felino).
- Especie o raza.
- Sexo (Macho o Hembra).
- Fecha de nacimiento.

Los códigos se generan automáticamente a partir de:

- **Propietarios:** 2001.
- **Mascotas:** 1001.

Los códigos se incrementan con cada nuevo registro y se mantienen al modificar los datos.

## 🧠 Algoritmos y estructuras utilizadas

### Registro

Permite ingresar y almacenar la información de propietarios y mascotas.

Incluye validaciones básicas:
- Teléfono de nueve dígitos numéricos.
- Tipo de animal permitido.
- Sexo permitido.
- Fecha de nacimiento válida y no futura.
- Verificación de campos obligatorios.

### Búsqueda lineal

Recorre los códigos de las mascotas hasta localizar el solicitado.

Si encuentra una coincidencia, muestra la información completa del registro; de lo contrario, informa que la mascota no existe.

### Modificación

Localiza una mascota por su código y permite actualizar un campo seleccionado, conservando sus códigos de identificación.

### Eliminación

Busca el registro, solicita confirmación y elimina los datos correspondientes.

En C# se utiliza `RemoveAt()`. En PSeInt se desplazan las posiciones de los nueve arreglos y se reduce la cantidad de registros.

### Visualización

Recorre los registros almacenados y presenta los nueve campos relacionados con cada mascota.

### Ordenamiento burbuja

Utiliza el algoritmo **Bubble Sort** para ordenar alfabéticamente las mascotas.

Al intercambiar dos nombres, también intercambia los datos asociados en las demás listas o arreglos, evitando que la información del propietario y la mascota se desordene.

## 💻 Tecnologías utilizadas

| Tecnología | Aplicación |
|---|---|
| C# | Desarrollo del programa de consola. |
| .NET | Compilación y ejecución del proyecto C#. |
| PSeInt | Implementación del pseudocódigo. |
| Visual Studio Code | Edición y desarrollo de código. |
| Git | Control de versiones. |
| GitHub | Repositorio y colaboración del equipo. |

## 📁 Estructura del proyecto

```text
DESAFIO-ENTRE-PATAS-ARIAS/
│
├── VetEntrePatasArias/
│   ├── Program.cs
│   └── VetEntrePatasArias.csproj
│
├── PSeInt/
│   └── SistemaVeterinaria_EP.psc
│
└── README.md
```

**Archivos principales:**

- `Program.cs`: contiene el menú y los métodos del sistema en C#.
- `SistemaVeterinaria_EP.psc`: contiene el algoritmo principal, las funciones y los subprocesos en PSeInt.
- `README.md`: documentación del proyecto.

## ▶️ Ejecución del programa

### C#

**Requisito:** tener instalado el SDK de .NET compatible con el proyecto.

1. Clonar o descargar el repositorio.
2. Abrir la carpeta en Visual Studio Code.
3. Seleccionar la rama `ep/integracion`.
4. Abrir la terminal y ejecutar:

```bash
dotnet run --project VetEntrePatasArias/VetEntrePatasArias.csproj
```

5. Seleccionar una opción del menú.

### PSeInt

1. Abrir PSeInt.
2. Cargar `PSeInt/SistemaVeterinaria_EP.psc`.
3. Ejecutar el algoritmo.
4. Ingresar la fecha de referencia solicitada en formato `DD/MM/AAAA`.
5. Utilizar las opciones del menú.

## ✅ Pruebas de funcionamiento

Durante la integración se comprobaron las operaciones del sistema, incluyendo:

- Registro de propietarios y mascotas.
- Búsqueda por código.
- Modificación de datos existentes.
- Eliminación de registros.
- Visualización de la información.
- Ordenamiento de mascotas.
- Funcionamiento del menú principal.

También se verificó la incorporación de los módulos desarrollados por los cuatro integrantes.

## 📌 Consideraciones y limitaciones

- El programa utiliza almacenamiento temporal en memoria.
- Al finalizar la ejecución, los registros ingresados no se conservan.
- No utiliza bases de datos.
- La versión de PSeInt contempla un máximo de 100 registros.
- La aplicación funciona mediante un menú de consola, sin interfaz gráfica.
- El ordenamiento se realiza por nombre de mascota.
- El proyecto tiene fines académicos.

## 🌿 Organización de ramas

| Rama | Propósito |
|---|---|
| `Features-codes` | Prototipo inicial del sistema. |
| `ep/base-colaborativa` | Estructura compartida para el desarrollo. |
| `Integrante-1` | Desarrollo individual del Integrante 1. |
| `Integrante-2` | Desarrollo individual del Integrante 2. |
| `Integrante-3` | Desarrollo individual del Integrante 3. |
| `Integrante-4` | Desarrollo individual del Integrante 4. |
| `ep/integracion` | Integración de los módulos en C# y PSeInt. |

## 🎓 Conclusión

El desarrollo de **Entre Patas Arias** permitió aplicar los fundamentos de programación mediante un proyecto colaborativo.

La integración de los cuatro módulos dio como resultado un sistema que permite administrar registros de propietarios y mascotas, utilizando búsqueda lineal, ordenamiento burbuja, validaciones y estructuras de datos.

La implementación paralela en C# y PSeInt facilita comprender la relación entre el diseño de algoritmos y su aplicación en un lenguaje de programación.

---

**Universidad Privada del Norte (UPN)**  
**Curso:** Fundamentos de Programación  
**Proyecto:** Veterinaria Entre Patas Arias  
**Rama:** `ep/integracion`  
**Estado:** Integración funcional de los cuatro integrantes.
