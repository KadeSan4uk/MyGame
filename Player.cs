using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace MyGame
{
    public class Player
    {        
        private const int _Health = 400;
        private const int _Damage = 100;
        private const int _Level = 1;
        private const int _Experience = 0;
        public int Health;
        public int Damage;
        public int Level;
        public int Experience;      
                             
        private Queue<string> _logQueue;
        public Player(Queue<string> logQueue)
        {
            _logQueue = logQueue;
            Health = _Health;
            Damage = _Damage;
            Level = _Level;
            Experience = _Experience;
        }
        public void Hit(int damage)
        {
            Health -= damage;            
            
           _logQueue.Enqueue($" Враг нанес {damage} урона");                       
        }
        
        private int ProgresHealth;
        private int ProgresDamage;               
        public void ProgressDamageHealth(int damage,int health)
        {
            ProgresDamage += damage;
            ProgresHealth += health;                      
        }
        public void UpdateDamageHealth() 
        {
            Health = _Health+ProgresHealth; 
            Damage = _Damage+ProgresDamage;
        }        
    }   
}
