using PractiFly.WebApi.Context;
using PractiFly.WebApi.EntityDb.Materials;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PractiFly.Tests.EntityFromDb.TestData.Materials
{
    public class MaterialsTestData
    {
        private static IMaterialsContext _materialsContext = null!;

        public void AddCompetency()
        {
            var competency = new Competency[]
            {
                new()
                {
                    Id = 1,
                    Name = "Test1",
                    HeadingId = 1,
                    ParentId = 1, //TODO:
                    Note = "",
                },
                new()
                {
                    Id = 2,
                    Name = "Test2",
                    HeadingId = 1,
                    ParentId = 1, //TODO:
                    Note = "",
                }
            };
            //TODO:
            //_materialsContext.Materials.AddRange(competency); 
        }
        public void AddHeading()
        {
            var heading = new Heading[]
            {
                new()
                {
                    Id = 1,
                    Code = "01.01.01.01",
                    Name = "Test1",
                    Udc = "Test1",
                    Note = "",
                    Description = "",

                },
                new()
                {
                    Id = 1,
                    Code = "02.02.02.02",
                    Name = "Test2",
                    Udc = "Test2",
                    Note = "",
                    Description = "",


                }
            };
            //TODO:
            //_materialsContext.Materials.AddRange(heading); 
        }
        public void AddHeadingCompetencyg()
        {
            var headingCompetency = new HeadingCompetency[]
            {
                new()
                {
                    Id= 1,
                    CompetencyId = 1,
                    LevelId = 1,
                    Note = "",
                },
                new()
                {
                    Id= 2,
                    CompetencyId = 1,
                    LevelId = 1,
                    Note = "",
                }
            };
            //TODO:
            //_materialsContext.Materials.AddRange(headingCompetency); 
        }
        public void AddHeadingMaterial()
        {
            var headingMaterial = new HeadingMaterial[]
            {
                new()
                {
                    HeadingId = 1,
                    MaterialId = 1,
                    Note = "",

                },
                new()
                {
                    HeadingId = 1,
                    MaterialId = 1,
                    Note = "",
                }
            };
            //TODO:
            //_materialsContext.Materials.AddRange(headingMaterial); 
        }
        public void AddLanguage()
        {
            var language = new Language[]
            {
                new()
                {
                    Code = "en",
                    Name = "Англійська",
                    OriginalName = "English",
                    Note = "",
                },
                new()
                {
                    Code = "ua",
                    Name = "Українська",
                    OriginalName = "Ukrainian",
                    Note = "",
                }
            };
            //TODO:
            //_materialsContext.Materials.AddRange(language); 
        }
        public void AddLevel()
        {
            var level = new Level[]
            {
                new()
                {
                    Id = 1,
                    Name = "Test1",
                    Number = 1,
                    Note = "",
                    Description = "",
                },
                new()
                {
                    Id = 2,
                    Name = "Test2",
                    Number = 1,
                    Note = "",
                    Description = "",
                }
            };
            //TODO:
            //_materialsContext.Materials.AddRange(level); 
        }
        public void AddMaterial()
        {
            var material = new Material[]
            {
                new()
                {
                    Id = 1,
                    Name = "Test1",
                    // LanguageCode = "en",
                    Url = "",
                    IsPractical = true,
                    Note = "",

                },
                new()
                {
                    Id = 2,
                    Name = "Test2",
                    // LanguageCode = "ua",
                    Url = "",
                    IsPractical = false,
                    Note = "",
                }
            };
            //TODO:
            //_materialsContext.Materials.AddRange(material); 
        }
        public void AddMaterialBlock ()
        {
            var materialBlock = new MaterialBlock[]
            {
                new()
                {
                    ParentId = 1,
                    ChildId = 1,
                    Number = 1,
                    Note = "",
                },
                new()
                {
                    ParentId = 1,
                    ChildId = 1,
                    Number = 2,
                    Note = "",
                }
            };
            //TODO:
            //_materialsContext.Materials.AddRange(materialBlock); 
        }
        public void AddMaterialCompetency()
        {
            var materialCompetency = new MaterialCompetency[]
            {
                new()
                {
                    MaterialId = 1,
                    CompetencyId = 1,
                    Note = "",
                },
                new()
                {
                    MaterialId = 2,
                    CompetencyId = 2,
                    Note = "",
                }
            };
            //TODO:
            //_materialsContext.Materials.AddRange(materialCompetency); 
        }
        public void AddUnit()
        {
            var unit = new Unit[]
            {
                new()
                {
                    MaterialId = 1,
                    Text = "",
                    Url = "",
                },
                new()
                {
                    MaterialId = 2,
                    Text = "",
                    Url = "",
                }
            };
            //TODO:
            //_materialsContext.Materials.AddRange(unit); 
        }
    }
}
