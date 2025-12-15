# VS Code Workspace Setup

Dette er VS Code workspace-konfigurasjonen for Julekalender prosjektet.

## 🚀 Slik tester du lokalt:

### 1️⃣ Opprett database først

Kjør disse kommandoene i terminalen:

```bash
# Opprett migrations
dotnet ef migrations add InitialCreate --project src/Infrastructure/Infrastructure.csproj --startup-project src/WebApi/WebApi.csproj --output-dir Persistence/Migrations

# Opprett database
dotnet ef database update --project src/Infrastructure/Infrastructure.csproj --startup-project src/WebApi/WebApi.csproj
```

### 2️⃣ Start applikasjonen

**Trykk `F5`** eller:

1. Gå til **Run and Debug** (Ctrl+Shift+D)
2. Velg **"🎄 Launch Julekalender (API + Web)"**
3. Klikk grønn play-knapp

Dette åpner:
- **Swagger UI** på `https://localhost:7001/swagger` (API)
- **Web App** på `https://localhost:5001` (Blazor UI)

## 📁 Workspace Filer

- [launch.json](file:///c:/Users/jstorvik/Julekalender/JuleKalender/.vscode/launch.json) - Debug konfigurasjon
- [tasks.json](file:///c:/Users/jstorvik/Julekalender/JuleKalender/.vscode/tasks.json) - Build tasks
- [extensions.json](file:///c:/Users/jstorvik/Julekalender/JuleKalender/.vscode/extensions.json) - Anbefalte VS Code extensions

## 🛠️ Available Configurations

### Compound (Anbefalt)
- **🎄 Launch Julekalender (API + Web)** - Starter begge samtidig

### Individual
- **Launch API (Swagger)** - Kun API
- **Launch Web (Chrome)** - Kun Web app

## 📝 Tips

- Begge applikasjoner må kjøre samtidig for at appen skal fungere
- API må være oppe før Web kan koble til
- Se [QUICKSTART.md](file:///c:/Users/jstorvik/Julekalender/JuleKalender/QUICKSTART.md) for fullstendig guide
