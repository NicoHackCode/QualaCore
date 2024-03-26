# QualaCore

README - Proyecto Quala .NET 8
Pre-requisitos
.NET 8 SDK instalado en su máquina.
Sistema de gestión de base de datos (SQL Server).
Un editor de código o IDE que soporte proyectos .NET (Visual Studio, Visual Studio Code).

Configuración Inicial
Configurar la Cadena de Conexión
Descarga del Proyecto: Clone o descargue el proyecto a su máquina local.
Configuración de Base de Datos:
Navegue a los archivos appsettings.json en cada uno de los proyectos (BranchOfficeAPI y AuthAPI).
Modifique la cadena de conexión (ConnectionString) para apuntar a su instancia local de la base de datos o a la que desee utilizar.

Entity Framework Code First
Una vez configurada la cadena de conexión, siga estos pasos para actualizar la base de datos utilizando Entity Framework Code First:

Migraciones y Actualización de la Base de Datos:
Abra una terminal o consola de comandos.
Primer navegue al directorio del proyecto de BranchOfficeAPI.
Ejecute el comando:

dotnet ef database update

Repita el proceso para el proyecto de AuthAPI, navegando a su respectivo directorio y ejecutando el mismo comando.

Ejecución de los Proyectos
Iniciar los Proyectos:
Asegúrese de que las entidades estén actualizadas en la base de datos.
Ejecute el proyecto BranchOfficeAPI y el proyecto AuthAPI, Puede configurar visual studio para arranque los dos al tiempo.
Con ambas aplicaciones ejecutándose, podrá interactuar entre ellas para la gestión de sucursales y la autenticación.

Uso
Inicialización de la Base de Datos
Al iniciar los proyectos, se creará una base de datos en blanco. Es esencial ejecutar ambos proyectos para inicializar completamente la base de datos con la estructura necesaria para la autenticación y la gestión de sucursales.

Registro y Autenticación
Utilice el servicio de registro proporcionado en la interfaz de Swagger para crear un nuevo usuario.
Ingrese los datos solicitados en el formulario de registro.
Una vez registrado, puede utilizar las credenciales para autenticarse en la aplicación y acceder a las funcionalidades de gestión de sucursales.

Interacción entre Proyectos
El proyecto de BranchOfficeAPI se utiliza para la gestión de las sucursales.
El proyecto de AuthAPI se utiliza para la gestión de la autenticación y autorización.
Asegúrese de interactuar con ambos para explorar todas las funcionalidades ofrecidas.

Creado por Nicolás Flórez.