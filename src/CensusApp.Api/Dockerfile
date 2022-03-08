FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /source

COPY . .
RUN dotnet publish -c Release -o /app -r linux-x64 --self-contained false

FROM mcr.microsoft.com/dotnet/aspnet:5.0
EXPOSE 80 443

WORKDIR /app
COPY --from=build /app ./   
ENTRYPOINT ["dotnet", "CensusApp.Api.dll"]