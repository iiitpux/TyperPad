using System;
using System.Collections.Generic;
using System.Linq;
using TyperPad.Common.Helper;
using TyperPad.Common.Interface;
using TyperPad.Common.Model;

namespace TyperPad.Core
{
    public class Service
    {
        private bool _isRunning;
        private readonly IInput _input;
        private readonly IOutput _output;
        private readonly IDataStore _dataStore;
        private Settings _settings;

        private Key _prevKey = null;

        private List<Key> _prevModificators = new List<Key>();
        //todo событие что есть новый введеный символ

        public delegate void KeyDown(Key key, List<Key> modificators);

        public event KeyDown OnKeyDown;

        public Service(IInput input, IOutput output, IDataStore dataStore)
        {
            _input = input;
            _output = output;
            _dataStore = dataStore;
            //_settings = _dataStore.GetSettings(); //todo uncomment
            _settings = SettingsHelper.GetDefaultSettings(1000, 50000);
            
            //todo- Отписку сделать
            OnKeyDown += (key, modificators) => _output.Send(new OutputState()
            {
                Key = key,
                Modificators = modificators
            });
        }

        //todo- потокобезопасный словарь
        //todo- в бекграунд воркер засунуть надо
        //todo соъранять пред состояние в паттерне
        public void Run()
        {
            var inputState = _input.GetState(); //todo
            var outputState = GetOutputState(inputState);
            if (outputState != null)
                OnKeyDown?.Invoke(outputState.Key, outputState.Modificators);
            
            
//            _isRunning = true;
//            while (_isRunning)
//            {
//                var inputState = _input.GetState(); //todo
//                var outputState = GetOutputState(inputState);
//                if (outputState != null)
//                    OnKeyDown?.Invoke(outputState.Key, outputState.Modificators);
//            }
        }

        public void Stop()
        {
            _isRunning = false;
        }

        private OutputState GetOutputState(InputState inputState)
        {
            //todo вырезать все нажатые кнопки на которые ничего не забито
            
            var leftStickSectorGuid = _settings.LeftStick
                .SingleOrDefault(p => p.MinAngle <= inputState.LeftStick.Angle
                                      && p.MaxAngle >= inputState.LeftStick.Angle
                                      && p.MinLength <= inputState.LeftStick.Length
                                      && p.MaxLength >= inputState.LeftStick.Length)?.Id;
            
            Settings.Level level = null;
            List<string> inputPattern = new List<string>();
            
            inputPattern.AddRange(inputState.Buttons.Select(p=>p.ToString()));
            if(leftStickSectorGuid.HasValue)
                inputPattern.Add(leftStickSectorGuid.Value.ToString());

            foreach (var levelToInputState in _settings.Levels)
            {
                var pattern = new List<string>();
                pattern.AddRange(levelToInputState.Buttons.Select(p=>p.ToString()));
                if(levelToInputState.StickSectorId.HasValue)
                    pattern.Add(levelToInputState.StickSectorId.Value.ToString());

                if (pattern.Count() == inputPattern.Intersect(pattern).Count())
                {
                    level = levelToInputState.Level;
                    inputPattern = inputPattern.Except(pattern).ToList();
                    break;
                }
            }

            if (level == null)
            {
                if (_settings.Levels.Any())
                {
                    level = _settings.Levels.OrderBy(p => p.Level.Index).First().Level;
                }
                else
                {
                    throw new Exception("Хотябы один уровень должен быть");
                }
            }

            var modificatorKeys = new List<Key>();
            foreach (var modificatorKey in _settings.ModificatorKeys)
            {
                var pattern = new List<string>();
                pattern.AddRange(modificatorKey.Buttons.Select(p=>p.ToString()));
                if(modificatorKey.StickSectorId.HasValue)
                    pattern.Add(modificatorKey.StickSectorId.Value.ToString());

                if (pattern.Count() == inputPattern.Intersect(pattern).Count())
                {
                    modificatorKeys.Add(modificatorKey.Key);    
                    inputPattern = inputPattern.Except(pattern).ToList();
                }
            }
            
            var key = _settings.Keys
                .FirstOrDefault(p => p.Pattern.Count() == inputPattern.Intersect(p.Pattern).Count() 
                                     && p.LevelIndex == level.Index)
                ?.Key;

            if (key == _prevKey)
                return null;

            _prevKey = key;

            return new OutputState()
            {
                Key = key,
                Modificators = modificatorKeys 
            };
            return null;
        }
    }
}