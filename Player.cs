using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace MyGame
{
    public class Player
    {        
        private const int baseHealth = 400;
        private const int baseDamage = 100;
        private const int baseLevel = 1;
        private const int baseExperience = 0;
        public int Health;
        public int Damage;
        public int Level;
        public int Experience;      
                             
        private Queue<string> _logQueue;
        public Player(Queue<string> logQueue)
        {
            _logQueue = logQueue;
            Health = baseHealth;
            Damage = baseDamage;
            Level = baseLevel;
            Experience = baseExperience;
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
            Health = baseHealth+ProgresHealth; 
            Damage = baseDamage+ProgresDamage;
        }        
    }   
}
