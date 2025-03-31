#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG BUILD_CONFIGURATION=Release
#ENV ASPNETCORE_ENVIRONMENT=Development \
	#ASPNETCORE_HTTP_PORTS=8080
WORKDIR /src
COPY ["WebApplication3.csproj", "."]
RUN dotnet restore "./WebApplication3.csproj"
RUN dotnet add package Swashbuckle.AspNetCore
COPY . .
WORKDIR "/src/."
#ENV ASPNETCORE_ENVIRONMENT=Development \
	#ASPNETCORE_HTTP_PORTS=8080
RUN dotnet build "./WebApplication3.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./WebApplication3.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
#ENV ASPNETCORE_URLS=http://+:80
#RUN echo "Referrer-Policy: no-referrer"
ENTRYPOINT ["dotnet", "WebApplication3.dll"]

#docker build -t api3docker .
#docker run --name api3docker -d -p 5001:80 api3docker
