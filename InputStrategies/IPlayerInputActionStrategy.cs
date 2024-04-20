using System;
using System.Linq;
using System.Text;
using MyGame.CoreGame;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace MyGame
{
    public interface IPlayerInputActionStrategy
    {              
        InputPlayer.PlayerAction GetPlayerAction(Player player);       
    }
}
