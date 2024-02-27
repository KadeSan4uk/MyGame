using System.Data;
using System.Threading.Channels;

namespace MyGame
{
    public class StartGame
    {
        private static Random random = new Random();

        public static void Main(string[] args)
        {
            int countPlayerHelth = 4;
            int countExpereince = 0;
            int globalExperience = 1;
            int playerLevel = 1;
            int playerExperience=0; 
            int playerHealth = countPlayerHelth;            
            int playerDamage = 1;
            int enemyLevel = 1;
            int enemyHealth = 3;
            int enemyDamage = 1;
            bool fightState=false;
            bool enemy=false;
            int counterAction = 1;
            bool enemyFresh=false;

            while (true)
            {               
                Console.WriteLine();
                Console.WriteLine($"  <=== ход {counterAction} ===>");                
                                
                fightState = enemy is true;

                if (enemy)                
                    enemyFresh = false;
                //г)
                if (!enemy)
                {
                    int chance = random.Next(0, 100);

                    if (chance > 80)
                    {
                        Console.WriteLine($" В дверях вашей лочуги появился враг!");
                        enemy = true;
                    }
                    else
                    {
                        Console.WriteLine($" В мире ничего не произошло");
                    }                    
                }               
                // a)
                Console.WriteLine($" Состояние игрока: {(fightState? "в бою  ":"в покое")} || Уровень героя {playerLevel}\n" +
                    $"\t\t\t   || Жизни героя {playerHealth}");
                Console.WriteLine();

                if (enemy)
                {
                    Console.WriteLine($"\t\t\t   || Уровень врага {enemyLevel}");
                    Console.WriteLine($"\t\t\t   || Жизни врага {enemyHealth}");
                }
                // б)
                if (!enemy) 
                {
                    Console.WriteLine($" Возможные действие:");
                    Console.WriteLine($" 3) = искать врага");
                }
                else
                {
                    Console.WriteLine($" Возможные действие:");
                    Console.WriteLine($" 1) = атаковать");
                    Console.WriteLine($" 2) = сбежать");
                }
                //в)
                Console.Write($" Выбрать действие:");
                string action = Console.ReadLine();
                switch (action)
                {
                    case "1":
                        if (enemy)
                        {
                            int chance = random.Next(0, 100);

                            if (chance > 5)
                            {                                
                                if(enemyHealth>1)
                                {
                                    Console.WriteLine($" \t\t\t   |=> Герой нанес {playerDamage} урона");
                                    enemyHealth -= playerDamage;
                                }                             
                                else if(enemyHealth <=1)
                                {
                                    Console.WriteLine($" \t\t\t   |=> Герой нанес сокрушительный удар");
                                    Console.WriteLine($"\t\t\t    < Враг повержен! >");
                                    Console.WriteLine($" Герой получил {globalExperience} опыта");
                                    playerExperience += globalExperience;
                                    countExpereince++;
                                    if (countExpereince > 2)
                                    {
                                        playerLevel += 1;
                                        Console.WriteLine($"\t\t\t Герой достиг {playerLevel} уровня! ");
                                        playerDamage++;
                                        countPlayerHelth++;
                                        countExpereince = 0;
                                    }
                                    enemy = false;
                                    enemyHealth = 3;
                                    playerHealth = countPlayerHelth;
                                }
                            }
                            else
                            {
                                Console.WriteLine($" Промах");                                
                            }
                        }
                        else
                        {
                            Console.WriteLine($" Враг отсутсвует, ищем врага");
                            int chance = random.Next(0, 100);

                            if (chance > 20)
                            {
                                Console.WriteLine($" Враг найден!");
                                enemy = true;
                                enemyFresh = true;
                            }
                            else
                            {
                                Console.WriteLine($" Неудачный поиск врага");
                            }
                        }
                        break;
                    case "2":
                        if (enemy)
                        {
                            int chance = random.Next(0, 100);
                            if (chance > 10)
                            {
                                Console.WriteLine($" Герой сбежал от врага");
                                enemy = false;
                            }
                            else
                            {
                                Console.WriteLine($" Побег неудался");
                            }
                        }
                        else
                        {
                            Console.WriteLine($" Врага не найден");
                        }
                        break;
                    case "3":
                        if (enemy)
                        {
                            Console.WriteLine($" Враг уже есть, повторите действие");
                            continue;
                        }
                        else
                        {
                            int chance = random.Next(0, 100);

                            if (chance > 20)
                            {
                                Console.WriteLine($" Результат поиска: Враг найден!");
                                enemy = true;
                                enemyFresh = true;
                            }
                            else
                            {
                                Console.WriteLine($" Результат поиска: Враг не найден");
                            }
                        }
                        break;
                }
                //д)
                if (enemy && enemyFresh==false)
                {
                    if (enemyHealth > 0)
                    {
                        Console.WriteLine($"\t\t\t   |=> Враг нанес {enemyDamage} урона");
                        playerHealth -= 1;
                    }
                    
                    if (playerHealth <= 0)
                    {
                        Console.Clear();
                        Console.WriteLine($" Герой погиб!\t|| Начать заново?");                     
                        Console.WriteLine($" Возможные действия:");
                        Console.WriteLine($" 1) = Начать заново");
                        Console.WriteLine($" 2) = Покинуть игру");

                        string actionEsc=Console.ReadLine();

                        switch (actionEsc)
                        {
                            case "1": counterAction = 1;
                                Console.Clear();
                                playerHealth = countPlayerHelth;
                                enemyHealth = 3;
                                countPlayerHelth = 4;
                                playerExperience = 0;
                                enemy = false;                                
                                continue;

                            case "2": Console.WriteLine($" Выход из игры");                                      
                                return;

                            default:                                
                                    Console.WriteLine($" Выход из игры");                                    
                                return;                              
                        }                        
                    }
                }                
                counterAction++;                
            }           
        }
    }
}