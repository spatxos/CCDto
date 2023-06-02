FROM registry.cn-hongkong.aliyuncs.com/ccdto/api.template:latest AS build-env
WORKDIR /App
# Copy everything
COPY . ./
RUN ls

# Build runtime image
WORKDIR /App
COPY --from=build-env /App .
ENV ASPNETCORE_HOSTINGSTARTUPASSEMBLIES=SkyAPM.Agent.AspNetCore
ENV SKYWALKING__SERVICENAME=api.dbtable.application
ENTRYPOINT ["dotnet", "Api.Template.dll", "api.dbtable.application"]

