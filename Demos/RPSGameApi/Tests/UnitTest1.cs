using Domain;
using Models;
using Storage;
using System;
using System.Collections.Generic;
using Xunit;

namespace Tests.RPS_GameApi
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            //arange
            IDataBaseAccess mockDbAccess = new MockDataBaseAccess();
            GamePlayLogic gpl = new GamePlayLogic(mockDbAccess);

            //act
            List<Player> players = gpl.GetAllPlayers();

            //assert
            Assert.Equal(3, players.Count);
            Assert.True(players[0].Fname == "jimmy");
        }
    }
}
