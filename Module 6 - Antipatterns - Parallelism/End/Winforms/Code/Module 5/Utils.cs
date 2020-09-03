using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Winforms.Code.Module_5
{
    public static class Utils
    {
        public static void WriteComparison(double time1, double time2)
        {
            var difference = time2 - time1;
            difference = Math.Round(difference, 2);
            var porcentualIncrement = ((time2 - time1) / time1) * 100;
            porcentualIncrement = Math.Round(porcentualIncrement, 2);
            Console.WriteLine($"Difference {difference} ({porcentualIncrement}%)");
        }

        public static void PrepareExecution(string destinationParallel, string destinationSequential)
        {
            if (!Directory.Exists(destinationParallel))
            {
                Directory.CreateDirectory(destinationParallel);
            }

            if (!Directory.Exists(destinationSequential))
            {
                Directory.CreateDirectory(destinationSequential);
            }

            DeleteFiles(destinationSequential);
            DeleteFiles(destinationParallel);
        }

        public static void DeleteFiles(string directory)
        {
            var files = Directory.EnumerateFiles(directory);
            foreach (var file in files)
            {
                File.Delete(file);
            }
        }
    }
}
