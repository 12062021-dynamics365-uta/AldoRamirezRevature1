using Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storage
{
    public interface IDataBaseAccess
    {
        List<Player> GetAllPlayers();
        Task<DataTableReader> LoginAsync(string fname, string lname);
        Task<Player> RegisterNewPlayerAsync(string fname, string lname);
    }
}
