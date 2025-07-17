using LC.CORE.Helpers;
using LC.CORE.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using static LC.CORE.Structs.Select2Structs;

namespace LC.CORE.Services.Implementations
{
    public class Select2Service : ISelect2Service
    {
        public IHttpContextAccessor _httpContextAccessor;

        public Select2Service(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public int GetCurrentPage()
        {
            var request = _httpContextAccessor.HttpContext.Request;

            int.TryParse(request.Query[ConstantHelpers.SELECT2.SERVER_SIDE.REQUEST_PARAMETERS.CURRENT_PAGE], out int result);
            return result;
        }

        public string GetQuery()
        {
            var request = _httpContextAccessor.HttpContext.Request;

            return request.Query.ContainsKey(ConstantHelpers.SELECT2.SERVER_SIDE.REQUEST_PARAMETERS.QUERY) ?
                request.Query[ConstantHelpers.SELECT2.SERVER_SIDE.REQUEST_PARAMETERS.QUERY].ToString() :
                null;
        }

        public string GetRequestType()
        {
            var request = _httpContextAccessor.HttpContext.Request;

            return request.Query.ContainsKey(ConstantHelpers.SELECT2.SERVER_SIDE.REQUEST_PARAMETERS.REQUEST_TYPE) ?
                request.Query[ConstantHelpers.SELECT2.SERVER_SIDE.REQUEST_PARAMETERS.REQUEST_TYPE].ToString() :
                null;
        }

        public string GetSearchTerm()
        {
            var request = _httpContextAccessor.HttpContext.Request;

            return request.Query.ContainsKey(ConstantHelpers.SELECT2.SERVER_SIDE.REQUEST_PARAMETERS.SEARCH_TERM) ?
                request.Query[ConstantHelpers.SELECT2.SERVER_SIDE.REQUEST_PARAMETERS.SEARCH_TERM].ToString() :
                null;
        }

        public RequestParameters GetRequestParameters()
        {
            return new RequestParameters
            {
                CurrentPage = GetCurrentPage(),
                Query = GetQuery(),
                RequestType = GetRequestType(),
                SearchTerm = GetSearchTerm()
            };
        }
    }
}
