FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build-env
WORKDIR /app

# Copy csproj and restore
COPY *.csproj  ./
RUN dotnet restore "./ApplicationName.Function.csproj" -s "https://api.nuget.org/v3/index.json"

# Copy everything else and publish
COPY . ./
RUN dotnet publish "ApplicationName.Function.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/azure-functions/dotnet:3.0 AS base
WORKDIR /home/site/wwwroot
EXPOSE 80
COPY --from=build-env /app/publish .
ENV AzureWebJobsScriptRoot=/home/site/wwwroot \
    AzureFunctionsJobHost__Logging__Console__IsEnabled=true