FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
RUN dotnet nuget add source "https://pkgs.dev.azure.com/haagensens/_packaging/haagensens/nuget/v3/index.json" --username "haagensens" --password "" --store-password-in-clear-text
COPY ["src/PDH.MessagingDemo.Ui/PDH.MessagingDemo.Ui/PDH.MessagingDemo.Ui.csproj", "src/PDH.MessagingDemo.Ui/PDH.MessagingDemo.Ui/"]
COPY ["src/PDH.MessagingDemo.Shared/PDH.MessagingDemo.Shared.csproj", "src/PDH.MessagingDemo.Shared/"]
COPY ["src/PDH.MessagingDemo.Ui/PDH.MessagingDemo.Ui.Client/PDH.MessagingDemo.Ui.Client.csproj", "src/PDH.MessagingDemo.Ui/PDH.MessagingDemo.Ui.Client/"]
RUN dotnet restore "./src/PDH.MessagingDemo.Ui/PDH.MessagingDemo.Ui/PDH.MessagingDemo.Ui.csproj"
COPY . .
WORKDIR "/src/src/PDH.MessagingDemo.Ui/PDH.MessagingDemo.Ui"
RUN dotnet build "./PDH.MessagingDemo.Ui.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./PDH.MessagingDemo.Ui.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PDH.MessagingDemo.Ui.dll"]