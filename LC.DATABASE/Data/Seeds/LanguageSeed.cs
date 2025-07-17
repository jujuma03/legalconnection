using LC.ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LC.DATABASE.Data.Seeds
{
    public class LanguageSeed
    {
        public static async Task Seed(LegalConnectionContext context)
        {
            var idioms = new List<Language>
            {
                new Language { Name = "Inglés" },
                new Language { Name = "Chino" },
                new Language { Name = "Hindi" },
                new Language { Name = "Español" },
                new Language { Name = "Francés" },
                new Language { Name = "Árabe" },
                new Language { Name = "Ruso" },
                new Language { Name = "Portugués" },
                new Language { Name = "Bengali" },
                new Language { Name = "Alemán" },
                new Language { Name = "Japonés" },
                new Language { Name = "Coreano" }
            };

            await context.AddRangeAsync(idioms);
            await context.SaveChangesAsync();
        }
    }
}
