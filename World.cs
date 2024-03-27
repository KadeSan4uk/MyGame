namespace MyGame
{
    public class World
    {
        private Random random = new Random();
        private Logger _log;

        private Player _player;
        private Enemy? _currentEnemy;
        private int _currentRound = 1;

        public World(Logger log)
        {
            _log = log;
            _player = CreatePlayer();
        }

        public bool IsRunning => true;

        public void PrintStatus()
        {
            _currentEnemy?.HealthStatus();
            _player.StatusPlayer();
        }

        public void PerformGlobalAction()
        {
            Console.WriteLine();
            Console.WriteLine($" \t <=== Ход {_currentRound} ===>");

            if (_currentEnemy is null)
            {
                if (TryGenerateNewEnemy(out var newEnemy))
                {
                    _currentEnemy = newEnemy;
                    _player.SetEnemy(_currentEnemy);
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
            _player.PerformPlayerAction();

            if (_currentEnemy != null &&
                _currentEnemy.TryHit(out var damage))
            {
                _player.Hit(damage);
            }
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

        private void OnPlayerFindEnemy()
        {
            _currentEnemy = CreateEnemy();
            _player.SetEnemy(_currentEnemy);
        }

        private void OnEnemyDie()
        {
            if (_currentEnemy != null)
            {
                _player.UpdateExperience(_currentEnemy.DieExperience);
                _player.UpdateDamageHealth();
            }

            _currentEnemy = null;
            _player.SetEnemy(null);
        }

        private Player CreatePlayer()
        {
            var player = new Player(_log);

            player.DiedEvent += OnPlayerDied;
            player.FindEnemyEvent += OnPlayerFindEnemy;

            return player;
        }

        private Enemy CreateEnemy()
        {
            var enemy = new Enemy(_log);

            enemy.DieEvent += OnEnemyDie;

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
                    //currentRound = 0;
                    Console.Clear();
                    _log.Clear();
                    _player = CreatePlayer();
                    _currentEnemy = null;
                    break;

                case "2":
                    Console.Clear();
                    Console.WriteLine($" Вышли из игры");
                    //exit = true;
                    break;

                default:
                    Console.Clear();
                    Console.WriteLine($" Вышли из игры");
                    //exit = true;
                    break;
            }
        }
    }
}