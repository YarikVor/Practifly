using PractiFly.WebApi.Context;
using PractiFly.WebApi.EntityDb.Users;

namespace PractiFly.Tests.EntityFromDb.TestData.Users;

public class UsersTestData
{
    private static readonly IUsersContext _usersContext = null!;

    public void AddGroup()
    {
        var group = new Group[]
        {
            new()
            {
                Id = 1,
                Name = "Test1",
                FoundationDate = DateOnly.FromDateTime(DateTime.Now),
                TerminationDate = DateOnly.FromDateTime(DateTime.Now),
                Note = ""
            },
            new()
            {
                Id = 2,
                Name = "Test2",
                FoundationDate = DateOnly.FromDateTime(DateTime.Now),
                TerminationDate = DateOnly.FromDateTime(DateTime.Now),
                Note = ""
            }
        };
        //TODO:
        //_usersContext.Users.AddRange(group); 
    }

    public void AddGroupCourse()
    {
        var groupCourse = new GroupCourse[]
        {
            new()
            {
                GroupId = 1,
                CourseId = 1,
                LevelId = 1,
                IsCompleted = false,
                Note = ""
            },
            new()
            {
                GroupId = 1,
                CourseId = 1,
                LevelId = 1,
                IsCompleted = true,
                Note = ""
            }
        };
        //TODO:
        //_usersContext.Users.AddRange(groupCourse);
    }

    public void AddUser()
    {
        var user = new User[]
        {
            new()
            {
                Id = 1,
                FirstName = "Test1",
                LastName = "Test1",
                Email = "",
                Phone = "",
                FilePhoto = "",
                RegistrationDate = DateOnly.FromDateTime(DateTime.Now)
            },
            // Add one
            new()
            {
                Id = 2,
                FirstName = "Test2",
                LastName = "Test2",
                Email = "",
                Phone = "",
                FilePhoto = "",
                RegistrationDate = DateOnly.FromDateTime(DateTime.Now)
            }
        };

        _usersContext.Users.AddRange(user);
    }

    public void AddUserCourse()
    {
        var userCourse = new UserCourse[]
        {
            new()
            {
                UserId = 1,
                CourseId = 1,
                LevelId = 1,
                IsCompleted = true,
                //LastTime = TimeOnly.FromDateTime(DateTime.Now),
                LastThemeId = 1,
                Grade = 1,
                Note = ""
            },
            new()
            {
                UserId = 1,
                CourseId = 1,
                LevelId = 1,
                IsCompleted = true,
                //LastTime = TimeOnly.FromDateTime(DateTime.Now),
                LastThemeId = 1,
                Grade = 1,
                Note = ""
            }
        };
        //TODO:
        //_usersContext.Users.AddRange(userCourse);
    }

    public void AddUserGroup()
    {
        var userGroup = new UserGroup[]
        {
            new()
            {
                UserId = 1,
                GroupId = 1,
                IsActive = true,
                Note = ""
            },
            new()
            {
                UserId = 1,
                GroupId = 1,
                IsActive = false,
                Note = ""
            }
        };
        //TODO:
        //_usersContext.Users.AddRange(userGroup);
    }

    public void AddUserHeading()
    {
        var userHeading = new UserHeading[]
        {
            new()
            {
                UserId = 1,
                HeadingId = 1,
                LevelId = 1,
                Note = ""
            },
            new()
            {
                UserId = 1,
                HeadingId = 1,
                LevelId = 1,
                Note = ""
            }
        };
        //TODO:
        //_usersContext.Users.AddRange(userHeading);
    }

    public void AddUserMaterial()
    {
        var userMaterial = new UserMaterial[]
        {
            new()
            {
                UserId = 1,
                MaterialId = 1,
                IsCompleted = true,
                ResultUrl = "https:...",
                Grade = 1,
                Note = ""
            },
            new()
            {
                UserId = 1,
                MaterialId = 1,
                IsCompleted = false,
                ResultUrl = "https://...",
                Grade = 2,
                Note = ""
            }
        };
        //TODO:
        //_usersContext.Users.AddRange(userMaterial);
    }

    public void AddUserTheme()
    {
        var userTheme = new UserTheme[]
        {
            new()
            {
                UserId = 1,
                ThemeId = 1,
                IsCompleted = true,
                LevelId = 1,
                Grade = 1,
                Note = ""
            },
            new()
            {
                UserId = 1,
                ThemeId = 1,
                IsCompleted = false,
                LevelId = 1,
                Grade = 3,
                Note = ""
            }
        };
        //TODO:
        //_usersContext.Users.AddRange(userTheme);
    }
}