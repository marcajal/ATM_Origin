using ATM_Origin.Core.RequestFilters;
using System;
using System.Collections.Generic;
using System.Text;

namespace ATM_Origin_Infrastucture.Interfaces
{
    public interface IUriService
    {
        Uri GetPostPaginationUri(TarjetaRequestFilter filter, string actionUrl);
    }
}
