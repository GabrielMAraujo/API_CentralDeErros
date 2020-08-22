#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY API_CentralDeErros.API/API_CentralDeErros.API.csproj API_CentralDeErros.API/
COPY API_CentralDeErros.Model/API_CentralDeErros.Model.csproj API_CentralDeErros.Model/
COPY API_CentralDeErros.Service/API_CentralDeErros.Service.csproj API_CentralDeErros.Service/
COPY API_CentralDeErros.Infra/API_CentralDeErros.Infra.csproj API_CentralDeErros.Infra/
RUN dotnet restore "API_CentralDeErros.API/API_CentralDeErros.API.csproj"
COPY . .
WORKDIR "/src/API_CentralDeErros.API"
RUN dotnet build "API_CentralDeErros.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "API_CentralDeErros.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "API_CentralDeErros.API.dll"]
