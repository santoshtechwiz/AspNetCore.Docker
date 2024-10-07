# Stage 1: Build the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy the csproj file and restore the dependencies
COPY *.csproj ./
RUN dotnet restore

# Copy the rest of the application and build it
COPY . ./
RUN dotnet publish -c Release -o /out

# Stage 2: Run the application
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# Copy the build output from the previous stage
COPY --from=build /out .

# Expose port 80 for HTTP traffic
EXPOSE 5000

# Run the application
ENTRYPOINT ["dotnet", "AspNetCore.Docker.dll"]
