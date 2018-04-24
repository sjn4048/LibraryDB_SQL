using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace LibraryDB
{
    class DB_Connector
    {
        public SqlConnection Connect()
        {
            string connectString = "Data Source=.;Initial Catalog=Exp5_LibraryDB;Integrated Security=True";
            StringBuilder result = new StringBuilder();

            SqlConnection sqlCnt = new SqlConnection(connectString);
            sqlCnt.Open();
            //SqlCommand command = sqlCnt.CreateCommand();
            //command.CommandType = CommandType.Text;
            //command.CommandText = "SELECT * FROM book_info";
            //SqlDataReader reader = command.ExecuteReader();
            //while (reader.Read()) { result.Append(reader["author"]); }

            return sqlCnt;
        }
    }
}
