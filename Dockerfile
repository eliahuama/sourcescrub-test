FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src/SourceScrub.API
ENV ASPNETCORE_ENVIRONMENT=Development
EXPOSE 5000
ENTRYPOINT ["dotnet", "run"]
