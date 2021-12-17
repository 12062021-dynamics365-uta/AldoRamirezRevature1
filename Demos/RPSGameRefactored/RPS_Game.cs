using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rock_Paper_Scissors_Demo1
{
    interface RPS_Game
    {
        Choice ValidateUserChoice(string choice);
        Choice GetComputerChoice();
        Player PlayRound(Choice p1Choice, Choice p2Choice);
        Player WinnerYet();
    }
}
