using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace dotnet_api.Methods
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
            string value = OnlyNumbers(parMetodo);
            if (value.Length == 0)
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
            string value = OnlyNumbers(parMetodo);
            if (value.Length == 0)
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
            string value = OnlyNumbers(parMetodo);
            if (value.Length == 0)
            {
                ret = valorRetorno;
            }
            else
            {
                ret = Convert.ToDecimal(parMetodo);
            }
            return ret;
        }


        public static string OnlyNumbers(string strNumero)
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

        public static DateTime Data(string data)
        {
            
            int dd = 0;
            int mm = 0;
            int yyyy = 0;

            string[] partes = data.Split(new char[] { '/', '-' });
            dd = Convert.ToInt32(partes[0]);
            mm = Convert.ToInt32(partes[1]);
            yyyy = Convert.ToInt32(partes[2].Substring(0,4));

            DateTime dt = new DateTime(yyyy, mm, dd);

            return dt;
        }

        public static DateTime GetDateTime(string parMetodo)
        {
            return GetDateTime(parMetodo, false);
        }

        public static DateTime GetDateTime(string parMetodo, bool parInverteDiaMes)
        {
            DateTime ret;
            string tmpMetodo = parMetodo;
            string dia = "";
            string mes = "";
            string ano = "";
            string tmp = "";

            DateTime dt;
            if(DateTime.TryParse(tmpMetodo, out dt))
            {
                return dt;
            }

            string AMD_Regex = @"([\d]{4}).([\d]{2}).([\d]{2})";
            if(Regex.IsMatch(tmpMetodo, AMD_Regex))
            {
                tmpMetodo = Regex.Replace(tmpMetodo, AMD_Regex, "$3-$2-$1");
            }

            if(tmpMetodo == null || tmpMetodo.Length == 0)
            {
                ret = "01/01/2000".ToDateTime();
            }
            else
            {
                dia = (parInverteDiaMes == false) ? tmp.Substring(0, 2) : tmp.Substring(2, 2);
                mes = (parInverteDiaMes == false) ? tmp.Substring(2, 2) : tmp.Substring(0, 2);
                ano = tmp.Substring(4, 4);

                if (Library.CheckDate(Convert.ToInt32(dia), Convert.ToInt32(mes), Convert.ToInt32(ano)) == false)
                {
                    ret = "01/01/2000".ToDateTime(); 
                }
                else
                {
                    tmp = dia + "/" + mes + "/" + ano;
                    ret = Convert.ToDateTime(tmp);
                }
            }

            return ret;
        }

        public static bool CheckDate(int dia, int mes, int ano)
        {
            int ultimoDiaMes = DateTime.DaysInMonth(ano, mes);
            if (dia > ultimoDiaMes || dia < 1)
                return false;
            else
                return true;
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

        public static string DownFile(IConfiguration config)
        {
            string Retorno = "";

            SqlConnection conn = new SqlConnection(config.GetConnectionString("UsersAppCon"));
            conn.Open();
            string query = "SELECT top 1 DataFiles FROM Files ORDER BY id Desc";
            SqlCommand cmd = new SqlCommand(query, conn);

            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            conn.Close();

            if(dt.Rows.Count == 0 || dt == null)
            {
                return Retorno;
            }

            byte[] buffer = dt.AsEnumerable().Select(c => c.Field<byte[]>("DataFiles")).SingleOrDefault();

            if (buffer == null)
            {
                return Retorno;
            }

            Retorno = Encoding.Default.GetString(buffer);

            return Retorno;
        }

        public static DataTable ToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);
            //Get all the properties
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                //Setting column names as Property names
                dataTable.Columns.Add(prop.Name);
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    //inserting property values to datatable rows
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            //put a breakpoint here and check datatable
            return dataTable;
        }

        public static DataTable dataUF()
        {
            DataTable dtUF = new DataTable();
            DataColumn column;
            DataRow row;

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Int32");
            column.ColumnName = "id";
            dtUF.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "UF";
            dtUF.Columns.Add(column);
            
            row = dtUF.NewRow();
            row["id"] = 11;
            row["UF"] = "RO";
            dtUF.Rows.Add(row);

            row = dtUF.NewRow();
            row["id"] = 12;
            row["UF"] = "AC";
            dtUF.Rows.Add(row);

            row = dtUF.NewRow();
            row["id"] = 13;
            row["UF"] = "AM";
            dtUF.Rows.Add(row);

            row = dtUF.NewRow();
            row["id"] = 14;
            row["UF"] = "RR";
            dtUF.Rows.Add(row);

            row = dtUF.NewRow();
            row["id"] = 15;
            row["UF"] = "PA";
            dtUF.Rows.Add(row);

            row = dtUF.NewRow();
            row["id"] = 16;
            row["UF"] = "AP";
            dtUF.Rows.Add(row);

            row = dtUF.NewRow();
            row["id"] = 17;
            row["UF"] = "TO";
            dtUF.Rows.Add(row);

            row = dtUF.NewRow();
            row["id"] = 21;
            row["UF"] = "MA";
            dtUF.Rows.Add(row);

            row = dtUF.NewRow();
            row["id"] = 22;
            row["UF"] = "PI";
            dtUF.Rows.Add(row);

            row = dtUF.NewRow();
            row["id"] = 23;
            row["UF"] = "CE";
            dtUF.Rows.Add(row);

            row = dtUF.NewRow();
            row["id"] = 24;
            row["UF"] = "RN";
            dtUF.Rows.Add(row);

            row = dtUF.NewRow();
            row["id"] = 25;
            row["UF"] = "PB";
            dtUF.Rows.Add(row);

            row = dtUF.NewRow();
            row["id"] = 26;
            row["UF"] = "PE";
            dtUF.Rows.Add(row);

            row = dtUF.NewRow();
            row["id"] = 27;
            row["UF"] = "AL";
            dtUF.Rows.Add(row);

            row = dtUF.NewRow();
            row["id"] = 28;
            row["UF"] = "SE";
            dtUF.Rows.Add(row);

            row = dtUF.NewRow();
            row["id"] = 29;
            row["UF"] = "BA";
            dtUF.Rows.Add(row);

            row = dtUF.NewRow();
            row["id"] = 31;
            row["UF"] = "MG";
            dtUF.Rows.Add(row);

            row = dtUF.NewRow();
            row["id"] = 32;
            row["UF"] = "ES";
            dtUF.Rows.Add(row);

            row = dtUF.NewRow();
            row["id"] = 33;
            row["UF"] = "RJ";
            dtUF.Rows.Add(row);

            row = dtUF.NewRow();
            row["id"] = 35;
            row["UF"] = "SP";
            dtUF.Rows.Add(row);

            row = dtUF.NewRow();
            row["id"] = 41;
            row["UF"] = "PR";
            dtUF.Rows.Add(row);

            row = dtUF.NewRow();
            row["id"] = 42;
            row["UF"] = "SC";
            dtUF.Rows.Add(row);

            row = dtUF.NewRow();
            row["id"] = 43;
            row["UF"] = "RS";
            dtUF.Rows.Add(row);

            row = dtUF.NewRow();
            row["id"] = 50;
            row["UF"] = "MS";
            dtUF.Rows.Add(row);

            row = dtUF.NewRow();
            row["id"] = 51;
            row["UF"] = "MT";
            dtUF.Rows.Add(row);

            row = dtUF.NewRow();
            row["id"] = 52;
            row["UF"] = "GO";
            dtUF.Rows.Add(row);

            row = dtUF.NewRow();
            row["id"] = 53;
            row["UF"] = "DF";
            dtUF.Rows.Add(row);

            return dtUF;
        }
    }
}
