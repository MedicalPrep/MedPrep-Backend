# MEDPREP BACKEND

## Description

This is the backend for the MedPrep application.

## GTTING STARTED

1. Clone the repository

```bash
git clone https://github.com/anjolaoluwaakindipe/MedPrep-Backend.git
```

2. Make sure to have the latest version of dotnet installed (version 8 or higher). To installed dotnet click [here](https://dotnet.microsoft.com/en-us/download)
3. Make sure to have the latest version of postgresql installed. To install postgresql click [here](https://www.postgresql.org/download/)
4. Create a appsettings.Development.json file and use the keys in the appsettings.json file to fill in the values

```bash
touch appsettings.Development.json
```

5. Install all dependencies

6. You will need to make sure your database is updated to the latest migrations. Make sure to update the appsettings.Development.json file with the right data as indicated in step 4. You can run these command to interact with your database:

```bash
dotnet ef database update --project ./src/MedPrep.Api # Update database to latest migration

```

```bash
dotnet ef database drop -p ./src/MedPrep.Api # Drop database incase of braking changes in migration
```

```bash
dotnet ef migrations add <MigrationName> -p=./src/MedPrep.Api # Creates a new migration with specified name. Please make sure to make name Camel case e.g "InitialMigrations"
```

6. Start the application

```bash
dotnet run --project ./src/MedPrep.Api
```

## Commands

-   Install all dependencies

```bash
dotnet restore .
```

-   Building the project

```bash
dotnet build
```

-   Running the project

```bash
dotnet run --project ./src/MedPrep.Api
```

-   Running the project with hot reload

```bash
dotnet watch run --project ./src/MedPrep.Api
```

-   Update Database

```bash
dotnet ef database update --project ./src/MedPrep.Api # Update database to latest migration

```

-   Drop database

```bash
dotnet ef database drop -p ./src/MedPrep.Api # Drop database incase of braking changes in migration
```

-   Add new Migration

```bash
dotnet ef migrations add <MigrationName> -p=./src/MedPrep.Api # Creates a new migration with specified name. Please make sure to make name Camel case e.g "InitialMigrations"
```
