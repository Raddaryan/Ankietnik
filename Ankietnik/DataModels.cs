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
        internal object Payload;
    }

    public class User
    {
        internal int Id;
        internal string Username;
        internal EncryptedPassword Password;
        internal int Role;
        internal int Group;
    }

    public class Question
    {
        public int Id { get; set; }
        public string Content { get; set; }
    }

    public struct Response
    {
        internal int QuestionId;
        internal int Content;
    }

    public class Answer
    {
        public int QuestionId { get; set; }
        public string Content { get; set; }
        public bool Response { get; set; }
    }

    public class Questionnaire
    {
        internal int? Id;
        internal int OwnerId;
        internal int GroupId;
        internal List<Question> Questions;
    }

    public enum OperationStatus { Failed, Success}
    public enum WarningType { Success, Info, Warning, Danger }
}