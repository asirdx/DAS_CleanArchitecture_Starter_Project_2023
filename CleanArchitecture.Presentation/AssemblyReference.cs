using System.Reflection;

namespace CleanArchitecture.Presentation;

public static class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(Assembly).Assembly; // Web API project does not have controller, They all moved t ohere for a central location. 
}
