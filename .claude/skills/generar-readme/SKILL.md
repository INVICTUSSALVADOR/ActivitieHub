---
name: generar-readme
description: Analiza todo el repositorio EventsHub y (re)genera README.md en la raíz con documentación actualizada del proyecto. Úsala cuando el usuario pida documentar el proyecto o actualizar el README.
---

# Generar README de EventsHub

Objetivo: dejar un README.md en la raíz que permita a un compañero o profesor clonar,
levantar y entender el proyecto sin preguntar nada. Escríbelo en español.

## 1. Analiza (solo lectura)
- Lista los archivos versionados con `git ls-files` (no leas bin/, obj/, node_modules/, dist/).
- Lee: EventsHub.slnx, todos los *.csproj, Program.cs de Api y OpenApi, controllers,
  entidades de Domain, AppDbContext, DbInitializer, migraciones (solo nombres),
  appsettings*.json, launchSettings.json, .config/dotnet-tools.json, src/nswag/*.nswag,
  restart-api.ps1, stop-api.ps1, docs/*.md, web/package.json, web/vite.config.ts,
  web/src/**, tests/** (NUnit y colección Bruno), CLAUDE.md y el README actual.
- Saca versiones, puertos, rutas, variables de entorno y comandos de los archivos;
  no los inventes. Si algo no se puede confirmar, márcalo como "Pendiente" en vez de
  adivinar.
- Revisa `git log --oneline -20` para la sección de estado/avance.

## 2. Escribe README.md con estas secciones
1. Título + descripción breve (qué es, curso ICI 2026 01).
2. Tecnologías: tabla capa → tecnología → versión (tomada de csproj/package.json).
3. Estructura del repositorio: árbol corto de carpetas principales con una línea cada una.
4. Arquitectura: capas Domain → Persistence → Application → Api, el host OpenApi y el
   frontend; un diagrama Mermaid de dependencias entre proyectos y del flujo
   web → API → SQLite.
5. Requisitos previos (.NET SDK, Node/npm, herramienta dotnet-ef, mkcert/HTTPS).
6. Puesta en marcha: backend (dotnet restore / restart-api.ps1 y sus flags), frontend
   (web/.env con VITE_API_URL, npm install, npm run dev), puertos y CORS.
7. Base de datos: SQLite, migraciones automáticas al arrancar, datos semilla, cómo
   crear una migración y cómo reiniciar los datos (-Reset).
8. API: tabla de endpoints (método, ruta, descripción, respuestas) tomada de los
   controllers y de src/openapi/EventsHub.v1.json; modelo Event con sus campos.
9. OpenAPI y cliente generado: pasos resumidos con enlace a docs/OpenApi 1.md;
   archivos que no se editan a mano.
10. Pruebas: cómo correr NUnit (todas y una sola), qué hace GlobalTestSetup, cómo
    correr la colección Bruno y su entorno local.
11. Scripts útiles: restart-api.ps1 / stop-api.ps1.
12. Convenciones: ramas PARCIAL-NN, formato de commits `feat(PARCIAL-NN) - ...`.
13. Estado actual / pendientes: lo que se ve a medias en el código (p. ej. Application
    vacío, WeatherForecast de plantilla, frontend mínimo).

## 3. Reglas
- No dupliques docs/OpenApi 1.md; resúmelo y enlázalo.
- Enlaces relativos a archivos del repo; bloques de código con lenguaje
  (powershell, bash, json).
- Si ya existe contenido escrito a mano en el README, consérvalo o intégralo; no lo
  borres sin avisar.
- No modifiques ningún otro archivo. Al terminar, muestra un resumen de las secciones
  y lista los datos que quedaron como "Pendiente".
