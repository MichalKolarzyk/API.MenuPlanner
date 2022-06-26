#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Api.MenuPlannerTest/Api.MenuPlannerTest.csproj", "Api.MenuPlannerTest/"]
COPY ["API.MenuPlanner/API.MenuPlanner.csproj", "API.MenuPlanner/"]
RUN dotnet restore "Api.MenuPlannerTest/Api.MenuPlannerTest.csproj"
COPY . .
WORKDIR "/src/Api.MenuPlannerTest"
RUN dotnet build "Api.MenuPlannerTest.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Api.MenuPlannerTest.csproj" -c Release -o /app/publish

ENTRYPOINT ["dotnet"]