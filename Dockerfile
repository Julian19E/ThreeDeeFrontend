﻿FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 5000
EXPOSE 5001
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["ThreeDeeFrontend/ThreeDeeFrontend.csproj", "ThreeDeeFrontend/"]
COPY ["ThreeDeeApplication/ThreeDeeApplication.csproj", "ThreeDeeApplication/"]
COPY ["ThreeDeeInfrastructure/ThreeDeeInfrastructure.csproj", "ThreeDeeInfrastructure/"]
RUN dotnet restore "ThreeDeeFrontend/ThreeDeeFrontend.csproj"
COPY . .
WORKDIR "/src/ThreeDeeFrontend"
RUN dotnet build "ThreeDeeFrontend.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ThreeDeeFrontend.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ThreeDeeFrontend.dll"]
