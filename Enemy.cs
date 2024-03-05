namespace MyGame
{
    public class Enemy
    {   
        public int Health { get; private set; } = 3;
        public int Damage { get; private set; } = 1;
        public int Level { get; private set; } = 1;
        public bool IsAlive => Health > 0;
        
        private Queue<string> _logQueue;

        public Enemy(Queue<string> logQueue)
        {
            _logQueue = logQueue;
        }
        
        public void Hit(int damage)
        {
            Health -= damage;
            
            if (!IsAlive)
            {
                _logQueue.Enqueue($" Enemy received {damage} damage");
            }
            else
            {
                _logQueue.Enqueue($" Enemy received {damage} damage, and died.");
            }
        }
    }
}
