using LC.ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LC.DATABASE.Data.Seeds
{
    public class SpecialitySeed
    {
        public static async Task Seed(LegalConnectionContext context)
        {
            var list = new List<Speciality>
            {
                new Speciality 
                {
                    ColloquialName = "Herencia" ,
                    OfficialName = "Herencia",
                    SpecialityThemes = new List<SpecialityTheme>
                    {
                        new SpecialityTheme
                        {
                            OfficialName = "Herencia Tema",
                            ColloquialName = "Herencia Tema",
                        }
                    }
                },
                new Speciality
                {
                    ColloquialName = "Civil" ,
                    OfficialName = "Civil",
                    SpecialityThemes = new List<SpecialityTheme>
                    {
                        new SpecialityTheme
                        {
                            OfficialName = "Civil Tema",
                            ColloquialName = "Civil Tema",
                        }
                    }
                },
                new Speciality
                {
                    ColloquialName = "Mercantil" ,
                    OfficialName = "Mercantil",
                    SpecialityThemes = new List<SpecialityTheme>
                    {
                        new SpecialityTheme
                        {
                            OfficialName = "Mercantil Tema",
                            ColloquialName = "Mercantil Tema",
                        }
                    }
                },
                new Speciality
                {
                    ColloquialName = "Fiscal" ,
                    OfficialName = "Fiscal",
                    SpecialityThemes = new List<SpecialityTheme>
                    {
                        new SpecialityTheme
                        {
                            OfficialName = "Fiscal Tema",
                            ColloquialName = "Fiscal Tema",
                        }
                    }
                }
            };

            await context.Specialities.AddRangeAsync(list);
            await context.SaveChangesAsync();
        }
    }
}
