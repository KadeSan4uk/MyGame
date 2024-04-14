
namespace MyGame
{
    public class InputPlayer
    {
        private IPlayerInputActionStrategy? _strategy;
        public enum PlayerAction
        {
            None,
            Hit,
            Escape,
            Search
        }

        public void SetStrategy(IPlayerInputActionStrategy strategy)
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
