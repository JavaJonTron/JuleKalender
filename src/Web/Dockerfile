# Build Stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy solution and project files
COPY ["JuleKalender.sln", "./"]
COPY ["src/Domain/Domain.csproj", "src/Domain/"]
COPY ["src/Application/Application.csproj", "src/Application/"]
COPY ["src/Infrastructure/Infrastructure.csproj", "src/Infrastructure/"]
COPY ["src/WebApi/WebApi.csproj", "src/WebApi/"]
COPY ["src/Web/Web.csproj", "src/Web/"]

# Restore dependencies
RUN dotnet restore "src/Web/Web.csproj"

# Copy the rest of the source code
COPY . .

# Build and Publish
WORKDIR "/src/src/Web"
RUN dotnet build "Web.csproj" -c Release -o /app/build
RUN dotnet publish "Web.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Runtime Stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080
ENTRYPOINT ["dotnet", "Web.dll"]
