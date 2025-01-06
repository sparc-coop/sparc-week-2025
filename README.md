# Sparc Week 2025

## Introduction

This repository contains the code for the Sparc Week 2025 projects done by the [Sparc Cooperative](https://github.com/sparc-coop) team.

## Getting Started

- Clone this repository and create a new branch for your project.
- Open the Sparc.Blossom.Template solution. This solution has a base Sparc Blossom 9.0 project which mimics the Blazor Web App base template.
- Craft your project using the base template as a starting point.

## About Blossom 9.0

This project uses the new Blossom 9.0 framework. The Blossom Framework is a set of libraries and tools that help you build apps faster. It is inspired from many core architectural principles:

- **Domain Driven Design**: Blossom is built around the principles of [Domain Driven Design](https://martinfowler.com/bliki/DomainDrivenDesign.html). This means that the code is organized around the business domain, and not around the technology.
- **Screaming Architecture**: Blossom uses a [Screaming Architecture](https://blog.cleancoder.com/uncle-bob/2011/09/30/Screaming-Architecture.html), which means that the core business logic is at the center of the application, and the technology is at the edges.
- **Clean Architecture**: Blossom is inspired from [Clean Architecture](https://blog.cleancoder.com/uncle-bob/2012/08/13/the-clean-architecture.html), which means that the code is organized in layers, again with the core business logic at the center. However, Blossom is not a strict implementation of Clean Architecture, as it is more flexible and pragmatic.
- **Modular Monolith**: Blossom is built around the concept of a [Modular Monolith](https://www.milanjovanovic.tech/blog/what-is-a-modular-monolith), which means that the application is built as a single monolithic application, but it is organized in modules, which can be developed and deployed independently.
- **Hexagonal Architecture**: Blossom is inspired from [Hexagonal Architecture](https://en.wikipedia.org/wiki/Hexagonal_architecture_(software)), which means that the core business logic is decoupled from the technology, and the technology is abstracted away from the core business logic.

If you see a pattern here, you've got it! Blossom is crafted around the idea of "just building your app", and letting the framework and the tools take care of the rest. 
No more fighting with architectural boilerplate, no more worrying about how to structure your code, no more struggling with the technology. Just build your app!

## A Tour Of Blossom 9.0

### Program.cs

This is the entry point of the application. It sets up the host, configures the services, and runs the application.

In the Blossom template, you will note that the Program.cs file uses a very basic top-level structure, but replaces `WebApplication.CreateBuilder` with `BlossomApplication.CreateBuilder`. This is the opening act of Blossom's magic. This means is that you are building a 
**Blossom** application, which is not just a web application -- it will run on Android, iOS, Desktop, Web Assembly, and Blazor Server, all with the same single project!

You can configure services and dependency injection in this file the same way as any other ASP.NET Core application, but with the added benefit of Blossom's modular architecture.

### Html.razor

This is the entry UI point into the Blossom application (normally called `App.razor` in Blazor templates). We call it `Html.razor` because the top level component is the `<html>` tag, not the app itself. `Program.cs` references this file as the entry point.

### compilerconfig.json

This template comes ready for use with Web Compiler 2022, a VS extension to compile SCSS/SASS files directly inside the project. This is important because the Blossom template takes advantage of SASS to clean up CSS, at two levels:

- the global CSS (`Html.scss`, which compiles to `wwwroot/app.css`)
- the individual Blazor isolated component CSS (example: `_Shared\MainLayout.razor.scss`, which compiles to `_Shared\MainLayout.razor.css` so that Blazor picks it up as isolated CSS applied *only* to the `MainLayout.razor` file)

### __Imports.cs and _Imports.razor

These two files are the same as in any Blazor project, but with a slight difference: they import the Blossom namespace, in two ways:

-- `__Imports.cs` sets up a global using statement for `Sparc.Blossom` namespace, so that you don't have to reference it everywhere in individual files
-- `_Imports.razor` sets up the connection to the Blossom auto-generated API for the client, so that your UI and Razor components can use this API instead of your project's entities directly. More on this below!

### Sparc.Blossom.Template.csproj

The main thing to note here is the *cleanliness* of the project file. It only imports one single NuGet package: `Sparc.Blossom`. This is the only package you need to build a Blossom application. Everything else is handled by the framework, or is a plugin detail you can add later, like a cloud database.

### Counter/Index.razor

This is based on the default counter component that comes with the Blazor template, but augmented with additional interactivity. It is here to begin to show you how to use the Blossom API in your components.

Note that in `_Imports.razor`, we `@inject BlossomApi Api`. This means that the Blossom API is automatically available to all components you build. This is a powerful feature of Blossom, as it allows you to build your application without worrying about the API, and without having to write any API code yourself. 
Blossom generates it all, including:

- safe DTOs that disconnect your client from your entities entirely,
- a client-side API that is automatically generated from all public methods in your entities.

This means you simply need to build the *logic* of your app, and the API is automatically generated for you.

In `Counter/Index.razor`, you will see this injected `Api` used to:

- Create a new Counter in the Blossom Repository.
- Increment and decrement the counter, using API calls that directly map to the public methods in the `Counter.cs` entity. (Note: Look how cleanly you can call these methods when they have no parameters!)
- Multiply the counter by various amounts, again calling a public method in the Counter entity, but this time with a parameter.

### Counter/Counter.cs

This file is an example of a **Blossom Entity**. Entities are the first of two core items of your application, and are based on [Domain-Driven Design Entities](https://khalilstemmler.com/articles/typescript-domain-driven-design/entities/). Entities are 
the main place where you should put your business logic. 

`Counter.cs` inherits from `BlossomEntity<string>`, letting Blossom know that this is an entity with a string ID. Entities differ from other objects in that they are identifiable by a unique ID, allowing them to be stored and retrieved uniquely from the Blossom repository. 
Otherwise, this class is an extremely simple example of an entity, with a single property `Value` and a few public methods to manipulate that value. Note how simple your logic can be, and then go back to `Counter/Index.razor` and note how simple it is to call this logic.

### Weather/Index.razor

This is a more complex example of a Blossom component. It uses the Blossom API to get forecasts in various ways from the Blossom repository. 

Every Blossom project comes with a built-in In Memory Repository, automatically configured for you. This means you can build your entire application without worrying about the database, and then switch to a cloud database later.

For this project, we pre-load the repository with a few random weather forecasts in `Program.cs`. 

This component demonstrates how to retrieve forecasts from your project's repository with one line of code. It uses the second core item of your application: **Blossom Queries**. 

Queries are the way you retrieve data from your repository, and are based on [Specifications](https://specification.ardalis.com/). 
Every Blossom query you write translates into an API call that is automatically generated for you.

### Weather/Forecasts.cs

To write a Blossom Query, you create a class for your "collection" of entities. In this case we have a collection of Weather Forecasts, named `Forecasts.cs`.
This collection is called an **Aggregate** in blossom, based on [Domain-Driven Design Aggregates](https://martinfowler.com/bliki/DDD_Aggregate.html). 
This class inherits from `BlossomAggregate<Forecast>` to let Blossom know to generate an API for these queries.

To write a query, you simply write a public method that returns a `BlossomQuery<Forecast>`, which under the hood is a Specification (but you don't have to worry about that!)
You start the query with `Query()`, then use normal LINQ methods to build your query. The query will not run until the API actually calls it.

Also note that this class is optional, only needed if you actually need to write queries. If you don't need to retrieve custom lists of things, you can simply use the Blossom API to retrieve and save entities directly, as we did for the Counter.

### Weather/Forecast.cs

This is the entity for the Weather Forecast. It is again a simple entity, with some randomizers to create random forecasts that appear realistic. 

It also includes an `UpdateToLatest()` method that simulates an update of the forecast to current weather. This is hooked up to the "Update to Latest Data" button in `Weather/Index.razor`. 

Note that when you click this button, the temperature and other properties automatically change, *both* in the Today section, and the Forecast table below which includes today's date. This demonstrates another feature of Blossom: **Realtime Updates**. 

The "entities" you are working with in the Razor files are actually not entities at all -- they are automatically generated DTOs that are kept in sync with the actual entities in the repository. This means that when you update an entity in the repository, the changes are automatically propagated to the DTOs that the client uses, and the UI is automatically updated, even if the entities exist in multiple disconnected components!

## Development Tips for Blossom Projects

- If you want to **create** an entity: write a public constructor on the entity. This turns into a `Create()` method on the Blossom API.
- If you want to **update** an entity: write a public method on the entity with the logic to update it. This turns into a method on the DTO you use in your API, named the same as the method you wrote.
- If you want to **get a list of entities**: write a BlossomQuery inside a BlossomAggregate. Each query turns into a method on the Blossom API that you can call to get the entities.
- If you want to **get 1 entity by id**: you don't have to write anything. Just use the API's `Get(id)` method that comes with Blossom, or simply use the entity that comes from your query directly!
- If you want to **delete an entity**: you don't have to write anything. Just use the `Delete()` method on the DTO itself!

## Other Tips
- Set the File Nesting settings to "Blossom" for the best IDE experience. 