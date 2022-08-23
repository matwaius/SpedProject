using System.Globalization;
using System.Text;

namespace dotnet_api.Metodos
{
    public static class Library
    {
        public static string GetString(string parMetodo, string valorRetorno = "")
        {
            string ret;
            if (parMetodo == null)
            {
                parMetodo = "";

            }
            if (parMetodo.Length == 0)
            {
                ret = valorRetorno;
            }
            else
            {
                ret = parMetodo;
            }
            return ret;
        }

        public static Int16 GetInt16(string parMetodo, Int16 valorRetorno = 0)
        {
            Int16 ret;
            if(parMetodo == null)
            {
                parMetodo = "";

            }
            if (parMetodo.Length == 0)
            {
                ret = valorRetorno;
            }
            else
            {
                ret = Convert.ToInt16(parMetodo);
            }
            return ret;
        }

        public static Int32 GetInt32(string parMetodo, Int32 valorRetorno = 0)
        {
            Int32 ret;
            if (parMetodo == null)
            {
                parMetodo = "";

            }
            parMetodo = SoNumeros(parMetodo);
            if (parMetodo.Length == 0)
            {
                ret = valorRetorno;
            }
            else
            {
                ret = Convert.ToInt32(parMetodo);
            }
            return ret;
        }

        public static Int64 GetInt64(string parMetodo, Int64 valorRetorno = 0)
        {
            Int64 ret;
            if (parMetodo == null)
            {
                parMetodo = "";

            }
            parMetodo = SoNumeros(parMetodo);
            if (parMetodo.Length == 0)
            {
                ret = valorRetorno;
            }
            else
            {
                ret = Convert.ToInt64(parMetodo);
            }
            return ret;
        }

        public static decimal GetDecimal(string parMetodo, decimal valorRetorno = 0)
        {
            decimal ret;
            if (parMetodo == null)
            {
                parMetodo = "";

            }
            parMetodo = SoNumeros(parMetodo);
            if (parMetodo.Length == 0)
            {
                ret = valorRetorno;
            }
            else
            {
                ret = Convert.ToDecimal(parMetodo);
            }
            return ret;
        }


        public static string SoNumeros(string strNumero)
        {
            if (string.IsNullOrEmpty(strNumero) || strNumero.Trim().Length == 0)
                return "";
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < strNumero.Length; ++i)
            {
                if (char.IsDigit(strNumero[i]))
                    sb.Append(strNumero[i]);
            }
            return sb.ToString();
        }



        public static DateTime ToDateTime(this string s,
                 string format = "ddMMyyyy", string cultureString = "tr-TR")
        {
            try
            {
                var r = (s == "") ? DateTime.Now : DateTime.ParseExact(
                    s: s,
                    format: format,
                    provider: CultureInfo.GetCultureInfo(cultureString));

                return r;
            }
            catch (FormatException)
            {
                throw;
            }
            catch (CultureNotFoundException)
            {
                throw; // Given Culture is not supported culture
            }
        }


        public static DateTime ToDateTime(this string s,
                    string format, CultureInfo culture)
        {
            try
            {
                var r = (s == "") ? DateTime.Now : DateTime.ParseExact(s: s, format: format,
                                        provider: culture);
                return r;
            }
            catch (FormatException)
            {
                throw;
            }
            catch (CultureNotFoundException)
            {
                throw; // Given Culture is not supported culture
            }

        }
    }
}
