<h1 align="center">E.T. Nº12 D.E. 1º "Libertador Gral. José de San Martín"</h1>
<p align="center">
  <img src="https://et12.edu.ar/imgs/computacion/vamoaprogramabanner.png" alt="Banner Computación">
</p>

## Computación 2025

**Asignatura**: Programación sobre Redes  
**Curso**: 6° 8°

# MarketWeight

MarketWeight permite visualizar la oferta y demanda de criptomonedas a través de una interfaz web sencilla. Las operaciones quedan registradas en una base de datos MySQL. La aplicación facilita el análisis para la toma de decisiones sobre el comportamiento del mercado.

## Comenzando 🚀

Clona el repositorio con GitHub Desktop o desde la terminal:

```bash
git clone https://github.com/carlosb-dev/MarketWeight.git
```

### Pre-requisitos 📋

- .NET SDK - [Link](https://dotnet.microsoft.com/es-es/download/dotnet/8.0)
- MySQL 8.0.41 - [Link](dev.mysql.com/downloads/installer/)

## Configuración de base de datos 📦

Los scripts se encuentran en la carpeta `scripts sql`. Desde allí podrás levantar toda la estructura:

1. Abre una terminal en `scripts sql`.
2. Ingresa a MySQL (asegurate de tener `mysql` en el PATH):
   ```bash
   mysql -u tuUsuario -p
   ```
3. Una vez dentro del cliente de MySQL ejecuta:
   ```sql
   SOURCE Install.sql;
   ```
   Ese archivo incluye: DDL, Procedures, Funciones, Triggers e Inserts de prueba, por lo que deja la BD lista para poder correr el proyecto.

## Ejecutar la aplicación MVC 🖥️

El sitio principal está en `src/MarketWeight.MVC`, un proyecto ASP.NET Core 8.0 que usa `MySqlConnector` y los repositorios definidos en `MarketWeight.Ado.Dapper`.

1. Restaura dependencias (desde la raíz del repositorio):
   ```bash
   dotnet restore MarketWeight.sln
   ```
2. Ajustá la cadena de conexión `DefaultConnection` en `./MarketWeight.MVC/appsettings.Development.json` para que apunte a tu servidor MySQL.
3. Ejecutá la aplicación:
   ```bash
   dotnet run --project src/MarketWeight.MVC/MarketWeight.MVC.csproj
   ```
   Esto levanta el sitio en el puerto configurado en `Properties/launchSettings.json`. Durante el arranque, `Program.cs` registra `IRepoMoneda` e `IRepoUsuario`, por lo que la interfaz ya carga los datos de la base generada en el paso anterior.

4. Conéctate al puerto establecido desde tu navegador y empieza a probar la aplicación. 🙌

## Construido con 🛠️

- C# 12.0 / ASP.NET Core 8.0
- MySQL 8.0
- Visual Studio Code

## Autores ✒️

- **Carlos Bello** - [carlete-afk](https://github.com/carlete-afk)
- **Walter Benítez** - [Walter-Cooking](https://github.com/Walter-Cooking)
- **Jorge Casco** - [jorge-link](https://github.com/jorge-link)
- **Guido Gavilán** - [guido-rar](https://github.com/guido-rar)
- **Francisco García** - [SirFrancis2007](https://github.com/SirFrancis2007)

## Licencia 📄

Este proyecto está bajo la Licencia Creative Commons Attribution 4.0 International. Revisá [LICENSE](LICENSE) para más detalles.
