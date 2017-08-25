using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mogwai.DDO.Explorer.UI
{
    public enum DateFilterOptions
    {
        [Description("On or Before this Date")]
        OnOrBefore,

        [Description("On this Date")]
        On,

        [Description("On or After this Date")]
        OnOrAfter
    }
}
