# 🚀 Instrucciones Rápidas - MiAreaVol API

## ✅ Configuración Temporal con SQLite

He cambiado la configuración a SQLite para que funcione inmediatamente. Esto te permitirá ver los datos y Swagger funcionando.

## 📋 Pasos Rápidos (2 minutos)

### 1. Restaurar Dependencias
```bash
dotnet restore
```

### 2. Crear Migración
```bash
dotnet ef migrations add InitialCreate
```

### 3. Crear Base de Datos
```bash
dotnet ef database update
```

### 4. Ejecutar Aplicación
```bash
dotnet run
```

**¡Listo!** La aplicación automáticamente creará 200 registros de prueba.

## 🌐 URLs de Acceso

- **Swagger UI**: `http://localhost:5098/swagger`
- **API Base**: `http://localhost:5098`
- **Estado BD**: `http://localhost:5098/api/seeder/status`
- **Todas las áreas**: `http://localhost:5098/api/area`
- **Todos los volúmenes**: `http://localhost:5098/api/volumen`

## 🧪 Verificación Rápida

### 1. Verificar que funciona:
```bash
curl http://localhost:5098/api/test
```

### 2. Ver estado de la BD:
```bash
curl http://localhost:5098/api/seeder/status
```
**Debería mostrar:**
```json
{
  "areas": 100,
  "volumenes": 100,
  "total": 200,
  "status": "Poblada"
}
```

### 3. Ver registros de área:
```bash
curl http://localhost:5098/api/area
```

### 4. Ver registros de volumen:
```bash
curl http://localhost:5098/api/volumen
```

## 📊 Datos Generados

- **100 registros de áreas**: Cuadrados, rectángulos, triángulos, círculos, trapecios
- **100 registros de volúmenes**: Cubos, prismas, cilindros, esferas, conos, paralelepípedos, pirámides

## 🔄 Migración a MySQL (Más Tarde)

Una vez que funcione con SQLite, puedes migrar a MySQL Railway:

1. Cambiar `MiAreaVol.csproj`:
   ```xml
   <PackageReference Include="Pomelo.EntityFrameworkCore.MySql" Version="9.0.0-preview.3.efcore.9.0.0" />
   ```

2. Cambiar `appsettings.json`:
   ```json
   "DefaultConnection": "Server=hopper.proxy.rlwy.net;Port=14094;Database=railway;Uid=root;Pwd=smFBmeeETAkxGcGuLJifqRXZZODKFzLE;CharSet=utf8mb4;"
   ```

3. Cambiar `Program.cs`:
   ```csharp
   options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
   ```

4. Crear nueva migración:
   ```bash
   dotnet ef migrations add InitialCreateMySQL
   dotnet ef database update
   ```

## 🚨 Si hay problemas

### Error de migración:
```bash
# Eliminar migraciones existentes
rmdir /s Migrations
dotnet ef migrations add InitialCreate
dotnet ef database update
```

### Error de compilación:
```bash
dotnet clean
dotnet restore
dotnet build
```

### Puerto en uso:
- Cambiar puerto en `Properties/launchSettings.json`
- O matar el proceso que usa el puerto

## ✅ Verificación Final

Si todo funciona:
1. ✅ `http://localhost:5098/api/test` - Mensaje de bienvenida
2. ✅ `http://localhost:5098/swagger` - Interfaz de Swagger
3. ✅ `http://localhost:5098/api/seeder/status` - 200 registros
4. ✅ `http://localhost:5098/api/area` - 100 áreas
5. ✅ `http://localhost:5098/api/volumen` - 100 volúmenes

**¡Disfruta tu API funcionando!** 🎉 