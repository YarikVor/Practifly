using AutoMapper;
using PractiFly.DbEntities.Users;
using PractiFly.WebApi.Dto.Admin.UserView;
using PractiFly.WebApi.Dto.Profile;

namespace PractiFly.WebApi.AutoMapper.Ex;

public static class UserEx
{
    public static void ChangeUser(this User user, UserProfileInfoEditDto userDto)
    {
        user.FirstName = userDto.FirstName;
        user.LastName = userDto.LastName;
        user.PhoneNumber = userDto.PhoneNumber;
        user.Email = userDto.Email;
        user.Birthday = userDto.Birthday;
        //user.FilePhoto = userDto.FilePhoto;
    }

    public static void ChangeUserInAdmin(this User user, UserProfileForAdminUpdateDto userDto)
    {
        user.FirstName = userDto.FirstName;
        user.LastName = userDto.LastName;
        user.Email = userDto.Email;
        user.PhoneNumber = userDto.Phone;
        //user.FilePhoto = userDto.FilePhoto;
        user.Birthday = userDto.Birthday;
    }
}