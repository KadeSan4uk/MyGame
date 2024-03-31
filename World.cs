using System;

namespace MyGame
{
    public class World
    {
        private Logger _log;
        private Player _player;        
        private Random _random=new();
        private int _currentRound = 1;
        public bool IsRunning {get;set;}

        public World(Logger log)
        {
            _log = log;
            _player = CreatePlayer();   
        }       

        public void PrintStatus()
        {
            _player.StatusPlayer(); 
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
            if (_player.Enemy is null)
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

            var action = Console.ReadLine();

            switch (action)
            {
                case "1":
                    if (_player.Enemy is not null)
                    {
                        if (_player.Enemy.TryHit(out var damage))
                        {
                            _player.TakeDamage(damage);
                        }                       

                        if (_player.TryHit(out damage))
                        {
                            _player.Enemy?.TakeDamage(damage);                            
                        }                        
                    }

                    break;
                case "2":
                    if (_player.Enemy is not null)
                    {
                        int chance = _random.Next(0, 100);
                        if (chance > 20)
                        {
                            _player.EscapeLuck();                           
                        }
                        else
                        {
                            _player.EscapeFalse();
                        }
                    }

                    break;
                case "3":
                    if (_player.Enemy is null)
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
                    IsRunning=true;                    
                    break;
            }
        }
    }
}