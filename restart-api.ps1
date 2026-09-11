<#
.SYNOPSIS
    Reinicia el entorno de desarrollo de EventsHub.Api antes de una prueba.

.PARAMETER Reset
    Borra eventshub.db (y sus archivos -shm/-wal) para volver a sembrar los datos
    con IDs nuevos.

.PARAMETER Clean
    Corre "dotnet clean" antes de levantar la API (bin/ y obj/ de toda la solución).

.EXAMPLE
    .\restart-api.ps1
    Mata procesos colgados y levanta la API conservando los datos actuales.

.EXAMPLE
    .\restart-api.ps1 -Reset
    Mata procesos, borra la base de datos (IDs nuevos al sembrar) y levanta la API.

.EXAMPLE
    .\restart-api.ps1 -Reset -Clean
    Reinicio completo: procesos, base de datos y binarios compilados.
#>
param(
    [switch]$Reset,
    [switch]$Clean
)

$root = $PSScriptRoot
$apiDir = Join-Path $root "src\EventsHub.Api"

Write-Host "==> Deteniendo procesos dotnet/EventsHub en ejecución..." -ForegroundColor Cyan
Get-Process | Where-Object { $_.ProcessName -match 'dotnet|EventsHub|VBCSCompiler|MSBuild' } |
    Stop-Process -Force -ErrorAction SilentlyContinue
Start-Sleep -Milliseconds 500

if ($Reset) {
    Write-Host "==> Borrando eventshub.db para regenerar los datos sembrados..." -ForegroundColor Cyan
    Remove-Item (Join-Path $apiDir "eventshub.db*") -ErrorAction SilentlyContinue
}

if ($Clean) {
    Write-Host "==> Ejecutando dotnet clean..." -ForegroundColor Cyan
    dotnet clean $root
}

Write-Host "==> Levantando la API con dotnet watch..." -ForegroundColor Cyan
Set-Location $apiDir
dotnet watch
