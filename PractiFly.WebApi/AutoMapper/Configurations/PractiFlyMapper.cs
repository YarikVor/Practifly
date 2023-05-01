using AutoMapper;
using PractiFly.DbContextUtility.Context.PractiflyDb;
using IConfigurationProvider = AutoMapper.IConfigurationProvider;

namespace PractiFly.WebApi.AutoMappers;

public class PractiFlyMapper : Mapper
{
    /*public PractiFlyMapper(IPractiflyContext context) :
        base(new PractiFlyMapperConfiguration(context))
    {
        
    }*/
    
    public PractiFlyMapper(IConfigurationProvider configurationProvider) :
        base(configurationProvider)
    {
    }
}