using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Text;

namespace LC.DATABASE.Data
{
    public class LegalConnectionContextFactory : IDesignTimeDbContextFactory<LegalConnectionContext>
    {
        public LegalConnectionContextFactory()
        {

        }

        public LegalConnectionContext CreateDbContext(string[] args)
        {
            DbContextOptionsBuilder<LegalConnectionContext> builder = new DbContextOptionsBuilder<LegalConnectionContext>();
            builder.UseSqlServer(
                "Server=209.151.152.37;Initial Catalog=LEGALCONNECTION;Persist Security Info=False;User ID=sa;Password=lc2020++;MultipleActiveResultSets=True;Encrypt=True;TrustServerCertificate=True;Connection Timeout=300;",
                //"Server=localhost;Database=TEST;Trusted_Connection=True;MultipleActiveResultSets=true",
                options =>
            {
                options.CommandTimeout((int)TimeSpan.FromMinutes(20).TotalSeconds);
                options.EnableRetryOnFailure();
            }); ;

            return new LegalConnectionContext(builder.Options);
        }
    }
}
