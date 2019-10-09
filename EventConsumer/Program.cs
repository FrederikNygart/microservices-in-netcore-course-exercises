using System;

namespace EventConsumer
{
    class Program
    {
        static void Main(string[] args)
        {
          var subscriber = new EventSubscriber("localhost:5000");
          subscriber.Start();
        }
    }
}
