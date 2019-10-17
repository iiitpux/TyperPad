using System;
using System.Collections.Generic;
using SharpDX.DirectInput;
using TyperPad.Common.Enum;
using TyperPad.Common.Interface;
using TyperPad.Common.Model;

namespace TyperPad.SharpDxInput
{
    public class InputService : IInput
    {
        public InputService() //todo remove init
        {
            Init();
        }

        private Joystick _joystick = null;
        private bool _isInit = false;

        public void Init()
        {
            var directInput = new DirectInput();

            // Find a Joystick Guid
            var joystickGuid = Guid.Empty;

            foreach (var deviceInstance in directInput.GetDevices(DeviceType.Gamepad,
                DeviceEnumerationFlags.AllDevices))
                joystickGuid = deviceInstance.InstanceGuid;

            // If Gamepad not found, look for a Joystick
            if (joystickGuid == Guid.Empty)
                foreach (var deviceInstance in directInput.GetDevices(DeviceType.Joystick,
                    DeviceEnumerationFlags.AllDevices))
                    joystickGuid = deviceInstance.InstanceGuid;

            // If Joystick not found, throws an error
            if (joystickGuid != Guid.Empty)
            {
                // Instantiate the joystick
                _joystick = new Joystick(directInput, joystickGuid);

                Console.WriteLine("Found Joystick/Gamepad with GUID: {0}", joystickGuid);

                // Query all suported ForceFeedback effects
                var allEffects = _joystick.GetEffects();
                foreach (var effectInfo in allEffects)
                    Console.WriteLine("Effect available {0}", effectInfo.Name);

                // Set BufferSize in order to use buffered data.
                _joystick.Properties.BufferSize = 128;

                // Acquire the joystick
                _joystick.Acquire();
            }

            _isInit = true;
        }

        public InputState GetState()
        {
            if (!_isInit)
                throw new Exception("need init");

            if (_joystick == null)
                throw new Exception("Gamepad not connected");

            _joystick.Poll();
            var state = _joystick.GetCurrentState();

            var x = state.X - 32787;
            var y = state.Y - 32787;
            InputState.Stick leftStick = new InputState.Stick()
            {
                Angle = new Angle(0),
                Length = 0
            };

            var length = (int) Math.Sqrt(x * x + y * y);
            if (length>1000)
            {
                int angle = (int)(Math.Atan2(-y, -x) * 180 / Math.PI + 180);
                angle = angle + 90;
                if (angle > 360)
                    angle = angle - 360;
                
                leftStick.Angle = new Angle(angle);
                leftStick.Length = length;
            }

            return new InputState()
            {
                Buttons = new List<EButton>(),
                LeftStick = leftStick
            };
        }
    }
}