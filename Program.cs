using System.Text;

namespace MyGame
{
    public class Program
    {
        private static World? world;
        private static Logger? logger;
        

        public static void Main(string[] args)
        {           
            logger = new Logger(Console.WriteLine);
            world = new World(logger);

            while (!world.IsRunning)
            {
                Console.Clear();
                logger.ShowLog();
                world.PerformGlobalAction();
                world.PrintStatus();
                world.PerformActorsActions();
                world.NextTurn();
            }
        }
    }
}