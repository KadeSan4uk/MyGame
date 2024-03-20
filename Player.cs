

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
        private int ProgresHealth;
        private int ProgresDamage;
        private Logger _log;
        

        public Player(Logger log)
        {        
            _log = log;
            Health = _Health;
            Damage = _Damage;
            Level = _Level;
            Experience = _Experience;
        }

        

        public void Hit(int damage)
        {
            Health -= damage;

           _log.AddLog($" Враг нанес {damage} урона");
        }       
                      
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
