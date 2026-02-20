using Assets._Scripts.Manager.Language.Attributes;


namespace Assets._Scripts.Manager.Language.Token
{
    public static class LanguageManagerToken
    {
        public static class common
        {
            public static string error_token { get { return LanguageManager.Instance.GetTranslation("common", "error_token"); } }
            [LanguageManagerIgnoreToken()]
            public static string error_token_en_US { get { return LanguageManager.Instance.GetTranslation("en_US", "common", "error_token"); } }
            public static string accept_token { get { return LanguageManager.Instance.GetTranslation("common", "accept_token"); } }
            [LanguageManagerIgnoreToken()]
            public static string accept_token_en_US { get { return LanguageManager.Instance.GetTranslation("en_US", "common", "accept_token"); } }
            public static string close_token { get { return LanguageManager.Instance.GetTranslation("common", "close_token"); } }
            [LanguageManagerIgnoreToken()]
            public static string close_token_en_US { get { return LanguageManager.Instance.GetTranslation("en_US", "common", "close_token"); } }
            public static string ok_token { get { return LanguageManager.Instance.GetTranslation("common", "ok_token"); } }
            [LanguageManagerIgnoreToken()]
            public static string ok_token_en_US { get { return LanguageManager.Instance.GetTranslation("en_US", "common", "ok_token"); } }
        }
    }
}