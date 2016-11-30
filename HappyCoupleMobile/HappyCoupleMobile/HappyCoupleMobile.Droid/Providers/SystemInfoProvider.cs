using System;
using System.IO;
using HappyCoupleMobile.Providers;
using HappyCoupleMobile.Providers.Interfaces;
using SQLite.Net.Interop;
using SQLite.Net.Platform.XamarinAndroid;

namespace HappyCoupleMobile.Droid.Providers
{
    public class SystemInfoProvider : BaseSystemInfoProvider, ISystemInfoProvider
    {
        public ISQLitePlatform SqLitePlatform => new SQLitePlatformAndroid();
        public string SqliteDatabasePath => Path.Combine(PersonalDirectoryPath, "happy.db3");

        public string PersonalDirectoryPath => Environment.GetFolderPath(Environment.SpecialFolder.Personal);

    }
}