﻿using System;
using System.Collections.Generic;
using System.Threading;
using TyperPad.Common.Enum;
using TyperPad.Common.Interface;
using TyperPad.Common.Model;

namespace TyperPad.Fake
{
    public class FakeInputService : IInput
    {
        public Guid? Init()
        {
            return Guid.NewGuid();
        }

        public InputState GetState(Settings.GamepadSettings settings)
        {
            Thread.Sleep(10000);
            return new InputState()
            {
                Buttons = new List<EButton>()
                {
                    EButton.RSecond
                },
                LeftStick = new InputState.Stick()
                {
                    Angle =  new Angle(1),
                    Length = 20000
                },
                RightStick = new InputState.Stick()
            };
        }

        public InputRawState GetRawState()
        {
            throw new NotImplementedException();
        }
    }
}