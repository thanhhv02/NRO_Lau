using AssemblyCSharp.Mod.OnScreenMod;
using AssemblyCSharp.Mod.PickMob;
using AssemblyCSharp.Mod.Xmap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace AssemblyCSharp.Mod.ConnectQLTK
{
    
    public class ConnectQLTK
	{
		
	public static void Control(string backcommand)
		{
			if (backcommand.Contains("Zone"))
			{
				int zoneId = int.Parse(backcommand.Replace("Zone", ""));
				Service.gI().requestChangeZone(zoneId, -1);
				return;
			}
			if (backcommand.Contains("Map"))
			{
				int idMap = int.Parse(backcommand.Replace("Map", ""));
				XmapController.FinishXmap();
				XmapController.StartRunToMapId(idMap);
			}
			if (backcommand.Contains("Skill"))
			{
				int num = int.Parse(backcommand.Replace("Skill", ""));
				GameScr.gI().doUseSkillNotFocus(global::Char.myCharz().getSkill(global::Char.myCharz().nClass.skillTemplates[num]));
			}
			if (backcommand.Contains("Chat"))
			{
				Service sv = new Service();
				string text = backcommand.Replace("Chat", "");
				sv.chat(text);
			}
		}

		// Token: 0x0600001F RID: 31 RVA: 0x000089D4 File Offset: 0x00006BD4
		public void ReceiveData(IAsyncResult ar)
		{
			int num = ((Socket)ar.AsyncState).EndReceive(ar);
			byte[] array = new byte[num];
			Array.Copy(this.receivedBuf, array, num);
			Control(Encoding.ASCII.GetString(array));
			this._clientSocket.BeginReceive(this.receivedBuf, 0, this.receivedBuf.Length, SocketFlags.None, new AsyncCallback(this.ReceiveData), this._clientSocket);
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00008A48 File Offset: 0x00006C48
		public void LoopConnect()
		{
			//int num = 0;
			//while (!this._clientSocket.Connected)
			//{
			//	try
			//	{
			//		num++;
			//		this._clientSocket.Connect(IPAddress.Loopback, 100);
			//	}
			//	catch (SocketException)
			//	{
			//		GameScr.info1.addInfo("Kết Nối Lỗi Lần : " + num.ToString(), 0);
			//	}
			//}
			//GameScr.info1.addInfo("Đã Kết Nối!", 0);
			if (!this._clientSocket.Connected)
			{
				try
				{
					this._clientSocket.Connect(IPAddress.Loopback, 100);
				}
				catch (SocketException)
				{
					GameScr.info1.addInfo("Lỗi", 0);
					return;
				}
				GameScr.info1.addInfo("Đã Kết Nối!", 0);
			}
   //         else
   //         {
			//	try
			//	{
			//		num++;
			//		this._clientSocket.Disconnect(true);
			//	}
			//	catch (SocketException)
			//	{
			//		GameScr.info1.addInfo("Lỗi", 0);
			//		return;
			//	}
			//	GameScr.info1.addInfo("Ngắt Kết Nối!", 0);
			//}
			
		}

		// Token: 0x06000021 RID: 33 RVA: 0x000036EB File Offset: 0x000018EB
		public static bool isConnect;
		public void CreateSocket()
		{

			
				Thread thr = new Thread(delegate ()
				{
					this.LoopConnect();
					this._clientSocket.BeginReceive(this.receivedBuf, 0, this.receivedBuf.Length, SocketFlags.None, new AsyncCallback(this.ReceiveData), this._clientSocket);
					byte[] bytes = Encoding.ASCII.GetBytes("@@" + /*Main.arguments["acc"]*/ AutoLogin.Account);
					this._clientSocket.Send(bytes);
				})
				{
					IsBackground = true
				};
				thr.Start();
															
		}
		private byte[] receivedBuf = new byte[1024];

		// Token: 0x04000019 RID: 25
		private Socket _clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
		// Token: 0x06000022 RID: 34 RVA: 0x0000370A File Offset: 0x0000190A
		public void SendDataBack()
		{
			new Thread(delegate ()
			{
				while (this._clientSocket.Connected)
				{
					byte[] bytes = Encoding.ASCII.GetBytes(NinjaUtil.getMoneys(global::Char.myPetz().cPower));
					this._clientSocket.Send(bytes);
					Thread.Sleep(5000);
				}
			})
			{
				IsBackground = true
			}.Start();
		}
		public bool HotKey()
        {
            switch (GameCanvas.keyAsciiPress)
            {
                case '0':
					
						CreateSocket();
						//GameScr.info1.addInfo("Kết nối on", 0);
					
					              break;
                default:
                    return false;
            }
            return true;
        }
		public bool Chat(string text)
        {
			if(text == "senddata")
            {
				SendDataBack();
			}
            else
            {
				return false;
            }
			return true;
        }

	}
}
