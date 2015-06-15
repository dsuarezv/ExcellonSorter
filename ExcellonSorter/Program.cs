using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcellonSorter
{
    class Program
    {
        static void Main(string[] args)
        {
            if (!CheckUsage(args)) return;

            using (var r = new StreamReader(args[0]))
            {
                using (var w = new StreamWriter(args[1]))
                {
                    var p = new ExcellonParser();
                    p.ProcessStream(r, w);
                }
            }
        }

        static bool CheckUsage(string[] args)
        {
            if (args.Length != 2)
            {
                Console.WriteLine("Usage: ExcellonSorter <input file> <output file>");
                return false;
            }

            return true;
        }
    }
}
