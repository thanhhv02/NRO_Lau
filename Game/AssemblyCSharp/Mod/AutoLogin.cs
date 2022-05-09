using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace AssemblyCSharp.Mod
{
    public class AutoLogin
    {
		#region mod
		public static string FileLog = "Log";
		public static int ID;
		public static string FileAccount = "TK.txt";
		public static string Account;
		public static string Password;
		public static int Server;
		public static bool isLogin = false;

		public static Dictionary<string, string> arguments = new Dictionary<string, string>();

        //public static bool LoadFileAcc()
        //{
        //	if (File.Exists(FileLog))
        //	{
        //		string[] commandLineArgs = Environment.GetCommandLineArgs();
        //		for (int i = 1; i < commandLineArgs.Length; i += 2)
        //		{
        //			string key = commandLineArgs[i].Replace("--", "");
        //			Main.arguments.Add(key, commandLineArgs[i + 1]);
        //		}

        //              Account = arguments["tk"];
        //              Password = arguments["mk"];
        //              Server = int.Parse(arguments["sv"]);
        //		return true;
        //          }
        //	return false;
        //}
        public static bool LoadFileAcc()
        {
            //if (File.Exists(FileLog))
            //{
            //string[] array = File.ReadAllText(FileLog).Split(new char[]
            //{
            //'|'
            //});
            //ID = int.Parse(array[0]);
            //Account = array[1];
            //Password = array[2];
            //Server = int.Parse(array[3]);
            //Server--;
            //File.Delete(FileLog);

            //}
            try {
                string[] en = Environment.GetCommandLineArgs();
                string[] array = en[1].Split('|');
                //ID = int.Parse(array[0]);
                Account = array[0];
                Password = array[1];
                Server = int.Parse(array[2]);
                Server--;
                return true;
            }
            catch
            {
                return false;
            }
        }
        public static void Login()
		{
			while (!ServerListScreen.loadScreen || ServerListScreen.isGetData)
			{
				Thread.Sleep(1000);
			}

			if (Account != null)
			{
				isLogin = true;
				Rms.saveRMSString("acc", Account);
				Rms.saveRMSString("pass", Password);
				if (Rms.loadRMSInt("svselect") != Server)
				{
					Rms.saveRMSInt("svselect", Server);
					ServerListScreen.ipSelect = Server;
					GameCanvas.serverScreen.selectServer();
				}
				while (!Session_ME.gI().isConnected())
				{
					Thread.Sleep(200);
				}
				//Thread.Sleep(500);
				if (GameCanvas.loginScr == null)
				{
					GameCanvas.loginScr = new LoginScr();
				}
				GameCanvas.loginScr.switchToMe();
				GameCanvas.loginScr.doLogin();

			}
        }
        //public static void Login()
        //{
        //	while (!ServerListScreen.loadScreen)
        //	{
        //		Thread.Sleep(100);
        //	}

        //	if (Account != null)
        //	{
        //		isLogin = true;
        //		Rms.saveRMSString("acc", Account);
        //		Rms.saveRMSString("pass", Password);
        //		if (Rms.loadRMSInt("svselect") != Server)
        //		{
        //			Rms.saveRMSInt("svselect", Server);
        //			ServerListScreen.ipSelect = Server;
        //			GameCanvas.serverScreen.selectServer();
        //		}
        //		while (!Session_ME.gI().isConnected())
        //		{
        //			Thread.Sleep(100);
        //		}
        //		//Thread.Sleep(500);
        //		if (GameCanvas.loginScr == null)
        //		{
        //			GameCanvas.loginScr = new LoginScr();
        //		}
        //		GameCanvas.loginScr.switchToMe();
        //		GameCanvas.loginScr.doLogin();

        //	}
        //}
        public static void _login()
		{
			if (Rms.loadRMSInt("svselect") != Server)
			{
				Rms.saveRMSInt("svselect", Server);
				ServerListScreen.ipSelect = Server;
				GameCanvas.serverScreen.selectServer();
			}
			while (!Session_ME.gI().isConnected())
			{
				Thread.Sleep(100);
			}
			Thread.Sleep(1000);
			if (GameCanvas.loginScr == null)
			{
				GameCanvas.loginScr = new LoginScr();
			}
			GameCanvas.loginScr.switchToMe();
			GameCanvas.loginScr.doLogin();
			LoginScr.serverName = ServerListScreen.nameServer[ServerListScreen.ipSelect];
		}
		#endregion
	}
}
