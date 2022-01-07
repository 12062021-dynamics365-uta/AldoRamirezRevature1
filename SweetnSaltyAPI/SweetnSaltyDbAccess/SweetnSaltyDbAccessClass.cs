using System;
using System.Data.Common;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace SweetnSaltyDbAccess
{
    public class SweetnSaltyDbAccessClass : ISweetnSaltyDbAccessClass
    {
        private readonly string str = "Data source = ALDITONE-DESKTO\\SQLEXPRESS; initial Catalog=SweetnSaltyDB; integrated security = true";
        private readonly SqlConnection _con;

        //constructor
        public SweetnSaltyDbAccessClass()
        {
            this._con = new SqlConnection(this.str);
            _con.Open();
        }

        public async Task<SqlDataReader> PostFlavor(string flavor)
        {
            string insertQuery = "INSERT INTO Flavor VALUES (@flavor);";
            
            using (SqlCommand cmd = new SqlCommand(insertQuery, this._con))
            {
                cmd.Parameters.AddWithValue("@flavor", flavor);
                try
                {
                    await cmd.ExecuteNonQueryAsync();

                    string retrieveFlavor = "SELECT TOP 1 * FROM Flavor ORDER BY FlavorId DESC;";
                    using (SqlCommand cmd2 = new SqlCommand(retrieveFlavor, this._con))
                    {
                        SqlDataReader dr = await cmd2.ExecuteReaderAsync();
                        return dr;
                    }
                }
                catch (DbException ex)
                {
                    Console.WriteLine($"Exception in DatabaseAccessClass {ex}");
                    return null;
                }
            }
        }

        public async Task<SqlDataReader> PostPerson(string fname, string lname)
        {
            string insertQuery = "INSERT INTO Person VALUES (@fname, @lname);";

            using (SqlCommand cmd = new SqlCommand(insertQuery, this._con))
            {
                cmd.Parameters.AddWithValue("@fname", fname);
                cmd.Parameters.AddWithValue("@lname", lname);
                try
                {
                    await cmd.ExecuteNonQueryAsync();

                    string retrieveFlavor = "SELECT TOP 1 * FROM Person ORDER BY PersonId DESC;";
                    using (SqlCommand cmd2 = new SqlCommand(retrieveFlavor, this._con))
                    {
                        SqlDataReader dr = await cmd2.ExecuteReaderAsync();
                        return dr;
                    }
                }
                catch (DbException ex)
                {
                    Console.WriteLine($"Exception in DatabaseAccessClass {ex}");
                    return null;
                }
            }
        }

        public async Task<SqlDataReader> GetPerson(string fname, string lname)
        {
            string retrieveFlavor = "SELECT * FROM Person WHERE FirstName = @fname AND LastName = @lname;";

            using (SqlCommand cmd = new SqlCommand(retrieveFlavor, this._con))
            {
                cmd.Parameters.AddWithValue("@fname", fname);
                cmd.Parameters.AddWithValue("@lname", lname);

                SqlDataReader dr = await cmd.ExecuteReaderAsync();
                return dr;
            }
        }

        public async Task<SqlDataReader> GetAllFlavors()
        {
            string retrieveFlavors = "SELECT * FROM Flavor;";

            using (SqlCommand cmd = new SqlCommand(retrieveFlavors, this._con))
            {
                SqlDataReader dr = await cmd.ExecuteReaderAsync();
                return dr;
            }
        }
    }
}
