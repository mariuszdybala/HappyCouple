using SQLite.Net.Interop;
using Xamarin.Forms;

namespace HappyCoupleMobile.Providers.Interfaces
{
    public interface ISystemInfoProvider
    {
        ISQLitePlatform SqLitePlatform { get; }
        bool IsNetworkAvailable { get; }
        string SqliteDatabasePath { get; }
        string PersonalDirectoryPath { get; }
        TargetPlatform DeviceOs { get; }
    }
}