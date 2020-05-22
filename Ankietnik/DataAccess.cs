using System.Data;
using System.Data.SqlClient;

namespace Ankietnik
{
    /// <summary>
    /// Klasa pomocnicza zawierające generyczne metody komunikacji z bazą danych (wykonywanie zapytań, łączenie z bazą itp.).
    /// </summary>
    internal class DataAccess
    {
        private SqlConnection _conn;
        private static DataAccess _instance;
        
        internal static DataAccess Instance
        {
            get
            {
                if (_instance != null)
                    return _instance;

                return _instance = new DataAccess();
            }
        }

        internal SqlConnection Conn
        {
            get
            {
                if (_conn != null)
                    return _conn;

                return _conn = new SqlConnection(Constants.CONN_STRING);
            }
        }

        internal DataTable GetDataTableFromQuery(string sqlQuery)
        {
            var dataSet = new DataSet();
            var cmd = new SqlCommand(sqlQuery, Conn);
            Conn.Open();

            using (var dataAdapter = new SqlDataAdapter(cmd))
            {
                dataAdapter.Fill(dataSet);
            }

            Conn.Close();
            return dataSet.Tables[0];
        }

        internal int ExecuteSqlQuery(string sqlQuery)
        {
            var cmd = new SqlCommand(sqlQuery, Conn);
            Conn.Open();

            var result = cmd.ExecuteNonQuery();
            Conn.Close();

            return result;
        }

        internal int ExecuteScalar(string sqlQuery)
        {
            var cmd = new SqlCommand(sqlQuery, Conn);
            Conn.Open();

            var result = cmd.ExecuteScalar();
            Conn.Close();

            return (int)result;
        }


        private DataAccess()
        {
        }

        ~DataAccess()
        {
            if (Conn.State == ConnectionState.Open) 
                Conn.Close();
        }
    }
}