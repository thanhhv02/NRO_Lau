using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AssemblyCSharp.Mod.PickMob
{
    public class Menu
    {
        public string Name { get; set; }
        public int IdAction { get; set; }

        public static string MenuAutoPath = "Data\\Menu\\MenuAuto.txt";
        public static string MenuCapsulePath = "Data\\Menu\\MenuCapsuleAuto.txt";
        public static string MenuMain = "Data\\Menu\\MenuMain.txt";
    }
}
