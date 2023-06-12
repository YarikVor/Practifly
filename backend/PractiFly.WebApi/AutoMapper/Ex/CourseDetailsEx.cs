using PractiFly.DbEntities.Users;
using PractiFly.WebApi.Dto.CourseDetails;

namespace PractiFly.WebApi.AutoMapper.Ex;

public static class CourseDetailsEx
{
    public static void EditData(this UserMaterial userMaterial, UserMaterialSendDto dto)
    {
        userMaterial.IsCompleted = dto.IsCompleted;
        userMaterial.ResultUrl = dto.ResultUrl;
    }
}