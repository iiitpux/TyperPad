﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TyperPad.Common.Enum;
using TyperPad.Common.Model;
using static TyperPad.Common.Model.Settings;

namespace TyperPad.Common.Helper
{
    public static class SettingsHelper
    {
        public static Settings GetDefaultSettings(int stickMinLength, int stickMaxLength)
        {
            var leftStickSectorIds = new List<Guid>();
            for (int i = 0; i < 8; i++)
            {
                leftStickSectorIds.Add(Guid.NewGuid());
            }

            var leftStick = GetDefaultSectors(leftStickSectorIds, stickMinLength, stickMaxLength);
            var rightStickSectorIds = new List<Guid>();
            for (int i = 0; i < 8; i++)
            {
                rightStickSectorIds.Add(Guid.NewGuid());
            }

            var rightStick = GetDefaultSectors(rightStickSectorIds, stickMinLength, stickMaxLength);

            var levels = GetDefaultLevels();

            var keys = GetDefaultKeyItems(levels, leftStick, rightStick, GetDefaultKey());

            keys.Add(new KeyItem()
            {
                Buttons = new List<EButton>() {EButton.A},
                LevelIndex = 0,
                Key = Key.Alt
            });
            keys.Add(new KeyItem()
            {
                Buttons = new List<EButton>() {EButton.B},
                LevelIndex = 0,
                Key = Key.Control
            });
            keys.Add(new KeyItem()
            {
                Buttons = new List<EButton>() {EButton.X},
                LevelIndex = 0,
                Key = Key.Win
            });
            keys.Add(new KeyItem()
            {
                Buttons = new List<EButton>() {EButton.Y},
                LevelIndex = 0,
                Key = Key.Shift
            });
            var gamepadButtons = new Dictionary<int, EButton>();
            gamepadButtons.Add(0, EButton.A);
            gamepadButtons.Add(1, EButton.B);
            gamepadButtons.Add(3, EButton.X);
            gamepadButtons.Add(4, EButton.Y);
            gamepadButtons.Add(7, EButton.RFirst);
            gamepadButtons.Add(9, EButton.RSecond);
            gamepadButtons.Add(6, EButton.LFirst);
            gamepadButtons.Add(8, EButton.LSecond);
            gamepadButtons.Add(10, EButton.Select);
            gamepadButtons.Add(11, EButton.Start);

            var gamepadDirectionButtons = new Dictionary<int, EButton>();
            gamepadDirectionButtons.Add(0, EButton.Up);
            gamepadDirectionButtons.Add(9000, EButton.Right);
            gamepadDirectionButtons.Add(18000, EButton.Down);
            gamepadDirectionButtons.Add(27000, EButton.Left);

            var gamepad = new GamepadSettings()
            {
                MinLength = 10000,
                MaxLenght = 50000,
                Buttons = gamepadButtons,
                DirectionButtons = gamepadDirectionButtons
            };
            return new Settings(keys, levels, leftStick, rightStick, gamepad);
        }

        private static List<KeyItem> GetDefaultKeyItems(List<LevelToInputState> levels, List<StickSector> leftStick,
            List<StickSector> rightStick, Tuple<Dictionary<int, Key[,]>, Dictionary<int, Key[,]>> keys)
        {
            var result = new List<KeyItem>();
            foreach (var level in levels)
            {
                //todo тут нужен не левелтуинпут а просто левел
                if (keys.Item1.ContainsKey(level.Level.Index))
                {
                    var leftKeyItem = GetKeyItem(keys.Item1[level.Level.Index], leftStick, level.Level.Index);
                    result.AddRange(leftKeyItem);

                    var rightKeyItem = GetKeyItem(keys.Item2[level.Level.Index], rightStick, level.Level.Index);
                    result.AddRange(rightKeyItem);
                }
            }

            return result;
        }

        private static List<KeyItem> GetKeyItem(Key[,] keys, List<StickSector> stickSectors, int levelIndex)
        {
            var result = new List<KeyItem>();
            int rows = keys.GetUpperBound(0) + 1;
            int columns = keys.GetUpperBound(1) + 1;

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    var currentKey = keys[i, j];
                    if (currentKey == null)
                        continue;

                    var angle = GetAngleByPosition(i, j);
                    if (angle == null)
                        continue;

                    var stick = stickSectors.SingleOrDefault(p => angle.IsBetween(p.MinAngle, p.MaxAngle));
                    if (stick == null)
                        continue;

                    result.Add(new KeyItem()
                    {
                        Key = currentKey,
                        StickSectorId = stick.Id,
                        LevelIndex = levelIndex
                    });
                }
            }

