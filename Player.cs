using System.Numerics;

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
        private int experience = 1;
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
        public void Miss()
        {
            _log.AddLog($" Игрок промахнулся");
        }
        public void EscapeLuck()
        {
            _log.AddLog($" Побег удался");
        }
        public void EscapeFalse()
        {
            _log.AddLog($" Неудачная попытка побега");
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
        public void GetExperience()
        {
            _log.AddLog($" Игрок получил {experience} опыта");
            Experience += experience;
        }
        public void UpdateExperience()
        {
            Level++;
            _log.AddLog($" Игрок достиг {Level} уровня!");
            _log.AddLog($" Жизни + 50, Урон + 50");
            ProgressDamageHealth(50, 50);
            UpdateDamageHealth();
            Experience = 0;
        }
        public void HealthStatus()
        {
            Console.WriteLine($" Игрок уровень:\t {Level}");
            Console.WriteLine($" \t жизни:\t {Health}\n");
        }
    } 
   
}
