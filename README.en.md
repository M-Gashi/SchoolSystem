# School System

A Windows desktop application for managing school data, students, teachers, academic structure, and Quran learning progress. It is built with Windows Forms and separates the user interface, business logic, and data-access responsibilities into distinct layers.

For a presentation-focused overview suitable for recruiters and GitHub visitors, see the [project portfolio case study](PORTFOLIO.md).

## Current features

- Manage people, students, and teachers.
- Manage educational stages, grades, sections, and subjects.
- Assign subjects to students.
- Track Quran parts, pages, and student progress.
- Record recitation errors and review learning progress.

## Solution architecture

| Project | Responsibility |
| --- | --- |
| `SchoolSystem.Presentation` | Windows Forms user interface |
| `SchoolSystem.Business` | Business rules and operation validation |
| `SchoolSystem.Data` | SQL Server access through ADO.NET |
| `SchoolSystem.Enums` | Enums and types shared across layers |
| `SchoolSystem.Tests` | Unit tests for business logic that does not require a database |

## Domain organization

Files within each layer are grouped by business domain instead of being placed in generic folders. The main domains are:

| Domain | Example content |
| --- | --- |
| `People` | People, countries, and basic personal data |
| `Students` | Student data and user interfaces |
| `Teachers` | Teacher data and user interfaces |
| `AcademicStructure` | Stages, grades, sections, and the academic tree |
| `Subjects` | Subjects and student-subject assignments |
| `Quran` | Pages, parts, tracks, and student progress |

The same domain appears across layers when needed. For example, student interfaces are located in `SchoolSystem.Presentation/Students`, student business logic in `SchoolSystem.Business/Students`, and student data access in `SchoolSystem.Data/Students`.

## Requirements

- Windows 10 or later.
- Visual Studio 2022 with the **.NET desktop development** workload.
- .NET Framework 4.8 Developer Pack.
- Microsoft SQL Server.

## Database setup

The application expects a local SQL Server database named `SchoolSystemDatabase`.

The connection configuration is stored in:

```text
SchoolSystem.Presentation/App.config
```

The default configuration uses Windows Authentication:

```xml
<add name="SchoolSystemDatabase"
     connectionString="Data Source=.;Initial Catalog=SchoolSystemDatabase;Integrated Security=True;TrustServerCertificate=True;"
     providerName="System.Data.SqlClient" />
```

Change `Data Source` to match your SQL Server instance. Do not commit real usernames or passwords to the repository; use secure local configuration when credentials are required.

Create a new database schema by running:

```text
Database/Scripts/001_CreateSchema.sql
```

Additional setup instructions and reference-data notes are available in `Database/README.md`. The schema script does not include person, student, or teacher records.

## Running the application

1. Open `SchoolSystem.Presentation/SchoolSystem.sln` in Visual Studio.
2. Confirm that the database is available and that the connection string in `SchoolSystem.Presentation/App.config` is correct.
3. Set `SchoolSystem.Presentation` as the startup project.
4. Build the solution using **Build > Build Solution**.
5. Press `F5` to run the application.

The solution can also be built from Developer PowerShell for Visual Studio:

```powershell
msbuild .\SchoolSystem.Presentation\SchoolSystem.sln /p:Configuration=Debug
```

## Tests

`SchoolSystem.Tests` contains MSTest tests covering:

- Default values for subject-tree and academic-structure nodes.
- Constructing nodes from data and adding child nodes.
- Extracting a Quran page number from an image filename.

These tests do not connect to SQL Server or modify any data. To run them in Visual Studio, open **Test Explorer** and select **Run All Tests**. After building the solution, they can also be run from Developer PowerShell:

```powershell
vstest.console .\SchoolSystem.Tests\bin\Debug\SchoolSystem.Tests.dll
```

## Local assets

- `Image` contains images used by the main navigation interfaces. During a build, the project automatically copies them into an `Image` folder beside the executable.
- `Quran` contains Quran page images used for synchronization and display. Because of their size, these images are not copied into the build output.

When synchronizing Quran pages, the application searches for the image folder in this order:

1. The folder selected by the user and saved in the `QuranFolderPath` setting.
2. A `Quran` folder beside the executable.
3. The `Quran` folder in the project root during development.
4. A folder selected by the user through the folder-selection dialog.

If the Quran folder is moved to another computer, running synchronization from its new location updates the page paths stored in the database.

## Current project status

- The solution targets .NET Framework 4.8.
- The application is designed for Windows.
- The project currently includes 9 unit tests and no database integration tests.
- The database must be available before using database-dependent features.
