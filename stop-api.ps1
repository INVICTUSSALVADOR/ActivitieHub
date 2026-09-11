<#
.SYNOPSIS
    Detiene EventsHub.Api y mata todos los procesos relacionados que puedan
    quedar bloqueando archivos (dotnet watch, compiladores en segundo plano, etc).

.EXAMPLE
    .\stop-api.ps1
#>

Write-Host "==> Buscando procesos dotnet/EventsHub en ejecución..." -ForegroundColor Cyan
$procesos = Get-Process | Where-Object { $_.ProcessName -match 'dotnet|EventsHub|VBCSCompiler|MSBuild' }

if (-not $procesos) {
    Write-Host "No hay procesos en ejecución. Nada que detener." -ForegroundColor Yellow
    return
}

$procesos | Select-Object Id, ProcessName | Format-Table -AutoSize

Write-Host "==> Deteniendo procesos..." -ForegroundColor Cyan
$procesos | Stop-Process -Force -ErrorAction SilentlyContinue
Start-Sleep -Milliseconds 500

$restantes = Get-Process | Where-Object { $_.ProcessName -match 'dotnet|EventsHub|VBCSCompiler|MSBuild' }
if ($restantes) {
    Write-Host "==> Advertencia: algunos procesos siguen activos:" -ForegroundColor Red
    $restantes | Select-Object Id, ProcessName | Format-Table -AutoSize
} else {
    Write-Host "==> Todos los procesos fueron detenidos correctamente." -ForegroundColor Green
}
