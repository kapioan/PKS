using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporatePractice1.Handlers
{
    public static class ArgumentHandler
    {
        public static string[] ParseArgs(string args)
        {
            if (string.IsNullOrEmpty(args))
                return Array.Empty<string>();

            return args.Split("::").Select(x => x.TrimStart().TrimEnd()).Where(x => !string.IsNullOrEmpty(x)).ToArray();
        }

        public static T Parse<T>(string str)
        {
            return (T)Convert.ChangeType(str, typeof(T));
        }
    }
}
