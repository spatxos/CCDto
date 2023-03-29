FROM registry.cn-hongkong.aliyuncs.com/ccdto/api.template:latest AS build-env
WORKDIR /App
# Copy everything
COPY . ./
RUN ls

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /App
COPY --from=build-env /App .
ENTRYPOINT ["dotnet", "Api.Template.dll", "api.dbtable.application"]

