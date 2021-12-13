FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /project
COPY . .
RUN dotnet restore
RUN dotnet publish --no-self-contained -c Release -o ./publish

FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build /project/publish .
ENV ENV_NAME=""
ENTRYPOINT dotnet API.dll --environment "$ENV_NAME"