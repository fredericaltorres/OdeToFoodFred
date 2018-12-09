# Database and Entity Framework Configuration

## Assembly should already be part of the project

## Command line tool
Check if entity framework cli is there
```bash
C:\> dotnet ef --version
```

With the lastest ASP.NET Core EF is on by default

```xml
<ItemGroup>
    <DotNetCliToolReference 
        Include="Microsoft.EntityFrameworkCore.Tools.DotNet"
        Version="2.0.0"
    ></DotNetCliToolReference> 
</ItemGroup>
```

## How to create the database

```
:: List all db context defined in the project and info
c:\>dotnet ef dbcontext list
c:\>dotnet ef dbcontext info

:: Create the database based on the connection string
c:\>dotnet ef database update -v

c:\>dotnet ef migrations add --help
c:\>dotnet ef migrations add InitialCreate -v
c:\>dotnet ef migrations update 

```
