# Etapa de construcción
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia el archivo de proyecto y restaura dependencias
COPY ["RestauranteBackend.csproj", "./"]
RUN dotnet restore "RestauranteBackend.csproj"

# Copia el resto del código y publica la aplicación
COPY . .
RUN dotnet publish "RestauranteBackend.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Etapa final: Imagen ligera de ASP.NET para producción
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

# Instalar netcat-openbsd para la comprobación de disponibilidad de MySQL
RUN apt-get update && apt-get install -y netcat-openbsd

# Copia la aplicación publicada
COPY --from=build /app/publish .

# Exponer el puerto (asegúrate de que sea el puerto correcto)
EXPOSE 80

# Comando por defecto para ejecutar la aplicación
CMD ["dotnet", "RestauranteBackend.dll"]
