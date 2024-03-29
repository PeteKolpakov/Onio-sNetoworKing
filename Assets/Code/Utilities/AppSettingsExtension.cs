using Photon.Chat;
using Photon.Realtime;

namespace OniosNetworKing
{
    public static class AppSettingsExtension
    {
        public static ChatAppSettings GetChatSettings(this AppSettings appSettings)
        {
            return new ChatAppSettings
            {
                AppIdChat = appSettings.AppIdChat,
                AppVersion = appSettings.AppVersion,
                FixedRegion = appSettings.IsBestRegion ? null : appSettings.FixedRegion,
                NetworkLogging = appSettings.NetworkLogging,
                Protocol = appSettings.Protocol,
                Server = appSettings.IsDefaultNameServer ? null : appSettings.Server
            };
        }
    }
}
