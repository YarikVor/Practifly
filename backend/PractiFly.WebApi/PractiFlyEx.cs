using System.Diagnostics;
using Bogus;
using Microsoft.EntityFrameworkCore;
using PractiFly.DbEntities.Courses;
using PractiFly.DbEntities.Materials;
using PractiFly.DbEntities.Users;
using Practifly.FakerGenerator;

namespace PractiFly.WebApi;

public static class PractiFlyEx
{
    //[Conditional("GENERATE_TEST_DATA")]
    public static void GenerateTestDataIfEmpty(this DbContext context)
    {
        //context.Database.EnsureCreated();

        Randomizer.Seed = new Random(0);

        var fakerManager = new PractiFlyFakerManager();

        AddData<User>();

        AddData<Language>();
        AddData<Group>();
        AddData<Course>();
        AddData<Level>();
        AddData<Heading>();
        AddData<Material>();

        AddData<Theme>();
        AddData<Competency>();
        AddData<ThemeMaterial>();
        AddData<CourseCompetency>();
        AddData<CourseDependencyType>();
        AddData<CourseDependency>();
        AddData<CourseHeading>();
        AddData<CourseMaterial>();
        AddData<HeadingCompetency>();
        AddData<HeadingMaterial>();
        AddData<MaterialBlock>();
        AddData<MaterialCompetency>();
        AddData<Unit>();
        AddData<GroupCourse>();
        AddData<UserCourse>();
        AddData<UserGroup>();
        AddData<UserHeading>();
        AddData<UserMaterial>();
        AddData<UserTheme>();

        void AddData<TEntity>() where TEntity : class
        {
            var dbSet = context.Set<TEntity>();

            if (!dbSet.Any())
            {
                var entities = fakerManager.Generate<TEntity>(5);

                dbSet.AddRange(entities);

                context.SaveChanges();
            }
        }
    }
}