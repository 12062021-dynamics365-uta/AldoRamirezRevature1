using System;
using Models;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using System.Data.Common;

namespace Storage
{
    public class DataBaseAccess : IDataBaseAccess
    {
        // in readl life you dont want to keep your Cnn String here.... 
        // it will be pushed t our GitHub and anyone could see it.
        private readonly string str = "Data source = ALDITONE-DESKTO\\SQLEXPRESS; initial Catalog=RPSGameDB; integrated security = true";
        private readonly SqlConnection _con;

        //constructor
        public DataBaseAccess()
        {
            this._con = new SqlConnection(this.str);
            _con.Open();
        }

        public List<Player> GetAllPlayers()
        {
            string sqlQuery = "SELECT * FROM Players;";
            List<Player> players = new List<Player>();
            using (SqlCommand cmd = new SqlCommand(sqlQuery, this._con))
            {
                SqlDataReader dr = cmd.ExecuteReader();
                //players = this._mapper.EntityToPlayerList(dr);
                this._con.Close();// make sure this class is Transient... not songleton or Scoped.
            }
            return players;
        }

        public async Task<DataTableReader> LoginAsync(string fname, string lname)
        {
            string sqlQuery = "SELECT TOP 1 * FROM Players WHERE FirstName = @fname and LastName = @lname;";


            using (SqlCommand cmd = new SqlCommand(sqlQuery, this._con))
            {
                cmd.Parameters.AddWithValue("@fname", fname);
                cmd.Parameters.AddWithValue("@lname", lname);

                SqlDataReader dr = await cmd.ExecuteReaderAsync();

                DataTable table = new DataTable("Players");
                table.Load(dr);
                DataTableReader dtr = table.CreateDataReader();

                //this._con.Close();// make sure this class is Transient... not songleton or Scoped.
                dr.Close();
                return dtr;
            }
        }

        public async Task<Player> RegisterNewPlayerAsync(string fname, string lname)
        {
            // call the async method in ADO.NET to post that player
            string sqlQuery = "INSERT INTO Players (FirstName, LastName, Losses, Wins) VALUES (@fname, @lname, 0, 0);";

            using (SqlCommand cmd = new SqlCommand(sqlQuery, this._con))
            {
                cmd.Parameters.AddWithValue("@fname", fname);
                cmd.Parameters.AddWithValue("@lname", lname);

                try
                {
                    int dr = await cmd.ExecuteNonQueryAsync();
                    return new Player() { Fname = fname, Lname = lname, Losses = 0, Wins = 0};
                } 
                catch (DbException ex)
                {
                    return null;
                }
            }
        }
    }
}
