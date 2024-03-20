using System.Text;

namespace MyGame
{
	public class Program
	{
		private static Random random;
		private static Logger logger;
		private static Player player;
		private static Actions _action;
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
            _action = new Actions(logger);
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
			Console.WriteLine($" Состояние игрока: {(enemy is not null ? "в бою\n  " : "в покое\n")}");

			player.HealthStatus();

            if (enemy is not null)
			{
				enemy.EnemyHealthStatus();
            }
		}

		static void PerformPlayerAction()
		{
			if (enemy is null)
			{
				_action.ActionSearch();
			}
			else
			{
				_action.ActionAttack();
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
								player.GetExperience();
								player.UpdateDamageHealth();                                                                                            
                                enemy = null;

                                if (player.Experience > 2)
								{
									player.UpdateExperience();
								}						
							}
						}
						else
						{
							player.Miss();
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
							player.EscapeLuck();
							enemy = null;
						}
						else
						{
                            player.EscapeFalse();
						}
					}

					break;
				case "3":
					if (enemy is  null)																	
					{
						int chance = random.Next(0, 100);

						if (chance > 20)
						{
							_action.EnemySearchLuck();
							createEnemy = true;
						}
						else
						{
                            _action.EnemySearchFalse();
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
						enemy.EnemyMiss();
					}
				}

				if (player.Health <= 0)
				{
					_action.PlayerIsDead();

					string? actionEsc = Console.ReadLine();

					switch (actionEsc)
					{
						case "1":
							currentRound = 0;
							Console.Clear();
							logger.Clear();
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