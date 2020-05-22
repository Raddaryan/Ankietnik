using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Ankietnik
{
    /// <summary>
    /// Klasa zawirająca metody pozwalające na dostęp do danych związanych z ankietami i pytaniami.
    /// </summary>
    public static class QuestionService
    {
        /// <summary>
        /// Zwraca obiekt ankiety (Questionnaire) dla podanego ID.
        /// </summary>
        internal static Questionnaire GetQuestionnaire(int questId)
        {
            var queryBuilder = new StringBuilder();
            queryBuilder.Append(
                $"{SQL.Select}{Constants.QUEST_QUESTID_FIELD}, {SQL.QuestionnaireFieldList}" +
                $"{SQL.From} {Constants.QUEST_TABLE_NAME} {SQL.Where} " +
                SQL.SingleCriteria(new SQL.LogicComparison()
                {
                    LeftOperand = Constants.QUEST_QUESTID_FIELD,
                    RightOperand = questId,
                    Operator = SQL.LogicOperator.Equal
                })
            );

            try
            {
                var dataAccessor = DataAccess.Instance;
                var userDataTable = dataAccessor.GetDataTableFromQuery(queryBuilder.ToString());
                var dataRow = userDataTable?.Rows.Count > 0 ? userDataTable.Rows[0] : null;

                if (dataRow == null)
                {
                    return null;
                }
                else
                {
                    return new Questionnaire()
                    {
                        Id = int.Parse(dataRow[Constants.QUEST_QUESTID_FIELD].ToString()),
                        OwnerId = int.Parse(dataRow[Constants.QUEST_OWNERID_FIELD].ToString()),
                        GroupId = int.Parse(dataRow[Constants.QUEST_GROUPID_FIELD].ToString()),
                        Questions = GetQuestions(int.Parse(dataRow[Constants.QUEST_QUESTID_FIELD].ToString()))
                    };
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// Zwraca listę pytań należących do ankiety o podanym ID.
        /// </summary>
        internal static List<Question> GetQuestions(int questId)
        {
            var questions = new List<Question>();
            var queryBuilder = new StringBuilder();
            queryBuilder.Append(
                SQL.Select + SQL.QuestionFieldList +
                $"{SQL.From} {Constants.QUESTIONS_TABLE_NAME} {SQL.Where} " +
                SQL.SingleCriteria(new SQL.LogicComparison()
                {
                    LeftOperand = Constants.QUESTIONS_PARENTID_FIELD,
                    RightOperand = questId,
                    Operator = SQL.LogicOperator.Equal
                })
            );

            try
            {
                var dataAccessor = DataAccess.Instance;
                var userDataTable = dataAccessor.GetDataTableFromQuery(queryBuilder.ToString());
                var dataRows = userDataTable?.Rows.Count > 0 ? userDataTable.Rows : null;

                if (dataRows == null)
                {
                    return null;
                }
                else
                {
                    foreach (DataRow row in dataRows)
                    {
                        questions.Add(new Question()
                        {
                            Id = int.Parse(row[Constants.QUESTIONS_QUESTIONID_FIELD].ToString()),
                            Content = row[Constants.QUESTIONS_CONTENT_FIELD].ToString()
                        });
                    }

                    return questions;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// Zwraca listę ankiet należących do grupy o podanym ID.
        /// </summary>
        internal static List<Questionnaire> GetQuestionnairesForGroup(int groupId)
        {
            var questionnaires = new List<Questionnaire>();
            var queryBuilder = new StringBuilder();
            queryBuilder.Append(
                $"{SQL.Select}{Constants.QUEST_QUESTID_FIELD}, {SQL.QuestionnaireFieldList}" +
                $"{SQL.From} {Constants.QUEST_TABLE_NAME} {SQL.Where} " +
                SQL.SingleCriteria(new SQL.LogicComparison()
                {
                    LeftOperand = Constants.QUEST_GROUPID_FIELD,
                    RightOperand = groupId,
                    Operator = SQL.LogicOperator.Equal
                })
            );

            try
            {
                var dataAccessor = DataAccess.Instance;
                var userDataTable = dataAccessor.GetDataTableFromQuery(queryBuilder.ToString());
                var dataRows = userDataTable?.Rows.Count > 0 ? userDataTable.Rows : null;

                if (dataRows == null)
                {
                    return null;
                }
                else
                {
                    foreach (DataRow row in dataRows)
                    {
                        questionnaires.Add(new Questionnaire()
                        {
                            Id = int.Parse(row[Constants.QUEST_QUESTID_FIELD].ToString()),
                            OwnerId = int.Parse(row[Constants.QUEST_OWNERID_FIELD].ToString()),
                            GroupId = groupId
                        });
                    }

                    return questionnaires;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// Zwraca listę ankiet, które nadal oczekują na odpowiedż użytkownika o podanym ID.
        /// </summary>
        internal static List<Questionnaire> GetPendingQuestionnairesForUser(int userId)
        {
            var questionnaires = new List<Questionnaire>();
            var queryBuilder = new StringBuilder();
            queryBuilder.Append(
                $"{SQL.Select}{Constants.QUEST_QUESTID_FIELD}, {SQL.QuestionnaireFieldList}" +
                $"{SQL.From} {Constants.QUEST_TABLE_NAME} {SQL.Where} " +
                $"{Constants.QUEST_QUESTID_FIELD} {SQL.In} (" +
                    SQL.Select + Constants.QUEST_QUESTID_FIELD +
                    $" {SQL.From} {Constants.PENDING_TABLE_NAME} {SQL.Where} " +
                    SQL.SingleCriteria(new SQL.LogicComparison()
                    {
                        LeftOperand = Constants.USERS_USERID_FIELD,
                        RightOperand = userId,
                        Operator = SQL.LogicOperator.Equal
                    }) + ")"
            );

            try
            {
                var dataAccessor = DataAccess.Instance;
                var userDataTable = dataAccessor.GetDataTableFromQuery(queryBuilder.ToString());
                var dataRows = userDataTable?.Rows.Count > 0 ? userDataTable.Rows : null;

                if (dataRows == null)
                {
                    return null;
                }
                else
                {
                    foreach (DataRow row in dataRows)
                    {
                        questionnaires.Add(new Questionnaire()
                        {
                            Id = int.Parse(row[Constants.QUEST_QUESTID_FIELD].ToString()),
                            OwnerId = int.Parse(row[Constants.QUEST_OWNERID_FIELD].ToString()),
                            GroupId = int.Parse(row[Constants.QUEST_GROUPID_FIELD].ToString())
                        });
                    }

                    return questionnaires;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// Zwraca listę ankiet, które nadal oczekują na odpowiedż użytkownika o podanej nazwie.
        /// </summary>
        internal static List<Questionnaire> GetPendingQuestionnairesForUser(string userName)
        {
            return GetPendingQuestionnairesForUser(AccountService.GetUser(userName).Id);
        }

        /// <summary>
        /// Zwraca listę ankiet, które nadal oczekują na odpowiedż danego użytkownika.
        /// </summary>
        internal static List<Questionnaire> GetPendingQuestionnairesForUser(User user)
        {
            return GetPendingQuestionnairesForUser(user.Id);
        }

        /// <summary>
        /// Zwraca listę ankiet, które zostały już wypełnione przez użytkownika o podanym ID.
        /// </summary>
        internal static List<Questionnaire> GetCompletedQuestionnairesForUser(int userId)
        {
            var questionnaires = new List<Questionnaire>();
            var queryBuilder = new StringBuilder();
            queryBuilder.Append(
                $"{SQL.Select}{Constants.QUEST_QUESTID_FIELD}, {SQL.QuestionnaireFieldList}" +
                $"{SQL.From} {Constants.QUEST_TABLE_NAME} {SQL.Where} " +
                $"{Constants.QUEST_QUESTID_FIELD} {SQL.Not} {SQL.In} (" +
                    SQL.Select + Constants.QUEST_QUESTID_FIELD +
                    $" {SQL.From} {Constants.PENDING_TABLE_NAME} {SQL.Where} " +
                    SQL.SingleCriteria(new SQL.LogicComparison()
                    {
                        LeftOperand = Constants.USERS_USERID_FIELD,
                        RightOperand = userId,
                        Operator = SQL.LogicOperator.Equal
                    }) + ")"
            );

            try
            {
                var dataAccessor = DataAccess.Instance;
                var userDataTable = dataAccessor.GetDataTableFromQuery(queryBuilder.ToString());
                var dataRows = userDataTable?.Rows.Count > 0 ? userDataTable.Rows : null;

                if (dataRows == null)
                {
                    return null;
                }
                else
                {
                    foreach (DataRow row in dataRows)
                    {
                        questionnaires.Add(new Questionnaire()
                        {
                            Id = int.Parse(row[Constants.QUEST_QUESTID_FIELD].ToString()),
                            OwnerId = int.Parse(row[Constants.QUEST_OWNERID_FIELD].ToString()),
                            GroupId = int.Parse(row[Constants.QUEST_GROUPID_FIELD].ToString())
                        });
                    }

                    return questionnaires;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// Zwraca listę ankiet, które zostały już wypełnione przez użytkownika o podanej nazwie.
        /// </summary>
        internal static List<Questionnaire> GetCompletedQuestionnairesForUser(string userName)
        {
            return GetCompletedQuestionnairesForUser(AccountService.GetUser(userName).Id);
        }

        /// <summary>
        /// Zwraca listę ankiet, które zostały już wypełnione przez danego użytkownika.
        /// </summary>
        internal static List<Questionnaire> GetCompletedQuestionnairesForUser(User user)
        {
            return GetCompletedQuestionnairesForUser(user.Id);
        }

        /// <summary>
        /// Dodaje ankietę do tabeli 'Questionnaires', dodaje listę pytań do tabeli 'Questions' 
        /// oraz dodaje wpisy do tabeli 'Pending' dla wszystkich użytkowników należących do przypisanej grupy.
        /// </summary>
        internal static OperationResult CreateQuestionnaire(Questionnaire quest)
        {
            var result = new OperationResult();
            var queryBuilder = new StringBuilder();
            try
            {
                // add questionnaire to db
                queryBuilder.Append(
                $"{SQL.Insert}{Constants.QUEST_TABLE_NAME} ({SQL.QuestionnaireFieldList}) {SQL.Values} (" +
                SQL.ValuesList(new List<object>() {
                        quest.OwnerId,
                        quest.GroupId
                    }) + ")"
                );

                var dataAccessor = DataAccess.Instance;
                dataAccessor.ExecuteSqlQuery(queryBuilder.ToString());
                result.Status = OperationStatus.Success;
                result.Message = Constants.RegistrationSuccessMsg;

                // add all questions to db
                queryBuilder.Clear();
                queryBuilder.Append($"{SQL.Insert}{Constants.QUESTIONS_TABLE_NAME} ({SQL.QuestionFieldList}) {SQL.Values} ");
                    
                foreach (var question in quest.Questions)
                {
                    queryBuilder.Append(
                        "(" + SQL.ValuesList(new List<object>() {
                            question.Id,
                            quest.Id,
                            question.Content
                         }) + ")"
                    );

                    if (quest.Questions.IndexOf(question) != quest.Questions.Count - 1)
                        queryBuilder.Append(", ");
                }

                // add questionnaire to the pending table for all the users from the target group
                var userIds = AccountService.GetUserIdsForGroup(quest.GroupId);

                if (userIds != null)
                {
                    queryBuilder.Clear();
                    queryBuilder.Append(
                        $"{SQL.Insert}{Constants.PENDING_TABLE_NAME} (" +
                        $"{SQL.ValuesList(new List<object>() { Constants.QUEST_QUESTID_FIELD, Constants.USERS_USERID_FIELD})}) {SQL.Values}"
                    );

                    foreach (var userId in userIds)
                    {
                        queryBuilder.Append($"({quest.Id}, {userId})");
                        if (userIds.IndexOf(userId) != userIds.Count - 1)
                            queryBuilder.Append(", ");
                    }
                }

                result.Status = OperationStatus.Success;
                result.Message = Constants.CreateQuestionnaireSuccessMsg;
            } 
            catch (Exception ex)
            {
                result.Status = OperationStatus.Failed;
                result.Message = Constants.CreateQuestionnaireErrorMsg;
            }

            return result;
        }

        /// <summary>
        /// Metoda pomocnicza zwracająca ArrayList numerów ID dla podanej listy ankiet. Używana do powiązania z DropDownList w 'Main', 'MainOwner', 'Check' i 'CheckOwner'
        /// </summary>
        internal static ArrayList GetArrayListOfIds(List<Questionnaire> quests)
        {
            var list = new ArrayList();
            if (quests != null && quests.Count != 0)
            {
                foreach (var quest in quests)
                {
                    list.Add(quest.Id);
                }
            }

            return list;
        }

        /// <summary>
        /// Zapisuje odpowiedzi użytkownika na bazie danych podpisane zaszyfrowanym podpisem po uprzedniej weryfikacji hasłem.
        /// </summary>
        internal static OperationResult SubmitResponse(int questId, List<Response> responses, string userName, string passCode)
        {
            if (responses == null || responses.Count == 0)
            {
                return new OperationResult() {
                    Status = OperationStatus.Failed,
                    Message = Constants.EmptyResponseListMsg
                };
            }

            var user = AccountService.GetUser(userName);
            if (!CryptoService.VerifyPassword(passCode, user.Password))
            {
                return new OperationResult()
                {
                    Status = OperationStatus.Failed,
                    Message = Constants.IncorrectPasswordMsg
                };
            }

            var signature = CryptoService.GenerateSignature(userName, passCode);

            var queryBuilder = new StringBuilder();
            queryBuilder.Append(
                $"{SQL.Insert}{Constants.ANSWERS_TABLE_NAME} ({SQL.AnswersFieldList}) {SQL.Values} "
            );

            foreach (var response in responses)
            {
                queryBuilder.Append(
                    $"({SQL.ValuesList(new List<object>() { questId, response.QuestionId, response.Content, signature })})" +
                    (responses.IndexOf(response) == responses.Count - 1 ? String.Empty : ", ")
                );
            }

            try
            {
                var dataAccessor = DataAccess.Instance;
                dataAccessor.ExecuteSqlQuery(queryBuilder.ToString());

                return CompleteQuestionnaire(questId, userName);
            }
            catch (Exception ex)
            {
                return new OperationResult() {
                    Status = OperationStatus.Failed,
                    Message = ex.Message
                };
            }
        }

        /// <summary>
        /// Sprawdza czy odpowiedzi do podanej ankiety zapisane w bazie danych nie zostały usunięte lub zmodyfikowane.
        /// </summary>
        internal static OperationResult VerifyResponse(int questId, string userName, string passCode)
        {
            var user = AccountService.GetUser(userName);
            if (!CryptoService.VerifyPassword(passCode, user.Password))
            {
                return new OperationResult()
                {
                    Status = OperationStatus.Failed,
                    Message = Constants.IncorrectPasswordMsg
                };
            }     

            try
            {
                var answers = (List<Response>)GetAnswers(questId, userName, passCode).Payload;
                var questions = GetQuestions(questId);

                if (answers.Count == questions.Count)
                {
                    return new OperationResult()
                    {
                        Status = OperationStatus.Success,
                        Message = Constants.VerificationSuccessMsg
                    };
                } 
                else
                {
                    return new OperationResult()
                    {
                        Status = OperationStatus.Failed,
                        Message = Constants.VerificationFailedMsg
                    };
                }
            }
            catch (Exception ex)
            {
                return new OperationResult()
                {
                    Status = OperationStatus.Failed,
                    Message = ex.Message
                };
            }

        }

        /// <summary>
        /// Zwraca listę odpowiedzi na podaną ankietę przesłane przez podanego użytkownika, po uprzedniej weryfikacji hasłem.
        /// </summary>
        internal static OperationResult GetAnswers(int questId, string userName, string passCode)
        {
            var user = AccountService.GetUser(userName);
            if (!CryptoService.VerifyPassword(passCode, user.Password))
            {
                return new OperationResult()
                {
                    Status = OperationStatus.Failed,
                    Message = Constants.IncorrectPasswordMsg
                };
            }

            var signature = CryptoService.GenerateSignature(userName, passCode);
            var questCompare = new SQL.LogicComparison() { LeftOperand = $"A.{Constants.QUEST_QUESTID_FIELD}", RightOperand = questId, Operator = SQL.LogicOperator.Equal };
            var keyCompare = new SQL.LogicComparison() { LeftOperand = $"A.{Constants.ANSWERS_SIGNATURE_FIELD}", RightOperand = signature, Operator = SQL.LogicOperator.Equal };

            var queryBuilder = new StringBuilder();
            queryBuilder.Append(
                $"{SQL.Select} " +
                $"A.{Constants.QUESTIONS_QUESTIONID_FIELD}, " +
                $"A.{Constants.QUEST_QUESTID_FIELD}, " +
                $"B.{Constants.QUESTIONS_CONTENT_FIELD}, " +
                $"A.{Constants.ANSWERS_SIGNATURE_FIELD}, " +
                $"A.{Constants.ANSWERS_ANSWER_FIELD} " +
                $"{SQL.From} {Constants.ANSWERS_TABLE_NAME} A {SQL.Join} {Constants.QUESTIONS_TABLE_NAME} B {SQL.On} " +
                $"A.{Constants.QUESTIONS_QUESTIONID_FIELD} = B.{Constants.QUESTIONS_QUESTIONID_FIELD} {SQL.Where} " +
                    SQL.MultipleCriteria(new Dictionary<SQL.LogicComparison, SQL.CriteriaConnector>() {
                        { questCompare, SQL.CriteriaConnector.AND },
                        { keyCompare, SQL.CriteriaConnector.NULL }
                    })
            );

            try
            {
                var dataAccessor = DataAccess.Instance;
                var answersTable = dataAccessor.GetDataTableFromQuery(queryBuilder.ToString());
                var dataRows = answersTable?.Rows.Count > 0 ? answersTable.Rows : null;

                if (dataRows == null)
                {
                    return new OperationResult()
                    {
                        Status = OperationStatus.Failed,
                        Message = Constants.GetAnswersFailedMsg
                    };
                }
                else
                {
                    var answers = new List<Answer>();
                    foreach (DataRow row in dataRows)
                    {
                        answers.Add(new Answer()
                        {
                            QuestionId = int.Parse(row[Constants.QUESTIONS_QUESTIONID_FIELD].ToString()),
                            Content = row[Constants.QUESTIONS_CONTENT_FIELD].ToString(),
                            Response = int.Parse(row[Constants.ANSWERS_ANSWER_FIELD].ToString()) == 0 ? false : true
                        });
                    }

                    return new OperationResult()
                    {
                        Status = OperationStatus.Success,
                        Message = string.Empty,
                        Payload = answers
                    };
                }
            }
            catch (Exception ex)
            {
                return new OperationResult()
                {
                    Status = OperationStatus.Failed,
                    Message = ex.Message
                };
            }
        }

        /// <summary>
        /// Zwraca listę ankiet stworzonych przez podanego użytkownika z rolą własciciela.
        /// </summary>
        internal static List<Questionnaire> GetQuestionnairesForOwner(string username)
        {
            var questionnaires = new List<Questionnaire>();
            var queryBuilder = new StringBuilder();
            queryBuilder.Append(
                $"{SQL.Select}{Constants.QUEST_QUESTID_FIELD}, {SQL.QuestionnaireFieldList}" +
                $"{SQL.From} {Constants.QUEST_TABLE_NAME} {SQL.Where} " +
                    SQL.SingleCriteria(new SQL.LogicComparison()
                    {
                        LeftOperand = Constants.QUEST_OWNERID_FIELD,
                        RightOperand = AccountService.GetUser(username).Id,
                        Operator = SQL.LogicOperator.Equal
                    })
            );

            try
            {
                var dataAccessor = DataAccess.Instance;
                var userDataTable = dataAccessor.GetDataTableFromQuery(queryBuilder.ToString());
                var dataRows = userDataTable?.Rows.Count > 0 ? userDataTable.Rows : null;

                if (dataRows == null)
                {
                    return null;
                }
                else
                {
                    foreach (DataRow row in dataRows)
                    {
                        questionnaires.Add(new Questionnaire()
                        {
                            Id = int.Parse(row[Constants.QUEST_QUESTID_FIELD].ToString()),
                            OwnerId = int.Parse(row[Constants.QUEST_OWNERID_FIELD].ToString()),
                            GroupId = int.Parse(row[Constants.QUEST_GROUPID_FIELD].ToString())
                        });
                    }

                    return questionnaires;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// Zwraca liczbę osób, które dotychczas udzieliły odpowieczi na podaną ankietę.
        /// </summary>
        internal static int GetNumberOfCompletedForQuest(int questId)
        {
            return GetTotalNumberForQuest(questId) - GetNumberOfPendingForQuest(questId);
        }

        /// <summary>
        /// Zwraca listę nazw użytkowników, którzy dotychczas nie udzielili odpowiedzi na podaną ankietę.
        /// </summary>
        internal static string GetListOfUsersPendingForQuest(int questId)
        {
            var queryBuilder = new StringBuilder();
            queryBuilder.Append(
                $"{SQL.Select} B.{Constants.USERS_USERNAME_FIELD} " +
                $"{SQL.From} {Constants.PENDING_TABLE_NAME} A {SQL.Join} {Constants.USERS_TABLE_NAME} B {SQL.On} " +
                $"A.{Constants.USERS_USERID_FIELD} = B.{Constants.USERS_USERID_FIELD} {SQL.Where} " +
                    SQL.SingleCriteria(new SQL.LogicComparison()
                    {
                        LeftOperand = $"A.{Constants.QUEST_QUESTID_FIELD}",
                        RightOperand = questId,
                        Operator = SQL.LogicOperator.Equal
                    })
            );

            try
            {
                var dataAccessor = DataAccess.Instance;
                var answersTable = dataAccessor.GetDataTableFromQuery(queryBuilder.ToString());
                var dataRows = answersTable?.Rows.Count > 0 ? answersTable.Rows : null;

                if (dataRows == null)
                {
                    return string.Empty;
                }
                else
                {
                    var userList = new StringBuilder();
                    foreach (DataRow row in dataRows)
                    {
                        userList.Append($"{row[0]}{(dataRows.IndexOf(row) == dataRows.Count - 1 ? string.Empty : ", ")}");
                    }

                    return userList.ToString();
                }
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Zwraca listę pytań należących do podanej ankiety wraz z wynikiem (ilość odpowiedzi pozytywnych).
        /// </summary>
        internal static List<Score> GetScoresForQuest(int questId)
        {
            var queryBuilder = new StringBuilder();
            queryBuilder.Append(
                $"{SQL.Select} " +
                $"A.{Constants.QUESTIONS_QUESTIONID_FIELD}, " +
                $"B.{Constants.QUESTIONS_CONTENT_FIELD}, " +
                $"SUM({Constants.ANSWERS_ANSWER_FIELD}) as {Constants.SCORE_FIELD} " +
                $"{SQL.From} {Constants.ANSWERS_TABLE_NAME} A {SQL.Join} {Constants.QUESTIONS_TABLE_NAME} B {SQL.On} " +
                $"A.{Constants.QUESTIONS_QUESTIONID_FIELD} = B.{Constants.QUESTIONS_QUESTIONID_FIELD} {SQL.Where} " +
                    SQL.SingleCriteria(new SQL.LogicComparison()
                    {
                        LeftOperand = $"A.{Constants.QUEST_QUESTID_FIELD}",
                        RightOperand = questId,
                        Operator = SQL.LogicOperator.Equal
                    }) +
                $" {SQL.GroupBy} A.{Constants.QUESTIONS_QUESTIONID_FIELD}, B.{Constants.QUESTIONS_CONTENT_FIELD}"
            );

            try
            {
                var dataAccessor = DataAccess.Instance;
                var answersTable = dataAccessor.GetDataTableFromQuery(queryBuilder.ToString());
                var dataRows = answersTable?.Rows.Count > 0 ? answersTable.Rows : null;

                var scores = new List<Score>();
                if (dataRows == null)
                {
                    return new List<Score>();
                }
                else
                {
                    foreach (DataRow row in dataRows)
                    {
                        scores.Add(new Score()
                        {
                            QuestionId = int.Parse(row[Constants.QUESTIONS_QUESTIONID_FIELD].ToString()),
                            Content = row[Constants.QUESTIONS_CONTENT_FIELD].ToString(),
                            Result = int.Parse(row[Constants.SCORE_FIELD].ToString())
                        });
                    }
                }

                return scores;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// Pomocnicza metoda zwracająca liczbę osób, które dotychczas nie udzieliły odpowiedzi na podaną ankietę.
        /// Używana do obliczania liczby osób zwracanej w 'GetNumberOfCompletedForQuest'.
        /// </summary>
        private static int GetNumberOfPendingForQuest(int questId)
        {
            var queryBuilder = new StringBuilder();
            queryBuilder.Append(
                $"{SQL.Select} {SQL.Count} {SQL.From} {Constants.PENDING_TABLE_NAME} {SQL.Where} " +
                    SQL.SingleCriteria(new SQL.LogicComparison()
                    {
                        LeftOperand = Constants.QUEST_QUESTID_FIELD,
                        RightOperand = questId,
                        Operator = SQL.LogicOperator.Equal
                    })
            );

            try
            {
                var dataAccessor = DataAccess.Instance;
                var result = dataAccessor.ExecuteScalar(queryBuilder.ToString());
                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// Pomocnicza metoda zwracająca całkowitą liczbę osób, do których podana ankieta została przypisana.
        /// Używana do obliczania liczby osób zwracanej w 'GetNumberOfCompletedForQuest'.
        /// </summary>
        private static int GetTotalNumberForQuest(int questId)
        {
            var groupId = GetQuestionnaire(questId).GroupId;
            var queryBuilder = new StringBuilder();
            queryBuilder.Append(
                $"{SQL.Select} {SQL.Count} {SQL.From} {Constants.USERS_TABLE_NAME} {SQL.Where} " +
                    SQL.SingleCriteria(new SQL.LogicComparison()
                    {
                        LeftOperand = Constants.USERS_GROUP_FIELD,
                        RightOperand = groupId,
                        Operator = SQL.LogicOperator.Equal
                    })
            );

            try
            {
                var dataAccessor = DataAccess.Instance;
                var result = dataAccessor.ExecuteScalar(queryBuilder.ToString());
                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// Pomocnicza metoda sprawdzająca czy użytkownik o podanej nazwiejest właścicielem podanej ankiety.
        /// </summary>
        private static bool VerifyOwner(int questId, string username)
        {
            var user = AccountService.GetUser(username);
            return user.Role == Constants.Roles[Constants.Role.Owner] &&
                   GetQuestionnaire(questId).OwnerId == user.Id;
        }

        /// <summary>
        /// Pomocnicza metoda wywoływana w 'SubmitResponse' usuwająca wpis dla podanej ankiety i podanego użytkownika z tabeli 'Pending'.
        /// </summary>
        private static OperationResult CompleteQuestionnaire(int questId, string userName)
        {
            var userId = AccountService.GetUser(userName).Id;
            var queryBuilder = new StringBuilder();
            var userCompare = new SQL.LogicComparison() { LeftOperand = Constants.USERS_USERID_FIELD, RightOperand = userId, Operator = SQL.LogicOperator.Equal };
            var questCompare = new SQL.LogicComparison() { LeftOperand = Constants.QUEST_QUESTID_FIELD, RightOperand = questId, Operator = SQL.LogicOperator.Equal };
            queryBuilder.Append(
                $"{SQL.Delete}{Constants.PENDING_TABLE_NAME} {SQL.Where}" +
                SQL.MultipleCriteria(new Dictionary<SQL.LogicComparison, SQL.CriteriaConnector>()
                {
                    { userCompare, SQL.CriteriaConnector.AND },
                    { questCompare, SQL.CriteriaConnector.NULL }
                })
            );

            try
            {
                var dataAccessor = DataAccess.Instance;
                dataAccessor.ExecuteSqlQuery(queryBuilder.ToString());

                return new OperationResult()
                {
                    Status = OperationStatus.Success,
                    Message = Constants.ResponseSubmittedMsg
                };
            }
            catch (Exception ex)
            {
                return new OperationResult()
                {
                    Status = OperationStatus.Failed,
                    Message = ex.Message
                };
            }
        }

    }
}