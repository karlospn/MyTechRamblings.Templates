# MyTechRamblings.Templates

This repository contains a practical example about how to pack multiple dotnet templates in a single NuGet package and use it within Visual Studio or the .NET CLI.

> **To work with these templates you need Visual Studio 16.10 or higher.**

Each template has a ``.template.config`` folder. This folder contains a definition of how the template works within Visual Studio and the dotnet cli.

Each template contains a series of preprocessor directives an placeholder values so it cannot be used directly from this repository, instead of you need to use it within Visual Studio or the .NET CLI.

The repository contains a powershell ``create-nuget.ps1`` that is used to pack the templates inside a NuGet package.     
If you want to use the templates you need to ran the powershell script and after that you can install it using the ``dotnet new -i MyTechRamblings.Templates.0.1.0.nupkg`` command.

I have published the nuget, so if you don't want to create the nuget by yourself you can get it from here: https://www.nuget.org/packages/MyTechRamblings.Templates

This repository is tied to a post in my blog. You can read it here: 
//TODO: Add blog post

# Templates

Right now there are only 3 templates, more incoming in a near future.

## Template 1: NET 5 WebApi

- It is a solution template. It will create a Web Api that uses a N-layer architecture with 3 layers:
    - WebApi 
    - Library
    - Repository

- It uses the ``Microsoft.Build.CentralPackageVersions`` MSBuild SDK. This SDK allows us to manage all the NuGet package versions in a single file.   
The file can be found in the ``/build`` folder. 

- The template also has the following features already configured:
  - HealthChecks
  - Swagger
  - Serilog
  - AutoMapper
  - Microsoft.Identity.Web

## Parameters

When you create a new solution using the template via Visual Studio or the .NET CLI the template you will be asked to specify the following parameters.

Each parameter has a default value, so you can either ran with the defaults or you can tailor the template to your own needs.

| Parameter             | Description                                                                                                                                                                                                                                                                                                | Default value           |
|-----------------------|------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|-------------------------|
| **Docker**                | Adds an optimised Dockerfile to add the ability to build a Docker image.                                                                                                                                                                                                                                   | _true_                    |
| **ReadMe**                | Adds a README markdown file describing the project.                                                                                                                                                                                                                                                     | _true_                    |
| **Tests**                 | Adds an Integration and Unit Test project in the solution.                                                                                                                                                                                                                                                 | _true_                    |
| **GitHub**                | Adds an GitHub Action file that allow us to deploy the api into an Azure Web App.                                                                                                                                                                                                                          | _false_                   |
| **AzurePipelines**        | Adds an Azure pipelines YAML file that allow us to deploy the api into an Azure Web App.                                                                                                                                                                                                                   | _true_                    |
| **DeploymentType**        | Specifies how you want to deploy the api. The possible values are "DeployAsZip" or "DeployAsContainer".    Depending of the value you choose it will create a different deployment pipeline.    If you choose to not create nor a GitHub Action neither an Azure pipelines YAML this parameter is useless. | _DeployAsZip_             |
| **AcrName**               | An Azure ACR registry name. Only used if you are going to be deploying with container.                                                                                                                                                                                                                     | _acrcponsndev_            |
| **AzureSubscriptionName** | An Azure DevOps Service Endpoint subscription. Only used if you are going to be deploying with Azure Pipelines.                                                                                                                                                                                            | _cponsn-dev-subscription_ |
| **AppServiceName**        | The name of the Azure App Service where the code will be deployed.                                                                                                                                                                                                                                         | _app-svc-demo-dev_        |
| **Authorization**         | Enables the use of authorization using Microsoft.Identity.Web                                                                                                                                                                                                                                              | _true_                    |
| **AzureAdTenantId**       | Azure Active Directory Tenant Id. Only necessary if Authorization is enabled.                                                                                                                                                                                                                              | _8a0671e2-3a30-4d30-9cb9-ad709b9c744a_                       |
| **AzureAdDomain**         | Azure Active Directory Domain Name. Only necessary if Authorization is enabled.                                                                                                                                                                                                                            | _carlosponsnoutlook.onmicrosoft.com_                       |
| **AzureAdClientId**       | Azure Active Directory App Client Id. Only necessary if Authorization is enabled.                                                                                                                                                                                                                          | _fdada45d-8827-466f-82a5-179724a3c268_                       |
| **AzureAdSecret**         | Azure Active Directory App Secret Value. Only necessary if Authorization is enabled.                                                                                                                                                                                                                       | _1234_                       |
| **HealthCheck**           | Enables the use of healthchecks.                                                                                                                                                                                                                                                                           | true                    |
| **HealthCheckPath**       | HealthCheck path. Only necessary if HealthCheck is enabled.                                                                                                                                                                                                                                                | _/health_                 |
| **Swagger**               | Enables the use of Swagger.                                                                                                                                                                                                                                                                                | _true_                    |
| **SwaggerPath**           | Swagger path. Do not add a backslash. Only necessary if Swagger is enabled.                                                                                                                                                                                                                                | _api-docs_                |
| **Contact**               | The contact details to use if someone wants to contact you. Only necessary if Swagger is enabled.                                                                                                                                                                                                          | _user@example.com_        |
| **CompanyName**           | The name of the company. Only necessary if Swagger is enabled.                                                                                                                                                                                                                                             | _mytechramblings_         |
| **CompanyWebsite**        | The website of the comany. Only necessary if Swagger is enabled.                                                                                                                                                                                                                                           | _www.mytechramblings.com_ |
| **ApiDescription**        | The description of the api. Only necessary if Swagger is enabled.                                                                                                                                                                                                                                          | _Put your api info here._  |

