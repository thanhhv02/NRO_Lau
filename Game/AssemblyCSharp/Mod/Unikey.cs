using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace AssemblyCSharp.Mod
{
    public class Unikey
    {

        public static bool _Unikey = false;

       
        public static void ReplaceText(string n, string c)
        {
            n.Replace(n, c);
        }
        public static bool Chat(string text)
        {
            if(text == "unikey")
            {
                _Unikey = !_Unikey;
                GameScr.info1.addInfo("Unikey: "+(_Unikey ? "On" : "Off"), 0);
            }
            else
            {
                return false;
            }
            return true;
        }
        public static string[] Telex = new string[]
        {
            "uow",

            "aa","aá","aà","aả","aã","aạ",

            "aw","ăs","ăr","ăx","ăf","ăj",

            "as","af","ax","ar","aj",

            "dd","đd",

            "es","ef","ex","ej","er","ee",

            "is","if","ir","ix","ij",

            "os","of","or","ox","oj",

            "oo",

            "ow","ơw",

            "us","uf","ur","ux","uj",

            "uuw",

            "uw",

            "uyr","uys","uyf","uyj","uyx",

            "ys","yf","yr","yx","yj",

            "uów","ươf","ươs","ươx","ươj","ươr",

            "ưus","ưú","ưuf","ưù","ưur","ưủ","ưux","ưũ","ưuj","ưụ",

            "eé","eè","eẻ","eẹ","eẽ",

            "oỏ",
            "oò",
            "oó",
            "oõ",
            "oọ",
            "ơj",
            "ơs",
            "ơx",
            "ơs",
            "ơx",
            "ơf",
            "ơr",
            "ưr",
            "ưf",
            "ưs",
            "ưj",
            "ưx",
        };
        public static string[] Unicode = new string[]
        {
             //uow
             "ươ",

             //"aa","aá","aà","aả","aã","aạ",
             "â","ấ","ầ","ẩ","ẫ","ậ",

             //"aw","ăs","ăr","ăx","ăf","ăj",
             "ă","ắ","ẳ","ẵ","ằ","ặ",

             //"as","af","ax","ar","aj",
             "á","à","ã","ả","ạ",
             //
             "đ","dd",
             //
             "é","è","ẽ","ẹ","ẻ","ê",
             //
             "í","ì","ỉ","ĩ","ị",
             //
             "ó","ò","ỏ","õ","ọ",
             //
             "ô",
             //
             "ơ","ow",
             //
             "ú","ù","ủ","ũ","ụ",
             //
             "ưu",
             //
             "ư",
             //
             "uỷ","uý","uỳ","uỵ","uỹ",
             //
             "ý","ỳ","ỷ","ỹ","ỵ",
             //
             "ướ","ườ","ướ","ưỡ","ượ","ưở",
             //
             "ứu","ứu","ừu","ừu","ửu","ửu","ữu","ữu","ựu","ựu",
             //
             "ế","ề","ể","ệ","ễ",
             "ổ",
             "ồ",
             "ố",
             "ỗ",
             "ộ",
             "ợ",
             "ớ",
             "ỡ",
             "ờ",
             "ở",
             "ử",
             "ừ",
             "ứ",
             "ự",
             "ữ",
        };
        public static string Convert(string text)
        {
            for(int i  = 0; i < Unicode.Length; i++)
            {
                text = text.Replace(Telex[i], Unicode[i]);
            }
            return text;
        }
    }
}
