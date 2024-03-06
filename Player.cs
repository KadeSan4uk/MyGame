using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace MyGame
{
    public class Player
    {        
        public int baseHealth { get; private set; } = 400;
        public int baseDamage { get; private set; } = 100;
        public int baseLevel { get; private set; } = 1;
        public int baseExperience { get; private set; } = 0;


        
        public int Health { get; set; } = 400;
        public int Damage { get; set; } = 100;
        public int Level { get; set; } = 1;
        public int Experience { get; set; } = 0;                

        private Queue<string> _logQueue;
        public Player(Queue<string> logQueue)
        {
            _logQueue = logQueue;
        }
        public void Hit(int damage)
        {
            Health -= damage;            
            
           _logQueue.Enqueue($" Враг нанес {damage} урона");                       
        }
    }   
}
