FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY ["src/StockMode.EmailWorker/StockMode.EmailWorker.csproj", "src/StockMode.EmailWorker/"]
COPY ["src/StockMode.Application/StockMode.Application.csproj", "src/StockMode.Application/"]
COPY ["src/StockMode.Domain/StockMode.Domain.csproj", "src/StockMode.Domain/"]
COPY ["src/StockMode.Domain.Core/StockMode.Domain.Core.csproj", "src/StockMode.Domain.Core/"]
COPY ["src/StockMode.Infra.CrossCutting.IoC/StockMode.Infra.CrossCutting.IoC.csproj", "src/StockMode.Infra.CrossCutting.IoC/"]
COPY ["src/StockMode.Infra.Data/StockMode.Infra.Data.csproj", "src/StockMode.Infra.Data/"]
COPY ["src/StockMode.Infra.Services/StockMode.Infra.Services.csproj", "src/StockMode.Infra.Services/"]
RUN dotnet restore "src/StockMode.EmailWorker/StockMode.EmailWorker.csproj"

COPY ["src/StockMode.EmailWorker/appsettings.json", "src/StockMode.EmailWorker/"]

COPY . .
WORKDIR "/src/src/StockMode.EmailWorker"
RUN dotnet build "StockMode.EmailWorker.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "StockMode.EmailWorker.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "StockMode.EmailWorker.dll"]