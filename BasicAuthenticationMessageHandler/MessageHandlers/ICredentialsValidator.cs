using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageHandlers
{
    public interface ICredentialsValidator
    {
        bool Validate(Credentials credentials);
    }
}
