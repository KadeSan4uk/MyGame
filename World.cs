using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace MyGame
{
    public class World
    {
        private Random random = new Random();        
        private Logger _log;        
        
        public World( Logger log)
        {            
            _log = log;             
        }
        public Enemy? WorldTurn(int currentRound,ref Enemy? enemy)
        {
            Console.WriteLine();
            Console.WriteLine($" \t <=== Ход {currentRound} ===>");

            if (enemy is null)
            {
                int chance = random.Next(0, 100);

                if (chance > 50)
                {                     
                    enemy = new Enemy(_log);
                    Console.WriteLine($" В дверях вашей лочуги появился враг!");                    
                }
                else
                {
                    Console.WriteLine($" В мире ничего не произошло");
                }
            }
            return enemy;
        }

    }
}
