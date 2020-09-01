using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Winforms.Code.Module_2
{
    public class UI_Not_Freezes
    {
        public void SyncVersion()
        {
            Thread.Sleep(5000);
        }

        public async Task AsyncVersion()
        {
            await Task.Delay(TimeSpan.FromSeconds(5));
        }
    }
}
