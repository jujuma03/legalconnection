using System;
using System.Collections.Generic;
using System.Text;
using static LC.CORE.Structs.PaginationStructs;

namespace LC.CORE.Services.Interfaces
{
    public interface IPaginationService
    {
        int GetRecordsPerDraw();
        int GetPage();
        string GetSearchValue();
        SentParameters GetSentParameters();
    }
}
