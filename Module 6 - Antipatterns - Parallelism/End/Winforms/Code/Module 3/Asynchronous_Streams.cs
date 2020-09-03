using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Winforms.Code.Module_3
{
    public class Asynchronous_Streams
    {
        public async Task btnIniciar_Click()
        {
            await foreach (var name in GenerateNames())
            {
                Console.WriteLine(name);
            }
        }

        private async IAsyncEnumerable<string> GenerateNames()
        {
            yield return "Felipe";
            await Task.Delay(2000);
            yield return "Claudia";
        }
    }
}
