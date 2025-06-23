# 🚀 Configuración MySQL Railway - MiAreaVol API

## ⚠️ IMPORTANTE: Cerrar aplicación primero

**Antes de continuar, cierra cualquier instancia de la aplicación que esté ejecutándose:**
- Presiona `Ctrl+C` en la terminal donde está corriendo `dotnet run`
- O cierra la terminal completamente

## 🔧 Configuración MySQL Railway

### 1. Verificar configuración actual
Tu connection string está configurado correctamente:
```
Server=hopper.proxy.rlwy.net;Port=14094;Database=railway;Uid=root;Pwd=smFBmeeETAkxGcGuLJifqRXZZODKFzLE;CharSet=utf8mb4;
```

### 2. Restaurar dependencias
```bash
dotnet restore
```

### 3. Eliminar migraciones existentes (si existen)
```bash
# En Windows PowerShell:
if (Test-Path "Migrations") { Remove-Item -Recurse -Force "Migrations" }

# O manualmente:
# - Eliminar la carpeta "Migrations" del explorador de archivos
```

### 4. Crear nueva migración para MySQL
```bash
dotnet ef migrations add InitialCreateMySQL
```

### 5. Actualizar base de datos en Railway
```bash
dotnet ef database update
```

### 6. Compilar proyecto
```bash
dotnet build
```

### 7. Ejecutar aplicación
```bash
dotnet run
```

## 🌐 URLs de Verificación

Una vez que la aplicación esté corriendo:

- **Swagger UI**: `http://localhost:5098/swagger`
- **Estado de la BD**: `http://localhost:5098/api/seeder/status`
- **Todas las áreas**: `http://localhost:5098/api/area`
- **Todos los volúmenes**: `http://localhost:5098/api/volumen`
- **Test API**: `http://localhost:5098/api/test`

## 📊 Verificación de Datos

### Estado de la Base de Datos
```bash
curl http://localhost:5098/api/seeder/status




{
  "areas": 100,
  "volumenes": 100,
  "total": 200,
  "status": "Poblada"
}
```

### Ver registros de área
```bash
curl http://localhost:5098/api/area
```

### Ver registros de volumen
```bash
curl http://localhost:5098/api/volumen
```

## 🗄️ Verificar Tablas en MySQL Railway

### Opción 1: Usar MySQL Workbench
1. Conectar a: `hopper.proxy.rlwy.net:14094`
2. Usuario: `root`
3. Contraseña: `smFBmeeETAkxGcGuLJifqRXZZODKFzLE`
4. Base de datos: `railway`

### Opción 2: Usar línea de comandos
```bash
mysql -h hopper.proxy.rlwy.net -P 14094 -u root -p railway
# Ingresa la contraseña: smFBmeeETAkxGcGuLJifqRXZZODKFzLE
```

### Comandos SQL para verificar:
```sql
-- Ver todas las tablas
SHOW TABLES;

-- Ver estructura de tabla de áreas
DESCRIBE CalculosArea;

-- Ver estructura de tabla de volúmenes
DESCRIBE CalculosVolumen;

-- Contar registros
SELECT COUNT(*) FROM CalculosArea;
SELECT COUNT(*) FROM CalculosVolumen;

-- Ver algunos registros
SELECT * FROM CalculosArea LIMIT 5;
SELECT * FROM CalculosVolumen LIMIT 5;
```

## 🚨 Solución de Problemas

### Error: "No se puede cargar archivo"
- **Solución**: Cerrar la aplicación que está ejecutándose
- **Comando**: `Ctrl+C` en la terminal de la aplicación

### Error de conexión a MySQL
- **Verificar**: Connection string en `appsettings.json`
- **Verificar**: Que Railway esté activo
- **Verificar**: Credenciales correctas

### Error de migración
```bash
# Eliminar migraciones y crear nuevas
if (Test-Path "Migrations") { Remove-Item -Recurse -Force "Migrations" }
dotnet ef migrations add InitialCreateMySQL
dotnet ef database update
```

### Error de compilación
```bash
dotnet clean
dotnet restore
dotnet build
```

## ✅ Verificación Final

Si todo funciona correctamente:

1. ✅ `http://localhost:5098/api/test` - Mensaje de bienvenida
2. ✅ `http://localhost:5098/swagger` - Interfaz de Swagger funcionando
3. ✅ `http://localhost:5098/api/seeder/status` - 200 registros totales
4. ✅ `http://localhost:5098/api/area` - 100 registros de áreas
5. ✅ `http://localhost:5098/api/volumen` - 100 registros de volúmenes
6. ✅ Tablas visibles en MySQL Railway

## 📝 Datos Generados

- **100 registros de áreas**: Cuadrados, rectángulos, triángulos, círculos, trapecios
- **100 registros de volúmenes**: Cubos, prismas, cilindros, esferas, conos, paralelepípedos, pirámides

**¡Tu API estará completamente funcional con MySQL Railway!** 🎉 