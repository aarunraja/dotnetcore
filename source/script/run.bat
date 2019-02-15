@echo off
cd..
dotnet clean
dotnet restore
dotnet build
dotnet run -p src\host\webapi\Bapatla.CMS.WebApi.csproj
pause