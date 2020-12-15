using Microsoft.Win32;
using System;
using System.Windows.Forms;

namespace CatTools
{
    class AuthenticateWin
    {
        const string USERROOT = @"HKEY_CURRENT_USER\SOFTWARE\CatTools\Licenses";

        public static bool Authenticate(string SerialNumber)
        {
            //TODO:
            if(CheckPatnerId())
            {
                return CheckTrial(CheckSerial());                 
            }
            InsertKeyRegistry(SerialNumber);
            return true;
        }

        private static bool CheckPatnerId()
        {            
            if (Registry.GetValue(USERROOT, "PatnerId", null) == null)
            {
                return false;
            }            
                return true;
        }

        private static bool CheckTrial(bool serial)
        {
            if (!serial)
            {
                var cryptDate = Registry.GetValue(USERROOT, "CurrentStatus", null).ToString();
                var decryptDate = Criptografia.Decriptar("asddgfthcgdopdftASTFGHQf", "ARSTDGXFTUDOPEHT", cryptDate);
                return ValidateTrial(decryptDate);
            }
            return true;
        }

        private static bool ValidateTrial(string date)
        {
            if(DateTime.Parse(DateTime.Now.ToString("dd-MM-yyyy")) <= DateTime.Parse(date))
            {
                MessageBox.Show("VERSÃO DE AVALIAÇÃO","DEMO",MessageBoxButtons.OK,MessageBoxIcon.Information);
                return true;
            }
            MessageBox.Show("VERSÃO DE AVALIAÇÃO EXPIRADA", "DEMO", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return false;
        }

        private static bool CheckSerial()
        {
            var KeyNumber = Registry.GetValue(USERROOT, "Serial Number", null);
            if (KeyNumber == null || KeyNumber.ToString() == "")
            {
                return false;
            }
            return true;
        }

        private static void InsertKeyRegistry(string SerialNumber)
        {
            if (SerialNumber != null)
            {
                SerialNumber = Criptografia.Encriptar("asddgfthcgdopdftASTFGHQf", "ARSTDGXFTUDOPEHT", SerialNumber);
            }

            var ExpireDate = DateTime.Now.AddDays(5);
            var cryptDate = Criptografia.Encriptar("asddgfthcgdopdftASTFGHQf", "ARSTDGXFTUDOPEHT", ExpireDate.ToString("dd-MM-yyyy"));

            Registry.SetValue(USERROOT, "PatnerId", 1, RegistryValueKind.DWord);
            Registry.SetValue(USERROOT, "Serial Number", SerialNumber, RegistryValueKind.String);
            Registry.SetValue(USERROOT, "CurrentStatus", cryptDate, RegistryValueKind.String);
        }
    }
}
