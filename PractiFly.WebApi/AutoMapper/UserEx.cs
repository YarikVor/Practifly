using Microsoft.AspNetCore.Identity;
using PractiFly.DbEntities.Users;
using PractiFly.WebApi.Dto.Admin.UserView;
using PractiFly.WebApi.Dto.Profile;
using PractiFly.WebApi.Dto.Registration;

namespace PractiFly.WebApi.AutoMapper
{
    public static class UserEx
    {
        public static UserFullnameItemDto ToUserFullnameItemDto(this User user)
        {
            return new UserFullnameItemDto() { Id = user.Id, Fullname = string.Concat(user.FirstName," ", user.LastName) };
        }

        public static void ChangeUser(this User user, UserProfileInfoCreateDto userDto)
        {
            user.FirstName = userDto.FirstName;
            user.LastName = userDto.LastName;
            user.PhoneNumber = userDto.PhoneNumber;
            user.Email = userDto.Email;
            user.Birthday = userDto.Birthday;
            user.FilePhoto = userDto.FilePhoto;
        }
        
    }
}
