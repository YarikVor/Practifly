using Microsoft.AspNetCore.Mvc;
using PractiFly.DbContextUtility.Context.PractiflyDb;

namespace PractiFly.WebApi.Controllers;

public class CourseDetailsController : Controller
{
    private readonly IPractiflyContext _context;
    
    public CourseDetailsController(IPractiflyContext context)
    {
        _context = context;
    }
    
    
}