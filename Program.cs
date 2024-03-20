using System.Text;

namespace MyGame
{
	public class Program
	{
		private static Random random;
		private static Logger logger;
		private static Player player;
        private static Enemy? enemy = null;
		private static bool missChance=false;
		private static bool createEnemy = false;
        private static int experience = 1;
        private static int currentRound = 1;
		private static bool exit = false;
		private static string namePlayer = "";
		private static bool isPlayerName=false;
        

        public static void Main(string[] args)
		{
            random = new();
            logger = new Logger();
            player = new Player(logger);
            while (!exit)
			{
				Console.Clear();
				logger.ShowLog();
				WorldTurn();
				Status();
				PerformPlayerAction();
				PerformEnemyAction();
				currentRound++;
			}
		}

		static void WorldTurn()
		{		
            Console.WriteLine();
			Console.WriteLine($" \t <=== Ход {currentRound} ===>");

			if (enemy is null)
			{
				int chance = random.Next(0, 100);

				if (chance > 50)
				{
					enemy = new Enemy(logger);
					Console.WriteLine($" В дверях вашей лочуги появился враг!");
				}
				else
				{
					Console.WriteLine($" В мире ничего не произошло");
				}
			}
		}

		static void Status()
		{
			Console.WriteLine(
				$" Состояние игрока: {(enemy is not null ? "в бою\n  " : "в покое\n")}");

			Console.WriteLine($" Игрок уровень:\t {player.Level}");
			Console.WriteLine($" \t жизни:\t {player.Health}\n");

			if (enemy is not null)
			{
				Console.WriteLine($"  Враг уровень:\t {enemy.Level}");
				Console.WriteLine($"   \t жизни:\t {enemy.Health}\n");
			}
		}

		static void PerformPlayerAction()
		{						
			if (enemy is null)
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

            string? action = Console.ReadLine();			

            switch (action)
			{
				case "1":
					if (enemy is not null)
					{
						int chance = random.Next(0, 100);
						if (missChance)
						{
							chance = 99;
						}

						if (chance > 20)
						{							
                            enemy.Hit(player.Damage);
							missChance=false;
							
							if (enemy.IsAlive is false)
							{
								logger.AddLog($" Игрок получил {experience} опыта");

								player.UpdateDamageHealth();                                
                                player.Experience += experience;                                
                                enemy = null;

                                if (player.Experience > 2)
								{
									player.Level++;
									logger.AddLog($" Игрок достиг {player.Level} уровня!");
                                    logger.AddLog($" Жизни + 50, Урон + 50");
                                    player.ProgressDamageHealth(50, 50);
                                    player.UpdateDamageHealth();
                                    player.Experience = 0;
								}						
							}
						}
						else
						{
							logger.AddLog($" Игрок промахнулся");
							missChance = true;							
						}
					}

					break;
				case "2":
					if (enemy is not null)
					{
						int chance = random.Next(0, 100);
						if (chance > 20)
						{
							logger.AddLog($" Побег удался");
							enemy = null;
						}
						else
						{
                            logger.AddLog($" Неудачная попытка побега");
						}
					}

					break;
				case "3":
					if (enemy is not null)
					{
						Console.WriteLine($" Враг уже есть, повторите действие");
					}
					else
					{
						int chance = random.Next(0, 100);

						if (chance > 20)
						{
                            logger.AddLog($" Результат поиска: Враг найден!");
							createEnemy = true;
						}
						else
						{
                            logger.AddLog($" Результат поиска: Никого");
						}
					}
					break;
			}
		}

		static void PerformEnemyAction()
		{
			if (enemy is not null)
			{
				if (enemy.Health > 0)
				{
					int chance = random.Next(0, 100);
					
					if (chance > 20)
					{
						player.Hit(enemy.Damage);
					}
					else
					{
						logger.AddLog($" Враг промахнулся");
					}
				}

				if (player.Health <= 0)
				{
					Console.Clear();
					logger.Clear();
					Console.WriteLine($" Игрок погиб!\t|| Начать заново?");
                    Console.WriteLine($" Возможные действия:");
                    Console.WriteLine($" 1) = Начать заново");
					Console.WriteLine($" 2) = Покинуть игру");

					string? actionEsc = Console.ReadLine();

					switch (actionEsc)
					{
						case "1":
							currentRound = 0;
							Console.Clear();
                            player = new Player(logger);
                            enemy = null;
							break;

						case "2":
							Console.Clear();
							Console.WriteLine($" Вышли из игры");
							exit = true;
							break;

						default:
							Console.Clear();
                            Console.WriteLine($" Вышли из игры");
                            exit = true;
							break;
					}
				}
			}
			else if (createEnemy)
			{
				enemy = new Enemy(logger);
				createEnemy = false;
			}
		}	

		private static void PlayerName()
		{
            while (!isPlayerName)
            {
                Console.WriteLine($"Введите имя: максимум 10 символов.");
                namePlayer = Console.ReadLine();
                if (namePlayer.Length < 11)
                {
                    isPlayerName = true;
                }
                else
                {
                    Console.WriteLine($"Недопустимое количество символов.");
                    isPlayerName = false;
                }
            }
        }
	}
}