using AutoMapper;
using PractiFly.DbContextUtility.Context.PractiflyDb;

namespace PractiFly.WebApi.AutoMappers;

public partial class PractiFlyProfile : Profile
{
    private readonly PractiFlyContext _context;


    public PractiFlyProfile(IPractiflyContext context)
    {


    }
}