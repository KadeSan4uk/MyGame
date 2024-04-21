using System;
using System.Linq;
using System.Text;
using MyGame.CoreGame;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace MyGame.ActionStrategies
{
    public interface IPlayerActionStrategy
    {
        public void ActionPlayerStrategy(Player player);
    }
}
