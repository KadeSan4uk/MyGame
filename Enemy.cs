
namespace MyGame
{
    public class Enemy
    {   
        private const int _Health = 300;
        private const int _Damage = 100;
        private const int _Level = 1;
        public int Health;
        public int Damage;
        public int Level;
        
        
        private Queue<string> _logQueue;

        public Enemy(Queue<string> logQueue)
        {
            _logQueue = logQueue;
            Health=_Health;
            Damage=_Damage;
            Level=_Level;
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
                _logQueue.Enqueue($" Игрок нанес {damage} урона, враг погиб.");
            }
        }
    }
}
