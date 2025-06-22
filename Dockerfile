# Use the official .NET SDK image as a build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /source

# Copy the solution file and restore dependencies
COPY *.sln .
COPY MiAreaVol/*.csproj ./MiAreaVol/
RUN dotnet restore

# Copy the rest of the application code
COPY . .
WORKDIR /source/MiAreaVol
RUN dotnet publish -c release -o /app --no-restore

# Use the official ASP.NET Core runtime image for the final stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app .

# Expose port 8080 and set the entry point
# Railway uses the PORT environment variable to bind the container.
# ASP.NET Core apps default to port 8080 if Kestrel isn't configured otherwise.
EXPOSE 8080
ENTRYPOINT ["dotnet", "MiAreaVol.dll"] 