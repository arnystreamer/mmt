FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
# EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Jimx.MMT.API/Jimx.MMT.API.csproj", "Jimx.MMT.API/"]
RUN dotnet restore "Jimx.MMT.API/Jimx.MMT.API.csproj"
COPY . .
WORKDIR "/src/Jimx.MMT.API"
RUN dotnet build "Jimx.MMT.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Jimx.MMT.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Jimx.MMT.API.dll"]