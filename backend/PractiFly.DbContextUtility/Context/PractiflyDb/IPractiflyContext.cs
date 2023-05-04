using PractiFly.DbContextUtility.Context.Courses;
using PractiFly.DbContextUtility.Context.Materials;
using PractiFly.DbContextUtility.Context.Users;

namespace PractiFly.DbContextUtility.Context.PractiflyDb;

public interface IPractiflyContext : ICoursesContext, IMaterialsContext, IUsersContext
{
}