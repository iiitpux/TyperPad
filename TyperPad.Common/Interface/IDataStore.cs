using System;
using System.Collections.Generic;
using TyperPad.Common.Model;

namespace TyperPad.Common.Interface
{
    public interface IDataStore
    {
        Settings GetSettings(Guid profileId);
        List<Profile> GetProfiles();
        void SaveSettings(Settings settings);
    }
}