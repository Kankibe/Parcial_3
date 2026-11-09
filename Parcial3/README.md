# Universidad Tecnológica de Panamá

**Facultad de Ingeniería de Sistemas Computacionales**  
**Campus Víctor Levis Sasso**

# Parte práctica del Parcial 3

**Asignatura:** Herramientas de la Programación Aplicada III (.Net)  
**Grupo:** 1IL133  
**Instructora:** Ing. Irina Fong  
**Estudiante:** Kankibe González  
**Fecha:** 9 de noviembre de 2026

---

## Descripción general

Este repositorio contiene los ejercicios desarrollados para la parte práctica del Parcial 3. Está organizado en tres proyectos de consola realizados en C# con .NET 8.0:

1. **Parte1_Figuras:** implementación de una clase abstracta para representar figuras geométricas y calcular sus áreas y perímetros.
2. **Parte2_Json:** creación de un objeto y conversión de sus datos a formato JSON utilizando Newtonsoft.Json.
3. **Parte3_Login:** sistema de registro e inicio de sesión conectado a MySQL. Utiliza BCrypt para proteger las contraseñas y registra los intentos de inicio de sesión.

## Tecnologías utilizadas

| Tecnología | Versión / paquete | Uso |
|---|---|---|
| C# | .NET 8.0 | Desarrollo de los tres ejercicios |
| Visual Studio | 2026 | Entorno de desarrollo |
| Newtonsoft.Json | 13.0.3 | Serialización de objetos a JSON |
| BCrypt.Net-Next | 4.2.0 | Generación y verificación de hashes de contraseñas |
| MySqlConnector | 2.6.2 | Conexión entre C# y MySQL |
| MySQL Server | 8.0.46 | Almacenamiento de usuarios e intentos de inicio de sesión |
| MySQL Workbench | 8.0 | Administración de la base de datos |

---

## Estructura del proyecto

```text
Parcial3/
├── imagenes_Captura de pantallas/
│   ├── Captura de pantalla 2026-11-09 105849.png
│   ├── Captura de pantalla 2026-11-09 105857.png
│   ├── Captura de pantalla 2026-11-09 105904.png
│   ├── Captura de pantalla 2026-11-09 105914.png
│   ├── Captura de pantalla 2026-11-09 110800.png
│   └── Captura de pantalla 2026-11-09 110918.png
│
├── Parte1_Figuras/
│   ├── Class1.cs
│   ├── figura.cs
│   └── Parte1_Figuras.csproj
│
├── Parte2_Json/
│   ├── Program.cs
│   └── Parte2_Json.csproj
│
└── Parte3_Login/
    └── Parte3_Login/
        ├── Program.cs
        ├── Loginservicio.cs
        ├── Conexion.cs
        ├── Backup_parte3_parcial3.sql
        └── Parte3_Login.csproj
```

## Parte 1: Figuras geométricas

En este ejercicio se utiliza la herencia y el polimorfismo mediante una clase abstracta llamada `Figura`. Esta clase define dos métodos abstractos:

- `CalcularArea()`: obtiene el área de la figura.
- `CalcularPerimetro()`: obtiene el perímetro de la figura.

Se implementan tres clases derivadas:

| Clase | Datos utilizados | Área | Perímetro |
|---|---|---|---|
| `Rectangulo` | Base y altura | base × altura | 2 × (base + altura) |
| `Cuadrado` | Lado | lado × lado | 4 × lado |
| `Triangulo` | Base, altura y dos lados adicionales | (base × altura) / 2 | base + lado 2 + lado 3 |

En `Main` se crea un arreglo de tipo `Figura` que almacena un rectángulo, un cuadrado y un triángulo. Luego se recorre el arreglo para mostrar el nombre de cada clase, su área y su perímetro. Esto permite utilizar objetos de distintas clases mediante una misma referencia de tipo `Figura`.

### Ejecución

Desde la carpeta `Parte1_Figuras`, ejecutar:

```bash
dotnet run
```

**Resultado:** se muestran los cálculos de las tres figuras geométricas en la consola.

![Resultado de figuras geométricas](imagenes_Captura%20de%20pantallas/Captura%20de%20pantalla%202026-11-09%20110800.png)

---

## Parte 2: Serialización a JSON

Este ejercicio utiliza el paquete **Newtonsoft.Json** para convertir un objeto de C# en una cadena con formato JSON.

Se crea un objeto anónimo con los siguientes datos:

- **Nombre:** Cristell
- **Edad:** 20
- **Carrera:** Ingeniería en Sistemas

