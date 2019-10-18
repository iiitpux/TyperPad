using System;
using Newtonsoft.Json;
using TyperPad.Common.Interface;
using TyperPad.Common.Model;

namespace TyperPad.Fake
{
    public class FakeOutputService : IOutput
    {
        public void Init()
        {
        }

        public void Send(OutputState state)
        {
            Console.WriteLine(JsonConvert.SerializeObject(state));
        }
    }
}