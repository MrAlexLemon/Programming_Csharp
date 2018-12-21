using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using Lab5;

class Updated
{
    public void Upd(int id, string name)
    {
        string st = "UPDATE Airline SET name = @name WHERE id = @id";
        //string query = "UPDATE Input SET X = @Xi WHERE Id=@id";//Запит до бази даних - поміняти значення за вказаним id

        using (SqlConnection con = new SqlConnection(Program.connectionString))
        {
            using (SqlCommand command = new SqlCommand(st, con))
            {
                try
                {
                    con.Open();
                    command.Parameters.AddWithValue("@id", id);
                    command.Parameters.AddWithValue("@name", name);
                    command.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    Console.WriteLine("Update Error");
                }
                //con.Close();
            }
        }



    }

    public void testupd()
    {
        SqlConnection sqlConn = new SqlConnection(Program.connectionString);
        SqlCommand sqlComm = new SqlCommand();
        sqlComm = sqlConn.CreateCommand();
        sqlComm.CommandText = @"UPDATE Airline SET name=@name WHERE id=@id";
        sqlComm.Parameters.Add("@name", SqlDbType.Text);
        sqlComm.Parameters["@name"].Value = "Lol";
        sqlComm.Parameters.Add("@id", SqlDbType.Int);
        sqlComm.Parameters["@id"].Value = 1;
        sqlConn.Open();
        sqlComm.ExecuteNonQuery();
        sqlConn.Close();
    }
}

