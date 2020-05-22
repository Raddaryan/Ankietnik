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

    /// <summary>
    /// Obiekt do komunikacji backend-frontend.
    /// Status przyjmuje wartości Success (1) lub Failed (0)
    /// </summary>
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

    /// <summary>
    /// Obiekt reprezentujący odpowiedź na pytanie w ankiecie służący do zapisywania odpowiedzi do bazy danych.
    /// </summary>
    public struct Response
    {
        internal int QuestionId;
        internal int Content;
    }

    /// <summary>
    /// Obiekt reprezentujący odpowiedź na pytanie w ankiecie służący do uzyskiwania listy odpowiedzi z bazy danych i wyświetlanie ich.
    /// </summary>
    public class Answer
    {
        public int QuestionId { get; set; }
        public string Content { get; set; }
        public bool Response { get; set; }
    }

    /// <summary>
    /// Obiekt reprezentujący pytanie oraz ilość twierdzących odpowiedzi jaka wpłyneła na nie do tej pory.
    /// </summary>
    public class Score
    {
        public int QuestionId { get; set; }
        public string Content { get; set; }
        public int Result { get; set; }

    }

    /// <summary>
    /// Obiekt reprezentujący ankietę wraz z listą pytań.
    /// </summary>
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