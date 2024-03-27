using System.Text.RegularExpressions;

namespace MyGame
{
    public class Enemy
    {
        public event Action? DieEvent;       

        private const int _Health = 300;
        private const int _Damage = 100;
        private const int _Level = 1;
        private int Health;
        private int Damage;
        private int Level;       

        public bool IsAlive => Health > 0;
        public int DieExperience => 1;
        
        private Logger _log;
        private Random _random = new();

        public Enemy(Logger log)
        {
            _log = log;
            Health = _Health;
            Damage = _Damage;
            Level = _Level;
        }

        public void Hit(int damage)
        {
            Health -= damage;

            if (IsAlive)
            {
                _log.AddLog($" Игрок нанес {damage} урона");
            }
            else
            {
                _log.AddLog($" Игрок нанес {damage} урона, враг погиб.");
                DieEvent?.Invoke();
            }
        }

        public bool TryHit(out int damage)
        {
            int chance = _random.Next(0, 100);

            if (chance > 20)
            {
                damage = Damage;
                return true;
            }

            _log.AddLog($" Враг промахнулся");
            damage = 0;
            return false;
        }

        public void HealthStatus(Enemy? enemy)
        {     if(enemy is not null)
            {
                Console.WriteLine($"  Враг уровень:\t {Level}");
                Console.WriteLine($"   \t жизни:\t {Health}\n");                
            }            
        }        
    }
}