namespace MyGame.CoreGame
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
       
        public PlayerAction GetPlayerAction(Player _player)
        {

            if (_player.Enemy is not null)
            {
                _strategy = new OnEnemyInputStrategy();
            }
            else
            {
                _strategy = new OffEnemyInputStrategy();
            }

            return _strategy.GetPlayerAction(_player);
        }
    }

}
