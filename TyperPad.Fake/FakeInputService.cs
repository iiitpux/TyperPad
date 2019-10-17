using System.Collections.Generic;
using TyperPad.Common.Enum;
using TyperPad.Common.Interface;
using TyperPad.Common.Model;

namespace TyperPad.Fake
{
    public class FakeInputService : IInput
    {
        public InputState GetState()
        {
            return new InputState()
            {
                Buttons = new List<EButton>()
                {
                    EButton.RSecond,
                    EButton.X
                },
                LeftStick = new InputState.Stick()
                {
                    Angle =  new Angle(1),
                    Length = 20000
                },
                RightStick = new InputState.Stick()
            };
        }
    }
}