using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace MyGame
{
    public class OnEnemyStrategy:IPlayerActionStrategy
    {
        public InputPlayer.PlayerAction GetPlayerAction(Player player)
        {
            int action;
            bool inputIsCorrect = false;

            do
            {
                Console.SetCursorPosition(0, Console.CursorTop);

                Console.WriteLine("Возможные действия:");
                Console.WriteLine("1) = Атаковать");
                Console.WriteLine("2) = Сбежать");

                string? input = Console.ReadLine();

                inputIsCorrect = int.TryParse(input, out action);

                if (!inputIsCorrect || action < 1 || action > 2)
                {
                    Console.SetCursorPosition(0, Console.CursorTop - 5);
                    Console.WriteLine("Неверный ввод. Повторите действие.");
                    inputIsCorrect = false;
                }
            } while (!inputIsCorrect);

            return (InputPlayer.PlayerAction) action;
        }
    }
}

