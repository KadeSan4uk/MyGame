
namespace MyGame
{
    public class InputPlayer
    {        
        public enum PlayerAction
        {
            Hit = 1,
            Escape,
            Search
        }

        public PlayerAction GetPlayerAction(Player _player)
        {            
            int action;
            bool InputIsCorrect = false;

            do
            {
                Console.SetCursorPosition(0, Console.CursorTop);

                if (_player.Enemy is null)
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

                string? input = Console.ReadLine();

                InputIsCorrect = int.TryParse(input, out action);

                if(_player.Enemy is null)
                {
                    if(!InputIsCorrect || action < 3 || action > 3)
                    {
                        Console.SetCursorPosition(0, Console.CursorTop - 4);
                        Console.WriteLine(" Неверный ввод. Повторите действие.");
                        InputIsCorrect = false;
                    }
                }
                else
                {
                    if (!InputIsCorrect || action < 1 || action > 2)
                    {
                        Console.SetCursorPosition(0, Console.CursorTop - 5);
                        Console.WriteLine(" Неверный ввод. Повторите действие.");
                        InputIsCorrect = false;
                    }
                }      
                
            } while (!InputIsCorrect);

            switch (action)
            {
                case 1: return PlayerAction.Hit;
                case 2: return PlayerAction.Escape;
                case 3: return PlayerAction.Search;
                default: return 0;
            }
        }
    }

}
