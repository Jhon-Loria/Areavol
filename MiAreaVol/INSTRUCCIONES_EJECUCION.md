# 🚀 Instrucciones para Ejecutar MiAreaVol API

## ⚠️ Problema Resuelto

He corregido la configuración de Swagger y agregado un controlador de prueba. Ahora la API debería funcionar correctamente.

## 🗄️ Base de Datos con Datos de Prueba

✅ **Seeder Automático**: La aplicación ahora incluye un seeder que automáticamente pobla la base de datos con:
- **100 registros de áreas** (cuadrados, rectángulos, triángulos, círculos, trapecios)
- **100 registros de volúmenes** (cubos, prismas, cilindros, esferas, conos, paralelepípedos, pirámides)

## 📋 Pasos para Ejecutar (OBLIGATORIOS)

### 1. Restaurar Dependencias
```bash
dotnet restore
```

### 2. Crear Migración Inicial
```bash
dotnet ef migrations add InitialCreate
```

### 3. Crear Base de Datos
```bash
dotnet ef database update
```

### 4. Ejecutar la Aplicación
```bash
dotnet run
```

**🎉 ¡Listo!** La aplicación automáticamente poblará la base de datos con 200 registros de prueba.

## 🌐 URLs de Acceso

Una vez ejecutada la aplicación, podrás acceder a:

### ✅ URLs Principales:
- **API Base**: `https://localhost:7110` o `http://localhost:5098`
- **Swagger UI**: `https://localhost:7110/swagger` o `http://localhost:5098/swagger`
- **Test API**: `https://localhost:7110/api/test` o `http://localhost:5098/api/test`
- **Health Check**: `https://localhost:7110/api/test/health` o `http://localhost:5098/api/test/health`

### 🗄️ URLs de Base de Datos:
- **Estado BD**: `http://localhost:5098/api/seeder/status` - Ver cuántos registros hay
- **Ejecutar Seeder**: `POST http://localhost:5098/api/seeder/seed` - Poblar BD manualmente

### 🔍 URLs de Prueba:
- `http://localhost:5098/api/test` - Debería mostrar: "¡MiAreaVol API está funcionando correctamente! 🎉"
- `http://localhost:5098/api/test/health` - Debería mostrar información de estado
- `http://localhost:5098/api/seeder/status` - Debería mostrar: `{"areas": 100, "volumenes": 100, "total": 200}`

## 📊 Endpoints Disponibles

### Seeder Controller (`/api/seeder`)
- `GET /api/seeder/status` - Ver estado de la base de datos
- `POST /api/seeder/seed` - Ejecutar seeder manualmente
- `DELETE /api/seeder/clear` - Información sobre limpieza

### Test Controller (`/api/test`)
- `GET /api/test` - Mensaje de bienvenida
- `GET /api/test/health` - Estado de la API

### Área Controller (`/api/area`)
- `POST /api/area/calcular` - Calcular área
- `GET /api/area` - Obtener todos los cálculos (100 registros)
- `GET /api/area/{id}` - Obtener por ID
- `PUT /api/area/{id}` - Actualizar cálculo
- `DELETE /api/area/{id}` - Eliminar cálculo
- `GET /api/area/buscar/tipo/{tipo}` - Buscar por tipo
- `GET /api/area/buscar/nombre/{nombre}` - Buscar por nombre
- `GET /api/area/figuras-soportadas` - Figuras soportadas

### Volumen Controller (`/api/volumen`)
- `POST /api/volumen/calcular` - Calcular volumen
- `GET /api/volumen` - Obtener todos los cálculos (100 registros)
- `GET /api/volumen/{id}` - Obtener por ID
- `PUT /api/volumen/{id}` - Actualizar cálculo
- `DELETE /api/volumen/{id}` - Eliminar cálculo
- `GET /api/volumen/buscar/tipo/{tipo}` - Buscar por tipo
- `GET /api/volumen/buscar/nombre/{nombre}` - Buscar por nombre
- `GET /api/volumen/figuras-soportadas` - Figuras soportadas

## 🧪 Pruebas Rápidas

### 1. Probar que la API funciona:
```bash
curl http://localhost:5098/api/test
```