            return result;
        }

        private static List<StickSector> GetDefaultSectors(List<Guid> ids, int stickMinLength, int stickMaxLength)
        {
            var result = new List<StickSector>();
            var sweepAngle = 360 / ids.Count();
            var minAngle = -sweepAngle / 2;
            foreach (var guid in ids)
            {
                var maxAngle = minAngle + sweepAngle;
                result.Add(new StickSector()
                {
                    Id = guid,
                    MinAngle = new Angle(minAngle),
                    MaxAngle = new Angle(maxAngle),
                    MinLength = stickMinLength,
                    MaxLength = stickMaxLength
                });
                minAngle = maxAngle;
            }

            return result;
        }

        private static List<LevelToInputState> GetDefaultLevels()
        {
            var levelToInputState = new List<LevelToInputState>();
            levelToInputState.Add(new LevelToInputState()
            {
                Level = new Level(0),
                Buttons = new List<EButton>()
                {
                }
            });
            levelToInputState.Add(new LevelToInputState()
            {
                Level = new Level(1),
                Buttons = new List<EButton>()
                {
                    EButton.RSecond
                }
            });
            levelToInputState.Add(new LevelToInputState()
            {
                Level = new Level(2),
                Buttons = new List<EButton>()
                {
                    EButton.LSecond
                }
            });
            levelToInputState.Add(new LevelToInputState()
            {
                Level = new Level(3),
                Buttons = new List<EButton>()
                {
                    EButton.LSecond,
                    EButton.RSecond
                }
            });
            return levelToInputState;
        }

        private static Angle? GetAngleByPosition(int row, int column)
        {
            var angles = new Angle[,]
            {
                {new Angle(315), new Angle(0), new Angle(45)},
                {new Angle(270), null, new Angle(90)},
                {new Angle(225), new Angle(180), new Angle(135)},
            };

            if (row > angles.GetUpperBound(0) + 1 || column > angles.GetUpperBound(1) + 1)
                return null;

            return angles[row, column];
        }

        private static Tuple<Dictionary<int, Key[,]>, Dictionary<int, Key[,]>> GetDefaultKey()
        {
            var left = new Dictionary<int, Key[,]>();
            Key[,] leftNoModifKeys = new Key[,]
            {
                {Key.KeyI, Key.KeyU, Key.KeyE},
                {Key.KeyA, null, Key.KeyO},
                {Key.KeyP, Key.KeyY, Key.KeyX}
            };
            left.Add(0, leftNoModifKeys);

            Key[,] left1ModifKeys = new Key[,]
            {
                {Key.KeyK, Key.KeyJ, Key.KeyQ},
                {Key.Comma, null, Key.Dot},
                {Key.BackSlash, Key.Semicolon, Key.Slash}
            };
            left.Add(1, left1ModifKeys);

            Key[,] left2ModifKeys = new Key[,]
            {
                {Key.Tab, Key.ArrowUp, null},
                {Key.ArrowLeft, null, Key.ArrowRight},
                {Key.Esc, Key.ArrowDown, null}
            };
            left.Add(2, left2ModifKeys);

            Key[,] left3ModifKeys = new Key[,]
            {
                {Key.Key1, Key.Key2, Key.Key3},
                {Key.Key4, null, Key.Key5},
                {Key.Key6, Key.Quote, Key.Equal}
            };
            left.Add(3, left3ModifKeys);

            var right = new Dictionary<int, Key[,]>();
            Key[,] rightNoModifKeys = new Key[,]
            {
                {Key.KeyD, Key.KeyH, Key.KeyT},
                {Key.KeyN, null, Key.KeyS},
                {Key.KeyF, Key.KeyG, Key.KeyC}
            };
            right.Add(0, rightNoModifKeys);

            Key[,] right1ModifKeys = new Key[,]
            {
                {Key.KeyR, Key.KeyL, Key.KeyB},
                {Key.KeyM, null, Key.KeyW},
                {Key.KeyV, Key.KeyZ, null}
            };
            right.Add(1, right1ModifKeys);

            Key[,] right2ModifKeys = new Key[,]
            {
                {Key.Home, Key.Space, Key.End},
                {Key.Backspace, null, Key.Delete},
                {Key.PageUp, Key.Enter, Key.PageDown}
            };
            right.Add(2, right2ModifKeys);

            Key[,] right3ModifKeys = new Key[,]
            {
                {Key.Key7, Key.Key8, Key.Key9},
                {Key.BraceLeft, null, Key.BraceRight},
                {Key.KeyV, Key.Key0, Key.Minus}
            };
            right.Add(3, right3ModifKeys);

            return new Tuple<Dictionary<int, Key[,]>, Dictionary<int, Key[,]>>(left, right);
        }
    }
}