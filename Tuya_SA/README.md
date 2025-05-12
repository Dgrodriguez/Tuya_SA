# Tuya_SA - Backend .NET con Clean Architecture

## Descripción  
Este repositorio contiene el desarrollo del backend para Tuya_SA, implementado en .NET 8 y siguiendo el principio de Clean Architecture para una estructura modular y mantenible.

## Tecnologías Utilizadas  
- .NET Core 8  
- SQL Server con Entity Framework Core  
- ASP.NET Web API  
- xUnit para pruebas unitarias  
- Postman para pruebas de API  
- Swagger para documentación automática  

## Configuración de la Base de Datos  
Nombre de la base de datos: TuyaSA_DB  
La configuración de la conexión se encuentra en `appsettings.json`

"ConnectionStrings": {
    "DefaultConnection": "Server=TU_SERVIDOR_SQL;Database=TuyaSA_DB;User Id=tu_usuario;Password=tu_contraseña;TrustServerCertificate=True;"
}


Debe reemplazarse TU_SERVIDOR_SQL, tu_usuario y tu_contraseña con los valores correctos.

Puertos y Configuración Previa
La API utiliza los siguientes puertos configurados en launchSettings.json:

HTTP: http://localhost:5259

HTTPS: https://localhost:7193 En IIS Express:

http://localhost:29187

SSL: https://localhost:44314

## Estructura del Proyecto  

El código sigue Clean Architecture, separando responsabilidades en capas bien definidas:  

- **Aplicacion/** → Contiene los casos de uso y lógica de negocio. Aquí se encuentra `OrderServices.cs`, responsable de manejar las operaciones sobre órdenes.  

- **Domain/** → Define las entidades centrales del sistema y los contratos de repositorio. Incluye:  
  - `Customer.cs` y `Order.cs`: Modelos que representan a los clientes y órdenes.  
  - `ICustomerRepository.cs`: Interfaz que define los métodos de acceso a datos relacionados con clientes.  

- **Infraestructura/** → Implementa la conexión con la base de datos y repositorios específicos.  
  - `AppDbContext.cs`: Contexto de Entity Framework para manejar las entidades.  
  - `Repositories/CustomerRepository.cs`: Implementa `ICustomerRepository`, permitiendo operaciones CRUD sobre clientes.  

- **Migrations/** → Contiene los archivos de migración de Entity Framework para mantener la estructura de la base de datos actualizada.  

- **Presentation/** → Define la API y los controladores que exponen los servicios.  
  - `Controllers/CustomerController.cs`: Maneja las solicitudes HTTP relacionadas con clientes.  
  - `Controllers/OrderController.cs`: Gestiona las operaciones sobre órdenes.  

- **Test/** → Contiene pruebas unitarias e integración para validar el comportamiento del sistema.  
  - `TestResult/PruebaUnitaria.xlsx`: Evidencia de pruebas funcionales.  
  - `TestResult/TestResults.trx`: Registro de pruebas automatizadas.  
  - `IntegrationTests.cs`: Pruebas de integración para validar endpoints.  
  - `OrderServiceTests.cs`: Pruebas unitarias sobre `OrderService`.  

- **Properties/** → Incluye configuraciones del proyecto, como `launchSettings.json` que define los puertos y entorno.  

- **Archivos clave en la raíz:**  
  - `appsettings.json`: Configuración de conexión a la base de datos.  
  - `Program.cs`: Punto de entrada de la aplicación.  

Esta estructura garantiza una separación clara entre la lógica de negocio, acceso a datos y presentación, asegurando modularidad y escalabilidad en el desarrollo del backend.  



Cómo Ejecutar el Proyecto
Requisitos previos:

Instalar .NET 8

Configurar la base de datos en appsettings.json

Pasos para ejecutar:
bash
dotnet restore  
dotnet build  
dotnet run  
La API estará disponible en http://localhost:5259.

Pruebas Automatizadas:
Pruebas unitarias con xUnit:
dotnet test

Generación de reporte de pruebas:
dotnet test --logger "trx;LogFileName=TestResults.trx"

Esto generará un archivo TestResults.trx con los resultados.

Evidencia de Pruebas
El repositorio contiene evidencia en la carpeta TestReports/, incluyendo:

Reporte de pruebas automatizadas (TestResults.trx)

Pruebas funcionales (PruebaUnitaria.xlsx)

Contribuciones
Si deseas mejorar la arquitectura o agregar nuevas funcionalidades, cualquier contribución es bienvenida.


Este `README.md` documenta los aspectos esenciales de tu proyecto, incluyendo configuración de la base de datos, ejecución, pruebas y evidencias.  
Si necesitas algún ajuste o agregar más detalles, dime y lo optimizamos.  
Listo para incluir en tu repositorio de GitHub.  
Dime si necesitas algo más. 