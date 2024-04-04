 
namespace MyGame
{
    public class Enemy
    {
        public event Action? DiedEventEnemy;       

        private const int Health = 300;
        private const int Damage = 100;
        private const int Level = 1;
        private int _health;
        private int _damage;
        private int _level; 
        
        private bool _isAlive => _health > 0;
        public int DieExperience => 1;
        
        private Logger _log;
        private Random _random = new();

        public Enemy(Logger log)
        {            
            _log = log;
            _health = Health;
            _damage = Damage;
            _level = Level;
        }

        public void TakeDamage(int damage)
        {
            _health -= damage;

            if (_isAlive)
            {
                if(damage != 0)
                _log.AddLog($" Игрок нанес {damage} урона");
            }
            else
            {
                _log.AddLog($" Игрок нанес {damage} урона, враг погиб.");
                DiedEventEnemy?.Invoke();
            }
        }

        public bool TryHit(out int damage)
        {
            int chance = _random.Next(0, 100);

            if (chance > 20)
            {
                damage = _damage;
                return true;
            }

            _log.AddLog($" Враг промахнулся");
            damage = 0;
            return false;
        }

        public void HealthStatus()
        {                 
            Console.WriteLine($"  Враг уровень:\t {_level}");
            Console.WriteLine($"   \t жизни:\t {_health}\n\n");                                      
        }        
    }
}