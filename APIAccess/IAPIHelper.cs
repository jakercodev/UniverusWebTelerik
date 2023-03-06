using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace UniverusWebTelerik.APIAccess
{
    public interface IAPIHelper
    {
        string CallAPIGet(string endpointurl, string param = "");

        Task CallAPIPost(string endpointurl, object paramObj);
    }
}