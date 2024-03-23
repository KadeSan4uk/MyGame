using System;

namespace MyGame
{
    public class Enemy
    {   
        private const int _Health = 300;
        private const int _Damage = 100;
        private const int _Level = 1;
        public int Health;
        public int Damage;
        public int Level;

        public bool IsAlive => Health > 0;
        private Logger _log;        

        public Enemy(Logger log)
        {
            _log = log;       
            Health=_Health;
            Damage=_Damage;
            Level=_Level;
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
            }
        }
        public void EnemyMiss()
        {
            _log.AddLog($" Враг промахнулся");
        }
        public void EnemyHealthStatus() 
        {
            Console.WriteLine($"  Враг уровень:\t {Level}");
            Console.WriteLine($"   \t жизни:\t {Health}\n");
        }
    }
}
