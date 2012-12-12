using System.Reflection;
using System.Resources;
using System.Security;
using System.Web;
using Alt.Composition.Internal;

[assembly: AssemblyTitle("Alt.Composition.Web.Mvc")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("")]
[assembly: AssemblyProduct("Alt.Composition.Web.Mvc")]
[assembly: AssemblyCopyright("Copyright © 2012")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]
[assembly: AssemblyVersion("1.0.0.0")]
[assembly: AssemblyFileVersion("1.0.0.0")]
[assembly: PreApplicationStartMethod(typeof(RequestContextDisposalModule), "Register")]
[assembly: SecurityTransparent]
[assembly: NeutralResourcesLanguage("en")]
