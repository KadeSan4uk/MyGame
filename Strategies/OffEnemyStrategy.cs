using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace MyGame
{
    public class OffEnemyStrategy:IPlayerActionStrategy
    {
        public InputPlayer.PlayerAction GetPlayerAction(Player player)
        {
            int action;
            bool inputIsCorrect = false;

            do
            {
                Console.SetCursorPosition(0, Console.CursorTop);

                Console.WriteLine("Возможное действие:");
                Console.WriteLine("3) = Искать врага");

                string? input = Console.ReadLine();

                inputIsCorrect = int.TryParse(input, out action);

                if (!inputIsCorrect || action != 3)
                {
                    Console.SetCursorPosition(0, Console.CursorTop - 4);
                    Console.WriteLine("Неверный ввод. Повторите действие.");
                    inputIsCorrect = false;
                }
            } while (!inputIsCorrect);

            return (InputPlayer.PlayerAction) action;
        }
    }
}
