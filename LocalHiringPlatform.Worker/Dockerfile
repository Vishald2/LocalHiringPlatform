# ============================
# Build Stage
# ============================

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /src

# Copy solution
COPY ["LocalHiringPlatform.sln", "."]

# Copy project files
COPY ["LocalHiringPlatform.Domain/LocalHiringPlatform.Domain.csproj", "LocalHiringPlatform.Domain/"]
COPY ["LocalHiringPlatform.Infrastructure/LocalHiringPlatform.Infrastructure.csproj", "LocalHiringPlatform.Infrastructure/"]
COPY ["LocalHiringPlatform.ServiceBus/LocalHiringPlatform.ServiceBus.csproj", "LocalHiringPlatform.ServiceBus/"]
COPY ["LocalHiringPlatform.Worker/LocalHiringPlatform.Worker.csproj", "LocalHiringPlatform.Worker/"]

RUN dotnet restore "LocalHiringPlatform.Worker/LocalHiringPlatform.Worker.csproj"

# Copy the remaining source code
COPY . .

RUN dotnet publish \
    "LocalHiringPlatform.Worker/LocalHiringPlatform.Worker.csproj" \
    -c Release \
    -o /app/publish \
    /p:UseAppHost=false

# ===========================
# Runtime Stage
# ===========================

FROM mcr.microsoft.com/dotnet/runtime:8.0 AS final

WORKDIR /app

COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "LocalHiringPlatform.Worker.dll"]