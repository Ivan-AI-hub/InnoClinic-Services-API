#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["ServicesAPI.Web/ServicesAPI.Web.csproj", "ServicesAPI.Web/"]
COPY ["ServicesAPI.Application/ServicesAPI.Application.csproj", "ServicesAPI.Application/"]
COPY ["ServicesAPI.Domain/ServicesAPI.Domain.csproj", "ServicesAPI.Domain/"]
COPY ["ServicesAPI.Persistence/ServicesAPI.Persistence.csproj", "ServicesAPI.Persistence/"]
COPY ["ServicesAPI.Presentation/ServicesAPI.Presentation.csproj", "ServicesAPI.Presentation/"]
RUN dotnet restore "ServicesAPI.Web/ServicesAPI.Web.csproj"
COPY . .
WORKDIR "/src/ServicesAPI.Web"
RUN dotnet build "ServicesAPI.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ServicesAPI.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ServicesAPI.Web.dll"]