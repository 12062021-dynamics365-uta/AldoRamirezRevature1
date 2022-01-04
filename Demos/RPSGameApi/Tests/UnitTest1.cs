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
        //arange
        private static IDataBaseAccess mockDbAccess = new MockDataBaseAccess();
        private static IMapper mockMapper = new MockMapper();
        private static GamePlayLogic gpl = new GamePlayLogic(mockDbAccess, mockMapper);
        [Fact]
        public void GetAllPlayersReturnsAListOfPlayers()
        {
            //arange is abov

            //act
            List<Player> players = gpl.GetAllPlayers();

            //assert
            Assert.Equal(3, players.Count);
            Assert.True(players[0].Fname == "jimmy");
        }

        [Theory]
        [InlineData("Aldo", "Ramirez")]
        [InlineData("Fake", "User")]
        public void LoginReturnsAPlayerObjectIfExistsNullIfNot(string fname, string lname)
        {
            //arrange is above

            //act
            Player result = gpl.Login(fname, lname);

            //Assert
            if(lname == "User")
            {
                Assert.Null(result);
            }
            else
            {
                Assert.Equal("Aldo", result.Fname);
                Assert.Equal("Ramirez", result.Lname);
            }
        }

    }
}
