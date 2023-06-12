using AutoMapper;
using IConfigurationProvider = AutoMapper.IConfigurationProvider;

namespace PractiFly.WebApi.AutoMappers;

public class PractiFlyMapper : Mapper
{
    public PractiFlyMapper(IConfigurationProvider configurationProvider) :
        base(configurationProvider)
    {
    }
}