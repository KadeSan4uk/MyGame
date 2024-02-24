using System.Threading.Channels;

namespace MyGame
{
    public class StartGame
    {
        private static Random random = new Random();

        public static void Main(string[] args)
        {
            bool fightState=false;
            bool enemy=false;

            while (true)
            {
                Console.Clear();
                fightState = enemy is true;
                // a)
                Console.WriteLine($"Состояние игрока :{(fightState? "в бою":"в покое")}");

                // б)
                Console.WriteLine($"Возможные действие:");
                Console.WriteLine($"1=бить");
                Console.WriteLine($"2=бежать");
                Console.WriteLine($"3=искать врага");
                Console.Write($"Выбери действие:");
                string action=Console.ReadLine();

                //в)
                switch (action)
                {
                    case "1":
                        if (enemy)
                        {
                            int chance = random.Next(0, 100);

                            if (chance > 10)
                            {
                                Console.WriteLine($"враг повержен");
                                enemy = false;
                            }
                            else
                            {
                                Console.WriteLine($"промах");
                            }
                        }
                        else 
                        {
                            Console.WriteLine($"враг отсутсвует, ищем врага");
                            int chance = random.Next(0, 100);

                            if (chance > 50)
                            {
                                Console.WriteLine($"враг найден");
                                enemy = true;
                            }
                            else
                            {
                                Console.WriteLine($"неудачный поиск врага");
                            }
                        }
                        break;
                    case "2":
                        if (enemy)
                        {
                            int chance = random.Next( 0, 100);
                            if (chance > 10)
                            {
                                Console.WriteLine($"убежал от врага");
                                enemy = false;
                            }
                            else
                            {
                                Console.WriteLine($"побег неудался");                                                               
                            }                           
                        }
                        else
                        {
                            Console.WriteLine($"врага не найден");
                        }
                        break;
                    case "3":
                        if (enemy)
                        {
                            Console.WriteLine($"враг уже есть");
                        }
                        else
                        {
                            int chance = random.Next(0, 100);

                            if (chance > 50)
                            {
                                Console.WriteLine($"враг найден");
                                enemy = true;
                            }
                            else
                            {
                                Console.WriteLine($"неудачный поиск врага");
                            }
                        }
                        break;
                }
            }           
        }
    }
}