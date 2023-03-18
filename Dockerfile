FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /source

COPY *.sln .
COPY src/*.csproj ./src/
RUN dotnet restore

COPY src/. ./src/
WORKDIR /source/src
RUN dotnet publish -c release -o /app --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
COPY --from=build /app ./
CMD ["dotnet", "ProgramTan.WebApi.dll"]
