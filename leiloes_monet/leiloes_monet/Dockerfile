# Use an image that includes .NET 8.0 SDK
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS builder

# Add your application code
WORKDIR /source
COPY . .

# Restore NuGet packages
RUN dotnet restore

# Publish your ASP.NET app targeting .NET 8.0
RUN dotnet publish -c Release -o /app --self-contained false

# Use an image that includes the ASP.NET runtime and .NET 8.0
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime

WORKDIR /app
COPY --from=builder /app ./

ENV PORT 8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "dotnetcoresample.dll"]