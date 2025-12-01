# ========== 1) Build Stage ==========
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiar el csproj primero (mejor cache)
COPY SOE_MDEIS_BACKEND_GESTION/SOE_MDEIS_BACKEND_GESTION.csproj SOE_MDEIS_BACKEND_GESTION/

RUN dotnet restore "SOE_MDEIS_BACKEND_GESTION/SOE_MDEIS_BACKEND_GESTION.csproj"

# Copiar todo el backend
COPY SOE_MDEIS_BACKEND_GESTION/ SOE_MDEIS_BACKEND_GESTION/

WORKDIR /src/SOE_MDEIS_BACKEND_GESTION
RUN dotnet publish -c Release -o /app/publish /p:UseAppHost=false


# ========== 2) Runtime Stage ==========
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

# Railway usa el puerto 8080
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "SOE_MDEIS_BACKEND_GESTION.dll"]
