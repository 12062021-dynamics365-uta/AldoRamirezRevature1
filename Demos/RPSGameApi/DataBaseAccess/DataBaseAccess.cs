using System;
using Models;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace Storage
{
    public class DataBaseAccess : IDataBaseAccess
    {
        // in readl life you dont want to keep your Cnn String here.... 
        // it will be pushed t our GitHub and anyone could see it.
        private readonly string str = "Data source =MARKCMOORE\\SQLEXPRESS;initial Catalog=RpsGameDb; integrated security =true";
        private readonly SqlConnection _con;
        //private readonly IMapper _mapper;

        //constructor
        public DataBaseAccess(/*IMapper mapper*/)
        {
            this._con = new SqlConnection(this.str);
            _con.Open();
            //this._mapper = mapper;
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
    }
}
