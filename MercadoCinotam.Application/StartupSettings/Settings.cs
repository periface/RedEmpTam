using Abp.Configuration;

namespace MercadoCinotam.StartupSettings
{
    public static class Settings
    {
        public static SettingInfo ThemeInitialConfig(int tenantId)
        {
            return new SettingInfo(tenantId, null, ConfigConst.Theme, ValueConst.MainTheme);
        }

        public static SettingInfo PayPalInitialConfig(int tenantId)
        {
            return new SettingInfo(tenantId, null, ConfigConst.PayPalStatus, ValueConst.PayPalStatusFalse);
        }

        public static SettingInfo PayPalSecretKey(int tenantId)
        {
            return new SettingInfo(tenantId, null, ConfigConst.PayPalCode, ValueConst.EmptyCode);
        }

        public static SettingInfo ThemeInitialConfig(int tenantId, string themeUniqueName)
        {
            return new SettingInfo(tenantId, null, ConfigConst.Theme, themeUniqueName);
        }
    }
}
