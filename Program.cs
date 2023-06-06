using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace FlyClock
{
    class Program
    {
        public string NewRegInfo()
        {
            Console.WriteLine("请输入注册名：");
            String RegName = Console.ReadLine()!;
            Console.WriteLine("请输入注册邮箱：");
            String RegEmail = Console.ReadLine()!;
            Console.WriteLine("请输入注册版本(X.X)");
            String AVer = Console.ReadLine()!;
            if (RegName.Length == 0 | RegEmail.Length == 0)
            {
                return "Err";
            }
            else if (!(Regn(AVer)))
            {
                return "VerErr";
            }
            string value = Regex.Match(AVer!, "\\d{1,}.\\d{1,}").Value;
            string text = RegEmail!.Trim().ToLower();
            string text2 = (RegName + value).Trim().ToLower() + 2;

            byte[] bytes = Encoding.UTF8.GetBytes(text);
            byte[] bytes2 = Encoding.UTF8.GetBytes(text2);
            MD5 md = MD5.Create();
            byte[] array = md.ComputeHash(bytes);
            byte[] array2 = md.ComputeHash(bytes2);
            string text3 = "";
            string text4 = "";
            for (int i = 0; i < array.Length; i++)
            {
                text3 += array[i].ToString("X2");
            }
            for (int j = 0; j < array2.Length; j++)
            {
                text4 += array2[j].ToString("X2");
            }
            string text5 = Regex.Replace(text4, "[^0-9]+", "");
            if (text5.Length < 6)
            {
                int num = 0;
                do
                {
                    text5 += num.ToString();
                    num++;
                }
                while (num < 6);
            }
            string text6 = "";
            string text7 = "2005-02-25".Replace("-", "").Substring(2, 6);
            for (int l = 0; l < 6; l++)
            {
                int num3 = int.Parse(text5.Substring(l, 1)) + int.Parse(text7.Substring(l, 1));
                num3 %= 10;
                text6 += text3.Substring(num3, 1);
            }
            return text6;
        }

        static bool Regn(string input)
        {
            Regex reg = new Regex("\\d{1,}.\\d{1,}");
            return reg.IsMatch(input);//返回值可以是bool
        }

        public static void Main()
        {
            Program p = new Program();
            string license = p.NewRegInfo();
            if (license == "Err")
            {
                Console.WriteLine("相关信息不能为空！");
            }
            else if (license == "VerErr")
            {
                Console.WriteLine("版本输入错误！\r\n版本形如：5.8");
            }
            else
            {
                Console.WriteLine(2 + license);
            }
        }
    }

}

