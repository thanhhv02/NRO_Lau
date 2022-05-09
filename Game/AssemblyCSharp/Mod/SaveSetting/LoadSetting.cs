using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using AssemblyCSharp.Mod.PickMob;
using AssemblyCSharp.Mod.Xmap;

namespace AssemblyCSharp.Mod.SaveSetting
{
    public class LoadSetting
    {
		public static bool IsCompleteAutoMap = false;
		public static bool IsCompleteAutoPet = false;
		public static bool IsCompleteAutoGoback = false;
		public static bool IsCompleteAutoTrain = false;
		public static bool IsCompleteAutoPean = false;
		public static bool IsCompleteAutoSkill = false;
		public static bool IsCompleteDoHoa = false;
		#region LOAD SETTING

		public static void LOAD_SETTING_AUTO_MAP()
		{
			string[] array3 = File.ReadAllText("Data\\AutoMapSetting.ini").Split(new char[]
			{
				'|'
			});
			for (int i = 0; i < array3.Length; i++)
			{
				// AN DUI GA
				if (array3[i].ToString() .Contains(Setttings.AnDuiGa.ToString()))
				{
					string[] array7 = array3[i].Split(new char[]
					{
							':'
					});
					PickMob.PickMobAuto.IsAnDuiGa = bool.Parse(array7[1]);
					continue;
				}
				// USE CSB
				if (array3[i].ToString() .Contains(Setttings.UseCapsuleNormal.ToString()))
				{
					string[] array7 = array3[i].Split(new char[]
					{
							':'
					});
					Pk9rXmap.IsUseCapsuleNormal = bool.Parse(array7[1]);
					continue;
				}
			}
			IsCompleteAutoMap = true;
		}
		public static void LOAD_SETTING_AUTO_PET()
		{
			string[] array3 = File.ReadAllText("Data\\AutoUpPetSetting.ini").Split(new char[]
			{
				'|'
			});
			for (int i = 0; i < array3.Length; i++)
			{
				// NHAT DO PET
				if (array3[i].ToString() .Contains(Setttings.PickPetDropItem.ToString()))
				{
					string[] array4 = array3[i].Split(new char[]
					{
							':'
					});

					PickMob.PickMobAuto.IsNhatDoUpDT = bool.Parse(array4[1]);
					continue;
				}
				// XOA DO HOA
				
				// AUTO CO DEN
				if (array3[i].ToString() .Contains(Setttings.AutoFlag_8.ToString()))
				{
					string[] array4 = array3[i].Split(new char[]
						{
							':'
						});
					PickMob.PickMobAuto.IsAutoFlag = bool.Parse(array4[1]);
					continue;
				}
				// TTNL WHEN HP LOW
				if (array3[i].ToString() .Contains(Setttings.UseTTNLWhenHP.ToString()))
				{
					string[] array4 = array3[i].Split(new char[]
						{
							':'
						});
					PickMobAutoController.aHP = int.Parse(array4[1]);
					continue;
				}
				// TTNL WHEN MP LOW
				if (array3[i].ToString() .Contains(Setttings.UseTTNLWhenMP.ToString()))
				{
					string[] array4 = array3[i].Split(new char[]
						{
							':'
						});
					PickMobAutoController.aMP = int.Parse(array4[1]);
					continue;
				}
				if (array3[i].ToString() .Contains(Setttings.XNhat.ToString()))
				{
					string[] array4 = array3[i].Split(new char[]
						{
							':'
						});
					PickMobAutoController.XNhat = int.Parse(array4[1]);
					continue;
				}
				if (array3[i].ToString() .Contains(Setttings.YNhat.ToString()))
				{
					string[] array4 = array3[i].Split(new char[]
						{
							':'
						});
					PickMobAutoController.YNhat = int.Parse(array4[1]);
					continue;
				}

			}
			IsCompleteAutoPet = false;
		}
		public static void LOAD_SETTING_AUTO_GOBACK()
		{
			string[] array3 = File.ReadAllText("Data\\AutoGobackSetting.ini").Split(new char[]
			{
				'|' 
			});
			for (int i = 0; i < array3.Length; i++)
			{
				GameScr.info1.addInfo("test: "+array3[i].ToString(), 0);
				//NE SIEU QUAI
				if (array3[i].ToString().Contains(Setttings.Goback.ToString()))
				{
					string[] array4 = array3[i].Split(new char[]
						{
							':'
						});
					Pk9rXmap.isGoBackbt = bool.Parse(array4[1].ToLower());
					continue;
				}
				// GO BACK
				if (array3[i].ToString().Contains(Setttings.GobackPosition.ToString()))
				{
					string[] array4 = array3[i].Split(new char[]
						{
							':'
						});
					Pk9rXmap.isGoBack = bool.Parse(array4[1].ToLower());
					continue;
				}
				if (array3[i].ToString().Contains(Setttings.cy.ToString()))
				{
					string[] array4 = array3[i].Split(new char[]
						{
							':'
						});
					XmapController.cy = int.Parse(array4[1]);
					continue;
				}
				if (array3[i].ToString().Contains(Setttings.cx.ToString()))
				{
					string[] array4 = array3[i].Split(new char[]
						{
							':'
						});
					XmapController.cx = int.Parse(array4[1]);
					continue;
				}
				if (array3[i].ToString() .Contains(Setttings.ZoneID.ToString()))
				{
					string[] array4 = array3[i].Split(new char[]
						{
							':'
						});
					XmapController.ZoneID = int.Parse(array4[1]);
					continue;
				}
				if (array3[i].ToString() .Contains(Setttings.MapID.ToString()))
				{
					string[] array4 = array3[i].Split(new char[]
						{
							':'
						});
					XmapController.MapID = int.Parse(array4[1]);
					continue;
				}
			}
			IsCompleteAutoGoback = true;
		}
		public static void LOAD_SETTING_AUTO_TRAIN()
		{
			string[] array3 = File.ReadAllText("Data\\AutoTrainSetting.ini").Split(new char[]
			{
				'|'
			});
			for (int i = 0; i < array3.Length; i++)
			{
				//NE SIEU QUAI
				if (array3[i].ToString() .Contains(Setttings.NeSieuQuai.ToString()))
				{
					string[] array4 = array3[i].Split(new char[]
						{
							':'
						});
					PickMobAuto.IsNeSieuQuai = bool.Parse(array4[1]);
					continue;
				}
				// GO BACK
				if (array3[i].ToString() .Contains(Setttings.TanSat.ToString()))
				{
					string[] array4 = array3[i].Split(new char[]
						{
							':'
						});
					PickMobAuto.IsTanSat = bool.Parse(array4[1]);
					continue;
				}
			}
			IsCompleteAutoTrain = false;
		}
		public static void LOAD_SETTING_AUTO_PEAN()
		{
			string[] array3 = File.ReadAllText("Data\\AutoPeanSetting.ini").Split(new char[]
			{
				'|'
			});
			for (int i = 0; i < array3.Length; i++)
			{
				// CHO DAU
				if (array3[i].ToString() .Contains(Setttings.ChoDau.ToString()))
				{
					string[] array4 = array3[i].Split(new char[]
						{
							':'
						});
					PickMob.PickMobAuto.IsChoDau = bool.Parse(array4[1]);
					continue;
				}
				// XIN DAU
				if (array3[i].ToString() .Contains(Setttings.XinDau.ToString()))
				{
					string[] array4 = array3[i].Split(new char[]
						{
							':'
						});
					PickMob.PickMobAuto.IsXinDau = bool.Parse(array4[1]);
					continue;
				}
				// THU DAU
				if (array3[i].ToString() .Contains(Setttings.ThuDau.ToString()))
				{
					string[] array4 = array3[i].Split(new char[]
						{
							':'
						});
					PickMob.PickMobAuto.IsAutoThuDau = bool.Parse(array4[1]);
					continue;
				}
				// USE HP POTION WHEN HP LOW
				if (array3[i].ToString() .Contains(Setttings.UseHPPotionWhen.ToString()))
				{
					string[] array4 = array3[i].Split(new char[]
						{
							':'
						});
					PickMob.PickMobAuto.HpBuff = int.Parse(array4[1]);
					continue;
				}
				// USE HP POTION WHEN MP LOW
				if (array3[i].ToString() .Contains(Setttings.UseMPPotionWhen.ToString()))
				{
					string[] array4 = array3[i].Split(new char[]
						{
							':'
						});
					PickMob.PickMobAuto.MpBuff = int.Parse(array4[1]);
					continue;
				}

			}
			IsCompleteAutoPean = false;
		}
		public static void LOAD_SETTING_AUTO_SKILL()
		{
			string[] array3 = File.ReadAllText("Data\\AutoSkillSetting.ini").Split(new char[]
			{
				'|'
			});
			for (int i = 0; i < array3.Length; i++)
			{
				// ATTACK FOR PET
				if (array3[i].ToString() .Contains(Setttings.AttackForPet.ToString()))
				{
					string[] array4 = array3[i].Split(new char[]
						{
							':'
						});
					PickMob.PickMobAuto.IsAttackForPet = bool.Parse(array4[1]);
					continue;
				}
			}
			IsCompleteAutoSkill = false;
		}
		public static void LOAD_SETTING_DO_HOA()
		{
			string[] array3 = File.ReadAllText("Data\\DoHoaSetting.ini").Split(new char[]
			{
				'|'
			});
			for (int i = 0; i < array3.Length; i++)
			{
				if (array3[i].ToString().Contains(Setttings.XoaDoHoa.ToString()))
				{
					string[] array4 = array3[i].Split(new char[]
						{
							':'
						});
					OnScreen.IsXoaMap = bool.Parse(array4[1]);
					continue;
				}
				// XOA DO HO GIAM CPU
				if (array3[i].ToString().Contains(Setttings.XoaDoHoaGiamCPU.ToString()))
				{
					string[] array4 = array3[i].Split(new char[]
						{
							':'
						});
					OnScreen.IsXoaMapV = bool.Parse(array4[1]);
					continue;
				}
			}
			IsCompleteDoHoa = false;
		}

		#endregion
	}
}
