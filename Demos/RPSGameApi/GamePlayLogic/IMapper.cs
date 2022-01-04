using Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public interface IMapper
    {
        List<Player> EntityToPlayerList(SqlDataReader dr);
        Player EntityToPlayer(SqlDataReader dr);
    }
}
