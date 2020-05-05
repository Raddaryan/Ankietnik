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

        internal const string UserNotFoundMsg = "Użytkownik o takim loginie nie istnieje.";
        internal const string IncorrectCredentialsMsg = "Niepoprawny login lub hasło.";
        internal const string UserAlreadyExistsMsg = "Użytkownik o takim loginie już istnieje.";
        internal const string DataAccessErrorMsg = "Wystąpił błąd przy próbie połączenia z bazą danych.";

        // database constants
        internal const string CONN_STRING = "temp_conn_string";

        // user
        internal const string USERS_TABLE_NAME = "Users";
        internal const string USERS_USERNAME_FIELD = "Username";
        internal const string USERS_SALT_FIELD = "Salt";
        internal const string USERS_KEY_FIELD = "Key";
        internal const string USERS_ROLE_FIELD = "RoleId";
        internal const string USERS_GROUP_FIELD = "GroupId";

        // group
        internal const string GROUPS_TABLE_NAME = "Groups";
        internal const string GROUP_ID_FIELD = "GroupId";

        internal enum Role { User, Owner}
        internal static readonly Dictionary<Role, int> Roles = new Dictionary<Role, int>
        {
            { Role.User, 0 },
            { Role.Owner, 1 }
        };
    }
}