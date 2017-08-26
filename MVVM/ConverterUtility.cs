using System.Linq;

namespace WPFMVVMUtility
{
    public class ConverterUtility
    {
        public const string NegationParameter = ":Negate";
        public const char Seperator = ',';

        public static bool IsNegate(string text)
        {
            return HasParameter(NegationParameter, text);
        }

        public static bool HasParameter(string parameter, string text)
        {
            if (string.IsNullOrEmpty(text))
                return false;

            string p = parameter.ToUpper();
            var lst = text.ToUpper().Split(Seperator);
            return lst.FirstOrDefault(x => x.Trim() == p) != null;
        }
    }
}
