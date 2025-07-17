using LC.ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LC.DATABASE.Data.Seeds
{
    public static class DepartmentSeed
    {
        public static async Task Seed(LegalConnectionContext context)
        {
            var result = new List<Department>()
            {
                new Department { Name = "Amazonas" },
                new Department { Name = "Áncash" },
                new Department { Name = "Apurímac" },
                new Department { Name = "Arequipa" },
                new Department { Name = "Ayacucho" },
                new Department { Name = "Cajamarca" },
                new Department { Name = "Callao" },
                new Department { Name = "Cusco" },
                new Department { Name = "Huancavelica" },
                new Department { Name = "Huánuco" },
                new Department { Name = "Ica" },
                new Department { Name = "Junín" },
                new Department { Name = "La Libertad" },
                new Department { Name = "Lambayeque" },
                new Department { Name = "Lima" },
                new Department { Name = "Loreto" },
                new Department { Name = "Madre de Dios" },
                new Department { Name = "Moquegua" },
                new Department { Name = "Pasco" },
                new Department { Name = "Piura" },
                new Department { Name = "Puno" },
                new Department { Name = "San Martín" },
                new Department { Name = "Tacna" },
                new Department { Name = "Tumbes" },
                new Department { Name = "Ucayali" }
            };

            await context.Departments.AddRangeAsync(result);
            await context.SaveChangesAsync();
        }
    }
}
