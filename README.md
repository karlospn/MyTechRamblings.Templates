# MyTechRamblings.Templates

This repository contains a practical example about how to pack multiple dotnet templates in a single NuGet package and use it within Visual Studio or the .NET CLI.

> **To work with these templates you need Visual Studio 16.10 or higher.**

Each template has a ``.template.config`` folder. This folder contains a definition of how the template works within Visual Studio and the dotnet cli.

Each template contains a series of preprocessor directives an placeholder values so it cannot be used directly from this repository, instead of you need to use it within Visual Studio or the .NET CLI.

The repository contains a powershell ``create-nuget.ps1`` that is used to pack the template inside a NuGet package.     
If you want to use the templates you need to ran the powershell script and after that you can install it using the ``dotnet new -i MyTechRamblings.Templates.0.1.0.nupkg`` command.

I have published the nuget, so if you don't want to create the nuget by yourself you can get it from here: https://www.nuget.org/packages/MyTechRamblings.Templates

This repository is tied to a post in my blog. You can read it here: 
//TODO: Add blog post

# Templates

//TODO