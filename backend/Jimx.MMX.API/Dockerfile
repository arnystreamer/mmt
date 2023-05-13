#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Jimx.MMX.API/Jimx.MMX.API.csproj", "Jimx.MMX.API/"]
RUN dotnet restore "Jimx.MMX.API/Jimx.MMX.API.csproj"
COPY . .
WORKDIR "/src/Jimx.MMX.API"
RUN dotnet build "Jimx.MMX.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Jimx.MMX.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Jimx.MMX.API.dll"]