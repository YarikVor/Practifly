using AutoMapper;
using PractiFly.DbContextUtility.Context.PractiflyDb;

namespace PractiFly.WebApi.AutoMappers;

public class PractiFlyMapperConfiguration : MapperConfiguration
{
    public PractiFlyMapperConfiguration(IPractiflyContext context)
        : base(new PractiFlyMapperConfigurationExpression(context))
    {
    }

}