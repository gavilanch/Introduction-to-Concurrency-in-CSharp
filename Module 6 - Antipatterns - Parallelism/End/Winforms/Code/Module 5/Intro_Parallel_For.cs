using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Winforms.Code.Module_5
{
    public class Intro_Parallel_For
    {
        public void btnStart_Click()
        {
            Console.WriteLine("sequential");

            for (int i = 0; i <= 10; i++)
            {
                Console.WriteLine(i);
            }

            Console.WriteLine("parallel");

            Parallel.For(0, 11, i => Console.WriteLine(i));
        }
    }
}
