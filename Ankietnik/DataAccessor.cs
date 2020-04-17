using System.Data;
using System.Data.SqlClient;

namespace Ankietnik
{
    internal class DataAccessor
    {
        private SqlConnection _conn;
        private DataAccessor _instance;
        
        internal DataAccessor Instance
        {
            get => _instance ?? new DataAccessor();
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


        private DataAccessor()
        {
        }

        ~DataAccessor()
        {
            if (Conn.State == ConnectionState.Open) 
                Conn.Close();
        }
    }
}