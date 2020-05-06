using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Ankietnik
{
    internal static class AccountService
    {
        internal static OperationResult Login(string userName, string password)
        {
            var result = new OperationResult();
            try
            {
                var user = GetUser(userName);
                if (user == null)
                {
                    result.Status = OperationStatus.Failed;
                    result.Message = Constants.UserNotFoundMsg;
                }
                else
                {
                    if (CryptoService.VerifyPassword(password, user.Password))
                    {
                        result.Status = OperationStatus.Success;
                    }
                    else
                    {
                        result.Status = OperationStatus.Failed;
                        result.Message = Constants.IncorrectCredentialsMsg;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Status = OperationStatus.Failed;
                result.Message = Constants.DataAccessErrorMsg;
            }

            return result;
        }

        internal static bool ComparePassword(string password, string retyped) => password == retyped;

        internal static OperationResult Register(string userName, string password, string retyped, int group)        
        {
            var result = new OperationResult();

            try
            {
                var user = GetUser(userName);

                if (user != null)
                {
                    result.Status = OperationStatus.Failed;
                    result.Message = Constants.UserAlreadyExistsMsg;
                }
                else
                {
                    var queryBuilder = new StringBuilder();
                    var encryptedPassword = CryptoService.EncryptPassword(password);
                    queryBuilder.Append(
                        $"{SQL.Insert}{Constants.USERS_TABLE_NAME} ({SQL.UserFieldList}) {SQL.Values} (" +
                        SQL.ValuesList(new List<object>() {
                            userName,
                            Convert.ToBase64String(encryptedPassword.Salt),
                            Convert.ToBase64String(encryptedPassword.Key),
                            (int)Constants.Role.User,
                            group
                        }) + ")"
                    );

                    var dataAccessor = DataAccess.Instance;
                    dataAccessor.ExecuteSqlQuery(queryBuilder.ToString());
                    result.Status = OperationStatus.Success;
                }
            }
            catch (Exception ex)
            {
                result.Status = OperationStatus.Failed;
                result.Message = Constants.DataAccessErrorMsg;
            }

            return result;
        }

        internal static User GetUser(string userName)
        {
            try
            {
                return GetUser(Constants.USERS_USERNAME_FIELD, userName);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        internal static User GetUser(int userId)
        {
            try
            {
                return GetUser(Constants.USERS_USERID_FIELD, userId);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        internal static List<int> GetGroupIdList()
        {
            var result = new List<int>();
            var queryBuilder = new StringBuilder();
            queryBuilder.Append($"{SQL.Select} {Constants.GROUP_ID_FIELD} {SQL.From} {Constants.GROUPS_TABLE_NAME}");

            try
            {
                var dataAccessor = DataAccess.Instance;
                var groupTable = dataAccessor.GetDataTableFromQuery(queryBuilder.ToString());
                if (groupTable != null && groupTable.Rows.Count > 0)
                {
                    foreach (DataRow row in groupTable.Rows)
                    {
                        result.Add(int.Parse(row[0].ToString()));
                    }
                }
            }
            catch (Exception ex)
            {
                return null;
            }

            return result;
        }

        private static User GetUser(string fieldName, object criteria)
        {
            var queryBuilder = new StringBuilder();
            queryBuilder.Append(
                SQL.Select + SQL.UserFieldList +
                $"{SQL.From} {Constants.USERS_TABLE_NAME} {SQL.Where} " +
                SQL.SingleCriteria(new SQL.LogicComparison()
                {
                    LeftOperand = fieldName,
                    RightOperand = criteria,
                    Operator = SQL.LogicOperator.Equal
                })
            );

            try
            {
                var dataAccessor = DataAccess.Instance;
                var userDataTable = dataAccessor.GetDataTableFromQuery(queryBuilder.ToString());
                var userData = userDataTable?.Rows.Count > 0 ? userDataTable.Rows[0] : null;

                if (userData == null)
                {
                    return null;
                }
                else
                {
                    return new User()
                    {
                        Username = userData[Constants.USERS_USERNAME_FIELD].ToString(),
                        Password = new EncryptedPassword()
                        {
                            Salt = Convert.FromBase64String(userData[Constants.USERS_SALT_FIELD].ToString()),
                            Key = Convert.FromBase64String(userData[Constants.USERS_KEY_FIELD].ToString())
                        },
                        Role = int.Parse(userData[Constants.USERS_ROLE_FIELD].ToString()),
                        Group = int.Parse(userData[Constants.USERS_GROUP_FIELD].ToString())
                    };
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}