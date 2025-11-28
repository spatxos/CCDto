# syntax=docker/dockerfile:1
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /workspace
COPY . .
RUN dotnet publish src/DispatchProxyDemo.Aggregator/DispatchProxyDemo.Aggregator.csproj -c Release -o /out

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /out .
ENV ASPNETCORE_URLS=http://+:5201
EXPOSE 5201
ENTRYPOINT ["dotnet","DispatchProxyDemo.Aggregator.dll"]

