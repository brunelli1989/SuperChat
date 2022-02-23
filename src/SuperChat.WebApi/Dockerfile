#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["src/SuperChat.WebApi/SuperChat.WebApi.csproj", "src/SuperChat.WebApi/"]
COPY ["src/SuperChat.Domain/SuperChat.Domain.csproj", "src/SuperChat.Domain/"]
COPY ["src/SuperChat.ExternalServices/SuperChat.ExternalServices.csproj", "src/SuperChat.ExternalServices/"]
RUN dotnet restore "src/SuperChat.WebApi/SuperChat.WebApi.csproj"
COPY . .
WORKDIR "/src/src/SuperChat.WebApi"
RUN dotnet build "SuperChat.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SuperChat.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SuperChat.WebApi.dll"]