using Microsoft.Win32;
using System;

namespace Software_Data
{
    class software_detail
    {
        public static void Main()
        {
            Microsoft.Win32.RegistryKey key = Registry.LocalMachine.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Uninstall");
            Console.WriteLine("{0, -80} {1,-10}", "SOFTWARE", "VERSION");
            Console.WriteLine("{0, -80} {1,-10}", "========", "=======");
            foreach (var v in key.GetSubKeyNames())
            {
                string keyName = key + "\\" + v;
                string a = (string)Registry.GetValue(keyName, "DisplayName", null);

                if (a != null)
                {
                    try
                    {
                        Console.WriteLine("{0, -80} {1,-10}", Registry.GetValue(keyName, "DisplayName", null), Registry.GetValue(keyName, "DisplayVersion", null));
                    }
                    catch (Exception e)
                    { }
                }
            }


        }
    }
}
