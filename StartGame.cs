using System.Threading.Channels;

namespace MyGame
{
    public class StartGame
    {
        private static Random random = new Random();

        public static void Main(string[] args)
        {
            int playerHealth = 3;            
            bool fightState=false;
            bool enemy=false;
            int counterAction = 0;
            bool enemyFresh=false;

            while (true)
            {
                Console.WriteLine();
                Console.WriteLine($"<=== ход {counterAction} ===>");
                
                fightState = enemy is true;

                if(enemy)
                    enemyFresh = false;

                // a)
                Console.WriteLine($"Состояние игрока :{(fightState? "в бою":"в покое")}, жизни {playerHealth}");

                // б)
                Console.WriteLine($"Возможные действие:");
                Console.WriteLine($"1=бить");
                Console.WriteLine($"2=бежать");

                if(!enemy)
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
                                enemyFresh = true;
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
                            Console.WriteLine($"враг уже есть, повторите действие");
                            continue;
                        }
                        else
                        {
                            int chance = random.Next(0, 100);

                            if (chance > 50)
                            {
                                Console.WriteLine($"враг найден");
                                enemy = true;
                                enemyFresh = true;
                            }
                            else
                            {
                                Console.WriteLine($"неудачный поиск врага");
                            }
                        }
                        break;
                }

                //д)
                if (enemy && enemyFresh==false)
                {
                    Console.WriteLine($"враг нанес удар");
                    playerHealth -= 1;
                    if (playerHealth == 0)
                    {
                        Console.WriteLine($"душа покидает тело героя");
                        Console.WriteLine($"Game Over!");
                        break;
                    }
                }

                //г)
                if (!enemy)
                {                    
                    int chance = random.Next(0, 100);

                    if (chance > 80)
                    {
                        Console.WriteLine($"в дверях вашей лочуги появился враг!");
                        enemy = true;
                    }
                    else
                    {
                        Console.WriteLine($"в мире ничего не произошло");
                    }              
                }

               
                counterAction++;          
            }           
        }
    }
}