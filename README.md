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

Zprovoznění projektu na vašem počítači je jednoduché. Budete k tomu potřebovat nainstalované rozhraní [.NET 8.0](https://dotnet.microsoft.com/download/dotnet/8.0) a SQL Server (např. LocalDB, který je součástí Visual Studia).

1.  **Stažení projektu**:
    Nejprve si stáhněte projekt do svého lokálního adresáře:
    ```bash
    git clone https://github.com/vas-nick/jobinator.git
    cd jobinator/Source/Jobinator
    ```

2. **Konfigurace databáze**:
    Ujistěte se, že v souboru appsettings.json je správně definován ConnectionStrings. Pokud využíváte Visual Studio a LocalDB, výchozí nastavení by mělo být funkční.

3.  **Příprava databáze**:
    Pomocí nástroje EF Core vytvořte databázové schéma a naplňte jej úvodními daty:
    ```bash
    dotnet ef database update
    ```

4.  **Spuštění aplikace**:
    Projekt spustíte standardním příkazem pro .NET CLI:
    ```bash
    dotnet run
    ```
    Aplikace bude následně dostupná na adrese http://localhost:5000 (případně na portu specifikovaném ve vašem výstupu konzole).

### 💡 Tipy pro prohlížení
- **Testovací data**: Při prvním spuštění se web automaticky naplní vzorovými inzeráty a uživateli, takže uvidíte funkční obsah bez nutnosti cokoliv vyplňovat.
- **Administrace**: Do správy webu (dashboardu) se dostanete přes adresu `/Admin`. Výchozí údaje pro vstup jsou: jméno `admin` a heslo `admin`.
- **Přihlášení za uživatele**: Všichni automaticky vytvoření uživatelé mají pro snadné testování nastavené stejné heslo: `Password123`.

## 👥 Autoři

Tento projekt vznikl jako týmová práce:

- **Matyáš Sýs**
- **Lukáš Hajnyš**
- **Jakub Tryzna**