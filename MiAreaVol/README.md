# MiAreaVol - API para Cálculo de Áreas y Volúmenes

Esta API permite calcular áreas y volúmenes de figuras geométricas, con persistencia en base de datos y historial de cálculos.

## Características

### Cálculo de Áreas
- **Cuadrado**: Área = lado²
- **Rectángulo**: Área = largo × ancho
- **Triángulo**: Área = (base × altura) / 2
- **Círculo**: Área = π × radio²
- **Trapecio**: Área = ((base + lado) × altura) / 2

### Cálculo de Volúmenes
- **Cubo**: Volumen = lado³
- **Prisma**: Volumen = base × altura
- **Cilindro**: Volumen = π × radio² × altura
- **Esfera**: Volumen = (4/3) × π × radio³
- **Cono**: Volumen = (1/3) × π × radio² × altura
- **Paralelepípedo**: Volumen = largo × ancho × altura
- **Pirámide**: Volumen = (1/3) × base × altura

## Estructura del Proyecto

```
MiAreaVol/
├── Controllers/
│   ├── AreaController.cs          # Controlador para cálculos de área
│   └── VolumenController.cs       # Controlador para cálculos de volumen
├── Services/
│   ├── IAreaService.cs            # Interfaz del servicio de área
│   ├── AreaService.cs             # Implementación del servicio de área
│   ├── IVolumenService.cs         # Interfaz del servicio de volumen
│   └── VolumenService.cs          # Implementación del servicio de volumen
├── Models/
│   ├── FiguraGeometrica.cs        # Modelo de entidad
│   ├── CalculoAreaRequest.cs      # DTO para solicitudes de área
│   ├── CalculoVolumenRequest.cs   # DTO para solicitudes de volumen
│   └── CalculoResponse.cs         # DTO para respuestas
├── Data/
│   └── MiAreaVolContext.cs        # Contexto de Entity Framework
└── Program.cs                     # Configuración de la aplicación
```

## Configuración

### 1. Instalar dependencias
```bash
dotnet restore
```

### 2. Instalar herramientas de Entity Framework (si no están instaladas)
```bash
dotnet tool install --global dotnet-ef
```

### 3. Crear la base de datos
```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
```

### 4. Ejecutar la aplicación
```bash
dotnet run
```

## Endpoints de la API

### Área Controller (`/api/area`)

#### Calcular área
```http
POST /api/area/calcular
Content-Type: application/json

{
  "tipoFigura": "cuadrado",
  "nombre": "Mi cuadrado",
  "lado": 5.0
}
```

#### Obtener historial
```http
GET /api/area/historial
```

#### Obtener cálculo por ID
```http
GET /api/area/{id}
```

#### Figuras soportadas
```http
GET /api/area/figuras-soportadas
```

### Volumen Controller (`/api/volumen`)

#### Calcular volumen
```http
POST /api/volumen/calcular
Content-Type: application/json

{
  "tipoFigura": "cubo",
  "nombre": "Mi cubo",
  "lado": 3.0
}
```

#### Obtener historial
```http
GET /api/volumen/historial
```

#### Obtener cálculo por ID
```http
GET /api/volumen/{id}
```

#### Figuras soportadas
```http
GET /api/volumen/figuras-soportadas
```

## Ejemplos de Uso

### Calcular área de un círculo
```json
{
  "tipoFigura": "circulo",
  "nombre": "Círculo de radio 5",
  "radio": 5.0
}
```

### Calcular volumen de un cilindro
```json
{
  "tipoFigura": "cilindro",
  "nombre": "Cilindro de agua",
  "radio": 2.0,
  "altura": 10.0
}
```

## Base de Datos

La aplicación utiliza SQL Server LocalDB por defecto. La cadena de conexión está configurada en `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=MiAreaVolDb;Trusted_Connection=true;MultipleActiveResultSets=true"
  }
}
```

## Tecnologías Utilizadas

- **.NET 9.0**
- **ASP.NET Core Web API**
- **Entity Framework Core**
- **SQL Server LocalDB**
- **Swagger/OpenAPI**

## Notas

- Todos los cálculos se guardan en la base de datos para mantener un historial
- Los parámetros son opcionales según el tipo de figura
- La API incluye validación de datos y manejo de errores
- Documentación automática disponible en Swagger UI 