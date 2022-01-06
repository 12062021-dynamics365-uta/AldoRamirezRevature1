using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using Storage;

namespace Tests.RPS_GameApi
{
    class MockDataBaseAccess : IDataBaseAccess
    {
        private readonly DataTable playerTable;
        private readonly DataTable emptyPlayerTable;

        public MockDataBaseAccess()
        {
            playerTable = new DataTable("Players");
            DataColumn idColumn = new DataColumn("PlayerId", typeof(int));
            DataColumn fNameColumn = new DataColumn("FirstName", typeof(string));
            DataColumn lNameColumn = new DataColumn("LastName", typeof(string));
            DataColumn lossesColumn = new DataColumn("Losses", typeof(int));
            DataColumn winsColumn = new DataColumn("Wins", typeof(int));

            playerTable.Columns.Add(idColumn);
            playerTable.Columns.Add(fNameColumn);
            playerTable.Columns.Add(lNameColumn);
            playerTable.Columns.Add(lossesColumn);
            playerTable.Columns.Add(winsColumn);
            

            DataRow newRow = playerTable.NewRow();
            newRow["PlayerId"] = 1;
            newRow["FirstName"] = "Aldo";
            newRow["LastName"] = "Ramirez";
            newRow["Losses"] = 0;
            newRow["Wins"] = 0;
            playerTable.Rows.Add(newRow);

            newRow = playerTable.NewRow();
            newRow["PlayerId"] = 2;
            newRow["FirstName"] = "Bob";
            newRow["LastName"] = "Ross";
            newRow["Losses"] = 0;
            newRow["Wins"] = 0;
            playerTable.Rows.Add(newRow);

            emptyPlayerTable = playerTable.Copy();
            emptyPlayerTable.Clear();
        }

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

        public async Task<DataTableReader> LoginAsync(string fname, string lname)
        {
            DataRow[] playerFound = playerTable.Select($"FirstName = '{fname}' AND LastName = '{lname}'");
            if (playerFound.Length != 0)
            {
                DataTable dataTable = emptyPlayerTable.Copy();
                dataTable.ImportRow(playerFound[0]);
                return dataTable.CreateDataReader();
            }
            else
                return emptyPlayerTable.CreateDataReader();
        }

        public Task<Player> RegisterNewPlayerAsync(string fname, string lname)
        {
            throw new NotImplementedException();
        }
    }
}
