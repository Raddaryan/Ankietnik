using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Ankietnik
{
    internal static class SQL
    {
        internal  const string Select = "SELECT ";
        internal const string Insert = "INSERT INTO ";
        internal const string Update = "UPDATE ";
        internal const string Delete = "DELETE ";

        internal const string From = "FROM ";
        internal const string Where = "WHERE ";
        internal const string Values = "VALUES";
        internal const string In = "IN ";
        internal const string Not = "NOT";
        internal const string Count = "COUNT(*)";
        internal const string Join = "JOIN";
        internal const string On = "ON";

        internal const string Space = " ";

        internal enum LogicOperator { Less, LessOrEqual, Equal, NotEqual, GreaterOrEqual, Greater }
        internal enum CriteriaConnector { AND, OR, NULL }

        internal static readonly Dictionary<LogicOperator, string> LogicOperators = new Dictionary<LogicOperator, string>()
        {
            { LogicOperator.Less, "<" },
            { LogicOperator.LessOrEqual, "<=" },
            { LogicOperator.Equal, "=" },
            { LogicOperator.NotEqual, "!=" },
            { LogicOperator.GreaterOrEqual, ">=" },
            { LogicOperator.Greater, ">" }
        };

        internal struct LogicComparison
        {
            internal string LeftOperand;
            internal object RightOperand;
            internal LogicOperator Operator;
        }

        internal static readonly string UserFieldList = FieldList(new List<string>()
        {
            Constants.USERS_USERNAME_FIELD,
            Constants.USERS_SALT_FIELD,
            Constants.USERS_KEY_FIELD,
            Constants.USERS_ROLE_FIELD,
            Constants.USERS_GROUP_FIELD
        });

        internal static readonly string QuestionnaireFieldList = FieldList(new List<string>()
        {
            Constants.QUEST_OWNERID_FIELD, Constants.QUEST_GROUPID_FIELD
        });

        internal static readonly string QuestionFieldList = FieldList(new List<string>() 
        { 
            Constants.QUESTIONS_QUESTIONID_FIELD, Constants.QUESTIONS_CONTENT_FIELD 
        });

        internal static readonly string AnswersFieldList = FieldList(new List<string>()
        {
            Constants.QUEST_QUESTID_FIELD, 
            Constants.ANSWERS_QUESTIONID_FIELD, 
            Constants.ANSWERS_ANSWER_FIELD, 
            Constants.ANSWERS_SIGNATURE_FIELD, 
            Constants.ANSWERS_SALT_FIELD
        });
            

        internal static string Quotify(string element) => $"'{element}'";

        internal static string ValuesList(IList<object> values)
        {
            var sb = new StringBuilder();
            for (var i = 0; i < values.Count; i++)
            {
                sb.Append((values[i] is string ? Quotify(values[i].ToString()) : values[i].ToString()));
                if (i != values.Count - 1) sb.Append($"{Constants.LIST_SEPARATOR} ");
            }

            return sb.ToString();
        }

        internal static string FieldList(IList<string> fieldNames)
        {
            var sb = new StringBuilder();
            foreach (var field in fieldNames)
            {
                sb.Append(
                    fieldNames.IndexOf(field) < fieldNames.Count - 1 
                        ? $"{field}{Constants.LIST_SEPARATOR} "
                        : $"{field} "
                );
            }

            return sb.ToString();
        }

        internal static string SingleCriteria(LogicComparison comparison)
        {
            return $"{comparison.LeftOperand} {LogicOperators[comparison.Operator]} " +
                   $"{(comparison.RightOperand is string ? Quotify(comparison.RightOperand.ToString()) : comparison.RightOperand)} ";
        }

        internal static string MultipleCriteria(Dictionary<LogicComparison, CriteriaConnector> criteria)
        {
            var sb = new StringBuilder();
            foreach (var kvp in criteria)
            {
                sb.Append($"{SingleCriteria(kvp.Key)}" +
                          $"{(kvp.Value == CriteriaConnector.NULL ? string.Empty : Space + kvp.Value.ToString() + Space)}");
            }

            return sb.ToString();
        }

    }
}