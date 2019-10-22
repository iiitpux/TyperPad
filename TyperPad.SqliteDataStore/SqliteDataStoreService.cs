using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.EntityFrameworkCore;
using TyperPad.Common.Helper;
using TyperPad.Common.Interface;
using TyperPad.Common.Model;

namespace TyperPad.SqliteDataStore
{
    public class SqliteDataStoreService : IDataStore
    {
        public Settings GetSettings(Guid profileId)
        {
            using (var db = new TyperPadContext())
            {
                InitOrUpdate(db);
                //todo fill settings
                // Create
//                Console.WriteLine("Inserting a new blog");
//                db.Add(new Model.Blog { Url = "http://blogs.msdn.com/adonet" });
//                db.SaveChanges();
            }

            throw new NotImplementedException();
        }

        public List<Profile> GetProfiles()
        {
            using (var db = new TyperPadContext())
            {
                InitOrUpdate(db);
                //todo get profiles
            }

            throw new NotImplementedException();
        }

        public void SaveSettings(Settings settings)
        {
        }

        private void InitOrUpdate(TyperPadContext db)
        {
            if (!db.Database.CanConnect())
            {
                db.Database.Migrate();
                //todo  stickSize?
                SaveSettings(SettingsHelper.GetDefaultSettings(0, 1000));
            }
            else
            {
                db.Database.Migrate();
            }
        }
    }
}