# Jobinator

Jobinator je webová aplikace typu job board (pracovní portál) vyvinutá v prostředí ASP.NET Core MVC. Cílem projektu bylo vytvořit přehlednou platformu pro propojování zaměstnavatelů a uchazečů o práci, která umožňuje efektivní správu nabídek a poptávek v různých odvětvích.

## 🚀 Klíčové vlastnosti

- **Správa inzerce**: Podpora dvou typů příspěvků — **Nabídka** (ze strany firem) a **Poptávka** (ze strany uchazečů).
- **Kategorizace**: Systém řazení inzerátů do kategorií jako IT, Logistika, Stavebnictví, Zdravotnictví a Finance.
- **Autentizace a bezpečnost**: Robustní systém registrace a přihlašování uživatelů využívající hashování hesel pomocí knihovny BCrypt.
- **Interakce**: Možnost označování příspěvků ("To se mi líbí") pro sledování zajímavých příležitostí.
- **Administrační rozhraní**: Kompletní dashboard pro administrátory umožňující správu uživatelských účtů a kontrolu obsahu portálu.

## 🛠️ Použité technologie

- **Backend**: C# / ASP.NET Core 8.0 (vzor MVC)
- **Databáze**: Microsoft SQL Server
- **ORM**: Entity Framework Core (přístup Code-First)
- **Zabezpečení**: BCrypt.Net-Next pro bezpečné ukládání hesel
- **Frontend**: Razor Pages, Bootstrap 5, jQuery
- **Nástroje**: EF Core Migrations, Bogus (seeding dat)

## 📦 Instalace a spuštění

Pro lokální spuštění projektu je vyžadováno rozhraní [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) a SQL Server (např. LocalDB, který je součástí Visual Studia).

1. **Klonování repozitáře**
   ```bash
   git clone https://github.com/sps-trutnov-eps/projekt-3ep-jobinator.git
   cd projekt-3ep-jobinator
   ```

2. **Konfigurace databáze**
   Projekt je přednastaven pro použití **SQL Server LocalDB**. Pokud vaše prostředí vyžaduje jinou konfiguraci, upravte `ConnectionStrings` v souboru:
   `Source/Jobinator/appsettings.json`

3. **Inicializace databáze**
   Pro vytvoření databázového schématu pomocí Entity Framework Migrations spusťte v adresáři `Source/Jobinator`:
   ```bash
   dotnet ef database update
   ```

4. **Spuštění aplikace**
   Aplikaci spustíte příkazem:
   ```bash
   dotnet run --project Source/Jobinator/Jobinator.csproj
   ```
   Po spuštění bude portál dostupný na adrese `http://localhost:5000` (nebo dle výpisu v konzoli).

### 💡 Užitečné informace
- **Testovací data**: Při prvním spuštění v režimu *Development* aplikace automaticky vygeneruje testovací uživatele a inzeráty pomocí knihovny Bogus.
- **Administrátorský přístup**: Pro přístup do správy systému využijte výchozí údaje (viz `appsettings.json`):
  - **Uživatel**: `admin`
  - **Heslo**: `admin`

## 👥 Autoři

Tento projekt vznikl jako týmová práce:

- **Matyáš Sýs**
- **Lukáš Hajnyš**
- **Jakub Tryzna**