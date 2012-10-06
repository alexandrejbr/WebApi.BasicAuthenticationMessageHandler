using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MessageHandlers;

namespace SampleUsageGlobal
{
    public class AlwaysTrueValidator:ICredentialsValidator
    {
        public bool Validate(Credentials credentials)
        {
            return true;
        }
    }
}