using System;
using System.Numerics;
using static System.Collections.Specialized.BitVector32;

namespace MyGame
{
    public class Player
    {
        public event Action? FindEnemyEvent;
        public event Action? DiedEventPlayer;

        private const int Health = 400;
        private const int Damage = 100;
        private const int Level = 1;
        private const int Experience = 0;
        private int _health;
        private int _damage;
        private int _level;
        private int _experience;
        private int _upHealth;
        private int _upDamage;
        private Logger _log;
        private Random _random = new Random();
        private Enemy? _enemy;
        private bool _missChance = false;

        public Player(Logger log)
        {
            _log = log;
            _health = Health;
            _damage = Damage;
            _level = Level;
            _experience = Experience;
        }

        public void TakeDamage(int damage)
        {
            _health -= damage;

            _log.AddLog($" Враг нанес {damage} урона");

            if (_health <= 0)
            {
                DiedEventPlayer?.Invoke();
            }
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

        public void UpDamageHealth(int damage, int health)
        {
            _upDamage += damage;
            _upHealth += health;
        }

        public void RestorHealthDamage()
        {
            _health = Health + _upHealth;
            _damage = Damage + _upDamage;
        }

        public void UpExperience(int experience)
        {
            _log.AddLog($" Игрок получил {experience} опыта");
            _experience += experience;

            if (_experience > 2)
            {
                LevelUp();
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
            Console.WriteLine($" Игрок уровень:\t {_level}");
            Console.WriteLine($" \t жизни:\t {_health}\n");
        }

        public void StatusPlayer()
        {
            Console.WriteLine(
                $" Состояние игрока: {(_enemy is not null ? "в бою\n  " : "в покое\n")}");

            HealthStatus();
        }

        public void PerformAction()
        {
            if (_enemy is null)
            {
                Console.WriteLine($" Возможное действие:");
                Console.WriteLine($" 3) = Искать врага");
            }
            else
            {
                Console.WriteLine($" Возможные действия:");
                Console.WriteLine($" 1) = Атаковать");
                Console.WriteLine($" 2) = Сбежать");
            }

            var action = Console.ReadLine();

            switch (action)
            {
                case "1":
                    if (_enemy is not null)
                    {
                        int chance = _random.Next(0, 100);
                        if (_missChance)
                        {
                            chance = 99;
                        }

                        if (chance > 20)
                        {
                            _enemy?.TakeDamage(_damage);
                            _missChance = false;
                        }
                        else
                        {
                            Miss();
                            _missChance = true;
                        }
                    }

                    break;
                case "2":
                    if (_enemy is not null)
                    {
                        int chance = _random.Next(0, 100);
                        if (chance > 20)
                        {                            
                            EscapeLuck();
                        }
                        else
                        {
                            EscapeFalse();
                        }
                    }

                    break;
                case "3":
                    if (_enemy is null)
                    {
                        int chance = _random.Next(0, 100);

                        if (chance > 20)
                        {
                            _log.AddLog($" Результат поиска: Враг найден!");
                            FindEnemyEvent?.Invoke();
                        }
                        else
                        {
                            _log.AddLog($" Результат поиска: Никого");
                        }
                    }
                    break;                     
            }
        }

        public void SetEnemy(Enemy? enemy)
        {
            _enemy = enemy;
        }
    }
}