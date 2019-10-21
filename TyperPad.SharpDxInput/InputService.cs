using System;
using System.Collections.Generic;
using System.Linq;
using SharpDX.DirectInput;
using TyperPad.Common.Enum;
using TyperPad.Common.Interface;
using TyperPad.Common.Model;

namespace TyperPad.SharpDxInput
{
    public class InputService : IInput
    {
        private Joystick _joystick = null;
        private bool _isInit = false;

        public Guid? Init()
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
            if (joystickGuid == Guid.Empty)
                return null;

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

            _isInit = true;
            return joystickGuid;
        }

        public InputState GetState(Settings.GamepadSettings settings)
        {
            var state = GetCurrentState();

            var x = state.X - 32787; //todo const
            var y = state.Y - 32787; //todo const
            var leftStick = new InputState.Stick()
            {
                Angle = new Angle(0),
                Length = 0
            };

            var length = (int) Math.Sqrt(x * x + y * y);
            if (length > 20000) //todo const
            {
                int angle = (int) (Math.Atan2(y, x) * 180 / Math.PI);
                leftStick.Angle = new Angle(angle + 90);
                leftStick.Length = length;
            }

            x = state.Z - 32787; //todo const
            y = state.RotationZ - 32787; //todo const
            var rightStick = new InputState.Stick()
            {
                Angle = new Angle(0),
                Length = 0
            };
            length = (int) Math.Sqrt(x * x + y * y);
            if (length > 20000) //todo const
            {
                int angle = (int) (Math.Atan2(y, x) * 180 / Math.PI);
                rightStick.Angle = new Angle(angle + 90);
                rightStick.Length = length;
            }

            var buttons = new List<EButton>();
            if (state.Buttons.Any())
                foreach (var button in settings.Buttons)
                {
                    if (state.Buttons[button.Key])
                        buttons.Add(button.Value);
                }

            if (state.PointOfViewControllers.Any())
                foreach (var directionButton in settings.DirectionButtons)
                {
                    if (state.PointOfViewControllers[0] == directionButton.Key)
                        buttons.Add(directionButton.Value);
                }

            return new InputState()
            {
                Buttons = buttons,
                LeftStick = leftStick,
                RightStick = rightStick
            };
        }

        public InputRawState GetRawState()
        {
            var state = GetCurrentState();
            
            var result = new InputRawState();
            for (int i = 0; i < state.Buttons.Length; i++)
            {
                if(state.Buttons[i])
                    result.Buttons.Add(i);
            }

            if (state.PointOfViewControllers[0] > -1)
                result.DirectionButton = state.PointOfViewControllers[0];

            return result;
        }

        private JoystickState GetCurrentState()
        {
            if (!_isInit)
                throw new Exception("need init");

            if (_joystick == null)
                throw new Exception("Gamepad not connected");

            _joystick.Poll();
            return _joystick.GetCurrentState();
        }
    }
}