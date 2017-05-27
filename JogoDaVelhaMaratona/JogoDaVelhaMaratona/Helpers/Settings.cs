// Helpers/Settings.cs
using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace JogoDaVelhaMaratona.Helpers
{
    public static class Settings
    {
        private static ISettings AppSettings => CrossSettings.Current;

        const string UserIdKey = "userId";
        private static readonly string UserIdDefault = string.Empty;
        public static string UserId
        {
            get { return AppSettings.GetValueOrDefault<string>(UserIdKey, UserIdDefault); }
            set { AppSettings.AddOrUpdateValue<string>(UserIdKey, value); }
        }

        const string AuthTokeyKey = "authtoken";
        static readonly string AuthTokeyDefault = string.Empty;
        public static string AuthToken
        {
            get { return AppSettings.GetValueOrDefault<string>(AuthTokeyKey, AuthTokeyDefault); }
            set { AppSettings.AddOrUpdateValue<string>(AuthTokeyKey, value); }
        }

        const string Player1NameKey = "player1Name";
        static readonly string Player1NameDefault = string.Empty;
        public static string Player1Name
        {
            get { return AppSettings.GetValueOrDefault<string>(Player1NameKey, Player1NameDefault); }
            set { AppSettings.AddOrUpdateValue<string>(Player1NameKey, value); }
        }

        const string Player2NameKey = "player2Name";
        static readonly string Player2NameDefault = string.Empty;
        public static string Player2Name
        {
            get { return AppSettings.GetValueOrDefault<string>(Player2NameKey, Player2NameDefault); }
            set { AppSettings.AddOrUpdateValue<string>(Player2NameKey, value); }
        }

        const string Player1ImageKey = "player1Image";
        static readonly string Player1ImageDefault = string.Empty;
        public static string Player1Image
        {
            get { return AppSettings.GetValueOrDefault<string>(Player1ImageKey, Player1ImageDefault); }
            set { AppSettings.AddOrUpdateValue<string>(Player1ImageKey, value); }
        }

        const string Player2ImageKey = "player2Image";
        static readonly string Player2ImageDefault = string.Empty;
        public static string Player2Image
        {
            get { return AppSettings.GetValueOrDefault<string>(Player2ImageKey, Player2ImageDefault); }
            set { AppSettings.AddOrUpdateValue<string>(Player2ImageKey, value); }
        }

        public static bool IsLoggedIn => !string.IsNullOrWhiteSpace(UserId);
    }
}