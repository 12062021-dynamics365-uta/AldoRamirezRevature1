using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Game
    {
        public Player Player1 { get; set; }
        public Player Player2 { get; set; }
        public List<Round> Rounds = new List<Round>();
    }
}
