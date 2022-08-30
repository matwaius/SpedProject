using System.Data;
using dotnet_api.Methods;

namespace dotnet_api.Methods.Classes
{
    public partial class C405 : IC405
    {
        public string REG { get; set; }
        public DateTime DT_DOC { get; set; }
        public Int32 CRO { get; set; }
        public Int32 CRZ { get; set; }
        public Int32 NUM_COO_FIN { get; set; }
        public decimal GT_FIN { get; set; }
        public decimal VL_BRT { get; set; }

        public C405 MountDataC405(string[] data)
        {
            C405 c405 = new C405();

            c405.REG = Library.GetString(data[1]);
            c405.DT_DOC = Library.ToDateTime(data[2], "ddMMyyyy");
            c405.CRO = Library.GetInt32(data[3]);
            c405.CRZ = Library.GetInt32(data[4]);
            c405.NUM_COO_FIN = Library.GetInt32(data[5]);
            c405.GT_FIN = Library.GetDecimal(data[6]);
            c405.VL_BRT = Library.GetDecimal(data[7]);

            return c405;
        }

    }


}
