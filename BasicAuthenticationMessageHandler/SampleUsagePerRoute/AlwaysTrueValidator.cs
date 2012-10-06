using MessageHandlers;

namespace SampleUsagePerRoute
{
    public class AlwaysTrueValidator:ICredentialsValidator
    {
        public bool Validate(Credentials credentials)
        {
            return true;
        }
    }
}