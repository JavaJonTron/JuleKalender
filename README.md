# 🎄 Julekalender Gift Tracker

En .NET 8 Blazor webapp med MudBlazor for å holde oversikt over julekalender-gaver mellom venner.

## 🏗️ Arkitektur

Prosjektet følger **Clean Architecture** og **Domain-Driven Design** prinsipper:

```
├── Domain/              # Entities, value objects, interfaces (ingen dependencies)
├── Application/         # Use cases (CQRS med MediatR), DTOs
├── Infrastructure/      # EF Core, PostgreSQL repositories
├── WebApi/             # REST API controllers
└── Web/                # Blazor Server UI med MudBlazor
```

## ✨ Funksjoner

- ✅ **Kategorier**: Opprett og administrer gavekategorier
- ✅ **Gaver**: Registrer gaver med navn, beskrivelse, dato, mottaker
- ✅ **Søk**: Søk i både kategorier og gaver
- ✅ **Statistikk**: Se antall gaver per kategori, sist brukt dato
- ✅ **Modern UI**: MudBlazor med mørk tema og julefarger

## 🚀 Kom i gang

### Forutsetninger

- .NET 8 SDK
- PostgreSQL database (eller Supabase)

### Oppsett

1. **Klon repository**
   ```bash
   git clone <repo-url>
   cd JuleKalender
   ```

2. **Oppdater connection string**
   
   I `src/WebApi/appsettings.json`:
   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Host=localhost;Database=julekalender;Username=postgres;Password=postgres"
     }
   }
   ```

3. **Installer EF Core tools** (hvis ikke allerede installert)
   ```bash
   dotnet tool install --global dotnet-ef
   ```

4. **Opprett database migrations**
   ```bash
   cd src/Infrastructure
   dotnet ef migrations add InitialCreate --startup-project ../WebApi
   dotnet ef database update --startup-project ../WebApi
   ```

5. **Kjør API**
   ```bash
   cd src/WebApi
   dotnet run
   ```
   API kjører på: `https://localhost:7001`

6. **Kjør Blazor Web** (i ny terminal)
   ```bash
   cd src/Web
   dotnet run
   ```
   Web kjører på: `https://localhost:5001`

## 📦 Deployment til Render

### Web Service (Anbefalt)

1. Opprett en **PostgreSQL database** på Render (eller bruk Supabase)

2. Opprett en **Web Service** på Render:
   - Build Command: `dotnet publish src/WebApi/WebApi.csproj -c Release -o out`
   - Start Command: `cd out && ./WebApi`
   - Add environment variable: `ConnectionStrings__DefaultConnection=<din-connection-string>`

3. For Blazor Web, opprett en separat Web Service:
   - Build Command: `dotnet publish src/Web/Web.csproj -c Release -o out`
   - Start Command: `cd out && ./Web`
   - Add environment variable: `ApiBaseUrl=<din-api-url>`

## 🛠️ Teknologi Stack

- **.NET 8** - Framework
- **Blazor Server** - UI framework
- **MudBlazor** - UI component library
- **Entity Framework Core** - ORM
- **PostgreSQL** - Database
- **MediatR** - CQRS pattern
- **Swagger** - API documentation

## 📝 API Endpoints

### Categories
- `GET /api/categories` - Hent alle kategorier
- `GET /api/categories?search={term}` - Søk kategorier
- `GET /api/categories/{id}` - Hent kategori med gaver
- `POST /api/categories` - Opprett kategori

### Gifts
- `GET /api/gifts` - Hent alle gaver
- `GET /api/gifts?categoryId={id}` - Gaver i kategori
- `GET /api/gifts?search={term}` - Søk gaver
- `POST /api/gifts` - Opprett gave

## 🎨 UI Sider

- **/** - Homepage med navigasjon
- **/categories** - Alle kategorier med søk
- **/categories/{id}** - Kategori detaljer med gaver
- **/gifts** - Alle gaver i grid-visning

## 📄 Lisens

Dette er et personlig prosjekt.
