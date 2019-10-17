using System;
using System.Collections.Generic;
using System.Linq;
using TyperPad.Common.Enum;

namespace TyperPad.Common.Model
{
    public class Settings
    {
        public List<KeyItem> Keys { private set; get; }
        public List<KeyItem> ModificatorKeys { private set; get; }
        public List<LevelToInputState> Levels { private set; get; }
        public List<StickSector> LeftStick { private set; get; }
        public List<StickSector> RightStick { private set; get; }

        public Settings(List<KeyItem> keys, List<LevelToInputState> levels, List<StickSector> leftStick,
            List<StickSector> rightStick)
        {
            Keys = keys.Where(p=>!p.Key.IsModificator).ToList();
            ModificatorKeys = keys.Where(p=>p.Key.IsModificator).ToList();
            Levels = levels;
            LeftStick = leftStick;
            RightStick = rightStick;
        }

        public class KeyItem
        {
            public Key Key { set; get; }
            public Guid? StickSectorId { set; get; }
            public List<EButton> Buttons { set; get; }
            public int LevelIndex { set; get; }

            private List<string> _pattern = null;

            public List<string> Pattern
            {
                get
                {
                    if (_pattern == null)
                    {
                        _pattern = new List<string>();
                        if (Buttons != null && Buttons.Any())
                            _pattern.AddRange(Buttons.Select(p => p.ToString()));
                        if (StickSectorId.HasValue)
                            _pattern.Add(StickSectorId.Value.ToString());
                    }

                    return _pattern;
                }
            }
        }


        public class StickSector
        {
            public Guid Id { set; get; }
            public Angle MinAngle { set; get; }
            public Angle MaxAngle { set; get; }
            public int MinLength { set; get; }
            public int MaxLength { set; get; }
        }

        public class Level
        {
            public Level(int index)
            {
                Index = index;
            }

            public int Index { private set; get; }
        }

        public class LevelToInputState
        {
            public Level Level { set; get; }
            public Guid? StickSectorId { set; get; }
            public List<EButton> Buttons { set; get; } = new List<EButton>();
        }
    }
}