### 2. Verificar estado de la base de datos:
```bash
curl http://localhost:5098/api/seeder/status
```

### 3. Ver todos los cálculos de área:
```bash
curl http://localhost:5098/api/area
```

### 4. Ver todos los cálculos de volumen:
```bash
curl http://localhost:5098/api/volumen
```

### 5. Buscar áreas por tipo:
```bash
curl http://localhost:5098/api/area/buscar/tipo/cuadrado
```

### 6. Buscar volúmenes por tipo:
```bash
curl http://localhost:5098/api/volumen/buscar/tipo/cubo
```

## 📊 Datos Generados

### Áreas (100 registros):
- **Cuadrados**: Con lados aleatorios entre 1-21 unidades
- **Rectángulos**: Con largo 2-17 y ancho 1-11 unidades
- **Triángulos**: Con base 2-14 y altura 1-9 unidades
- **Círculos**: Con radio 1-9 unidades
- **Trapecios**: Con bases y altura aleatorias

### Volúmenes (100 registros):
- **Cubos**: Con lados aleatorios entre 1-11 unidades
- **Prismas**: Con base 5-25 y altura 2-17 unidades
- **Cilindros**: Con radio 1-7 y altura 2-14 unidades
- **Esferas**: Con radio 1-6 unidades
- **Conos**: Con radio 1-5 y altura 2-12 unidades
- **Paralelepípedos**: Con dimensiones aleatorias
- **Pirámides**: Con base 5-30 y altura 2-14 unidades

## 🚨 Solución de Problemas

### ❌ Error 404 en Swagger
**Solución:**
1. Asegúrate de ejecutar TODOS los pasos en orden
2. Verifica que no haya errores en la consola
3. Intenta acceder a `http://localhost:5098/api/test` primero
4. Si funciona, entonces accede a `http://localhost:5098/swagger`

### ❌ Error de Compilación
**Solución:**
```bash
dotnet clean
dotnet restore
dotnet build
```

### ❌ Error de Base de Datos
**Solución:**
```bash
# Eliminar migraciones existentes (si las hay)
rmdir /s Migrations

# Crear nueva migración
dotnet ef migrations add InitialCreate
dotnet ef database update
```

### ❌ Puerto en Uso
**Solución:**
- Cambia el puerto en `Properties/launchSettings.json`
- O mata el proceso que está usando el puerto

### ❌ No se ven los datos
**Solución:**
1. Verifica que aparezca el mensaje "✅ Base de datos poblada con datos de prueba" en la consola
2. Accede a `http://localhost:5098/api/seeder/status` para ver el estado
3. Si no hay datos, ejecuta `POST http://localhost:5098/api/seeder/seed`

## 🔄 Migración a MySQL (Opcional)

Una vez que la API funcione correctamente con SQLite, puedes migrar a MySQL:

1. Cambiar `MiAreaVol.csproj`:
   ```xml
   <PackageReference Include="Pomelo.EntityFrameworkCore.MySql" Version="9.0.0-preview.3.efcore.9.0.0" />
   ```

2. Cambiar `Program.cs`:
   ```csharp
   options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"), 
       ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection")));
   ```

3. Cambiar `appsettings.json`:
   ```json
   "DefaultConnection": "mysql://root:smFBmeeETAkxGcGuLJifqRXZZODKFzLE@hopper.proxy.rlwy.net:14094/railway"
   ```

4. Eliminar migraciones existentes y crear nuevas:
   ```bash
   dotnet ef migrations remove
   dotnet ef migrations add InitialCreateMySQL
   dotnet ef database update
   ```

## ✅ Verificación Final

Si todo funciona correctamente, deberías poder:
1. ✅ Acceder a `http://localhost:5098/api/test`
2. ✅ Ver Swagger en `http://localhost:5098/swagger`
3. ✅ Ver 200 registros en la base de datos (`/api/seeder/status`)
4. ✅ Ver 100 registros de áreas (`/api/area`)
5. ✅ Ver 100 registros de volúmenes (`/api/volumen`)
6. ✅ Hacer cálculos de área y volumen
7. ✅ Ver todos los endpoints en Swagger UI 