using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Winforms.Code.Module_4
{
    public class sync_over_async
    {
        public async Task btnStart_Click()
        {
            // Antipattern: sync-over-async
            var value = GetValue().Result;

            // Optimal solution
            //var value = await GetValue();

            Console.WriteLine(value);

            var a = 2 + 2; // this does not block the current thread, hence no problem with this
        }

        private async Task<string> GetValue()
        {
            // sub-obtimal solution (not ideal in this case, still blocking the UI thread)
            await Task.Delay(1000).ConfigureAwait(false);
            return "Felipe";
        }
    }
}
