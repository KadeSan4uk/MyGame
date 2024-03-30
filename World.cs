namespace MyGame
{
    public class World
    {
        private Random random = new Random();
        private Logger _log;

        private Player _player;
        private Enemy? _enemy;
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
            _enemy?.HealthStatus(_enemy);            
        }

        public void PerformGlobalAction()
        {
            Console.WriteLine();
            Console.WriteLine($" \t <=== Ход {_currentRound} ===>");

            if (_enemy is null)
            {
                if (TryGenerateNewEnemy(out var newEnemy))
                {
                    _enemy = newEnemy;
                    _player.SetEnemy(_enemy);
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
            _player.PerformAction();

            //if (_enemy != null &&
            //    _enemy.TryHit(out var damage))
            //{
            //    _player.Hit(damage);
            //}

        }

        public void NextTurn() =>
            _currentRound++;

        private bool TryGenerateNewEnemy(out Enemy? enemy)
        {
            int chance = random.Next(0, 100);

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
            _enemy = CreateEnemy();
            _player.SetEnemy(_enemy);
        }

        private void OnEnemyDie()
        {
            if (_enemy != null)
            {
                _player.UpExperience(_enemy.DieExperience);
                _player.RestorHealthDamage();
            }

            _enemy = null;
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
                    _enemy = null;
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