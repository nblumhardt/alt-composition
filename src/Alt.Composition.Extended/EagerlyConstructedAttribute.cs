using System;
using Alt.Composition.Convention;

namespace Alt.Composition
{
    /// <summary>
    /// Marks a part as being constructed by the container up-front, before any
    /// exports are requested.
    /// </summary>
    /// <remarks>Currently works only when conventions are applied.</remarks>
    /// <seealso cref="ExtendedConventions.WithEagerConstructionSupport"/>
    [AttributeUsage(AttributeTargets.Class)]
    public class EagerlyConstructedAttribute : Attribute
    {
    }
}
