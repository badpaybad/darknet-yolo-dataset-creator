using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotCloud.YoloCreatorDataTrain.Extensions
{
   public static class StringExtensions
    {
        public static string ReplaceAll(this string src, string find, string replaceWith)
        {
            while (src.IndexOf(find) >= 0)
            {
                src = src.Replace(find, replaceWith);
            }
            return src;
        }

    }
}
