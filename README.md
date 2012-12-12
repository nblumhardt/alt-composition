alt-composition
===============

Extensions and third-party framework integration for the System.Composition container.

Alt.Composition.Settings
------------------------

Makes <appSettings> values from App.config or Web.config available for importing into parts.

    public class SomePart
	{
	    [Import, Setting("someKey")]
		public int SomeSetting { get; set; }
	}

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

	CompositionProvider.AddAssembly(typeof(SomePart).Assembly);

*Dialling back the opinions:*

The sub-packages:

 * Alt.Composition.Web.Mvc
 * Alt.Composition.Web.Http (not yet implemented)

Provide appropriate controller factories and can be installed directly if the default configuration or conventions are not required.

Alt.Composition.Web.Mvc
-----------------------

Provides a controller factory, and eventually other integration points, with ASP.NET MVC.

*Getting started:*

1. Install the package

2. Create conventions and ensure they register MVC controllers. The _AddDefaultMvcConventions()_ extension method helps with this:

	var conventions = new ConventionBuilder();
	conventions.AddDefaultMvcConventions();

3. Create a container, including the conventions and your MVC app's assembly:

	var container = new ContainerConfiguration()
		.WithDefaultConventions(conventions)
		.WithAssembly(typeof(MvcApplication).Assembly)
		.CreateContainer();

4. Initialize the MvcCompositionProvider:

	MvcCompositionProvider.Initialize(container);

Controllers will now be dependency injected using the container configured in (3).

Alt.Composition.Web.Http
------------------------

To be implemented.

Alt.Composition.Extended
------------------------

Simple additions to the System.Composition API.

License and Credits
-------------------

Available for commercial and open-source projects under MS-PL, see http://opensource.org/licenses/MS-PL.

Draws from many of the samples published by Microsoft at http://mef.codeplex.com.

Thanks are owing to the MEF team for putting out a great product, and to the awesome crew at Readify for pushing me get this project together covering some of the remaining gaps.

Copyright (c) 2012 Nicholas Blumhardt.