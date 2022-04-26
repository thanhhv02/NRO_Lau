

namespace AssemblyCSharp.Mod
{
    public class Noti
    {
        public static void ValStatus(string Val, bool chucNang)
        {
            GameScr.info1.addInfo(Val + (chucNang ? " On" : " Off"), 0);
        }
        
    }
}
