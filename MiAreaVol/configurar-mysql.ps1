# Script para configurar MySQL Railway
Write-Host "🔧 Configurando MySQL Railway..." -ForegroundColor Yellow

# 1. Limpiar proyecto
Write-Host "📦 Limpiando proyecto..." -ForegroundColor Cyan
dotnet clean
dotnet restore

# 2. Eliminar migraciones existentes si existen
if (Test-Path "Migrations") {
    Write-Host "🗑️ Eliminando migraciones existentes..." -ForegroundColor Cyan
    Remove-Item -Recurse -Force "Migrations"
}

# 3. Crear nueva migración para MySQL
Write-Host "📝 Creando migración para MySQL..." -ForegroundColor Cyan
dotnet ef migrations add InitialCreateMySQL

# 4. Actualizar base de datos
Write-Host "🗄️ Actualizando base de datos en Railway..." -ForegroundColor Cyan
dotnet ef database update

# 5. Compilar proyecto
Write-Host "🔨 Compilando proyecto..." -ForegroundColor Cyan
dotnet build

Write-Host "✅ Configuración completada!" -ForegroundColor Green
Write-Host "🚀 Ejecuta: dotnet run" -ForegroundColor Green
Write-Host "🌐 Swagger: http://localhost:5098/swagger" -ForegroundColor Green
Write-Host "📊 Estado BD: http://localhost:5098/api/seeder/status" -ForegroundColor Green 