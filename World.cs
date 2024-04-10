using System;

namespace MyGame
{
    public class World
    {
        private InputPlayer? _inputPlayer;
        private Logger _log;
        private Player _player;
        private Random _random = new();
        private int _currentRound = 1;
        public bool IsRunning { get; set; }

        public World(Logger log)
        {
            _log = log;
            _player = CreatePlayer();
        }

        public void PrintStatus()
        {
            _player.StatusPlayer();
             DrawBars(_player, ConsoleColor.Green);
            _player.HealthStatus();
            if(_player.Enemy is not null)
            {
                DrawBars(_player.Enemy, ConsoleColor.Red);
            }
            _player.Enemy?.HealthStatus();
        }

        public void PerformGlobalAction()
        {
            Console.WriteLine();
            Console.WriteLine($" \t <=== Ход {_currentRound} ===>");

            if (_player.Enemy is null)
            {
                if (TryGenerateNewEnemy(out var newEnemy))
                {
                    _player.Enemy = newEnemy;
                    _player.SetEnemy(_player.Enemy);
                    Console.WriteLine($" В дверях вашей лочуги появился враг!");
                }
                else
                {
                    Console.WriteLine($" В мире ничего не произошло");
                }
            }
        }

        public void PerformActorsActions()
        {
            _inputPlayer = new InputPlayer();

            InputPlayer.PlayerAction action = _inputPlayer.GetPlayerAction(_player);

            switch (action)
            {
                case InputPlayer.PlayerAction.Hit:
                    if (_player.Enemy is not null)
                    {
                        BattleActors();
                    }
                    break;
                case InputPlayer.PlayerAction.Escape:
                    if (_player.Enemy is not null)
                    {
                        _player.TryEscape();
                    }
                    break;
                case InputPlayer.PlayerAction.Search:
                    if (_player.Enemy is null)
                    {
                        TrySearchEnemy();
                    }
                    break;
            }
        }

        public void NextTurn() =>
            _currentRound++;

        private bool TryGenerateNewEnemy(out Enemy? enemy)
        {
            int chance = _random.Next(0, 100);

            if (chance > 50)
            {
                enemy = CreateEnemy();

                return true;
            }

            enemy = null;
            return false;
        }

        public void OnPlayerFindEnemy()
        {
            _player.Enemy = CreateEnemy();
            _player.SetEnemy(_player.Enemy);
        }

        private void OnEnemyDie()
        {
            if (_player.Enemy != null)
            {
                _player.UpExperience(_player.Enemy.DieExperience);
                _player.RestorHealthDamage();
            }

            _player.Enemy = null;
            _player.SetEnemy(null);
        }

        private Player CreatePlayer()
        {
            var player = new Player(_log);

            player.DiedEventPlayer += OnPlayerDied;
            player.FindEnemyEvent += OnPlayerFindEnemy;

            return player;
        }

        private Enemy? CreateEnemy()
        {
            var enemy = new Enemy(_log);

            enemy.DiedEventEnemy += OnEnemyDie;

            return enemy;
        }

        private void OnPlayerDied()
        {
            Console.Clear();
            Console.WriteLine($" Игрок погиб!\t|| Начать заново?");
            Console.WriteLine($" Возможные действия:");
            Console.WriteLine($" 1) = Начать заново");
            Console.WriteLine($" 2) = Покинуть игру");

            string? actionEsc = Console.ReadLine();

            switch (actionEsc)
            {
                case "1":
                    _currentRound = 0;
                    Console.Clear();
                    _log.Clear();
                    _player = CreatePlayer();
                    _player.Enemy = null;
                    break;

                case "2":
                    Console.Clear();
                    Console.WriteLine($" Вышли из игры");
                    IsRunning = true;
                    break;

                default:
                    Console.Clear();
                    Console.WriteLine($" Вышли из игры");
                    IsRunning = true;
                    break;
            }
        }

        public void TrySearchEnemy()
        {
            int chance = _random.Next(0, 100);

            if (chance > 20)
            {
                _log.AddLog($" Результат поиска: Враг найден!");
                _player.Enemy = CreateEnemy();
                _player.SetEnemy(_player.Enemy);
            }
            else
            {
                _log.AddLog($" Результат поиска: Никого");
            }
        }

        public void BattleActors()
        {
            if (_player.Enemy != null)
            {
                _player.Enemy.TryHit(out var damage);

                _player.TakeDamage(damage);

                _player.TryHit(out damage);

                _player.Enemy?.TakeDamage(damage);
            }
        }

        public void DrawBars(IBarDrawable barDrawable, ConsoleColor barColor)
        {
            int MaxHealth = 0;
            int health = 0;              
            barDrawable.GiveHealthForBars(ref health, ref MaxHealth);            
            int PartSize = 10;
            int BarSize = MaxHealth / PartSize;
            int HealthSize = health / BarSize;

            string HealthStatus = $"{health}\\{MaxHealth}";

            int barStartX = 0;
            int barStartY = Console.CursorTop;
            int startX = barStartX + (PartSize - HealthStatus.Length) / 2;

            Console.SetCursorPosition(startX, barStartY - 1);
            Console.Write(HealthStatus);

            Console.SetCursorPosition(barStartX, barStartY);
            Console.Write('[');
            for (int i = 0; i < PartSize; i++)
            {
                if (i < HealthSize)
                {
                    Console.BackgroundColor = barColor;
                    Console.Write(" ");
                }
                else
                {
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.Write(" ");
                }
            }
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Write(']');
        }      
    }
}