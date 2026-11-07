# Bicycle Rental & Mushroom Finder (C# Windows Forms)

## Requirements
- Windows
- .NET 8 SDK (https://dotnet.microsoft.com/download) **or** Visual Studio 2022 with the ".NET desktop development" workload

## Run
**Visual Studio:** open `RentalAndMushroomApp.csproj`, press F5.

**Command line:**
```
cd RentalAndMushroomApp
dotnet run
```

## Login
- Username: `admin`
- Password: `1234`

Choose **Bicycle Rental** or **Mushroom Finder**, then click Login.
Each form has a **Logout** button that returns to the login form.

## Files
- `Program.cs` – entry point
- `LoginForm.cs` – login + choice of form (hard-coded credentials, no backend)
- `BicycleForm.cs` – Problem 1: rental cost by hourly rate ranges
- `MushroomForm.cs` – Problem 2: identifies the mushroom in at most 3 yes/no questions
