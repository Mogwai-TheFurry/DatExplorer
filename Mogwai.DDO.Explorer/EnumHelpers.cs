using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Mogwai.DDO.Explorer
{
    public class EnumHelpers
    {
        public static string GetEnumDescription(Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            DescriptionAttribute[] attributes =
                (DescriptionAttribute[])fi.GetCustomAttributes(
                typeof(DescriptionAttribute),
                false);

            if (attributes != null &&
                attributes.Length > 0)
                return attributes[0].Description;
            else
                return value.ToString();
        }

        public static string GetFileExtension(Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            FileExtensionAttribute[] attributes =
                (FileExtensionAttribute[])fi.GetCustomAttributes(
                    typeof(FileExtensionAttribute),
                    false);

            if (attributes != null &&
                attributes.Length > 0)
                return attributes[0].FileExtension;
            else
                return value.ToString();
        }
    }
}
