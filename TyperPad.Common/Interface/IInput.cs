using TyperPad.Common.Model;
using System;
using System.Collections.Generic;
using System.Text;
using TyperPad.Common.Model;

namespace TyperPad.Common.Interface
{
    public interface IInput
    {
        /// <summary>
        /// Return gamepad guid
        /// </summary>
        /// <returns></returns>
        Guid? Init();
        InputState GetState(Settings.GamepadSettings settings);
        
        /// <summary>
        /// Return state for fill settings
        /// </summary>
        InputRawState GetRawState();
    }
}
