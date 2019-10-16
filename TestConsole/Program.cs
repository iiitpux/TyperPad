using System;
using TyperPad.Core;
using TyperPad.Fake;

namespace TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var service = new Service(new FakeInputService(), new FakeOutputService(), null);
            service.Run();
            Console.ReadLine();
            service.Stop();
            
        }
    }
}