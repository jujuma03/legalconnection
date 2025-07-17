using LC.CORE.Structs;
using LC.ENTITIES.Models;
using LC.REPOSITORY.Repositories.Interfaces;
using LC.SERVICE.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LC.SERVICE.Services.Implementations
{
    public class ShortcutService : IShortcutService
    {
        private readonly IShortcutRepository _shortcutRepository;
        public ShortcutService(IShortcutRepository shortcutRepository)
        {
            _shortcutRepository=shortcutRepository;
        }

        public async Task Delete(Shortcut shortcut)
        {
            await _shortcutRepository.Delete(shortcut);
        }

        public async Task<Shortcut> Get(Guid id)
        {
            return await _shortcutRepository.Get(id);
        }

        public async  Task<List<Shortcut>> GetAllActive(byte type)
        {
            return await _shortcutRepository.GetAllActive(type);
        }

        public async Task<DataTablesStructs.ReturnedData<object>> GetAllDatatable(DataTablesStructs.SentParameters sentParameters, byte type, byte v)
        {
            return await _shortcutRepository.GetAllDatatable(sentParameters, type, v);
        }

        public async Task Insert(Shortcut shortcut)
        {
            await _shortcutRepository.Insert(shortcut);
        }

        public async Task Update(Shortcut shortcut)
        {
            await _shortcutRepository.Update(shortcut);
        }
    }
}
