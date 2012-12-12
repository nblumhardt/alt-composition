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

Provides an opinionated, zero-configuration IoC setup for ASP.NET MVC and WebAPI.

The convention applied out of the box will match controllers, and will look for other parts in the _*.Parts.*_ namespace.

The sub-packages:

 * Alt.Composition.Web.Mvc
 * Alt.Composition.Web.Http

Provide appropriate controller factories and can be installed directly if the default configuration or conventions are not required.

Alt.Composition.Web.Mvc
-----------------------

Alt.Composition.Web.Http
------------------------

Alt.Composition.Extended
------------------------



