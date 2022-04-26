#Build Stage
FROM mcr.microsoft.com/dotnet/sdk:6.0-focal as build
WORKDIR /source
COPY . .
RUN dotnet restore "./CountriesStructure.API/CountriesStructure.API.csproj" --disable-parallel
RUN dotnet publish "./CountriesStructure.API/CountriesStructure.API.csproj" -c release -o /app --no-restore

#Serve Stage
FROM mcr.microsoft.com/dotnet/sdk:6.0-focal
WORKDIR /app
COPY --from=build /app ./
# EXPOSE 5000
ENTRYPOINT ["dotnet", "CountriesStructure.API.dll"]