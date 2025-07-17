using LC.CORE.Structs;
using LC.ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LC.SERVICE.Services.Interfaces
{
    public interface IShortcutService
    {
        Task<List<Shortcut>> GetAllActive(byte type);
        Task<DataTablesStructs.ReturnedData<object>> GetAllDatatable(DataTablesStructs.SentParameters sentParameters, byte type, byte v);
        Task<Shortcut> Get(Guid id);
        Task Insert(Shortcut shortcut);
        Task Delete(Shortcut shortcut);
        Task Update(Shortcut shortcut);
    }
}
