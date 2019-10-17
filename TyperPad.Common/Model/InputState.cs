using System;
using System.Collections.Generic;
using System.Text;
using TyperPad.Common.Enum;

namespace TyperPad.Common.Model
{
    public class InputState
    {
        public List<EButton> Buttons { set; get; }
        public Stick RightStick { set; get; }
        public Stick LeftStick { set; get; }

        public class Stick
        {
            public Angle Angle { set; get; }
            public int Length { set; get; }
        }
    }

    
}
