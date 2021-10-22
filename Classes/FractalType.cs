using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_TestProject2.Classes
{
    enum FractalType
    {
        [Description("Dragon curve")]
        DRAGON_CURVE,
        [Description("Koch snowflake")]
        KOCH_SNOWFLAKE,
        [Description("Barnsley fern")]
        BARNSLEY_FERN
    }

    static class EnumExtension
    {
        public static string GetDescription(this Enum value)
        {
            FieldInfo fieldInfo = value.GetType().GetField(value.ToString());
            if (fieldInfo == null) return null;
            var attribute = (DescriptionAttribute)fieldInfo.GetCustomAttribute(typeof(DescriptionAttribute));
            return attribute.Description;
        }
    }
}
