using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Winforms.Code.Module_2
{
    public class ConfigureAwait
    {
        public async Task btnStart_Click(Button btnCancel)
        {
            btnCancel.Text = "before";

            // error: Illegal call exception will be thrown below on btnCancel.Text = "after"
            //await Task.Delay(1000).ConfigureAwait(continueOnCapturedContext: false);

            // These two lines are equivalent (meaning true is the default)
            await Task.Delay(1000).ConfigureAwait(continueOnCapturedContext: true);
            await Task.Delay(1000);

            btnCancel.Text = "after";
        }
    }
}
