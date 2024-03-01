﻿using System.Data;
using System.Threading.Channels;

namespace MyGame
{
    public class StartGame
    {
        private static Random random = new Random();

        private static int countEnemyHealth = 3;
        private static int countPlayerHealth = 4;
        private static int countExpereince = 0;
        private static int globalExperience = 1;
        private static int playerLevel = 1;
        private static int playerExperience = 0;
        private static int playerHealth;
        private static int playerDamage = 1;
        private static int enemyLevel = 1;
        private static int enemyHealth;
        private static int enemyDamage = 1;
        private static bool fightState = false;
        private static bool enemy = false;        
        private static bool enemyFresh = false;
        private static bool isHeroAttack=false;
        private static bool isEnemyAttack=false;
        private static bool isEnemyOn = false;
        private static bool isEnemySearch = false;
        private static bool isEnemyDied = false;

        private static int counterAction = 1;
        private static bool Exit=false;

        public static void Main(string[] args)
        {
            playerHealth = countPlayerHealth;
            while (!Exit)
            {
                Console.Clear();
                WorldTurn();
                Status();
                PerformPlayerAction();
                PerformEnemyAction();             
                                               
            }           
        }
        static void WorldTurn()
        {
            Console.WriteLine();
            Console.WriteLine($" \t\t <=== ход {counterAction++} ===>");

            fightState = enemy is true;
            if (enemy)
                enemyFresh = false;

            if (!enemy)
            {
                int chance = random.Next(0, 100);

                if (chance > 80)
                {
                    Console.WriteLine($" В дверях вашей лочуги появился враг!");
                    isEnemySearch = false;
                    enemy = true;
                    fightState=true;
                    enemyHealth = countEnemyHealth;
                }
                else
                {
                    Console.WriteLine($" В мире ничего не произошло");
                }
            }
        }

        static void Status()
        {
            Console.WriteLine($" Состояние героя: {(fightState ? "в бою\n  " : "в покое\n")}");
            if (isEnemyOn)
            {
                Console.WriteLine($" Результат поиска:\t   |=> Враг найден!\n");
                isEnemyOn = false;
                isEnemySearch=false;
                isEnemyAttack = false;
            }
            if (isEnemySearch)
            {
                Console.WriteLine($" Результат поиска: \t   |=> Поиск врага\n");
            }
            
            Console.WriteLine($"{(isHeroAttack? $"|=> Герой нанес {playerDamage}  урона" :"\t\t\t")} " +
                $"  || Уровень героя {playerLevel}\n" +
                $"\t\t\t   || Жизни героя {playerHealth}\n");
            if (isEnemyDied)
            {
                Console.WriteLine($"\t\t\t   < Враг повержен! >");
                isEnemyDied = false;
            }

            if (fightState)
            {
                Console.WriteLine($"{(isEnemyAttack? $"|=> Враг нанес {enemyDamage} урона  " : "\t\t\t")}" +
                    $"   || Уровень врага {enemyLevel}");
                Console.WriteLine($"\t\t\t   || Жизни врага {enemyHealth}");
            }            
        }

        static void PerformPlayerAction()
        {
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

            Console.Write($" Выбрать действие:");
            string action = Console.ReadLine();

            switch (action)
            {
                case "1":
                    if (enemy)
                    {
                        int chance = random.Next(0, 100);

                        if (chance > 20)
                        {
                            enemyHealth -= playerDamage;
                            if (enemyHealth > 0)
                            {
                                Console.WriteLine($" \t\t\t   |=> Герой нанес {playerDamage} урона");
                                isHeroAttack = true;
                            }
                            else
                            {
                                Console.WriteLine($" \t\t\t   |=> Герой нанес сокрушительный удар");
                                Console.WriteLine($"\t\t\t    < Враг повержен! >");
                                isEnemyDied = true;
                                Console.WriteLine($" Герой получил {globalExperience} опыта");
                                playerExperience += globalExperience;
                                countExpereince++;
                                if (countExpereince > 2)
                                {
                                    playerLevel++;
                                    Console.WriteLine($"\t\t\t Герой достиг {playerLevel} уровня! ");
                                    playerDamage++;
                                    countPlayerHealth++;
                                    countExpereince = 0;
                                }
                                enemy = false;
                                playerHealth = countPlayerHealth;
                                isHeroAttack=false;
                                
                            }
                        }
                        else
                        {
                            Console.WriteLine($"\t\t\t   |=> Герой промахнулся");
                        }
                    }
                    break;
                case "2":
                    if (enemy)
                    {
                        int chance = random.Next(0, 100);
                        if (chance > 20)
                        {
                            Console.WriteLine($" Герой сбежал от врага");
                            enemy = false;
                            isHeroAttack = false;

                        }
                        else
                        {
                            Console.WriteLine($" Побег неудался");
                            isEnemySearch = false;
                            isHeroAttack = false;

                        }
                    }
                    break;
                case "3":
                    if (enemy)
                    {
                        Console.WriteLine($" Враг уже есть, повторите действие");
                        
                    }
                    else
                    {
                        int chance = random.Next(0, 100);

                        if (chance > 20)
                        {
                            isEnemyOn = true;
                            Console.WriteLine($" Результат поиска:\t   |=> Враг найден!");
                            enemyHealth = countEnemyHealth;
                            enemy = true;
                            enemyFresh = true;

                        }
                        else
                        {
                            isEnemySearch = true;
                            Console.WriteLine($" Результат поиска: \t   |=> Поиск врага");
                                isHeroAttack=false;

                        }
                    }
                    break;
            }

        }

        static void PerformEnemyAction()
        {
            if (enemy)
            {               
                if (enemy && enemyFresh == false)
                {
                    if (enemyHealth > 0)
                    {
                        int chance = random.Next(0, 100);
                        if (chance > 20)
                        {
                            isEnemyAttack = true;
                            Console.WriteLine($"\t\t\t   |=> Враг нанес {enemyDamage} урона");
                            playerHealth -= 1;
                        }
                        else
                        {
                            Console.WriteLine($"\t\t\t   |=> Герой увернулся");
                        }
                    }

                    if (playerHealth <= 0)
                    {
                        Console.Clear();
                        Console.WriteLine($" Герой погиб!\t|| Начать заново?");
                        Console.WriteLine($" Возможные действия:");
                        Console.WriteLine($" 1) = Начать заново");
                        Console.WriteLine($" 2) = Покинуть игру");

                        string actionEsc = Console.ReadLine();

                        switch (actionEsc)
                        {
                            case "1":
                                counterAction = 1;
                                Console.Clear();
                                playerHealth = countPlayerHealth;
                                enemyHealth = countEnemyHealth;                                
                                playerExperience = 0;
                                enemy = false;
                                isHeroAttack = false;

                                break;

                            case "2":
                                Console.Clear();
                                Console.WriteLine($" Выход из игры");
                                Exit=true;
                                break;

                            default:
                                Console.Clear();
                                Console.WriteLine($" Выход из игры");
                                Exit = true;
                                break;
                        }
                    }
                }
                
            }

        }
    }
}