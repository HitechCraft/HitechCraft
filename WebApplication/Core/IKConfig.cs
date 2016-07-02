namespace WebApplication.Core
{
    public static class IKConfig
    {
        public static bool TestMode
        {
            get
            {
                return true;
            }
        }

        public static string IKID
        {
            get
            {
                return "5294ac1ebf4efc2519330a7a";
            }
        }

        public static string IKSecret
        {
            get
            {
                return "gwAKGFVvx5Cu4gjj";
            }
        }

        public static string IKSecretTest
        {
            get
            {
                return "u7xRbNdRXlCDweu3";
            }
        }

        //TODO: генерировать ключ транзакции и проверять его при оплате
        public static string TransactionID
        {
            get
            {
                return "ID_57843753478";
            }
        }
    }
}