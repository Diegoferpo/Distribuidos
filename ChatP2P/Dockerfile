# Etapa de build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["ChatP2P.csproj", "./"]
RUN dotnet restore "ChatP2P.csproj"
COPY . .
WORKDIR "/src/."
ARG configuration=Release
RUN dotnet build "ChatP2P.csproj" -c $configuration -o /app/build

# Etapa de publicación
FROM build AS publish
RUN dotnet publish "ChatP2P.csproj" -c $configuration -o /app/publish

# Etapa final
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ChatP2P.dll"]
