using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rock_Paper_Scissors_Demo1
{
    class Player
    {
        public string Fname { get; set; }
        public string Lname { get; set; }
        public int Wins { get; set; }
        public int Losses { get; set; }

        public Player(string fName, string lName)
        {
            this.Fname = fName;
            this.Lname = lName;
        }
    }
}
