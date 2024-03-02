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
			Console.WriteLine($" \t\t <=== ход {currentRound} ===>");

			if (enemy is null)
			{
				int chance = random.Next(0, 100);

				if (chance > 80)
				{
					enemy = new Enemy(logQueue);
					AddLog($"{currentRound} В дверях вашей лочуги появился враг!");
				}
				else
				{
					AddLog($"{currentRound} В мире ничего не произошло");
				}
			}
		}

		static void Status()
		{
			Console.WriteLine(
				$" Состояние героя: {(enemy is not null ? "в бою\n  " : "в покое\n")}");

			Console.WriteLine($"Hero Level:\t {playerLevel}");
			Console.WriteLine($"Hero HP:\t {playerHealth}");

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
				Console.WriteLine($" Возможное действие:");
				Console.WriteLine($" 3) = искать врага");
			}
			else
			{
				Console.WriteLine($" Возможные действия:");
				Console.WriteLine($" 1) = атаковать");
				Console.WriteLine($" 2) = сбежать");
			}

			Console.Write($" Выбрать действие:");
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
								AddLog($"{currentRound} Герой получил {experienceGain} опыта");
								
								playerExpereince += experienceGain;

								if (playerExpereince > 2)
								{
									playerLevel++;
									AddLog($"{currentRound} Герой достиг {playerLevel} уровня! ");
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
							AddLog($"{currentRound} Герой промахнулся");
						}
					}

					break;
				case "2":
					if (enemy is not null)
					{
						int chance = random.Next(0, 100);
						if (chance > 20)
						{
							AddLog($"{currentRound} Герой сбежал от врага");
							enemy = null;
						}
						else
						{
							AddLog($"{currentRound} Побег неудался");
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
							AddLog($"{currentRound} Результат поиска: Враг найден!");
							createEnemy = true;
						}
						else
						{
							AddLog($"{currentRound} Результат поиска: Никого");
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
						AddLog($"{currentRound} Враг нанес {enemy.Damage} урона");
						playerHealth -= enemy.Damage;
					}
					else
					{
						AddLog($"{currentRound} Герой увернулся");
					}
				}

				if (playerHealth <= 0)
				{
					Console.Clear();
					logQueue.Clear();
					Console.WriteLine($" Герой погиб!\t|| Начать заново?");
					Console.WriteLine($" Возможные действия:");
					Console.WriteLine($" 1) = Начать заново");
					Console.WriteLine($" 2) = Покинуть игру");

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
							Console.WriteLine($" Выход из игры");
							exit = true;
							break;

						default:
							Console.Clear();
							Console.WriteLine($" Выход из игры");
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