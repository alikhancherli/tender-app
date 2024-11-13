
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["Tender.App.Api/Tender.App.Api.csproj", "Tender.App.Api/"]
COPY ["Tender.App.Infra/Tender.App.Infra.csproj", "Tender.App.Infra/"]
COPY ["Tender.App.Application/Tender.App.Application.csproj", "Tender.App.Application/"]
COPY ["Tender.App.Domain/Tender.App.Domain.csproj", "Tender.App.Domain/"]

RUN dotnet restore "Tender.App.Api/Tender.App.Api.csproj"
COPY . .
WORKDIR "/src/Tender.App.Api"
RUN dotnet build "Tender.App.Api.csproj" -c Release -o /app/build


WORKDIR "/src/Tender.App.Api"
FROM build AS publish
RUN dotnet publish "Tender.App.Api.csproj" -c Release -o /app/publish


FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

EXPOSE 5206

ENTRYPOINT ["dotnet", "Tender.App.Api.dll"]