Después, se utiliza `JsonConvert.SerializeObject()` para convertir el objeto en una cadena JSON y mostrarla en la consola.

### Ejecución

Desde la carpeta `Parte2_Json`, ejecutar:

```bash
dotnet restore
dotnet run
```

El proyecto utiliza el paquete `Newtonsoft.Json` versión `13.0.3`, declarado en el archivo `.csproj`.

**Resultado:** se muestra el mensaje inicial y la información del objeto convertida a JSON.

![Resultado de serialización JSON](imagenes_Captura%20de%20pantallas/Captura%20de%20pantalla%202026-11-09%20110918.png)

---

## Parte 3: Login con BCrypt y MySQL

Este ejercicio implementa un sistema de registro e inicio de sesión desde la consola, conectado a una base de datos MySQL llamada `loginbcrypt`.

### Registro de usuarios

El sistema solicita el nombre completo, el nombre de usuario y la contraseña. Antes de guardar la información, la contraseña se procesa con:

```csharp
BCrypt.Net.BCrypt.HashPassword(clave)
```

De esta manera, la base de datos almacena el hash generado y no la contraseña original. Si el registro se completa, se muestra un mensaje de confirmación. Si ocurre un error al insertar el usuario, se informa que no fue posible registrarlo, por ejemplo, cuando el nombre de usuario ya existe.

### Inicio de sesión

El usuario introduce su nombre de usuario y contraseña. El programa consulta el hash almacenado en MySQL y utiliza:

```csharp
BCrypt.Net.BCrypt.Verify(clave, hashGuardado)
```

BCrypt comprueba si la contraseña ingresada corresponde al hash guardado. Si los datos son correctos, el programa muestra el nombre completo y el usuario. En caso contrario, muestra el mensaje de usuario o contraseña incorrectos.

### Registro de intentos

Cada inicio de sesión genera un registro en la tabla `intentoslogin`, tanto si el acceso fue correcto como si fue fallido. Se almacena el nombre de usuario, el resultado del intento y la fecha.

### Base de datos

El archivo `Backup_parte3_parcial3.sql` contiene el respaldo de la base de datos `loginbcrypt`, incluyendo las tablas:

| Tabla | Descripción |
|---|---|
| `usuarios` | Guarda el identificador, nombre de usuario, nombre completo y hash de la contraseña. |
| `intentoslogin` | Registra el usuario que intentó ingresar, si el acceso fue exitoso y la fecha del intento. |

### Configuración y ejecución

1. Abrir MySQL Workbench y conectarse al servidor MySQL.
2. Abrir y ejecutar `Backup_parte3_parcial3.sql` para crear la base de datos y sus tablas.
3. Revisar `Conexion.cs` y ajustar servidor, puerto, nombre de la base de datos, usuario y contraseña según la configuración local de MySQL.
4. Abrir una terminal en la carpeta `Parte3_Login/Parte3_Login`.
5. Restaurar los paquetes y ejecutar el proyecto:

```bash
dotnet restore
dotnet run
```

El menú de consola permite seleccionar:

1. Registrar usuario.
2. Iniciar sesión.
3. Salir.

Al introducir una contraseña, el programa muestra asteriscos en la consola en lugar de mostrar los caracteres escritos.

### Evidencias

**Registro de un usuario:** se muestra el ingreso de los datos y la confirmación de que el usuario fue registrado.

![Registro de usuario](imagenes_Captura%20de%20pantallas/Captura%20de%20pantalla%202026-11-09%20105849.png)

**Inicio de sesión exitoso:** el sistema verifica la contraseña y muestra un mensaje de bienvenida.

![Inicio de sesión exitoso](imagenes_Captura%20de%20pantallas/Captura%20de%20pantalla%202026-11-09%20105857.png)

**Intento de registro con usuario existente:** el sistema informa que no pudo registrar el usuario.

![Usuario existente](imagenes_Captura%20de%20pantallas/Captura%20de%20pantalla%202026-11-09%20105904.png)

**Inicio de sesión fallido:** se muestra el mensaje correspondiente cuando la contraseña no coincide.

![Inicio de sesión fallido](imagenes_Captura%20de%20pantallas/Captura%20de%20pantalla%202026-11-09%20105914.png)

---

## Información del estudiante

**Nombre:** Kankibe González  
**Asignatura:** Herramientas de la Programación Aplicada III (.Net)  
**Grupo:** 1IL133  
**Instructora:** Ing. Irina Fong  
**Fecha de entrega:** 9 de noviembre de 2026
