using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Winforms.Code.Module_3
{
    public class Review_IEnumerable
    {
        public void btnStart_Click()
        {
            foreach (var name in GenerateNames())
            {
                Console.WriteLine(name);
            }
        }

        private IEnumerable<string> GenerateNames()
        {
            yield return "Felipe";
            yield return "Claudia";
        }

    }
}
