# syntax=docker/dockerfile:1
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /workspace
COPY . .
RUN dotnet publish src/DispatchProxyDemo.Api.DBConnection/DispatchProxyDemo.Api.DBConnection.csproj -c Release -o /out

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /out .
ENV ASPNETCORE_URLS=http://+:5101
EXPOSE 5101
ENTRYPOINT ["dotnet","DispatchProxyDemo.Api.DBConnection.dll"]

