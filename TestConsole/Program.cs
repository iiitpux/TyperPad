using System;
using Microsoft.EntityFrameworkCore;
using TyperPad.Core;
using TyperPad.Fake;
using TyperPad.InputSimulatorOutput;
using TyperPad.SharpDxInput;
using TyperPad.SqliteDataStore.Dto;

namespace TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var service = new Service(new InputService(),  new OutputService(), null);
            service.Init();
            service.Run();
            Console.ReadLine();
            service.Stop();
        }
    }
}