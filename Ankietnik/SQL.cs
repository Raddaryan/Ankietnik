using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Ankietnik
{
    internal static class SQL
    {
        internal  const string Select = "SELECT ";
        internal const string Insert = "INSERT ";
        internal const string Update = "UPDATE ";
        internal const string Delete = "DELETE ";

        internal const string From = "FROM ";
        internal const string Where = "WHERE ";

        internal const string Space = " ";

        internal enum LogicOperator { Less, LessOrEqual, Equal, NotEqual, GreaterOrEqual, Greater }
        internal static readonly Dictionary<LogicOperator, string> LogicOperators = new Dictionary<LogicOperator, string>()
        {
            { LogicOperator.Less, "<" },
            { LogicOperator.LessOrEqual, "<=" },
            { LogicOperator.Equal, "=" },
            { LogicOperator.NotEqual, "!=" },
            { LogicOperator.GreaterOrEqual, ">=" },
            { LogicOperator.Greater, ">" },
        };

        internal static string FieldList(IList<string> fieldNames)
        {
            var sb = new StringBuilder();
            foreach (var field in fieldNames)
            {
                sb.Append(
                    fieldNames.IndexOf(field) < fieldNames.Count - 1 
                        ? $"{field}{Constants.LIST_SEPARATOR} "
                        : field
                );
            }

            return sb.ToString();
        }

        internal static string LogicComparison(string leftOperand, string rightOperand, LogicOperator _operator)
        {
            return $"{leftOperand} {LogicOperators[_operator]} {rightOperand} ";
        }

    }
}