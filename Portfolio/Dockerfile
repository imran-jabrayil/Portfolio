# Stage 1: Base image for runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app

ENV TZ=Asia/Baku
RUN apt-get update && apt-get install -y tzdata && \
    ln -snf /usr/share/zoneinfo/$TZ /etc/localtime && \
    echo $TZ > /etc/timezone && \
    apt-get clean

# Expose necessary ports
EXPOSE 8080

# Stage 2: Build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy the project file and restore as distinct layers
COPY ["Portfolio/Portfolio.csproj", "Portfolio/"]
RUN dotnet restore "Portfolio/Portfolio.csproj"

# Copy the remaining source code and build the project
COPY . .
WORKDIR "/src/Portfolio"
RUN dotnet build "Portfolio.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Stage 3: Publish
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Portfolio.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Stage 4: Final image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Use ENTRYPOINT so the container starts the app when run
ENTRYPOINT ["dotnet", "Portfolio.dll"]
