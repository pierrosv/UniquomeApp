using AutoMapper;

namespace UniquomeApp.Application.Mappings;

public interface IMapFrom<T>
{
    void MappingFrom(Profile profile) => profile.CreateMap(typeof(T), GetType());
}