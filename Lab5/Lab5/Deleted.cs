using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using Lab5;

class Deleted
{
    public void deld(int id)
    {
        SqlConnection sqlConn = new SqlConnection(Program.connectionString);
        SqlCommand sqlComm = new SqlCommand();
        sqlComm = sqlConn.CreateCommand();
        sqlComm.CommandText = @"DELETE FROM Employees WHERE id=@id";
        sqlComm.Parameters.Add("@id", SqlDbType.Int);
        sqlComm.Parameters["@id"].Value = id;
        try
        {
            sqlConn.Open();
            sqlComm.ExecuteNonQuery();
            sqlConn.Close();
        }
        catch (SqlException ex)
        {
            Console.WriteLine("Delete Error");
        }
    }
}
    
