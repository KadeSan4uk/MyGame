
namespace MyGame
{
    public class Enemy
    {   
        private const int baseHealth = 300;
        private const int baseDamage = 100;
        private const int baseLevel = 1;
        public int Health;
        public int Damage;
        public int Level;
        
        
        private Queue<string> _logQueue;

        public Enemy(Queue<string> logQueue)
        {
            _logQueue = logQueue;
            Health=baseHealth;
            Damage=baseDamage;
            Level=baseLevel;
        }
        public bool IsAlive => Health > 0;

        public void Hit(int damage)
        {
            Health -= damage;
            
            if (IsAlive)
            {
                _logQueue.Enqueue($" Игрок нанес {damage} урона");
            }
            else
            {
                _logQueue.Enqueue($" Враг получил {damage} урона, и погиб.");
            }
        }
    }
}
