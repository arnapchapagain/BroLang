FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["BroLangCompiler/BroLangCompiler.csproj", "BroLangCompiler/"]
RUN dotnet restore "BroLangCompiler/BroLangCompiler.csproj"
COPY . .
WORKDIR "/src/BroLangCompiler"
RUN dotnet build "BroLangCompiler.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BroLangCompiler.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BroLangCompiler.dll"]
