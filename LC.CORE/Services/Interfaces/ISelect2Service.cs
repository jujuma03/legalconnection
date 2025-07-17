﻿using System;
using System.Collections.Generic;
using System.Text;
using static LC.CORE.Structs.Select2Structs;

namespace LC.CORE.Services.Interfaces
{
    public interface ISelect2Service
    {
        int GetCurrentPage();
        string GetQuery();
        string GetRequestType();
        string GetSearchTerm();
        RequestParameters GetRequestParameters();
    }
}
