using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goke.Core
{
    public class Text
    {
        //public static readonly char[] SPECIAL = "!@#%^&*()_+-/=[]{}|;:,.<>?`~".ToCharArray();
        //public static readonly char[] UPPERALPHABETH = "ABCDEFGHIJKLMNPQRSTUVWXYZ".ToCharArray();
        //public static readonly char[] LOWERALPHABETH = "abcdefghijklmnopqrstuvwxyz".ToCharArray();
        //public static readonly char[] NUMBER = "0123456789".ToCharArray();

        public static readonly char[] SPECIAL = "@#%&+=.?".ToCharArray();
        public static readonly char[] UPPERALPHABETH = "ABCDEFGHJKLMNPQRSTUVWXYZ".ToCharArray();
        public static readonly char[] LOWERALPHABETH = "abcdefghijkmnpqrstuvwxyz".ToCharArray();
        public static readonly char[] NUMBER = "123456789".ToCharArray();

        public static string Generate(
            int specialLength = 4,
            int upperAlphabethLength = 4,
            int lowerAlphabethLength = 4,
            int numberLength = 4
            )
        {
            var length = specialLength + upperAlphabethLength + lowerAlphabethLength + numberLength;

            StringBuilder pin = new();

            //
            int k = 0;
            var s = NUMBER[k];

            // 
            for (int i = 0; i < numberLength; i++)
            {
                k = Random.Shared.Next(NUMBER.Length);
                s = NUMBER[k];
                k = Random.Shared.Next(pin.Length);
                pin = pin.Insert(k, s);
            }

            // 
            for (int i = 0; i < specialLength; i++)
            {
                k = Random.Shared.Next(SPECIAL.Length);
                s = SPECIAL[k];
                k = Random.Shared.Next(1, pin.Length-1);
                pin = pin.Insert(k, s);
            }

            // 
            for (int i = 0; i < upperAlphabethLength; i++)
            {
                k = Random.Shared.Next(UPPERALPHABETH.Length);
                s = UPPERALPHABETH[k];
                k = Random.Shared.Next(pin.Length);
                pin = pin.Insert(k, s);
            }

            // 
            for (int i = 0; i < lowerAlphabethLength; i++)
            {
                k = Random.Shared.Next(LOWERALPHABETH.Length);
                s = LOWERALPHABETH[k];
                k = Random.Shared.Next(1, pin.Length - 1);
                pin = pin.Insert(k, s);
            }

            return pin.ToString();
        }

        public static string GeneratePassword(int length)
        {
            int textLength = length / 4;
            int reminderLength = length % 4;

            var pin = Generate(specialLength: textLength, numberLength: textLength, upperAlphabethLength: textLength, lowerAlphabethLength: textLength + reminderLength);

            return pin;
        }

        public static string GeneratePin()
        {
            var pin = Generate(specialLength: 1, numberLength: 12, upperAlphabethLength: 1, lowerAlphabethLength: 1);
            return pin;
        }

        public static string GeneratePin(int length)
        {
            var pin = Generate(specialLength: 1, numberLength: (length-3), upperAlphabethLength: 1, lowerAlphabethLength: 1);
            return pin;
        }

        public static string MakePinReadable(string pin, int segment=4)
        {
            int length = pin.Length;
            int reminderLength = length % segment;
            int textLength = length / segment;

            var n = textLength - (reminderLength == 0 ? 1:0);

            for (int i = 0; i < n; i++)
            {
                var k = (segment * (i + 1)) + i;
                pin = pin.Insert(k, " ");
            }

            return pin;
        }

    }
}
