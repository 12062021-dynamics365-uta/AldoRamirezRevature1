using Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storage
{
    public interface IDataBaseAccess
    {
        List<Player> GetAllPlayers();
        SqlDataReader Login(string fname, string lname);

    }
}
