using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Round
    {
        public Player Player1 { get; set; }
        public Player Player2 { get; set; }

        public Player Winner { get; set; }

        public Choice P1Choice { get; set; }

        public Choice P2Choice { get; set; }

    }
}
