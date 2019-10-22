using System;
using System.Collections.Generic;
using System.Text;
using TyperPad.Common.Enum;

namespace TyperPad.Common.Model
{
    public class InputRawState
    {
        public List<int> Buttons { set; get; } = new List<int>();
        public int DirectionButton { set; get; }
        public Stick RightStick { set; get; }
        public Stick LeftStick { set; get; }

        public class Stick
        {
            public int X { set; get; }
            public int Y { set; get; }
        }
    }

    
}
