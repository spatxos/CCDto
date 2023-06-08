# About
CCDTO以api_template项目作为模板，其他项目在此模板上进行注入，做成的一个个api服务

# Getting Started
1. 在`/settings/secrets/actions`中点击`New repository secret`添加以下信息

| Secrets Name   |      Description      |
|----------|:-------------:|
| DOCKER_REGISTRY |  docker仓库地址（阿里云镜像仓库） |
| DOCKER_REGISTRY_REGION |    仓库地区   |
| DOCKER_USERNAME | 仓库用户名 |
| DOCKER_PASSWORD | 仓库密码 |

2. 服务器上添加mysql，然后执行sql文件夹下的.sql文件添加表

# Plan
- Dapr or Orleans
- Kubernetes

# Additional Resources
- [Panda.DynamicWebApi](https://github.com/pda-team/Panda.DynamicWebApi)
- [FreeSql](https://github.com/dotnetcore/FreeSql)