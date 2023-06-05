FROM registry.cn-hongkong.aliyuncs.com/ccdto/api.template:latest AS build-env
WORKDIR ./
RUN ls ./
COPY . ./
ENV ASPNETCORE_HOSTINGSTARTUPASSEMBLIES=SkyAPM.Agent.AspNetCore
ENV SKYWALKING__SERVICENAME=api.dbtable.application
ENTRYPOINT ["dotnet", "Api.Template.dll", "api.dbtable.application"]

