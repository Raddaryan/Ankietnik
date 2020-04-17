using System.Text;

namespace Ankietnik
{
    internal static class AccountService
    {
        internal static OperationResult Login(string userName, string password)
        {
            return new OperationResult();
        }

        internal static OperationResult Register(string userName, string password)        
        {
            return new OperationResult();
        }

        internal static User GetUser(string userName)
        {
            var queryBuilder = new StringBuilder();

            return new User();
        }
        
    }
}