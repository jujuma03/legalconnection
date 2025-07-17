using LC.DATABASE.Data;
using LC.ENTITIES.Models;
using LC.REPOSITORY.Base;
using LC.REPOSITORY.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace LC.REPOSITORY.Repositories.Implementations
{
    public class LegalCaseResponseRepository : Repository<LegalCaseResponse>, ILegalCaseResponseRepository
    {
        public LegalCaseResponseRepository(LegalConnectionContext context) : base(context) { }

    }
}
