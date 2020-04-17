using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ankietnik
{
    internal struct EncryptedPassword
    {
        internal byte[] Salt;
        internal byte[] Key;
    }

    internal struct OperationResult
    {
        internal OperationStatus Status;
        internal string Message;
    }

    internal class User
    {
        internal string Username;
        internal EncryptedPassword Password;
        internal int Role;
        internal int Group;
    }

    internal enum OperationStatus { Failed, Success}
}