using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PractiFly.WebApi.Context;
using PractiFly.WebApi.EntityDb.Users;

namespace PractiFly.Tests.EntityFromDb.TestData.Courses
{
    internal class Course
    {
        private static IUsersContext _usersContext;

        public void AddUser()
        {
            var user = new User[]
            {
                new()
                {
                    Id = 1,
                    FirstName = "Test",
                    LastName = "Test",
                    Email = "",
                    Phone = "",
                    FilePhoto = "",
                    RegistrationDate = DateOnly.FromDateTime(DateTime.Now),
                },
                // Add one
            };
            
            _usersContext.Users.AddRange(user);
        }
        
    }
}
