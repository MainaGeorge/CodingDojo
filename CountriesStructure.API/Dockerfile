#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["../CountriesStructure.API/CountriesStructure.API.csproj", "../CountriesStructure.API/"]
COPY ["CountriesStructure.Library.csproj", "."]
RUN dotnet restore "../CountriesStructure.API/CountriesStructure.API.csproj"
COPY . .
WORKDIR "/src/../CountriesStructure.API"
RUN dotnet build "CountriesStructure.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CountriesStructure.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CountriesStructure.API.dll"]
