# 🐾 Entre Patas Arias - Consultorio Veterinario

![Estado del Proyecto](https://img.shields.io/badge/Estado-En_Desarrollo-blue.svg)
![Módulo](https://img.shields.io/badge/M%C3%B3dulo-Listado_Ordenamiento_y_CSV-green.svg)
![Lenguaje](https://img.shields.io/badge/Lenguaje-C%23-purple.svg)

## 📌 Descripción del avance

Esta rama, `Integrante-1`, reúne mi aporte individual al sistema veterinario **Entre Patas – Arias** para el curso de **Fundamentos de Programación**. Parte del trabajo de la Evaluación Parcial (EP) y continúa con mejoras para la Evaluación Final (EF).

Mi responsabilidad inicial fue el **listado de registros y el ordenamiento alfabético de mascotas**. Como avance del **Tema 5**, incorporé **persistencia de propietarios y mascotas en archivos CSV**, validación al leerlos y actualización de códigos automáticos.

Esta rama permite probar esas funcionalidades de forma individual. **No es todavía el programa integrado de los cuatro integrantes.**

---

## 🎯 Funcionalidades desarrolladas

### Listado y ordenamiento (EP)

- **`MostrarRegistros()`**: presenta los nueve campos de cada registro cargado.
- **`Ordenar()`**: aplica el método burbuja por nombre de mascota, sin distinguir mayúsculas y minúsculas; intercambia los datos de las nueve listas paralelas para mantener su correspondencia.
- **PSeInt**: contiene el módulo original de listado y ordenamiento con datos de prueba.

### Persistencia de datos (Tema 5, EF)

- **`GuardarPropietariosCSV()`**: escribe los códigos, nombres y teléfonos de los propietarios.
- **`GuardarMascotasCSV()`**: escribe las mascotas, su código de propietario y la fecha en formato `yyyy-MM-dd`.
- **`CargarDatosCSV()`**: recupera los dos archivos al iniciar. Comprueba su existencia, el número de líneas, la cantidad de campos, los códigos asociados y las fechas.
- **Listas temporales**: valida los registros antes de incorporarlos a las listas principales mediante `AddRange()`, evitando cargas parciales cuando se detecta un error de formato.
- **`ActualizarCodigos()`**: calcula los próximos códigos disponibles a partir de los registros recuperados.
- **Control de guardado**: la opción de guardar se bloquea cuando la carga inicial no finalizó correctamente.

---

## 📁 Archivos principales

| Archivo | Descripción |
|---|---|
| `VetEntrePatasArias/Program.cs` | Métodos de listado, ordenamiento, guardado, carga y validación en C#. |
| `VetEntrePatasArias/VetEntrePatasArias.csproj` | Configuración del proyecto de consola para .NET 10. |
| `VetEntrePatasArias/Datos/propietarios.csv` | Propietarios ficticios para pruebas. |
| `VetEntrePatasArias/Datos/mascotas.csv` | Mascotas ficticias vinculadas por código de propietario. |
| `PSeInt/Modulos/01_Ordenamiento.psc` | Subprocesos originales de listado y ordenamiento de la EP. |
| `PSeInt/SistemaVeterinaria_EP.psc` | Estructura inicial compartida en PSeInt. |

### Estructura de los CSV

`propietarios.csv`:

```csv
CodigoPropietario,Nombre,Telefono
2001,Juan Perez,123456789
2002,Maria Gomez,987654321
2003,Pedro Rodriguez,555555555
```

`mascotas.csv`:

```csv
CodigoMascota,CodigoPropietario,Mascota,Especie,TipoAnimal,Sexo,FechaNacimiento
1001,2001,Chocha,Perro,Canino,Macho,2020-05-15
1002,2002,Luna,Gato,Felino,Hembra,2020-03-10
1003,2003,Scott,Perro,Canino,Macho,2020-01-01
```

Los archivos incluidos son **datos de demostración**, no información real de clientes.

---

## ▶️ Cómo ejecutar y probar

Requiere el **SDK de .NET 10**. Desde la raíz del repositorio:

```powershell
cd VetEntrePatasArias
dotnet run
```

Ejecutar desde esa carpeta es importante: actualmente las rutas relativas buscan los archivos dentro de `VetEntrePatasArias/Datos/`.

| Opción | Acción en esta rama |
|---|---|
| 1–4 | Aparecen en el menú, pero sus módulos todavía no están conectados aquí. |
| 5 | Mostrar los registros recuperados de CSV. |
| 6 | Ordenar las mascotas y mostrar el resultado. |
| 7 | Guardar los registros actuales en ambos CSV, solo si la carga inicial fue válida. |
| 8 | Salir del programa. |

**Nota:** ordenar cambia las listas en memoria. Para conservar ese orden en los archivos, hay que seleccionar la opción **7**. El guardado **sobrescribe** los CSV existentes.

## 🧪 Pruebas realizadas

- Carga de los tres registros ficticios desde los CSV y visualización con la opción 5.
- Comparaciones e intercambios del método burbuja, conservando los datos del propietario.
- Cierre y nueva ejecución del programa para comprobar la recuperación de datos.
- Modificación temporal de una fecha a un valor inválido para comprobar el rechazo del archivo y evitar la transferencia parcial de las listas.
- Restauración de los CSV originales tras las pruebas.

---

## 👨‍💻 Mi participación

**Mauricio Arias — Integrante 1**

Desarrollé el listado y ordenamiento de registros, así como los métodos de persistencia CSV y las validaciones descritas para el Tema 5. También participé en la preparación de la estructura compartida de la EP.

## 🛠️ Tecnologías utilizadas

- **Lenguaje:** C# (.NET 10)
- **Pseudocódigo de la EP:** PSeInt
- **Editor:** Visual Studio Code
- **Control de versiones:** Git y GitHub
- **Persistencia:** archivos CSV mediante `System.IO`

---

## 🚧 Estado y limitaciones actuales

- Este avance **supone un propietario por mascota** y que los registros de ambos CSV están en el mismo orden. Todavía no permite modelar adecuadamente varios animales para un mismo propietario: eso se revisará en la integración.
- La lectura utiliza `Split(',')`; por ahora no admite correctamente campos con comas entre comillas. Tampoco valida todos los posibles problemas de archivos CSV, como encabezados modificados o identificadores duplicados.
- Si falta un CSV o su carga falla, el programa impide guardar. Aún falta permitir el inicio con archivos nuevos al integrar el registro de propietarios y mascotas.
- `Registrar()`, `Buscar()`, `Modificar()` y `Eliminar()` **no están conectados en esta rama individual**. Sus módulos pertenecen al trabajo de los demás integrantes.
- El módulo PSeInt incluido corresponde al avance original de la EP; **la persistencia del Tema 5 está implementada en C#**, no aún en PSeInt.

## 🌿 Organización colaborativa

- **`ep/base-colaborativa`**: estructura inicial compartida del equipo.
- **`Integrante-1`**: listado, ordenamiento y persistencia CSV de este avance individual.
- **`Integrante-2`, `Integrante-3` y `Integrante-4`**: aportes de registro, modificación/eliminación y búsqueda.
- **`ep/integracion`**: rama destinada a integrar y probar las funciones del equipo.

La integración de la persistencia con los demás módulos de la EF está pendiente. Este README describe **únicamente el trabajo disponible en `Integrante-1`**.
