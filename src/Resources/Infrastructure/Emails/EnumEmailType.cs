using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Infrastructure.Emails
{
    public enum EnumEmailType
    {
        [Description("Forgot password")]
        ForgotPassword = 0,
    }
}
