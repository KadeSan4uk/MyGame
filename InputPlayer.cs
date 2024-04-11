
namespace MyGame
{
    public class InputPlayer
    {
        private IPlayerActionStrategy? _strategy;
        public enum PlayerAction
        {
            Hit = 1,
            Escape,
            Search
        }

        public void SetStrategy(IPlayerActionStrategy strategy)
        {
            _strategy=strategy;
        }
        public PlayerAction GetPlayerAction(Player _player)
        {

            if (_player.Enemy is not null)
            {
                _strategy = new OnEnemyStrategy();
            }
            else
            {
                _strategy = new OffEnemyStrategy();
            }

            return _strategy.GetPlayerAction(_player);
        }
    }

}
