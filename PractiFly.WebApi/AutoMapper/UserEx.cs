using Microsoft.AspNetCore.Identity;
using PractiFly.DbEntities.Users;
using PractiFly.WebApi.Dto.Admin.UserView;
using PractiFly.WebApi.Dto.Registration;

namespace PractiFly.WebApi.AutoMapper
{
    public static class UserEx
    {
        public static UserFullnameItemDto ToUserFullnameItemDto(this User user)
        {
            return new UserFullnameItemDto() { Id = user.Id, Fullname = string.Concat(user.FirstName, user.LastName) };
        }

        public static User ToUser(this RegistrationDto registrationDto)
        {
            return new User
            {
                UserName = registrationDto.Username,
                Birthday = registrationDto.Birthday,
                Email = registrationDto.Email,
                FirstName = registrationDto.Name,
                LastName = registrationDto.Surname,
                PhoneNumber = registrationDto.Phone,
                RegistrationDate = DateOnly.FromDateTime(DateTime.Today),
                FilePhoto = "https://www.nicepng.com/maxp/u2y3a9e6t4o0a9w7/"
            };
        }
        //public static User ToUser(this UserProfileForAdminCreateDto userDto)
        //{
        //    return new User
        //    {
        //        UserName = userDto.Name,
        //        LastName = userDto.Surname,
        //        Email = userDto.Email,
        //        PhoneNumber = userDto.Phone,
        //        FilePhoto = userDto.FilePhoto,

        //    };
        //}
    }
}
