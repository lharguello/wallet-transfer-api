# WalletTransfer API

**WalletTransfer API** es una API RESTful construida con .NET 8 para gestionar billeteras y realizar transferencias de fondos entre ellas. La API sigue una arquitectura limpia, utiliza MediatR para la gestión de comandos y queries, y Entity Framework Core con MySQL para la persistencia. La documentación de la API se genera con Swagger/OpenAPI.

## Tabla de Contenido

- [Características](#características)
- [Requisitos](#requisitos)
- [Configuración](#configuración)
- [Ejecución](#ejecución)
- [Endpoints de la API](#endpoints-de-la-api)
- [Estructura del Proyecto](#estructura-del-proyecto)
- [Paquetes NuGet](#paquetes-nuget)
- [Configuración (`appsettings.json`)](#configuración-appsettingsjson)

## Características

- **Gestión de Billeteras:**
    - Crear, obtener, listar, actualizar y eliminar billeteras.
    - Actualizar el saldo de una billetera.
- **Transferencias entre Billeteras:**
    - Crear transacciones de transferencia especificando la billetera de origen y destino.
- **Validación:**
    - Validación de solicitudes implementada directamente en los Handlers de MediatR.
- **Documentación:**
    - Documentación de la API generada automáticamente con Swagger/OpenAPI (accesible en `/swagger`).
- **Arquitectura:**
    - Clean Architecture con separación de responsabilidades en capas (Presentation, Application, Domain, Infrastructure).
    - Patrón Mediator (con MediatR) para desacoplar la lógica de negocio.
    - Inyección de dependencias para una mejor testabilidad.
- **Persistencia:**
    - Entity Framework Core para interactuar con la base de datos MySQL.
- **Logging:**
    - Implementación de logging estructurado con Serilog a archivos y consola.

## Requisitos

Asegúrate de tener instalados los siguientes elementos en tu sistema:

- [.NET SDK](https://dotnet.microsoft.com/download) (versión recomendada: 8.0 o superior)
- [ASP.NET Core Runtime](https://dotnet.microsoft.com/download)
- [MySQL](https://www.mysql.com/downloads/) (configurado en `appsettings.json`).
- [Visual Studio](https://visualstudio.microsoft.com/es/) o [Visual Studio Code](https://code.visualstudio.com/) con C# extension (recomendado para el desarrollo).

## Configuración

1. **Clonar el repositorio:**
    ```bash
    git clone https://github.com/lharguello/WalletTransferAPI.git
    cd WalletTransferAPI
    ```

2. **Configurar la base de datos:**
    - Revisa la sección `ConnectionStrings` en el archivo `appsettings.json` y actualiza los valores si es necesario para que coincidan con tu configuración de MySQL.

3. **Aplicar migraciones de Entity Framework Core:**
    ```bash
    dotnet tool install --global dotnet-ef
    update-database
    ```
    (Asegúrate de estar en la raíz del repositorio o ajusta las rutas de los proyectos según tu estructura).

## Ejecución

1. **Navegar al directorio del proyecto API:**
    ```bash
    cd ./src/WalletTransfer.Api
    ```

2. **Ejecutar la aplicación:**
    ```bash
    dotnet run
    ```

3. **Acceder a la documentación de Swagger:**
    - Abre tu navegador y ve a `https://localhost:7121/swagger/index.html`

## Endpoints de la API

**Billeteras (`/api/wallets`)**

- `POST /api/wallets`: Crea una nueva billetera.
- `GET /api/wallets/{id}`: Obtiene una billetera por su ID.
- `GET /api/wallets`: Obtiene todas las billeteras.
- `PUT /api/wallets/{id}`: Actualiza la información de una billetera y actualiza el saldo de una billetera.
- `DELETE /api/wallets/{id}`: Elimina una billetera por su ID.

**Transacciones (`/api/transactions`)**

- `POST /api/wallets/{walletId}/transactions`: Crea una nueva transacción de transferencia entre dos billeteras.
- `GET /api/wallets/{walletId}/transactions`: Obtiene todas las transacciones de una billetera específica.

## Estructura del Proyecto

```
WalletTransfer.Api/             (Proyecto Principal)
├── Program.cs
├── appsettings.json
└── ...
WalletTransfer.Api.Presentation/             (Capa de Presentación)
├── Controllers/
│   ├── TransactionsController.cs
│   └── WalletsController.cs
├── Extensions/
├── Middleware/
├── Filters/
└── ...

WalletTransfer.Api.Application/   (Capa de Lógica de Aplicación)
├── Behaviors/
├── Dtos/
|    └── Responses/
├── Exceptions/
├── Features/
│   ├── Transactions/
│   │   ├── CreateTransaction/
│   │   ├── GetTransactions/
│   │   └── ...
│   └── Wallets/
│       ├── CreateWallet/
│       ├── DeleteWallet/
│       ├── UpdateWallet/
│       └── ...
├── Mappers/
└── Wrappers/
WalletTransfer.Api.Core/          (Capa de Dominio)
├── Entities/
│   ├── Transaction.cs
│   └── Wallet.cs
├── Enums/
│   └── TransactionType.cs
└── Interfaces/
    └── Repositories/
        ├── IGenericRepository.cs
        ├── ITransactionRepository.cs
        └── IWalletRepository.cs
WalletTransfer.Api.Infrastructure/ (Capa de Infraestructura)
├── Data/
|   └── EntityFramework/
│   ├── ApplicationDbContext.cs
│   ├── Configurations/
│   │   ├── TransactionConfiguration.cs
│   │   └── WalletConfiguration.cs
│   └── Migrations/
├── Repositories/
│   ├── GenericRepository.cs
│   ├── TransactionRepository.cs
│   └── WalletRepository.cs
└── ...
```

## Paquetes NuGet

- AutoMapper (14.0.0)  
- FluentValidation.AspNetCore (11.3.0)  
- MediatR (12.5.0)  
- MicroElements.Swashbuckle.FluentValidation (6.1.0)  
- Microsoft.EntityFrameworkCore.Tools (8.0.10)  
- Newtonsoft.Json (13.0.3)  
- Pomelo.EntityFrameworkCore.MySql (8.0.2)  
- Pomelo.EntityFrameworkCore.MySql.Design (1.1.2)  
- Serilog (4.2.0)  
- Serilog.AspNetCore (9.0.0)  
- Serilog.Extensions.Hosting (9.0.0)  
- Serilog.Settings.Configuration (9.0.0)  
- Swashbuckle.AspNetCore (6.1.1)  
- Swashbuckle.AspNetCore.Newtonsoft (6.1.1)  

## Configuración (`appsettings.json`)

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Port=3306;Database=wallet_transfer;Uid=root;Pwd=mysql;"
  },
  "Serilog": {
    "Using": [
      "Serilog.Sinks.File",
      "Serilog.Sinks.Console"
    ],
    "MinimumLevel": {
      "Default": "Information",
      "Override": {
        "Microsoft": "Warning",
        "System": "Warning"
      }
    },
    "WriteTo": [
      {
        "Name": "Console"
      },
      {
        "Name": "File",
        "Args": {
          "path": "logs/log-.txt",
          "rollOnFileSizeLimit": true,
          "formatter": "Serilog.Formatting.Compact.CompactJsonFormatter,Serilog.Formatting.Compact",
          "rollingInterval": "Day"
        }
      }
    ],
    "Enrich": [
      "FromLogContext",
      "WithThreadId",
      "WithMachineName"
    ]
  },
  "AllowedHosts": "*",
  "ApiKey": "YjAxZmM5ODMtZjgyNi00ZWFjLTlkNjgtMjE5ZDQxMzFiN2Iz"
}
```
