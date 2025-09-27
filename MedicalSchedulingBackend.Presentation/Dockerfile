#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["MedicalSchedulingBackend.Presentation/MedicalSchedulingBackend.Presentation.csproj", "MedicalSchedulingBackend.Presentation/"]
COPY ["MedicalSchedulingBackend.Application/MedicalSchedulingBackend.Application.csproj", "MedicalSchedulingBackend.Application/"]
COPY ["MedicalSchedulingBackend.Domain/MedicalSchedulingBackend.Domain.csproj", "MedicalSchedulingBackend.Domain/"]
COPY ["MedicalSchedulingBackend.Infrastructure/MedicalSchedulingBackend.Infrastructure.csproj", "MedicalSchedulingBackend.Infrastructure/"]
RUN dotnet restore "./MedicalSchedulingBackend.Presentation/MedicalSchedulingBackend.Presentation.csproj"
COPY . .
WORKDIR "/src/MedicalSchedulingBackend.Presentation"
RUN dotnet build "./MedicalSchedulingBackend.Presentation.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./MedicalSchedulingBackend.Presentation.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MedicalSchedulingBackend.Presentation.dll"]