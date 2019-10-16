using System;
using System.Collections.Generic;
using TyperPad.Common.Enum;
using TyperPad.Common.Interface;
using TyperPad.Common.Model;

namespace TyperPad.Fake
{
    public class FakeDataStoreService : IDataStore
    {
        public Settings GetSettings()
        {
            throw new NotImplementedException();
        }
    }
}