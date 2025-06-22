# Script para limpiar migraciones y configurar MySQL Railway

Write-Host "🧹 Limpiando migraciones existentes..." -ForegroundColor Yellow

# Eliminar carpeta de migraciones si existe
if (Test-Path "Migrations") {
    Remove-Item -Recurse -Force "Migrations"
    Write-Host "✅ Carpeta Migrations eliminada" -ForegroundColor Green
} else {
    Write-Host "ℹ️ No se encontró carpeta Migrations" -ForegroundColor Blue
}

# Limpiar proyecto
Write-Host "🧹 Limpiando proyecto..." -ForegroundColor Yellow
dotnet clean

# Restaurar dependencias
Write-Host "📦 Restaurando dependencias..." -ForegroundColor Yellow
dotnet restore

# Crear nueva migración para MySQL
Write-Host "🗄️ Creando migración para MySQL..." -ForegroundColor Yellow
dotnet ef migrations add InitialCreateMySQL

# Actualizar base de datos
Write-Host "🔄 Actualizando base de datos en Railway..." -ForegroundColor Yellow
dotnet ef database update

Write-Host "✅ Configuración completada!" -ForegroundColor Green
Write-Host "🚀 Ejecuta 'dotnet run' para iniciar la aplicación" -ForegroundColor Cyan 