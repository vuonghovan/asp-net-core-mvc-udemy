using System;
using System.ComponentModel;
using System.Reflection;

namespace Utilities.Enums
{
    public static class EnumExtensions
    {
        public static string GetDescription(this Enum en)
        {
            try
            {
                Type type = en.GetType();
                MemberInfo[] memInfo = type.GetMember((en.ToString()));
                if (memInfo.Length > 0)
                {
                    object[] attrs = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
                    if (attrs.Length > 0)
                    {
                        return ((DescriptionAttribute)attrs[0]).Description;
                    }
                }
                return en.ToString();
            }
            catch
            {
                return String.Empty;
            }
        }
    }
}
