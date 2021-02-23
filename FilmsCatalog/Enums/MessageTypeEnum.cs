using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace FilmsCatalog.Enums
{
    public enum MessageTypeEnum
    {
        [Description("alert-primary")]
        Primary,

        [Description("alert-success")]
        Success,

        [Description("alert-danger")]
        Danger
    }
}
