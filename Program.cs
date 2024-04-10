
namespace MyGame
{
    public class Program
    {
        private static World? _world;
        private static Logger? _logger;
        

        public static void Main(string[] args)
        {           
            _logger = new Logger(Console.WriteLine);
            _world = new World(_logger);
            Console.CursorVisible = false;

            while (!_world.IsRunning)
            {
                Console.Clear();
                _logger.ShowLog();
                _world.PerformGlobalAction();
                _world.PrintStatus();
                _world.PerformActorsActions();
                _world.NextTurn();
            }
        }
    }
}