using System;
using System.Collections.Generic;
using System.Text;
using TyperPad.Common.Model;

namespace TyperPad.Common.Interface
{
    public interface IOutput
    {
        void Send(OutputState state);
    }
}
