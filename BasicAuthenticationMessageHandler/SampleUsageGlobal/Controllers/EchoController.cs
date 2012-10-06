using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SampleUsageGlobal.Controllers
{
    public class EchoController : ApiController
    {
        [Authorize]
        public string Get(string echoStr)
        {
            return echoStr;
        }
    }
}