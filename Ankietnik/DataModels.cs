using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ankietnik
{
    public struct EncryptedPassword
    {
        internal byte[] Salt;
        internal byte[] Key;
    }

    public struct OperationResult
    {
        internal OperationStatus Status;
        internal string Message;
    }

    public class User
    {
        internal string Username;
        internal EncryptedPassword Password;
        internal int Role;
        internal int Group;
    }

    public enum OperationStatus { Failed, Success}
    public enum WarningType { Success, Info, Warning, Danger }
}