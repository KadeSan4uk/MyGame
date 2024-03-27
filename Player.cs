using System;
using System.Numerics;
using static System.Collections.Specialized.BitVector32;

namespace MyGame
{
    public class Player
    {
        public event Action? FindEnemyEvent;
        public event Action? DiedEvent;

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
        private Random _random = new Random();
        private Enemy? _currentEnemy;
        private bool missChance = false;

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

            if (Health <= 0)
            {
                DiedEvent?.Invoke();
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

        public void ProgressDamageHealth(int damage, int health)
        {
            ProgresDamage += damage;
            ProgresHealth += health;
        }

        public void UpdateDamageHealth()
        {
            Health = _Health + ProgresHealth;
            Damage = _Damage + ProgresDamage;
        }

        public void UpdateExperience(int experience)
        {
            _log.AddLog($" Игрок получил {experience} опыта");
            Experience += experience;

            if (Experience > 2)
            {
                LevelUp();
            }
        }

        private void LevelUp()
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

        public void StatusPlayer()
        {
            Console.WriteLine(
                $" Состояние игрока: {(_currentEnemy is not null ? "в бою\n  " : "в покое\n")}");

            HealthStatus();
        }

        public void PerformAction()
        {
            if (_currentEnemy is null)
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
                    if (_currentEnemy is not null)
                    {
                        int chance = _random.Next(0, 100);
                        if (missChance)
                        {
                            chance = 99;
                        }

                        if (chance > 20)
                        {
                            _currentEnemy?.Hit(Damage);
                            missChance = false;
                        }
                        else
                        {
                            Miss();
                            missChance = true;
                        }
                    }

                    break;
                case "2":
                    if (_currentEnemy is not null)
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
                    if (_currentEnemy is null)
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
            _currentEnemy = enemy;
        }
    }
}