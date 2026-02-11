using Krypton.Toolkit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SchoolManagementSystem
{

    public enum Provinces
    {
        BanteayMeanchey,
        Battambang,
        KampongCham,
        KampongChhnang,
        KampongSpeu,
        KampongThom,
        Kampot,
        Kandal,
        KohKong,
        Kratie,
        Mondulkiri,
        PhnomPenh,
        PreahVihear,
        PreyVeng,
        Pursat,
        Ratanakiri,
        SiemReap,
        PreahSihanouk,
        StungTreng,
        SvayRieng,
        Takeo,
        OddarMeanchey,
        Kep,
        Pailin,
        TboungKhmum
    }
    public static class KhmerUtils
    {


        private static readonly string[] KhmerMonths = {
            "", "មករា", "កុម្ភៈ", "មីនា", "មេសា", "ឧសភា", "មិថុនា",
            "កក្កដា", "សីហា", "កញ្ញា", "តុលា", "វិច្ឆិកា", "ធ្នូ"
        };
        //private static readonly string[] EnglistKhmerProvinces = {
        //    "Banteay Meanchey-បន្ទាយមានជ័យ","បាត់ដំបង","កំពង់ចាម","កំពង់ឆ្នាំង",
        //    "កំពង់ស្ពឺ","កំពង់ធំ","កំពត","កណ្ដាល",
        //    "កោះកុង","ក្រចេះ","មណ្ឌលគិរី","រាជធានីភ្នំពេញ",
        //    "ព្រះវិហារ","ព្រៃវែង","ពោធិ៍សាត់","រតនគិរី",
        //    "សៀមរាប","ព្រះសីហនុ","ស្ទឹងត្រែង","ស្វាយរៀង",
        //    "តាកែវ","ឧត្ដរមានជ័យ","កែប","ប៉ៃលិន","ត្បូងឃ្មុំ"
        //};
        public static readonly Dictionary<Provinces, string> ProvinceEngKh = new()
        {
            { Provinces.BanteayMeanchey, "បន្ទាយមានជ័យ" },
            { Provinces.Battambang, "បាត់ដំបង" },
            { Provinces.KampongCham, "កំពង់ចាម" },
            { Provinces.KampongChhnang, "កំពង់ឆ្នាំង" },
            { Provinces.KampongSpeu, "កំពង់ស្ពឺ" },
            { Provinces.KampongThom, "កំពង់ធំ" },
            { Provinces.Kampot, "កំពត" },
            { Provinces.Kandal, "កណ្ដាល" },
            { Provinces.KohKong, "កោះកុង" },
            { Provinces.Kratie, "ក្រចេះ" },
            { Provinces.Mondulkiri, "មណ្ឌលគិរី" },
            { Provinces.PhnomPenh, "ភ្នំពេញ" },
            { Provinces.PreahVihear, "ព្រះវិហារ" },
            { Provinces.PreyVeng, "ព្រៃវែង" },
            { Provinces.Pursat, "ពោធិ៍សាត់" },
            { Provinces.Ratanakiri, "រតនគិរី" },
            { Provinces.SiemReap, "សៀមរាប" },
            { Provinces.PreahSihanouk, "ព្រះសីហនុ" },
            { Provinces.StungTreng, "ស្ទឹងត្រែង" },
            { Provinces.SvayRieng, "ស្វាយរៀង" },
            { Provinces.Takeo, "តាកែវ" },
            { Provinces.OddarMeanchey, "ឧត្ដរមានជ័យ" },
            { Provinces.Kep, "កែប" },
            { Provinces.Pailin, "ប៉ៃលិន" },
            { Provinces.TboungKhmum, "ត្បូងឃ្មុំ" }
        };
        public static readonly Dictionary<Provinces, string> ProvinceEngCh = new()
        {
            { Provinces.BanteayMeanchey, "卜迭棉芷省" },
            { Provinces.Battambang, "马德望省" },
            { Provinces.KampongCham, "磅针省" },
            { Provinces.KampongChhnang, "磅清杨省" },
            { Provinces.KampongSpeu, "实居省" },
            { Provinces.KampongThom, "磅通省" },
            { Provinces.Kampot, "贡不省" },
            { Provinces.Kandal, "干拉省" },
            { Provinces.KohKong, "国公省" },
            { Provinces.Kratie, "桔井省" },
            { Provinces.Mondulkiri, "蒙多基里省" },
            { Provinces.PhnomPenh, "金边市" },
            { Provinces.PreahVihear, "柏威夏省" },
            { Provinces.PreyVeng, "波罗勉省" },
            { Provinces.Pursat, "菩萨省" },
            { Provinces.Ratanakiri, "拉达那基里省" },
            { Provinces.SiemReap, "暹粒省" },
            { Provinces.PreahSihanouk, "西哈努克市" },
            { Provinces.StungTreng, "上丁省" },
            { Provinces.SvayRieng, "柴桢省" },
            { Provinces.Takeo, "茶胶省" },
            { Provinces.OddarMeanchey, "奥多棉芷省" },
            { Provinces.Kep, "白马市" },
            { Provinces.Pailin, "拜灵市" },
            { Provinces.TboungKhmum, "特本克蒙省" }
        };
        public static string FormatProvince(Provinces province)
        {
            return Regex
                .Replace(province.ToString(), "(?<!^)([A-Z])", " $1")
                .ToUpper();
        }



        private static readonly string[] KhmerDigits = { "០", "១", "២", "៣", "៤", "៥", "៦", "៧", "៨", "៩" };
        private static readonly string[] ChinaDigits = { "零", "一", "二", "三", "四", "五", "六", "七", "八", "九", "十" };

        public static string ToKhmerMonth(int month)
        {
            if (month < 1 || month > 12)
                return "";
            return KhmerMonths[month];
        }

        public static string ToKhmerNumber(int number)
        {
            return ToKhmerNumber(number, 0); // no padding
        }
        public static string ToKhmerNumber(int number, int padLength)
        {
            string format = padLength > 0
                ? new string('0', padLength)
                : "0";

            return string.Concat(
                number.ToString(format).Select(d => KhmerDigits[d - '0'])
            );
        }
        public static string ToKhmerNumber(decimal number)
        {
            // Convert decimal to string with InvariantCulture to get dot as decimal separator
            string numberStr = number.ToString(System.Globalization.CultureInfo.InvariantCulture);

            return string.Concat(numberStr.Select(ch =>
            {
                if (char.IsDigit(ch))
                    return KhmerDigits[ch - '0'];
                else if (ch == '.')  // preserve decimal point
                    return ".";
                else
                    return ch.ToString(); // preserve other chars as string
            }));

        }

        public static string ToChinaNumber(int number)
        {
            return ToChinaNumber(number,0); // no padding
        }
        public static string ToChinaNumber(int number, int padLength)
        {
            string format = padLength > 0
                ? new string('0', padLength)
                : "0";

            return string.Concat(
                number.ToString(format).Select(d => ChinaDigits[d - '0'])
            );
        }
        //public static string ToChinaNumber(decimal number)
        //{
        //    // Convert decimal to string with InvariantCulture to get dot as decimal separator
        //    string numberStr = number.ToString(System.Globalization.CultureInfo.InvariantCulture);

        //    return string.Concat(numberStr.Select(ch =>
        //    {
        //        if (char.IsDigit(ch))
        //            return ChinaDigits[ch - '0'];
        //        else if (ch == '.')  // preserve decimal point
        //            return ".";
        //        else
        //            return ch.ToString(); // preserve other chars as string
        //    }));

        //}



        //public static string ToKhmerNumber(int number)
        //{
        //    //string[] khmerDigits = { "០", "១", "២", "៣", "៤", "៥", "៦", "៧", "៨", "៩" };
        //    return string.Concat(number.ToString("00")
        //        .Select(c => khmerDigits[c - '0']));
        //}


        public static string ToKhmerGender(string gender)
        {
            if (string.IsNullOrEmpty(gender))
                return "";

            gender = gender.Trim().ToLower();

            if (gender == "male")
                return "ប្រុស";  // short form male
            else if (gender == "m")
                return "ប";
            else if (gender == "female")
                return "ស្រី";
            else if (gender == "f")
                return "ស";  // short form female

            return "";
        }
        public static string ToChinaGender(string gender)
        {
            if (string.IsNullOrEmpty(gender))
                return "";

            gender = gender.Trim().ToLower();

            if (gender == "male")
                return "男";  // short form male
            else if (gender == "m")
                return "男";
            else if (gender == "female")
                return "女";
            else if (gender == "f")
                return "女";  // short form female

            return "";
        }

    }



}
