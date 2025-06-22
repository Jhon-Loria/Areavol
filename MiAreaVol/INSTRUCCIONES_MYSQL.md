# 🚀 Configuración MySQL Railway - MiAreaVol API

## ⚠️ Problema Resuelto

He corregido la cadena de conexión de MySQL. El problema era el formato de la cadena de conexión de Railway.

## 🔧 Cambios Realizados

### ✅ Cadena de Conexión Corregida:
```json
"DefaultConnection": "Server=hopper.proxy.rlwy.net;Port=14094;Database=railway;Uid=root;Pwd=smFBmeeETAkxGcGuLJifqRXZZODKFzLE;CharSet=utf8mb4;"
```

### ✅ Configuración Mejorada:
- Agregado `EnableRetryOnFailure()` para mejor estabilidad
- Mejorado el manejo de errores
- Script de PowerShell para limpiar migraciones

## 📋 Pasos para Configurar MySQL

### Opción 1: Usar Script Automático (RECOMENDADO)
```bash
# Ejecutar el script de PowerShell
.\limpiar-migraciones.ps1
```

### Opción 2: Comandos Manuales
```bash
# 1. Restaurar dependencias
dotnet restore

# 2. Eliminar migraciones existentes (IMPORTANTE)
if (Test-Path "Migrations") { Remove-Item -Recurse -Force "Migrations" }

# 3. Crear nueva migración para MySQL
dotnet ef migrations add InitialCreateMySQL

# 4. Actualizar Base de Datos en Railway
dotnet ef database update

# 5. Ejecutar la Aplicación
dotnet run
```

## 🌐 URLs de Acceso

Una vez ejecutada la aplicación:

- **API Base**: `https://localhost:7110` o `http://localhost:5098`
- **Swagger UI**: `https://localhost:7110/swagger` o `http://localhost:5098/swagger`
- **Estado BD**: `http://localhost:5098/api/seeder/status`
- **Todas las áreas**: `http://localhost:5098/api/area`
- **Todos los volúmenes**: `http://localhost:5098/api/volumen`

## 🗄️ Base de Datos MySQL Railway

### Configuración:
- **Host**: `hopper.proxy.rlwy.net`
- **Puerto**: `14094`
- **Base de datos**: `railway`
- **Usuario**: `root`
- **Contraseña**: `smFBmeeETAkxGcGuLJifqRXZZODKFzLE`

### Tablas Creadas:
1. **`CalculosArea`** - 100 registros de áreas
2. **`CalculosVolumen`** - 100 registros de volúmenes

## 🧪 Pruebas de Verificación

### 1. Verificar conexión a MySQL:
```bash
curl http://localhost:5098/api/seeder/status
```
**Respuesta esperada:**
```json
{
  "areas": 100,
  "volumenes": 100,
  "total": 200,
  "status": "Poblada"
}
```

### 2. Ver registros de área:
```bash
curl http://localhost:5098/api/area
```
**Debería mostrar 100 registros de áreas**

### 3. Ver registros de volumen:
```bash
curl http://localhost:5098/api/volumen
```
**Debería mostrar 100 registros de volúmenes**

### 4. Buscar por tipo:
```bash
curl http://localhost:5098/api/area/buscar/tipo/cuadrado
curl http://localhost:5098/api/volumen/buscar/tipo/cubo
```

## 🚨 Solución de Problemas

### ❌ Error de Conexión a MySQL
**Síntomas:** Error de conexión en la consola
**Solución:**
1. Verificar que la cadena de conexión sea correcta
2. Verificar que Railway esté activo
3. Verificar que el puerto 14094 esté abierto

### ❌ Error de Migración
**Síntomas:** Error al crear migración
**Solución:**
```bash
# Limpiar proyecto
dotnet clean
dotnet restore

# Eliminar migraciones y crear nuevas
if (Test-Path "Migrations") { Remove-Item -Recurse -Force "Migrations" }
dotnet ef migrations add InitialCreateMySQL
dotnet ef database update
```

### ❌ No se ven registros en Swagger
**Síntomas:** Endpoints vacíos
**Solución:**
1. Verificar que el seeder se ejecutó correctamente
2. Acceder a `http://localhost:5098/api/seeder/status`
3. Si no hay datos, ejecutar: `POST http://localhost:5098/api/seeder/seed`

### ❌ Error de Compilación
**Síntomas:** Errores de compilación
**Solución:**
```bash
dotnet clean
dotnet restore
dotnet build
```

## 📊 Verificación en Railway

### Para verificar que los datos están en Railway:

1. **Acceder a Railway Dashboard**
2. **Ir a la base de datos MySQL**
3. **Ejecutar consultas:**
   ```sql
   USE railway;
   SELECT COUNT(*) FROM CalculosArea;
   SELECT COUNT(*) FROM CalculosVolumen;
   SELECT * FROM CalculosArea LIMIT 5;
   SELECT * FROM CalculosVolumen LIMIT 5;
   ```

## ✅ Verificación Final

Si todo funciona correctamente:

1. ✅ **Conexión a MySQL**: Sin errores en consola
2. ✅ **Migración exitosa**: Tablas creadas en Railway
3. ✅ **Seeder ejecutado**: 200 registros creados
4. ✅ **Swagger funciona**: `http://localhost:5098/swagger`
5. ✅ **Datos visibles**: Endpoints muestran registros
6. ✅ **Búsquedas funcionan**: Filtros por tipo y nombre

## 🔧 Comandos de Emergencia

Si algo no funciona:

```bash
# Reiniciar completamente
dotnet clean
dotnet restore
if (Test-Path "Migrations") { Remove-Item -Recurse -Force "Migrations" }
dotnet ef migrations add InitialCreateMySQL
dotnet ef database update
dotnet run
```

## 🎯 Comando Rápido

Para ejecutar todo de una vez:
```bash
.\limpiar-migraciones.ps1 && dotnet run
```

**¡La aplicación debería conectarse a MySQL Railway y mostrar 200 registros!** 🎉 