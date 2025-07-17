using System;
using System.Collections.Generic;
using System.Text;

namespace LC.PAYMENT.Services.Culqi.Models.Base
{
    public class DataListModel<T> where T : class
    {
        public List<T> Data { get; set; }
        public DataListPagingModel Paging { get; set; }
    }
}
