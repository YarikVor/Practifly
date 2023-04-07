using PractiFly.WebApi.Context;

namespace PractiFly.WebApi.Db.Context;

public interface IPractiflyContext: ICoursesContext, IMaterialsContext, IUsersContext
{
    
}