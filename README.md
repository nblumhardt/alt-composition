alt-composition
===============

Extensions and third-party framework integration for the `System.Composition` IoC container.

Alt.Composition.Settings
------------------------

Makes `<appSettings>` values from _App.config_ or _Web.config_ available for importing into parts.

In App/Web.config:

```
<applicationSettings>
	<add key="SmtpHost" value="smtp.example.com" />
		...
```

Add the `Alt.Composition.Setting` attribute to imported properties, or to the parameters in an importing constructor:
 

```
public class SomePart
{
	[Import, Setting("someKey")]
	public int SomeSetting { get; set; }
}
```

To enable the feature, call `WithApplicationSettings()` on your `ContainerConfiguration`:

```
using Alt.Composition.Hosting;

...

var configuration = new ContainerConfiguration()
	.WithApplicationSettings()
	...
```

Alt.Composition.Web
-------------------

Provides an opinionated, zero-configuration IoC setup for ASP.NET MVC and WebAPI (not yet implemented).

The convention applied out of the box will match controllers, and will look for other parts in the _*.Parts.*_ namespace.

*Getting started*

 # Install the package
 # Add a /Parts folder to your ASP.NET MVC website
 # Add parts to the folder
 # Add constructors to your controllers accepting parts and their interfaces as dependencies.

*To add additional assemblies:*

In _Global.asax_:

```
CompositionProvider.AddAssembly(typeof(SomePart).Assembly);
```

*Dialling back the opinions:*

The sub-package: `Alt.Composition.Web.Mvc` provides lower-level building blocks and can be installed directly if the default configuration or conventions are not required.

Alt.Composition.Web.Mvc
-----------------------

Provides dependency injection into controllers and filter attributes. Enables application setting support and eager construction out-of-the-box; see below.

*Getting started:*

(1.) Install the package

(2.) Create conventions and ensure they register MVC controllers. The _AddDefaultMvcConventions()_ extension method helps with this:

```
var conventions = new ConventionBuilder();
conventions.AddDefaultMvcConventions();
```

(3.) Create a container, including the conventions and your MVC app's assembly:

```
var container = new ContainerConfiguration()
	.WithMvcConventions(conventions)
	.WithAssembly(typeof(MvcApplication).Assembly)
	.CreateContainer();
```

(4.) Initialize the MvcCompositionProvider:

```
MvcCompositionProvider.Initialize(container);
```

Controllers will now be dependency injected using the container configured in (3).

 (5.) Enable filter and filter attribute injection

```
CompositionFilterProvider.Install(FilterProviders.Providers);
ImportCapableFilterAttributeFilterProvider.Install(FilterProviders.Providers);
```

Alt.Composition.Extended
------------------------

Simple additions to the System.Composition API.

*Eager construction:*

Parts marked with `EagerlyConstructedAttribute` can be started at container creation time:

```
[EagerlyConstructed]
public class SomePart { }
```

Requires a convention to be applied:

```
var conventions = new ConventionBuilder();
conventions.SupportEagerConstruction();
```

When the container is created:

```
var container = ...
container.ConstructEagerParts();
```

License and Credits
-------------------

Available for commercial and open-source projects under MS-PL, see http://opensource.org/licenses/MS-PL. Draws from many of the samples published by Microsoft at http://mef.codeplex.com.

Icon courtesy Mila Redko via http://thenounproject.com/noun/road-junction/#icon-No14323.

Copyright (c) 2013 Nicholas Blumhardt.
