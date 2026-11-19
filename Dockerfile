FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY Directory.Build.props .editorconfig NooksApi.sln ./
COPY NooksApi/NooksApi.csproj NooksApi/

RUN dotnet restore NooksApi/NooksApi.csproj

COPY NooksApi/ NooksApi/
RUN dotnet publish NooksApi/NooksApi.csproj -c Release -o /app/publish --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime

RUN apt-get update \
    && apt-get install -y --no-install-recommends curl \
    && rm -rf /var/lib/apt/lists/*

WORKDIR /app
COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

USER $APP_UID

ENTRYPOINT ["dotnet", "NooksApi.dll"]
