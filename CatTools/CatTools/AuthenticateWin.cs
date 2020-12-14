using Microsoft.Win32;
using System;

namespace CatTools
{
    class AuthenticateWin
    {
        private static readonly string userRoot = "HKEY_CURRENT_USER";
        private static readonly string subKey = @"SOFTWARE\CatTools\Licenses";
        private static readonly string keyName = userRoot + "\\" + subKey;

        public static void Authenticate()
        {
            //TODO:
        }

        private static Boolean CheckSerial()
        {
            bool check = false;
            if (Registry.GetValue(keyName, "Serial", null) == null)
            {
                check = true;
            }
            return check;
        }

        private static Boolean CheckData()
        {
            bool check = false;
            if (Registry.GetValue(keyName, "Date", null) == null)
            {
                check = true;
            }
            return check;
        }

        private static void InsertKeyRegistry()
        {
            Registry.SetValue(keyName, "Serial", "", RegistryValueKind.String);
            Registry.SetValue(keyName, "Date", DateTime.Now.ToString("dd-MM-yyyy"), RegistryValueKind.String);
        }

        private static void GetKeyRegistry()
        {
            Console.WriteLine(Registry.GetValue(keyName, "Teste", null));
        }

        private static void DelkeyRegistry()
        {
            Registry.CurrentUser.DeleteSubKeyTree(@"SOFTWARE\1Eddy");
        }
    }
}
