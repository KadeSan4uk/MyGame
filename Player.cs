using System;
using System.Numerics;
using static System.Collections.Specialized.BitVector32;

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
        private ActionGame _action;
        private Random _random=new Random();

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
        public Enemy? StatusPlayer( ref Enemy? enemy)
        {
            Console.WriteLine($" Состояние игрока: {(enemy is not null ? "в бою\n  " : "в покое\n")}");

            HealthStatus();

            if (enemy is not null)
            {
                enemy.EnemyHealthStatus();
            }
            return enemy;
        }
        public Enemy? PerformPlayerAction(ref Enemy? enemy, bool missChance,bool createEnemy,string action)
        {           
             action = Console.ReadLine();

            if (enemy is null)            
                _action.ActionSearch();            
            else            
                _action.ActionAttack();            

            switch (action)
            {
                case "1":
                    if (enemy is not null)
                    {
                        int chance = _random.Next(0, 100);
                        if (missChance)
                        {
                            chance = 99;
                        }

                        if (chance > 20)
                        {
                            enemy.Hit(Damage);
                            missChance = false;

                            if (enemy.IsAlive is false)
                            {
                                GetExperience();
                                UpdateDamageHealth();
                                enemy = null;

                                if (Experience > 2)
                                {
                                    UpdateExperience();
                                }
                            }
                        }
                        else
                        {
                            Miss();
                            missChance = true;                            
                        }
                    }

                    break;
                case "2":
                    if (enemy is not null)
                    {
                        int chance = _random.Next(0, 100);
                        if (chance > 20)
                        {
                            EscapeLuck();
                            enemy = null;
                        }
                        else
                        {
                            EscapeFalse();
                        }
                    }

                    break;
                case "3":
                    if (enemy is null)
                    {
                        int chance = _random.Next(0, 100);

                        if (chance > 20)
                        {
                            _action.EnemySearchLuck();
                            createEnemy = true;
                        }
                        else
                        {
                            _action.EnemySearchFalse();
                        }
                    }
                    break;
            }
            return enemy;
        }
        
    } 
   
}
