using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MyGame.InputPlayer;
using System.Collections.Generic;

namespace MyGame
{    
    public class HitAction : IOnInput
    {
        public PlayerAction GetPlayerAction(Player _player)
        {
            return PlayerAction.Hit;
        }
    }

    public class EscapeAction : IOnInput
    {
        public PlayerAction GetPlayerAction(Player _player)
        {
            return PlayerAction.Escape;
        }
    }

    public class SearchAction : IOnInput
    {
        public PlayerAction GetPlayerAction(Player _player)
        {
            return PlayerAction.Search;
        }
    }
}
