using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace MyGame
{
    public class Logger
    {
        private const int maxLogElements= 10;
        private  Queue<string> _queue;

       
        public Logger()
        {
           _queue=new Queue<string>();
        }

        public void AddLog(string log)
        {           
            _queue.Enqueue(log);

            while (_queue.Count > maxLogElements)
                _queue.Dequeue();
        }
        public void ShowLog()
        {            
            while (_queue.Count > maxLogElements)
                _queue.Dequeue();

            foreach (var str in _queue)
                Console.WriteLine(str);
        }

        public void Clear()
        {
            _queue.Clear();
        }
    }
}
