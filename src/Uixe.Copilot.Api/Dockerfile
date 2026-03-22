FROM mcr.microsoft.com/dotnet/sdk:10.0-preview AS build
WORKDIR /src

COPY . .
RUN dotnet restore src/Uixe.Copilot.Api/Uixe.Copilot.Api.csproj
RUN dotnet publish src/Uixe.Copilot.Api/Uixe.Copilot.Api.csproj -c Debug -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:10.0-preview
WORKDIR /app

ENV ASPNETCORE_URLS=http://0.0.0.0:5057
EXPOSE 5057

COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Uixe.Copilot.Api.dll"]
