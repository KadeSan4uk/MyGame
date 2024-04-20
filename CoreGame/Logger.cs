namespace MyGame.CoreGame
{
    public class Logger
    {
        private const int MaxLogElements = 10;
        private Queue<string> _queue;
        private Action<string> _printAction;


        public Logger(Action<string> printAction)
        {
            _queue = new Queue<string>();
            _printAction = printAction;
        }

        public void AddLog(string log)
        {
            _queue.Enqueue(log);

            while (_queue.Count > MaxLogElements)
                _queue.Dequeue();
        }

        public void ShowLog()
        {
            while (_queue.Count > MaxLogElements)
                _queue.Dequeue();

            foreach (var str in _queue)
                _printAction?.Invoke(str);
        }

        public void Clear()
        {
            _queue.Clear();
        }
    }
}