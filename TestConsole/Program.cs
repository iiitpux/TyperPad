using System;
using TyperPad.Core;
using TyperPad.Fake;
using TyperPad.SharpDxInput;

namespace TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var service = new Service(new InputService(),  new FakeOutputService(), null);
            service.Init();
            service.Run();
            Console.ReadLine();
            service.Stop();
            
        }
    }
}