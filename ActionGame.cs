using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace MyGame
{       
    public class ActionGame
    {
        private Logger _log;
        
        public ActionGame(Logger log)
        {
            _log = log;
        }

        public void ActionSearch()
        {
            Console.WriteLine($" Возможное действие:");
            Console.WriteLine($" 3) = Искать врага");
        }
        public void ActionAttack() 
        {
            Console.WriteLine($" Возможные действия:");
            Console.WriteLine($" 1) = Атаковать");
            Console.WriteLine($" 2) = Сбежать");
        }     
        public void EnemySearchFalse()
        {
            _log.AddLog($" Результат поиска: Никого");
        }
        public void EnemySearchLuck() 
        {
            _log.AddLog($" Результат поиска: Враг найден!");

        }
        public void PlayerIsDead()
        {
            Console.Clear();
            Console.WriteLine($" Игрок погиб!\t|| Начать заново?");
            Console.WriteLine($" Возможные действия:");
            Console.WriteLine($" 1) = Начать заново");
            Console.WriteLine($" 2) = Покинуть игру");
        }
    }
}
