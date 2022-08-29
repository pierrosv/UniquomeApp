using System.Reflection;
using AutoMapper;

namespace UniquomeApp.Application.Mappings;

//TODO: Revisit use or remove
/// <summary>
/// 
/// </summary>
/// <remarks>
/// Having this class here serves nothing. It cannot locate the mappings of the actual projects. Jayson's Clean Architecture has a different project structure.
/// For now just copy that on the relevant projects
/// </remarks>
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        ApplyMappingsFromAssembly(Assembly.GetExecutingAssembly());
    }

    private void ApplyMappingsFromAssembly(Assembly assembly)
    {
        var typesWithMappingFrom = assembly.GetExportedTypes()
            .Where(t => t.GetInterfaces().Any(i =>
                i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMapFrom<>)))
            .ToList();

        foreach (var type in typesWithMappingFrom)
        {
            var instance = Activator.CreateInstance(type);
            var methodInfo = type.GetMethod("MappingFrom")
                             ?? type.GetInterface("IMapFrom`1").GetMethod("MappingFrom");
            methodInfo?.Invoke(instance, new object[] { this });
        }

        var typesWithMappingTo = assembly.GetExportedTypes()
            .Where(t => t.GetInterfaces().Any(i =>
                i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMapTo<>)))
            .ToList();
        foreach (var type in typesWithMappingTo)
        {
            var instance = Activator.CreateInstance(type);
            var methodInfo = type.GetMethod("MappingTo")
                             ?? type.GetInterface("IMapTo`1").GetMethod("MappingTo");
            methodInfo?.Invoke(instance, new object[] { this });
        }
    }
}