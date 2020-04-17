using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ankietnik
{
    internal static class Constants
    {
        internal const int DEFAULT_SALT_SIZE = 20;
        internal const char LIST_SEPARATOR = ',';

        internal const string UserNotFoundMsg = "User with specified username does not exist.";
        internal const string IncorrectCredentialsMsg = "Incorrect credentials have been provided.";
        internal const string UserAlreadyExistsMsg = "User with specified username already exists.";

        // database constants
        internal const string CONN_STRING = "temp_conn_string";

        // user
        internal const string USERS_TABLE_NAME = "Users";
        internal const string USERS_USERNAME_FIELD = "Username";
        internal const string USERS_SALT_FIELD = "Salt";
        internal const string USERS_KEY_FIELD = "Key";
        internal const string USERS_ROLE_FIELD = "RoleId";
        internal const string USERS_GROUP_FIELD = "GroupId";

        internal enum Role { User, Owner}
        internal static readonly Dictionary<Role, int> Roles = new Dictionary<Role, int>
        {
            { Role.User, 0 },
            { Role.Owner, 1 }
        };
    }
}