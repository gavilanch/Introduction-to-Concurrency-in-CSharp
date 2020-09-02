using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Winforms.Code.Module_4
{
    public class StartNew
    {
        public async Task btnStart_Click()
        {
            var resultStartNew = await Task.Factory.StartNew(async () =>
            {
                await Task.Delay(1000);
                return 7;
            }).Unwrap();

            var resultRun = await Task.Run(async () =>
            {
                await Task.Delay(1000);
                return 7;
            });

            Console.WriteLine($"Result StartNew: {resultStartNew}");
            Console.WriteLine("----");
            Console.WriteLine($"Result Task.run: {resultRun}");
        }
    }
}
