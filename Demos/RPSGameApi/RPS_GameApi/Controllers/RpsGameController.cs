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

        //All the methods in a controller are called Action methods
        //To be accessable to the outside world, they must be public.
        //public IActionResult Index()
        //{
           // return View();
        //}

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
        public ActionResult<Player> LoginPlayer(string fname, string lname)
        {
            //call the business (gpl) layer to retrieve the player, if it exists
            Player p = this._gamePlayLogic.Login(fname, lname);
            if (p == null)
                return NotFound();
            else
                return Ok(p);
        }
    }
}
