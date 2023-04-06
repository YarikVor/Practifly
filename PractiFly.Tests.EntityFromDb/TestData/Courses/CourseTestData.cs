using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PractiFly.WebApi.Context;
using PractiFly.WebApi.EntityDb.Courses;
using PractiFly.WebApi.EntityDb.Users;

namespace PractiFly.Tests.EntityFromDb.TestData.Courses
{
    public class CoursesTestData
    {
        private static ICoursesContext _courseContext = null!;

        public void AddCourse()
        {
            var course = new Course[]
            {
                new()
                {
                    Id = 1,
                    Name = "Test1",
                    OwnerId = 1,
                    Note = "",
                    Description = "",
                },
                new()
                {
                    Id = 2,
                    Name = "Test2",
                    OwnerId = 2,
                    Note = "",
                    Description = "",
                }
            };
            //TODO:
            //_courseContext.Courses.AddRange(course);
        }
        public void AddCourseCompetency()
        {
            var courseCompetency = new CourseCompetency[]
            {
                new()
                {
                    CourseId = 1,
                    CompetencyId = 1,
                    Note = "",
                },
                new()
                {
                    CourseId = 2,
                    CompetencyId = 2,
                    Note = "",

                }
            };
            //TODO:
            //_courseContext.Courses.AddRange(courseCompetency);
        }
        public void AddCourseDependency()
        {
            var courseDependency = new CourseDependency[]
            {
                new()
                {
                    CourseId = 1,
                    BaseCourseId = 1,
                    CourseDependencyTypeId = 1,
                    Note = "",
                },
                new()
                {
                    CourseId = 2,
                    BaseCourseId = 2,
                    CourseDependencyTypeId = 2,
                    Note = "",
                }
            };
            //TODO:
            //_courseContext.Courses.AddRange(courseDependency);
        }
        public void AddCourseDependencyType()
        {
            var courseDependencyType = new CourseDependencyType[]
            {
                new()
                {
                    Id = 1,
                    Name = "Test1",
                    Note = "",
                    Description = "",
                },
                new()
                {
                    Id = 2,
                    Name = "Test2",
                    Note = "",
                    Description = "",
                }
            };
            //TODO:
            //_courseContext.Courses.AddRange(courseDependencyType);
        }
        public void AddCourseHeading()
        {
            var courseHeading = new CourseHeading[]
            {
                new()
                {
                    CourseId = 1,
                    HeadingId = 1,
                    IsBasic = true,
                    Note = "",
                },
                new()
                {
                    CourseId = 2,
                    HeadingId = 2,
                    IsBasic = false,
                    Note = "",
                }
            };
            //TODO:
            //_courseContext.Courses.AddRange(courseHeading);
        }
        public void AddCourseMaterial()
        {
            var courseMaterial = new CourseMaterial[]
            {
                new()
                {
                    CourseId = 1,
                    MaterialId = 1,
                    PriorityLevel = 1,
                    IsReserved = true,
                    Note = "",
                },
                new()
                {
                    CourseId = 2,
                    MaterialId = 2,
                    PriorityLevel = 1,
                    IsReserved = false,
                    Note = "",
                }
            };
            //TODO:
            //_courseContext.Courses.AddRange(courseMaterial);
        }
        public void AddTheme()
        {
            var theme = new Theme[]
            {
                new()
                {
                    Id = 1,
                    Name = "Test1",
                    CourseId = 1,
                    LevelId = 1,
                    ParentId = 1,
                    Number = 1,
                    Note = "",
                    Description = "",
                },
                new()
                {
                    Id = 2,
                    Name = "Test2",
                    CourseId = 2,
                    LevelId = 1,
                    ParentId = 1,
                    Number = 1,
                    Note = "",
                    Description = "",
                }
            };
            //TODO:
            //_courseContext.Courses.AddRange(theme);
        }
        public void AddThemeMaterial()
        {
            var themeMaterial = new ThemeMaterial[]
            {
                new()
                {
                    ThemeId = 1,
                    MaterialId = 1,
                    Number = 1,
                    IsBasic = true,
                    LevelId = 1,
                    Note = "",
                },
                new()
                {
                    ThemeId = 2,
                    MaterialId = 2,
                    Number = 3,
                    IsBasic = false,
                    LevelId = 2,
                    Note = "",

                }
            };
            //TODO:
            //_courseContext.Courses.AddRange(themeMaterial);
        }
    }
}
