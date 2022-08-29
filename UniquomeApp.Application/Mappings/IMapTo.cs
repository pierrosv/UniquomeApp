using AutoMapper;

namespace UniquomeApp.Application.Mappings;

public interface IMapTo<T>
{
    void MappingTo(Profile profile) => profile.CreateMap(GetType(), typeof(T));
}