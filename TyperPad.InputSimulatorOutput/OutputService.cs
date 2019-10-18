using System;
using System.Collections.Generic;
using System.Linq;
using WindowsInput;
using WindowsInput.Native;
using TyperPad.Common.Interface;
using TyperPad.Common.Model;

namespace TyperPad.InputSimulatorOutput
{
    public class OutputService : IOutput
    {
        private InputSimulator _simulator;
        private Dictionary<string, VirtualKeyCode> _map;
        private bool _isInit = false;

        public void Init()
        {
            _simulator = new InputSimulator();
            _map = FillDictionary();
            _isInit = true;
        }


        public void Send(OutputState state)
        {
            if(!_isInit)
                throw new Exception("Need init");
                
            if (state?.Key == null &&
                (state?.Modificators == null || (state.Modificators != null && !state.Modificators.Any())))
                return;

            VirtualKeyCode? action = null;
            if (_map.ContainsKey(state.Key.Keyword))
                action = _map[state.Key.Keyword];

            var modificators = new List<VirtualKeyCode>();
            foreach (var mod in state.Modificators)
            {
                if (_map.ContainsKey(mod.Keyword))
                    modificators.Add(_map[mod.Keyword]);
            }

            if (action.HasValue && modificators.Any())
            {
                _simulator.Keyboard.ModifiedKeyStroke(modificators, action.Value);
            }
            else if (action.HasValue)
            {
                _simulator.Keyboard.KeyPress(action.Value);
            }
        }

        private Dictionary<string, VirtualKeyCode> FillDictionary()
        {
            var result = new Dictionary<string, VirtualKeyCode>();
            result.Add(Key.Esc.Keyword, VirtualKeyCode.ESCAPE);
            //todo
            //result.Add(Key.Esc.Tilda, VirtualKeyCode.OEM_3);
            
            result.Add(Key.Key1.Keyword, VirtualKeyCode.VK_1);
            result.Add(Key.Key2.Keyword, VirtualKeyCode.VK_2);
            result.Add(Key.Key3.Keyword, VirtualKeyCode.VK_3);
            result.Add(Key.Key4.Keyword, VirtualKeyCode.VK_4);
            result.Add(Key.Key5.Keyword, VirtualKeyCode.VK_5);
            result.Add(Key.Key6.Keyword, VirtualKeyCode.VK_6);
            result.Add(Key.Key7.Keyword, VirtualKeyCode.VK_7);
            result.Add(Key.Key8.Keyword, VirtualKeyCode.VK_8);
            result.Add(Key.Key9.Keyword, VirtualKeyCode.VK_9);
            result.Add(Key.Key0.Keyword, VirtualKeyCode.VK_0);
            result.Add(Key.KeyA.Keyword, VirtualKeyCode.VK_A);
            result.Add(Key.KeyB.Keyword, VirtualKeyCode.VK_B);
            result.Add(Key.KeyC.Keyword, VirtualKeyCode.VK_C);
            result.Add(Key.KeyD.Keyword, VirtualKeyCode.VK_D);
            result.Add(Key.KeyE.Keyword, VirtualKeyCode.VK_E);
            result.Add(Key.KeyF.Keyword, VirtualKeyCode.VK_F);
            result.Add(Key.KeyG.Keyword, VirtualKeyCode.VK_G);
            result.Add(Key.KeyH.Keyword, VirtualKeyCode.VK_H);
            result.Add(Key.KeyI.Keyword, VirtualKeyCode.VK_I);
            result.Add(Key.KeyJ.Keyword, VirtualKeyCode.VK_J);
            result.Add(Key.KeyK.Keyword, VirtualKeyCode.VK_K);
            result.Add(Key.KeyL.Keyword, VirtualKeyCode.VK_L);
            result.Add(Key.KeyM.Keyword, VirtualKeyCode.VK_M);
            result.Add(Key.KeyN.Keyword, VirtualKeyCode.VK_N);
            result.Add(Key.KeyO.Keyword, VirtualKeyCode.VK_O);
            result.Add(Key.KeyP.Keyword, VirtualKeyCode.VK_P);
            result.Add(Key.KeyQ.Keyword, VirtualKeyCode.VK_Q);
            result.Add(Key.KeyR.Keyword, VirtualKeyCode.VK_R);
            result.Add(Key.KeyS.Keyword, VirtualKeyCode.VK_S);
            result.Add(Key.KeyT.Keyword, VirtualKeyCode.VK_T);
            result.Add(Key.KeyU.Keyword, VirtualKeyCode.VK_U);
            result.Add(Key.KeyV.Keyword, VirtualKeyCode.VK_V);
            result.Add(Key.KeyW.Keyword, VirtualKeyCode.VK_W);
            result.Add(Key.KeyX.Keyword, VirtualKeyCode.VK_X);
            result.Add(Key.KeyY.Keyword, VirtualKeyCode.VK_Y);
            result.Add(Key.KeyZ.Keyword, VirtualKeyCode.VK_Z);
            result.Add(Key.Minus.Keyword, VirtualKeyCode.OEM_MINUS);
            result.Add(Key.Equal.Keyword, VirtualKeyCode.OEM_PLUS);
            result.Add(Key.Backspace.Keyword, VirtualKeyCode.BACK);
            result.Add(Key.Tab.Keyword, VirtualKeyCode.TAB);
            result.Add(Key.BraceLeft.Keyword, VirtualKeyCode.OEM_4);
            result.Add(Key.BraceRight.Keyword, VirtualKeyCode.OEM_6);
            result.Add(Key.BackSlash.Keyword, VirtualKeyCode.OEM_5);
            result.Add(Key.Semicolon.Keyword, VirtualKeyCode.OEM_1);
            result.Add(Key.Quote.Keyword, VirtualKeyCode.OEM_7);
            result.Add(Key.Enter.Keyword, VirtualKeyCode.RETURN);
            result.Add(Key.Comma.Keyword, VirtualKeyCode.OEM_COMMA);
            result.Add(Key.Dot.Keyword, VirtualKeyCode.OEM_PERIOD);
            result.Add(Key.Slash.Keyword, VirtualKeyCode.OEM_2);
            result.Add(Key.Space.Keyword, VirtualKeyCode.SPACE);
            result.Add(Key.Home.Keyword, VirtualKeyCode.HOME);
            result.Add(Key.End.Keyword, VirtualKeyCode.END);
            result.Add(Key.Delete.Keyword, VirtualKeyCode.DELETE);
            result.Add(Key.Pause.Keyword, VirtualKeyCode.PAUSE);
            result.Add(Key.Break.Keyword, VirtualKeyCode.CANCEL);
            result.Add(Key.PrintScreen.Keyword, VirtualKeyCode.SNAPSHOT);
            result.Add(Key.PageUp.Keyword, VirtualKeyCode.PRIOR);
            result.Add(Key.PageDown.Keyword, VirtualKeyCode.NEXT);
            result.Add(Key.ArrowUp.Keyword, VirtualKeyCode.UP);
            result.Add(Key.ArrowDown.Keyword, VirtualKeyCode.DOWN);
            result.Add(Key.ArrowLeft.Keyword, VirtualKeyCode.LEFT);
            result.Add(Key.ArrowRight.Keyword, VirtualKeyCode.RIGHT);
            result.Add(Key.Alt.Keyword, VirtualKeyCode.MENU);
            result.Add(Key.Win.Keyword, VirtualKeyCode.LWIN);
            return result;
        }
    }
}