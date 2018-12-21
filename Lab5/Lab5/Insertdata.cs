using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using Lab5;

class Insertdata
{

    public void insdat(int id,string name,string local)
    {
        // In a using statement, acquire the SqlConnection as a resource.
        SqlConnection sqlConn = new SqlConnection(Program.connectionString);
        SqlCommand sqlComm = new SqlCommand();
        sqlComm = sqlConn.CreateCommand();
        sqlComm.CommandText = @"INSERT INTO Airport (id,name,location) VALUES (@id,@name,@location)";
        sqlComm.Parameters.Add("@id", SqlDbType.Int);
        sqlComm.Parameters.Add("@name", SqlDbType.Text);
        sqlComm.Parameters.Add("@location", SqlDbType.Text);
        sqlComm.Parameters["@id"].Value = id;
        sqlComm.Parameters["@name"].Value = name;
        sqlComm.Parameters["@location"].Value = local;
        try
        {
            sqlConn.Open();
            sqlComm.ExecuteNonQuery();
            sqlConn.Close();
        }
        catch (SqlException ex)
        {
            Console.WriteLine("Insert Error");
        }
    }
}

