using System;
using HappyCoupleMobile.Providers;
using HappyCoupleMobile.Providers.Interfaces;
using SQLite.Net.Interop;
using SQLite.Net.Platform.XamarinIOS;
using System.IO;

namespace HappyCoupleMobile.iOS.Providers
{
    public class SystemInfoProvider : BaseSystemInfoProvider, ISystemInfoProvider
    {
        public ISQLitePlatform SqLitePlatform => new SQLitePlatformIOS();
        public string SqliteDatabasePath => Path.Combine(PersonalDirectoryPath, "happy.db3");

        public string PersonalDirectoryPath
        {
            get
            {
                string personalDirectoryPath =
                    Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "Library");
                if (!Directory.Exists(personalDirectoryPath))
                {
                    Directory.CreateDirectory(personalDirectoryPath);
                }
                return personalDirectoryPath;
            }
        }
    }
}