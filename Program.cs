using System.Text;

namespace MyGame
{
	public class Program
	{
		private static Random random = new();
		private static Queue<string> logQueue = new();

		private static int basePlayerHealth = 4;
		private static int playerExpereince = 0;
		private static int experienceGain = 1;
		private static int playerLevel = 1;
		private static int playerHealth;
		private static int playerDamage = 1;

		private static Enemy? enemy = null;
		private static bool createEnemy = false;

		private static int currentRound = 1;
		private static bool exit = false;

		public static void Main(string[] args)
		{
			playerHealth = basePlayerHealth;
			
			while (!exit)
			{
				Console.Clear();
				ShowLog();
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
			Console.WriteLine($" \t\t <=== Round {currentRound} ===>");

			if (enemy is null)
			{
				int chance = random.Next(0, 100);

				if (chance > 80)
				{
					enemy = new Enemy(logQueue);
					AddLog($" An enemy has appeared at the door of your shack !");
				}
				else
				{
					AddLog($" Nothing happened in the world");
				}
			}
		}

		static void Status()
		{
			Console.WriteLine(
				$" Player state: {(enemy is not null ? "in battle\n  " : "at rest\n")}");

			Console.WriteLine($"Player Level:\t {playerLevel}");
			Console.WriteLine($"Player HP:\t {playerHealth}");

			if (enemy is not null)
			{
				Console.WriteLine($"Enemy Level:\t {enemy.Level}");
				Console.WriteLine($"Enemy HP:\t {enemy.Health}");
			}
		}

		static void PerformPlayerAction()
		{
			Console.WriteLine();
			
			if (enemy is null)
			{
				Console.WriteLine($" Possible action:");
				Console.WriteLine($" 3) = look for the enemy");
			}
			else
			{
                Console.WriteLine($" Possible actions:");
                Console.WriteLine($" 1) = Attack");
				Console.WriteLine($" 2) = Run away");
			}
			
			string? action = Console.ReadLine();

			switch (action)
			{
				case "1":
					if (enemy is not null)
					{
						int chance = random.Next(0, 100);

						if (chance > 20)
						{
							enemy.Hit(playerDamage);
							
							if (enemy.IsAlive is false)
							{
								AddLog($" Player received {experienceGain} experience");
								
								playerExpereince += experienceGain;

								if (playerExpereince > 2)
								{
									playerLevel++;
									AddLog($" Player has reached level {playerLevel} !");
									playerDamage++;
									basePlayerHealth++;
									playerExpereince = 0;
								}

								enemy = null;
								playerHealth = basePlayerHealth;
							}
						}
						else
						{
							AddLog($" Player missed");
						}
					}

					break;
				case "2":
					if (enemy is not null)
					{
						int chance = random.Next(0, 100);
						if (chance > 20)
						{
							AddLog($" Player escaped from the enemy");
							enemy = null;
						}
						else
						{
							AddLog($" Escape failed");
						}
					}

					break;
				case "3":
					if (enemy is not null)
					{
						Console.WriteLine($" Enemy is already there, repeat the action");
					}
					else
					{
						int chance = random.Next(0, 100);

						if (chance > 20)
						{
							AddLog($" Search result: Enemy found!");
							createEnemy = true;
						}
						else
						{
							AddLog($" Search result: Nobody");
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
						AddLog($" Enemy dealt {enemy.Damage} damage");
						playerHealth -= enemy.Damage;
					}
					else
					{
						AddLog($" Enemy missed");
					}
				}

				if (playerHealth <= 0)
				{
					Console.Clear();
					logQueue.Clear();
					Console.WriteLine($" Player died!\t|| Start again?");
                    Console.WriteLine($" Possible actions:");
                    Console.WriteLine($" 1) = Start again");
					Console.WriteLine($" 2) = Leave the game");

					string? actionEsc = Console.ReadLine();

					switch (actionEsc)
					{
						case "1":
							currentRound = 1;
							Console.Clear();
							playerHealth = basePlayerHealth;
							enemy = null;
							break;

						case "2":
							Console.Clear();
							Console.WriteLine($" Left the game");
							exit = true;
							break;

						default:
							Console.Clear();
                            Console.WriteLine($" Left the game");
                            exit = true;
							break;
					}
				}
			}
			else if (createEnemy)
			{
				enemy = new Enemy(logQueue);
				createEnemy = false;
			}
		}

		private static void AddLog(string log)
		{
			const int maxLogElements = 10;

			logQueue.Enqueue(log);

			while (logQueue.Count > maxLogElements)
				logQueue.Dequeue();
		}

		private static void ShowLog()
		{
			const int maxLogElements = 10;
			
			while (logQueue.Count > maxLogElements)
				logQueue.Dequeue();
			
			foreach (var str in logQueue)
				Console.WriteLine(str);
		}
	}
}