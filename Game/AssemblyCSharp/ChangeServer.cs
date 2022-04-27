using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AssemblyCSharp
{
    public class ChangeServer
    {
        public static string file = "SV.txt";
        public static bool blue, pri, nroS, dream;
        public static void Check()
        {
            bool flag = File.Exists(file);
            if (flag)
            {
                string sv = File.ReadAllText(file);
                if (sv.ToLower().Trim().Equals("blue"))
                {
                    blue = true;
                    ServerListScreen.smartPhoneVN = "Blue 01:103.90.224.149:14445:0,0,0";
                }
                else if (sv.ToLower().Trim().Equals("nrosuper"))
                {
                    nroS = true;
                    ServerListScreen.smartPhoneVN = "Nro SUPER:103.90.224.147:14445:0,0,0";
                }
                else if (sv.ToLower().Trim().Equals("private"))
                {
                    blue = true;
                    ServerListScreen.smartPhoneVN = "Blue 01:103.90.224.149:14445:0,0,0";
                }
            }
        }
    }
}
