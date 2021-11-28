FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG ENV_NAME
WORKDIR /project
COPY . .
RUN dotnet restore
RUN dotnet publish --no-self-contained -c Release -o ./publish /p:EnvironmentName=${ENV_NAME}

FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build /project/publish .
ENTRYPOINT ["dotnet", "API.dll"]