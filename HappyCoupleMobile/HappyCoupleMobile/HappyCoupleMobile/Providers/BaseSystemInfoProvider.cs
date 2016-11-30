using System.Net.NetworkInformation;
using Xamarin.Forms;

namespace HappyCoupleMobile.Providers
{
    public class BaseSystemInfoProvider
    {
        public bool IsNetworkAvailable => NetworkInterface.GetIsNetworkAvailable();
        public virtual TargetPlatform DeviceOs => Device.OS;
    }
}