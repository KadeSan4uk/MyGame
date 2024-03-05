using System.Text;

namespace MyGame
{
	public class Program
	{
		private static Random random = new();
		private static Queue<string> logQueue = new();
		
		private static int experience=1;
		
		private static Player player = new Player(logQueue);
		private static Enemy? enemy = null;
		private static bool createEnemy = false;

		private static int currentRound = 1;
		private static bool exit = false;

		public static void Main(string[] args)
		{
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
			Console.WriteLine($" \t <=== Ход {currentRound} ===>");

			if (enemy is null)
			{
				int chance = random.Next(0, 100);

				if (chance > 50)
				{
					enemy = new Enemy(logQueue);
					AddLog($" В дверях вашей лочуги появился враг!");
				}
				else
				{
					AddLog($" В мире ничего не произошло");
				}
			}
		}

		static void Status()
		{
			Console.WriteLine(
				$" Состояние игрока: {(enemy is not null ? "в бою\n  " : "в покое\n")}");

			Console.WriteLine($" Игрок уровень:\t {player.Level}");
			Console.WriteLine($" Игрок жизни:\t {player.Health}\n");

			if (enemy is not null)
			{
				Console.WriteLine($" Враг уровень:\t {enemy.Level}");
				Console.WriteLine($" Враг жизни:\t {enemy.Health}\n");
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

						if (chance > 20)
						{
							enemy.Hit(player.Damage);
							
							if (enemy.IsAlive is false)
							{
								AddLog($" Игрок получил {experience} опыта");
								
								player.Experience += experience;

								if (player.Experience > 2)
								{
									player.Level++;
									AddLog($" Игрок достиг {player.Level} уровня!");
									player.Damage++;									
									player.Experience = 0;
								}

								enemy = null;
								player.Health = player.baseHealth;
							}
						}
						else
						{
							AddLog($" Игрок промахнулся");
						}
					}

					break;
				case "2":
					if (enemy is not null)
					{
						int chance = random.Next(0, 100);
						if (chance > 20)
						{
							AddLog($" Побег удался");
							enemy = null;
						}
						else
						{
							AddLog($" Неудачная попытка побега");
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
							AddLog($" Результат поиска: Враг найден!");
							createEnemy = true;
						}
						else
						{
							AddLog($" Результат поиска: Никого");
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
						AddLog($" Враг нанес {enemy.Damage} урона");
						player.Health -= enemy.Damage;
					}
					else
					{
						AddLog($" Враг промахнулся");
					}
				}

				if (player.Health <= 0)
				{
					Console.Clear();
					logQueue.Clear();
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
							player.Health = player.baseHealth;
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