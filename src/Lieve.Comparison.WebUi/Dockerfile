FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Lieve.Comparison.WebUi/Lieve.Comparison.WebUi.csproj", "src/Lieve.Comparison.WebUi/"]
RUN dotnet restore "src/Lieve.Comparison.WebUi/Lieve.Comparison.WebUi.csproj"
COPY . .
WORKDIR "/src/src/Lieve.Comparison.WebUi"
RUN dotnet build "Lieve.Comparison.WebUi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Lieve.Comparison.WebUi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Lieve.Comparison.WebUi.dll"]
