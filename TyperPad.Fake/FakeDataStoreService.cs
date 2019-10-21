using System;
using System.Collections.Generic;
using TyperPad.Common.Enum;
using TyperPad.Common.Helper;
using TyperPad.Common.Interface;
using TyperPad.Common.Model;

namespace TyperPad.Fake
{
    public class FakeDataStoreService : IDataStore
    {
        public void Init()
        {
        }

        public Settings GetSettings()
        {
            return SettingsHelper.GetDefaultSettings(1000, 50000);
        }
    }
}