---

## Template 2: A Worker Service that consumes RabbitMq messages

- It is a solution template. It creates a ``BackgroundService`` that consumes messages from a RabbitMq server.
- It is a solution template. It will create a Worker Service that uses a N-layer architecture with 3 layers:
    - WebApi 
    - Library
    - Repository

- It uses the ``Microsoft.Build.CentralPackageVersions`` MSBuild SDK. This SDK allows us to manage all the NuGet package versions in a single file.   
The file can be found in the ``/build`` folder. 

- The template also has the following features already configured:
  - Serilog
  - AutoMapper
  - Microsoft.Extensions.Hosting.Systemd
  - Microsoft.Extensions.Hosting.WindowsServices
  
## Parameters

When you create a new solution using the template via Visual Studio or the .NET CLI the template you will be asked to specify the following parameters.

Each parameter has a default value, so you can either ran with the defaults or you can tailor the template to your own needs.
| Parameter             | Description                                                                                                                                                                                                                                                                                                | Default value           |
|-----------------------|------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|-------------------------|
| **Docker**                | Adds an optimised Dockerfile to add the ability to build a Docker image.                                                                                                                                                                                                                                   | _true_                    |
| **ReadMe**                | Adds a README markdown file describing the project.                                                                                                                                                                                                                                                     | _true_                    |
| **Tests**                 | Adds an Integration and Unit Test project in the solution.                                                                                                                                                                                                                                                 | _true_                    |
| **GitHub**                | Adds an GitHub Action file that allow us to deploy the api into an Azure Web App.                                                                                                                                                                                                                          | _false_                   |
| **AzurePipelines**        | Adds an Azure pipelines YAML file that allow us to deploy the api into an Azure Web App.                                                                                                                                                                                                                   | _true_                    |
| **DeploymentType**        | Specifies how you want to deploy the api. The possible values are "DeployAsZip" or "DeployAsContainer".    Depending of the value you choose it will create a different deployment pipeline.    If you choose to not create nor a GitHub Action neither an Azure pipelines YAML this parameter is useless. | _DeployAsZip_             |
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

- It is a project template. 
- It creates a NET Core 3.1 Azure Function that is triggered by a timer.
- The template also has the following features already configured:
  - Dependency Injection.
  - Logging.

When you create a new solution using the template via Visual Studio or the .NET CLI the template you will be asked to specify the following parameters.

Each parameter has a default value, so you can either ran with the defaults or you can tailor the template to your own needs.

| Parameter             | Description                                                                                                                                                                                                                                                                                                | Default value           |
|-----------------------|------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|-------------------------|
| **Docker**                | Adds an optimised Dockerfile to add the ability to build a Docker image.                                                                                                                                                                                                                                   | _true_                    |
| **GitHub**                | Adds an GitHub Action file that allow us to deploy the function.                                                                                                                                                                                                                    | _false_                   |
| **AzurePipelines**        | Adds an Azure pipelines YAML file that allow us to deploy the function.                                                                                                                                                                                                                   | _true_                    |
| **DeploymentType**        | Specifies how you want to deploy the function. The possible values are "DeployAsZip" or "DeployAsContainer".    Depending of the value you choose it will create a different deployment pipeline.    If you choose to not create nor a GitHub Action neither an Azure pipelines YAML this parameter is useless. | _DeployAsZip_             |
| **AcrName**               | An Azure ACR registry name. Only used if you are going to be deploying with container.                                                                                                                                                                                                                     | _acrcponsndev_            |
| **AzureSubscriptionName** | An Azure DevOps Service Endpoint subscription. Only used if you are going to be deploying with Azure Pipelines.                                                                                                                                                                                            | _cponsn-dev-subscription_ |
| **FunctionName**          | The name of the Azure Function where the code will be deployed.                                                                                                                                                                                                                                            | _MyFunction_              |
| **Cron**                  | The timer trigger expression.                                                                                                                                                                                                                                                                              | _0 * * * * *_             |
| **LogLevel**              | Default LogLevel for the Azure Function.                                                                                                                                                                                                                                                                   | _Information_             |
---