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

        // database constants
        internal const string CONN_STRING = "temp_conn_string";

        // user
        internal const string USERS_TABLE_NAME = "Users";
        internal const string USERS_USERNAME_FIELD = "Username";
        internal const string USERS_SALT_FIELD = "Salt";
        internal const string USERS_KEY_FIELD = "Key";
        internal const string USERS_ROLE_FIELD = "RoleId";
        internal const string USERS_GROUP_FIELD = "GroupId";

    }
}