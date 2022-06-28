FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["SPM.API/SPM.API.csproj", "SPM.API/"]
RUN dotnet restore "SPM.API/SPM.API.csproj"
COPY . .
WORKDIR "/src/SPM.API"
RUN dotnet build "SPM.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SPM.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SPM.API.dll"]
