using System;

namespace IsNumber
{
    class Program
    {
        public static void Main(string[] args)
        {
            while (true)
            {

                string isn = Console.ReadLine().Trim();
                Console.WriteLine(isNumber(isn));
            }
        }

        public static bool isNumber(string s)
        {
            string x = s;
            if (s.Contains("e"))
            {
                //split the string by e
                string[] numb = s.Split('e');
                //if there are more characters 'e' then return false
                if (numb.Length > 2)
                    return false;

                if (!checkChars(numb[1], "-+"))
                    return false;

                System.Numerics.BigInteger r;
                if (!System.Numerics.BigInteger.TryParse(numb[1], out r))
                    return false;

                x = numb[0];
            }

            if (!checkChars(x, ".+-"))
                return false;

            if (x.Contains('.'))
            {
                string[] halves = x.Split('.');
                if (halves.Length > 2)
                    return false;
                System.Numerics.BigInteger r;
                if (halves[1].Length > 0 && (!System.Numerics.BigInteger.TryParse(halves[1], out r) || !checkChars(halves[1], "")))
                    return false;
                if (halves[0].Length > 0 && !System.Numerics.BigInteger.TryParse(halves[0], out r)
                    && !((halves[0] == "-" || halves[0] == "+") && halves[0].Length == 1 && halves[1].Length > 0))
                    return false;
                if (x.Length < 2)
                    return false;
            }
            else
            {
                System.Numerics.BigInteger r;
                if (!System.Numerics.BigInteger.TryParse(x, out r))
                    return false;
            }

            return true;
        }

        public static bool checkChars(string s, string additional)
        {
            string allow = "01234567890" + additional; //(withDot ? "." : "") + (withMin ? "-" : "");
            foreach (char c in s)
            {
                if (!allow.Contains(c))
                    return false;
            }
            return true;
        }
    }
}
