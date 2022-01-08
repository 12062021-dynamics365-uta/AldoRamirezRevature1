using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SweetnSaltyDbAccess
{
    public interface ISweetnSaltyDbAccessClass
    {
        Task<SqlDataReader> PostFlavor(string flavor);
        Task<SqlDataReader> PostPerson(string fname, string lname);
        Task<SqlDataReader> GetPerson(string fname, string lname);
        Task<SqlDataReader> GetAllFlavors();
        Task<SqlDataReader> GetPersonById(int id);
        Task<SqlDataReader> GetPersonFlavors(int id);
    }
}
