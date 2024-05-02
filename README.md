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
