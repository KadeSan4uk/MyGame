using System.Threading.Channels;

namespace MyGame
{
    public class StartGame
    {
        public static void Main(string[] args)
        {
            string name;
            string race;
            
            Console.WriteLine("Привет!");
            Console.WriteLine("Введи имя персонажа");
            name = Console.ReadLine();
            
            Console.WriteLine("Выбери рассу:\n1: Human 2: Orc");
            int flagPlayerClass = Convert.ToInt32(Console.ReadLine());
            while(flagPlayerClass != 1||flagPlayerClass!=2) 
            {
                if (flagPlayerClass == 1)
                {
                  
                    Console.WriteLine("Отлично ты выбрал Human!");
                    break;
                }
                else if (flagPlayerClass == 2)
                {
                   
                    Console.WriteLine("Отлично ты выбрал Orc!");
                    break;

                }
                else
                {
                    Console.WriteLine("Попробуй еще...");
                    Console.WriteLine("Выбери рассу:\n1: Human 2: Orc");
                    flagPlayerClass = Convert.ToInt32(Console.ReadLine());
                }

            }
            







        }
    }
}