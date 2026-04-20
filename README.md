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