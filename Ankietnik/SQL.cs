﻿using System.Collections;
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

        internal const string Space = " ";

        internal enum LogicOperator { Less, LessOrEqual, Equal, NotEqual, GreaterOrEqual, Greater }
        internal enum CriteriaConnector { AND, OR }

        internal static readonly Dictionary<LogicOperator, string> LogicOperators = new Dictionary<LogicOperator, string>()
        {
            { LogicOperator.Less, "<" },
            { LogicOperator.LessOrEqual, "<=" },
            { LogicOperator.Equal, "=" },
            { LogicOperator.NotEqual, "!=" },
            { LogicOperator.GreaterOrEqual, ">=" },
            { LogicOperator.Greater, ">" },
        };

        internal struct LogicComparison
        {
            internal string LeftOperand;
            internal string RightOperand;
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
            return $"{comparison.LeftOperand} {LogicOperators[comparison.Operator]} {comparison.RightOperand} ";
        }

        internal static string MultipleCriteria(Dictionary<LogicComparison, CriteriaConnector?> criteria)
        {
            var sb = new StringBuilder();
            foreach (var kvp in criteria)
            {
                sb.Append($"{SingleCriteria(kvp.Key)}" +
                          $"{(kvp.Value == null ? string.Empty : Space + kvp.Value.ToString() + Space)}");
            }

            return sb.ToString();
        }

    }
}