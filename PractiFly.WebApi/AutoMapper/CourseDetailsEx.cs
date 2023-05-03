using PractiFly.DbEntities.Users;
using PractiFly.WebApi.Dto.CourseDetails;

namespace PractiFly.WebApi.AutoMapper
{
    public static class CourseDetailsEx
    {
        public static void EditData(this UserMaterial userMaterial, UserMaterialInfoDto dto)
        {
            userMaterial.IsCompleted = dto.IsCompleted;
            userMaterial.ResultUrl = dto.ResultUrl;
        }
    }
}
