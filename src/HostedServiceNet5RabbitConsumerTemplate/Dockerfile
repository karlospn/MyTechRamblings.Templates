FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build-env
WORKDIR /app

# Set intermediate stage as build
LABEL stage=build

# Copy csproj and restore as distinct layers
COPY *.sln ./
COPY /src/ApplicationName.RabbitConsumer.Worker/*.csproj ./src/ApplicationName.RabbitConsumer.Worker/
COPY /src/ApplicationName.Library.Contracts/*.csproj ./src/ApplicationName.Library.Contracts/
COPY /src/ApplicationName.Library.Impl/*.csproj ./src/ApplicationName.Library.Impl/
COPY /src/ApplicationName.Repository.Contracts/*.csproj ./src/ApplicationName.Repository.Contracts/
COPY /src/ApplicationName.Repository.Impl/*.csproj ./src/ApplicationName.Repository.Impl/
COPY /build/*.props ./build/
COPY Packages.props ./ 

RUN dotnet restore "./src/ApplicationName.RabbitConsumer.Worker/ApplicationName.RabbitConsumer.Worker.csproj" -s "https://api.nuget.org/v3/index.json"

# Copy everything else and build
COPY . ./
RUN dotnet publish src/ApplicationName.RabbitConsumer.Worker/*.csproj -c Release -o /app/publish

# Build runtime imagedock
FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim
WORKDIR /app
COPY --from=build-env /app/publish .
ENTRYPOINT ["dotnet", "ApplicationName.RabbitConsumer.Worker.dll"]
