using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace Domain
{
    //this class will hold the methods to map what is returned from the Db to a known object within the Application here.
    public class Mapper : IMapper
    {
        public Player EntityToPlayer(DataTableReader dr)
        {
            //call dr.read method
            if (dr.Read())
            {
                Player p = new Player()
                {
                    PlayerId = dr.GetInt32(0),
                    Fname = dr.GetString(1),
                    Lname = dr.GetString(2),
                    Losses = dr.GetInt32(3),
                    Wins = dr.GetInt32(4)
                };
                dr.Close();
                return p;
            }

            return null;
        }

        //I don't thiknk we need any variables or properties in this class...


        /// <summary>
        /// This method will convert an entity returned from the Db to a Player object
        /// </summary>
        public List<Player> EntityToPlayerList(SqlDataReader dr)
        {
            List<Player> players = new List<Player>();
            while (dr.Read())
            {
                //Console.WriteLine(dr[0].ToString() + " " + dr[1].ToString() + "  " + dr[2].ToString() + "  " + dr[3].ToString() + "  " + dr[4].ToString());
                Player p = new Player()
                {
                    PlayerId = Convert.ToInt32(dr[0].ToString()),
                    Fname = dr[1].ToString(),
                    Lname = dr[2].ToString(),
                    Wins = Convert.ToInt32(dr[3].ToString()),
                    Losses = Convert.ToInt32(dr[4].ToString()),
                };
                players.Add(p);

            }
            return players;

        }
    }
}
