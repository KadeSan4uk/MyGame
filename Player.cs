using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace MyGame
{
    public class Player
    {       
        //playerHealth = basePlayerHealth;
        public int Health { get; private set; } = 4;
        public int Damage { get; private set; } = 1;
        public int Level { get; private set; } = 1;
        public bool IsAlive => Health > 0;

        private Queue<string> _logQueue;

        public Player(Queue<string> logQueue)
        {
            _logQueue = logQueue;
        }

        public void Hit(int damage)
        {
            Health -= damage;

            if (!IsAlive)
            {
                _logQueue.Enqueue($" Игрок получил {damage} урона");
            }           
        }
    }   
}
