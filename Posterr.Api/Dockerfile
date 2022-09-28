FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app

EXPOSE 8000

ENV ASPNETCORE_URLS=http://+:8000
ENV ASPNETCORE_ENVIRONMENT=Development

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Posterr.Api/Posterr.Api.csproj", "Posterr.Api/"]

RUN dotnet restore "Posterr.Api/Posterr.Api.csproj"
COPY . .
WORKDIR "/src/Posterr.Api"
RUN dotnet build "Posterr.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Posterr.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Posterr.Api.dll"]