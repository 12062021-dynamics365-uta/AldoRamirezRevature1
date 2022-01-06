using Domain;
using Microsoft.AspNetCore.Mvc;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RPS_GameApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RpsGameController : Controller
    {
        private readonly IGamePlayLogic _gamePlayLogic;

        public RpsGameController(IGamePlayLogic gpl)
        {
            this._gamePlayLogic = gpl;
        }

        /// <summary>
        /// The method will take the first and last names of the player
        /// adn check if that player exists in the Db
        /// if not return 'notfound'
        /// if so, return the player object
        /// </summary>
        /// <param name="fname"></param>
        /// <param name="lname"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("loginplayer/{fname}/{lname}")]
        public async Task<ActionResult<Player>> LoginPlayer(string fname, string lname)
        {
            //call the business (gpl) layer to retrieve the player, if it exists
            Player p = await this._gamePlayLogic.LoginAsync(fname, lname);
            if (p == null)
                return NotFound();
            else
                return Ok(p);
        }

        [HttpPost]
        [Route("registernewplayer/{fname}/{lname}")]
        public async Task<ActionResult<Player>> RegisterNewPlayer (string fname, string lname)
        {
            // call the business layer to register a player
            Player p = await this._gamePlayLogic.RegisterNewPlayerAsync(fname, lname);
            if (p != null)
                return Created($"http://5001/rpsgame/players/{p.PlayerId}", p); //make sure this URI is correct
            else
                return new UnprocessableEntityResult();
        }
    }
}
