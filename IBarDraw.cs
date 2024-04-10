using System;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace MyGame
{
    public interface IBarDraw
    {
        void GiveHealthForBars(ref int health, ref int MaxHealth);
        
    }
}
