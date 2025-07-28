# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/runtime:9.0 AS base
USER $APP_UID
WORKDIR /app


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/StockMode.EmailWorker/StockMode.EmailWorker.csproj", "src/StockMode.EmailWorker/"]
COPY ["src/StockMode.Application/StockMode.Application.csproj", "src/StockMode.Application/"]
COPY ["src/StockMode.Domain/StockMode.Domain.csproj", "src/StockMode.Domain/"]
COPY ["src/StockMode.Domain.Core/StockMode.Domain.Core.csproj", "src/StockMode.Domain.Core/"]
COPY ["src/StockMode.Infra.CrossCutting.IoC/StockMode.Infra.CrossCutting.IoC.csproj", "src/StockMode.Infra.CrossCutting.IoC/"]
COPY ["src/StockMode.Infra.Data/StockMode.Infra.Data.csproj", "src/StockMode.Infra.Data/"]
COPY ["src/StockMode.Infra.Services/StockMode.Infra.Services.csproj", "src/StockMode.Infra.Services/"]
RUN dotnet restore "./src/StockMode.EmailWorker/StockMode.EmailWorker.csproj"
COPY . .
WORKDIR "/src/src/StockMode.EmailWorker"
RUN dotnet build "./StockMode.EmailWorker.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./StockMode.EmailWorker.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "StockMode.EmailWorker.dll"]