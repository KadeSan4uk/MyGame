using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MyGame.InputPlayer;
using System.Collections.Generic;

namespace MyGame
{
    public interface IOnInput
    {
        PlayerAction GetPlayerAction(Player _player);
    }
}
