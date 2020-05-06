using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;

namespace Ankietnik
{
    public static class QuestionService
    {
        internal static Questionnaire GetQuestionnaire(int questId)
        {
            var queryBuilder = new StringBuilder();
            queryBuilder.Append(
                SQL.Select + SQL.QuestionnaireFieldList +
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
    }
}