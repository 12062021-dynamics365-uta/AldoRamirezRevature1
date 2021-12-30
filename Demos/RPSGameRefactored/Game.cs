using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rock_Paper_Scissors_Demo1
{
    class Game
    {
        public Player Player1 { get; set; }
        public Player Player2 { get; set; }

        public List<Round> rounds = new List<Round>();
    }
}
