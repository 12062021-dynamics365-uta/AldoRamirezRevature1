using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using Storage;

namespace Tests.RPS_GameApi
{
    class MockDataBaseAccess : IDataBaseAccess
    {
        public List<Player> GetAllPlayers()
        {
            List<Player> players = new List<Player>();
            Player p1 = new Player()
            {
                Fname = "jimmy",
                Lname = "Junes",
                Losses = 2,
                Wins = 21
            };
            Player p2 = new Player()
            {
                Fname = "jammy",
                Lname = "Jines",
                Losses = 2,
                Wins = 21
            };
            Player p3 = new Player()
            {
                Fname = "jemmy",
                Lname = "Janes",
                Losses = 2,
                Wins = 21
            };
            players.Add(p1);
            players.Add(p2);
            players.Add(p3);

            return players;
        }

        public DataTableReader LoginAsync(string fname, string lname)
        {
            DataTable table = new DataTable("Players");
            DataColumn idColumn = new DataColumn("PlayerId", typeof(int));
            DataColumn fNameColumn = new DataColumn("FirstName", typeof(string));
            DataColumn lNameColumn = new DataColumn("LastName", typeof(string));
            DataColumn lossesColumn = new DataColumn("Losses", typeof(int));
            DataColumn winsColumn = new DataColumn("Wins", typeof(int));

            table.Columns.Add(idColumn);
            table.Columns.Add(fNameColumn);
            table.Columns.Add(lNameColumn);
            table.Columns.Add(lossesColumn);
            table.Columns.Add(winsColumn);

            DataRow newRow = table.NewRow();
            newRow["PlayerId"] = 1;
            newRow["FirstName"] = "Aldo";
            newRow["LastName"] = "Ramirez";
            newRow["Losses"] = 0;
            newRow["Wins"] = 0;
            table.Rows.Add(newRow);

            DataTableReader dtr = table.CreateDataReader();

            if (fname == "Aldo" && lname == "Ramirez")
                return dtr;
            else
            {
                table.Clear();
                DataTableReader dtrEmpty = table.CreateDataReader();
                return dtrEmpty;
            }
        }

        public Task<Player> RegisterNewPlayerAsync(string fname, string lname)
        {
            throw new NotImplementedException();
        }
    }
}
