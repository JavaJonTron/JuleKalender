# 🚀 Quick Start Guide - Lokal Testing

## 🎄 Steg 1: Database Setup

Før du kan kjøre applikasjonen må du opprette database migrations:

```bash
# Fra root-mappen (JuleKalender/)
dotnet ef migrations add InitialCreate --project src/Infrastructure/Infrastructure.csproj --startup-project src/WebApi/WebApi.csproj --output-dir Persistence/Migrations

# Opprett database
dotnet ef database update --project src/Infrastructure/Infrastructure.csproj --startup-project src/WebApi/WebApi.csproj
```

## 🎯 Steg 2: Start Applikasjonen

### Metode 1: VS Code (Anbefalt) ✨

1. Åpne mappen i VS Code
2. Trykk `F5` eller gå til **Run and Debug** (Ctrl+Shift+D)
3. Velg **"🎄 Launch Julekalender (API + Web)"** fra dropdown
4. Klikk **Start Debugging** (grønn play-knapp)

**Dette starter automatisk:**
- ✅ API på `https://localhost:7001` med Swagger UI
- ✅ Web app på `https://localhost:5001` i Chrome

### Metode 2: Terminal (Manuelt)

**Terminal 1 - API:**
```bash
cd src/WebApi
dotnet run
```
Åpne Swagger: `https://localhost:7001/swagger`

**Terminal 2 - Web:**
```bash
cd src/Web
dotnet run
```
Åpne app: `https://localhost:5001`

## 🔍 Testing

1. **Opprett en kategori:**
   - Gå til "Kategorier"
   - Klikk "Legg til ny kategori"
   - Fyll inn navn og beskrivelse
   - Klikk "Lagre"

2. **Legg til gave:**
   - Klikk på kategorien du opprettet
   - Klikk "Legg til gave"
   - Fyll inn gave-detaljer
   - Klikk "Lagre"

3. **Test søk:**
   - Gå til "Alle Gaver"
   - Prøv søkefeltet

## 🛠️ Troubleshooting

### Database feil?
```bash
# Slett database og start på nytt
dotnet ef database drop --project src/Infrastructure/Infrastructure.csproj --startup-project src/WebApi/WebApi.csproj --force

# Opprett på nytt
dotnet ef database update --project src/Infrastructure/Infrastructure.csproj --startup-project src/WebApi/WebApi.csproj
```

### Port i bruk?
Endre porter i `appsettings.json`:
- WebApi: endre fra 7001 til annen port
- Web: endre fra 5001 til annen port

## 📌 Nyttige VS Code Snarveier

- `F5` - Start debugging
- `Shift+F5` - Stopp debugging
- `Ctrl+Shift+B` - Build all
- `Ctrl+Shift+P` → "Tasks: Run Build Task" - Build specific project

## 🎁 Neste Steg

Når lokal testing fungerer:
1. Se [README.md](file:///c:/Users/jstorvik/Julekalender/JuleKalender/README.md) for deployment til Render
2. Konfigurer Supabase PostgreSQL database
3. Deploy til produksjon

God testing! 🎄
