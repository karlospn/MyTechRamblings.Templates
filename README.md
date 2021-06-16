# MyTechRamblings.Templates

![status workflow](https://github.com/karlospn/pack-dotnet-templates-example/actions/workflows/main.yml/badge.svg)

This repository contains a practical example about how to pack multiple dotnet templates in a single NuGet package and use it within Visual Studio or the .NET CLI.

> **To work with these templates you need Visual Studio version 16.10 or higher.**

Each template contains a series of preprocessor directives and placeholder values, so don't try to clone the repository and use the templates directly because it won't work. You need to install the templates via .NET CLI and then use it with the .NET CLI or within Visual Studio.

If you want to install this templates there are 3 options available:

1- There is a powershell script in the root level of this repository ``create-nuget.ps1``. If you try execute the script, it will create a .nupkg file on your filesystem. Afterward, you can install the .nupkg using the ``dotnet new -i MyTechRamblings.Templates.0.3.0.nupkg`` command.   

2 - Run the ``dotnet new -i MyTechRamblings.Templates::0.3.0`` command. This command will try to fetch the package from ``nuget.org`` and install it.   

3 - Download the package from [nuget.org](https://www.nuget.org/packages/MyTechRamblings.Templates) and install it using the the ``dotnet new -i MyTechRamblings.Templates.0.3.0.nupkg`` command.   

After installing the templates pack you should run the ``dotnet new -l`` command and verify that the 3 templates appear on the list (``mtr-api``, ``mtr-rabbit-worker``, ``mtr-az-func-timer``).

This repository is tied to the post series _"How to build a .NET template and use it within Visual Studio"_ from my blog. You can read it [here](https://www.mytechramblings.com/posts/create-dotnet-templates-for-visual-studio-part-1/)

# Templates

Right now there are only 3 templates. More incoming in a near future.

## Template 1: NET 5 Web API

- It is an entire solution application. It uses a N-layer architecture with 3 layers:
    - ``WebApi`` layer.
    - ``Library`` layer.
    - ``Repository`` layer.
- It uses the ``Microsoft.Build.CentralPackageVersions`` MSBuild SDK. This SDK allows us to manage all the NuGet package versions in a single file. The file can be found in the ``/build`` folder.
- The api has the following features already built-in:
    - ``HealthChecks``
    - ``Swagger``
    - ``Serilog``
    - ``AutoMapper``
    - ``Microsoft.Identity.Web``
    - ``Dockerfile``
- It includes an ``Azure Pipelines`` YAML file and a ``GitHub Action`` YAML file that deploys the app into an Azure App Service.

## Template Parameters

When you create a new solution using the template via Visual Studio or the .NET CLI you will be asked to specify the following parameters.

Each parameter has a default value, so you can ran with the defaults if you like.

| Parameter Name        | Description                                                                                                                                                                                                                                                                                                | Default value           |
|-----------------------|------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|-------------------------|
| **Docker**                | Adds an optimised Dockerfile to add the ability to build a Docker image.                                                                                                                                                                                                                                   | _true_                    |
| **ReadMe**                | Adds a README markdown file describing the project.                                                                                                                                                                                                                                                     | _true_                    |
| **Tests**                 | Adds an integration test project and a unit test project in the solution.                                                                                                                                                                                                                                                 | _true_                    |
| **GitHub**                | Adds a GitHub Action file into the solution that deploys the api into an Azure Web App.                                                                                                                                                                                                                          | _false_                   |
| **AzurePipelines**        | Adds an Azure pipeline YAML file into the solution that deploys the api into an Azure Web App.                                                                                                                                                                                                                   | _true_                    |
| **DeploymentType**        | Specifies how you want to deploy the api. The possible values are ``DeployAsZip`` or ``DeployAsContainer``.    Depending of the value you choose the content of the deployment pipeline will vary. If you choose to not create neither a GitHub Action nor an Azure Pipeline this parameter is useless. | _DeployAsZip_             |
| **AcrName**               | An Azure ACR registry name. Only used if you are going to be deploying with container.                                                                                                                                                                                                                     | _acrcponsndev_            |
| **AzureSubscriptionName** | An Azure DevOps Service Endpoint subscription. Only used if you are going to be deploying with Azure Pipelines.                                                                                                                                                                                            | _cponsn-dev-subscription_ |
| **AppServiceName**        | The name of the Azure App Service where the code will be deployed.                                                                                                                                                                                                                                         | _app-svc-demo-dev_        |
| **Authorization**         | Enables or disables the use of authorization using ``Microsoft.Identity.Web``                                                                                                                                                                                                                                              | _true_                    |
| **AzureAdTenantId**       | Azure Active Directory Tenant Id. Only necessary if Authorization is enabled.                                                                                                                                                                                                                              | _8a0671e2-3a30-4d30-9cb9-ad709b9c744a_                       |
| **AzureAdDomain**         | Azure Active Directory Domain Name. Only necessary if Authorization is enabled.                                                                                                                                                                                                                            | _cpnoutlook.onmicrosoft.com_                       |
| **AzureAdClientId**       | Azure Active Directory App Client Id. Only necessary if Authorization is enabled.                                                                                                                                                                                                                          | _fdada45d-8827-466f-82a5-179724a3c268_                       |
| **AzureAdSecret**         | Azure Active Directory App Secret Value. Only necessary if Authorization is enabled.                                                                                                                                                                                                                       | _1234_                       |
| **HealthCheck**           | Enables or disables the use of healthchecks.                                                                                                                                                                                                                                                                           | true                    |
| **HealthCheckPath**       | HealthCheck path. Only necessary if HealthCheck is enabled.                                                                                                                                                                                                                                                | _/health_                 |
| **Swagger**               | Enables or disables the use of Swagger.                                                                                                                                                                                                                                                                                | _true_                    |
| **SwaggerPath**           | Swagger path. Do not add a backslash. Only necessary if Swagger is enabled.                                                                                                                                                                                                                                | _api-docs_                |
| **Contact**               | The contact details to use if someone wants to contact you. Only necessary if Swagger is enabled.                                                                                                                                                                                                          | _user@example.com_        |
| **CompanyName**           | The name of the company. Only necessary if Swagger is enabled.                                                                                                                                                                                                                                             | _mytechramblings_         |
| **CompanyWebsite**        | The website of the comany. Needs to be a valid URI. Only necessary if Swagger is enabled.                                                                                                                                                                                                                                           | _https://www.mytechramblings.com_ |
| **ApiDescription**        | The description of the api. Only necessary if Swagger is enabled.                                                                                                                                                                                                                                          | _Put your api info here._  |

---

## Template 2: A Worker Service that consumes RabbitMq messages

- It is an entire solution application. 
- The application is a ``BackgroundService`` that consumes messages from a RabbitMq server. It uses a N-layer architecture with 3 layers:
    - ``Worker`` layer.
    - ``Library`` layer.
    - ``Repository`` layer.
- It uses the ``Microsoft.Build.CentralPackageVersions`` MSBuild SDK. This SDK allows us to manage all the NuGet package versions in a single file. The file can be found in the ``/build`` folder.
- The service has the following features already built-in:
    - ``Serilog``
    - ``AutoMapper``
    - ``Microsoft.Extensions.Hosting.Systemd``
    - ``Microsoft.Extensions.Hosting.WindowsServices``
    - ``Dockerfile``
 - It includes an ``Azure Pipelines`` YAML file and a ``GitHub Action`` YAML file that deploys the app into an Azure App Service.
  
## Template Parameters

When you create a new solution using the template via Visual Studio or the .NET CLI you will be asked to specify the following parameters.

Each parameter has a default value, so you can either ran with the defaults or you can tailor the template to your own needs.

| Parameter Name           | Description                                                                                                                                                                                                                                                                                                | Default value           |
|-----------------------|------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|-------------------------|
| **Docker**                | Adds an optimised Dockerfile to add the ability to build a Docker image.                                                                                                                                                                                                                                   | _true_                    |
| **ReadMe**                | Adds a README markdown file describing the project.                                                                                                                                                                                                                                                     | _true_                    |
| **Tests**                 | Adds an integration test project and a unit test project in the solution.                                                                                                                                                                                                                                                 | _true_                    |
| **GitHub**                |  Adds a GitHub Action file into the solution that deploys the service into an Azure Web App.                                                                                                                                                                                                                          | _false_                   |
| **AzurePipelines**        | Adds an Azure pipeline YAML file into the solution that deploys the service into an Azure Web App.                                                                                                                                                                                                                   | _true_                    |
| **DeploymentType**        | Specifies how you want to deploy the api. The possible values are ``DeployAsZip`` or ``DeployAsContainer``.    Depending of the value you choose the content of the deployment pipeline will vary. If you choose to not create neither a GitHub Action nor an Azure Pipeline this parameter is useless. | _DeployAsZip_             |
| **AcrName**               | An Azure ACR registry name. Only used if you are going to be deploying with container.                                                                                                                                                                                                                     | _acrcponsndev_            |
| **AzureSubscriptionName** | An Azure DevOps Service Endpoint subscription. Only used if you are going to be deploying with Azure Pipelines.                                                                                                                                                                                            | cponsn-dev-subscription |
| **AppServiceName**        | The name of the Azure App Service where the code will be deployed.                                                                                                                                                                                                                                         | _app-svc-demo-dev_        |
| **QueueName**             | The name of the RabbitMq queue where the consumer is going to be attached.                                                                                                                                                                                                                                 | _Queue1_                  |
| **RabbitMqUsername**      | A RabbitMq user name used to connect to the server.                                                                                                                                                                                                                                                        | _demo_                    |
| **RabbitMqPassword**      | A RabbitMq user password used to connect to the server.                                                                                                                                                                                                                                                    | _1234_                    |
| **RabbitMqServer**        | The RabbitMq server uri.                                                                                                                                                                                                                                                                                   | _127.0.0.1_               |
| **RabbitMqPort**          | A RabbitMq server port used to connect to the server.                                                                                                                                                                                                                                                      | _5672_                    |

---

## Template 3: NET Core 3.1 Azure Function triggered by a timer

- This one is not an entire solution, instead it is just a single project. 
- It is a NET Core 3.1 Azure Function that is triggered by a timer.
- The function has the following features already built-in:
    - ``Dependency Injection``
    - ``Logging``
- It includes an ``Azure Pipelines`` YAML file and a ``GitHub Action`` YAML file that deploys the app into an Azure App Service.

> **QUICK TIP**   
> _If you want to test your Azure Function running with your local docker machine use this command:  ``docker run -d -e AzureWebJobsStorage="UseDevelopmentStorage=true;DevelopmentStorageProxyUri=http://host.docker.internal" <IMAGE_NAME>``_

## Template Parameters

When you create a new solution using the template via Visual Studio or the .NET CLI you will be asked to specify the following parameters.

Each parameter has a default value, so you can either ran with the defaults or you can tailor the template to your own needs.

| Parameter Name             | Description                                                                                                                                                                                                                                                                                                | Default value           |
|-----------------------|------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|-------------------------|
| **Docker**                | Adds an optimised Dockerfile to add the ability to build a Docker image.                                                                                                                                                                                                                                   | _true_                    |
| **GitHub**                | Adds a GitHub Action file into the project that deploys the function into Azure Functions.                                                                                                                                                                                                                    | _false_                   |
| **AzurePipelines**        | Adds an Azure pipeline YAML file into the project that deploys the function into Azure Functions.                                                                                                                                                                                                                   | _true_                    |
| **DeploymentType**        | Specifies how you want to deploy the api. The possible values are ``DeployAsZip`` or ``DeployAsContainer``.    Depending of the value you choose the content of the deployment pipeline will vary. If you choose to not create neither a GitHub Action nor an Azure Pipeline this parameter is useless. | _DeployAsZip_             |
| **AcrName**               | An Azure ACR registry name. Only used if you are going to be deploying with container.                                                                                                                                                                                                                     | _acrcponsndev_            |
| **AzureSubscriptionName** | An Azure DevOps Service Endpoint subscription. Only used if you are going to be deploying with Azure Pipelines.                                                                                                                                                                                            | _cponsn-dev-subscription_ |
| **FunctionName**          | The name of the Azure Function where the code will be deployed.                                                                                                                                                                                                                                            | _MyFunction_              |
| **Cron**                  | The timer trigger expression. Needs to be a Cron expression.                                                                                                                                                                                                                                                                             | _0 * * * * *_             |
| **LogLevel**              | Default Log Level for the Azure Function.                                                                                                                                                                                                                                                                   | _Information_             |
---
