# Etapa de build
FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /app

# Copiar solución y todos los proyectos
COPY LEGALCONNECTION.sln ./
COPY LC.WEB/ LC.WEB/
COPY LC.CORE/ LC.CORE/
COPY LC.ENTITIES/ LC.ENTITIES/
COPY LC.SERVICE/ LC.SERVICE/
COPY LC.DATABASE/ LC.DATABASE/
COPY LC.CORE.VIEW/ LC.CORE.VIEW/
COPY LC.HANGFIRE/ LC.HANGFIRE/
COPY LC.PAYMENT/ LC.PAYMENT/
COPY LC.REPOSITORY/ LC.REPOSITORY/

# Restaurar dependencias
WORKDIR /app/LC.WEB
RUN dotnet restore
RUN dotnet publish -c Release -o /out

# Etapa runtime
FROM mcr.microsoft.com/dotnet/aspnet:3.1
WORKDIR /app
COPY --from=build /out .

EXPOSE 80
ENTRYPOINT ["dotnet", "LC.WEB.dll"]
