using System.Drawing;

namespace MyGame
{
    public class Player:IBarDraw
    {
        public event Action? FindEnemyEvent;
        public event Action? DiedEventPlayer;

        private const int Health = 400;        
        private const int Damage = 50;
        private const int Level = 1;
        private const int Experience = 0;
        private int _maxHealth;
        private int _health;
        private int _damage;
        private int _level;
        private int _countExperience = 2;
        private int _experience;
        private int _upHealth;
        private int _upDamage;
        private Logger _log;
        public Enemy? Enemy { get; set; }
        private Random _random = new();


        public Player(Logger log)
        {
            _log = log;
            _health = Health;
            _maxHealth = Health;
            _damage = Damage;
            _level = Level;
            _experience = Experience;
        }        

        public void TakeDamage(int damage)
        {
            _health -= damage;
            if (damage != 0)
            {
                _log.AddLog($" Враг нанес {damage} урона");
            }

            if (_health <= 0)
            {
                DiedEventPlayer?.Invoke();
            }
        }
        public  bool TryHit(out int damage)
        {
            int chance = _random.Next(0, 100);

            if (chance > 15)
            {
                damage = _damage;
                return true;                
            }

            _log.AddLog($" Игрок промахнулся");
            damage = 0;
            return false;
        }

        public void Miss()
        {
            _log.AddLog($" Игрок промахнулся");
        }

        public void TryEscape()
        {
            int chance = _random.Next(0, 100);
            if (chance > 20)
            {
                EscapeLuck();
                Enemy = null;
            }
            else
            {
                EscapeFalse();
            }
        }

        public void EscapeLuck()
        {                   
            _log.AddLog($" Побег удался");
            RestorHealthDamage();            
        }

        public void EscapeFalse()
        {
            _log.AddLog($" Неудачная попытка побега");
        }

        public void UpDamageHealth(int damage, int health)
        {
            _upDamage += damage;
            _upHealth += health;
        }

        public void RestorHealthDamage()
        {
            _health = Health + _upHealth;
            _maxHealth = _health;
            _damage = Damage + _upDamage;
        }

        public void UpExperience(int experience)
        {
            _log.AddLog($" Игрок получил {experience} опыта");
            _experience += experience;

            if (_experience > _countExperience)
            {
                LevelUp();
                _countExperience++;
            }
        }

        private void LevelUp()
        {
            _level++;
            _log.AddLog($" Игрок достиг {_level} уровня!");
            _log.AddLog($" Жизни + 50, Урон + 50");
            UpDamageHealth(50, 50);
            RestorHealthDamage();
            _experience = 0;
        }

        public void HealthStatus()
        {
            Console.WriteLine($" Игрок уровень: {_level}");            
            Console.WriteLine($"\n");
        }

        public void StatusPlayer()
        {
            Console.WriteLine($" Состояние игрока: {(Enemy is not null ? "в бою\n\n " : "в покое\n\n")}");            
        }       

        public void GiveHealthForBars(ref int health,ref int MaxHealth)
        {
            health = _health;
            MaxHealth=_maxHealth;            
        }
        public void SetEnemy(Enemy? enemy)
        {
            Enemy = enemy;
        }
    }
}