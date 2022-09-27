using System.Data;
using dotnet_api.Methods;

namespace dotnet_api.Methods.Classes
{
    public partial class C470 : IC470
    {
        public string REG { get; set; }
        public string COD_ITEM { get; set; }
        public decimal QTD { get; set; }
        public decimal QTD_CANC { get; set; }
        public string UNID { get; set; }
        public decimal VL_ITEM { get; set; }
        public Int16 CST_ICMS { get; set; }
        public Int16 CFOP { get; set; }
        public decimal ALIQ_ICMS { get; set; }
        public decimal VL_PIS { get; set; }
        public decimal VL_COFINS { get; set; }
        public string DESCR_ITEM { get; set; }

        public C470 MountDataC470(string[] data)
        {
            C470 c470 = new C470();

            c470.REG = Library.GetString(data[1]);
            c470.COD_ITEM = Library.GetString(data[2]);
            c470.QTD = Library.GetDecimal(data[3]);
            c470.QTD_CANC = Library.GetDecimal(data[4]);
            c470.UNID = Library.GetString(data[5]);
            c470.VL_ITEM = Library.GetDecimal(data[6]);
            c470.CST_ICMS = Library.GetInt16(data[7]);
            c470.CFOP = Library.GetInt16(data[8]);
            c470.ALIQ_ICMS = Library.GetDecimal(data[9]);
            c470.VL_PIS = Library.GetDecimal(data[10]);
            c470.VL_COFINS = Library.GetDecimal(data[11]);

            return c470;
        }

    }


}
