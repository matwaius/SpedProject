using System.Data;
using dotnet_api.Methods;

namespace dotnet_api.Methods.Classes
{
    public partial class C460 : IC460
    {
        public string REG { get; set; }
        public string COD_MOD { get; set; }
        public string COD_SIT { get; set; }
        public Int32 NUM_DOC { get; set; }
        public DateTime DT_DOC { get; set; }
        public decimal VL_DOC { get; set; }
        public decimal VL_PIS { get; set; }
        public decimal VL_COFINS { get; set; }
        public string CPF_CNPJ { get; set; }
        public string NOM_ADQ { get; set; }

        public C460 MountDataC460(string[] data)
        {
            C460 c460 = new C460();

            c460.REG = Library.GetString(data[1]);
            c460.COD_MOD = Library.GetString(data[2]);
            c460.COD_SIT = Library.GetString(data[3]);
            c460.NUM_DOC = Library.GetInt32(data[4]);
            c460.DT_DOC = Library.ToDateTime(data[5], "ddMMyyyy");
            c460.VL_DOC = Library.GetDecimal(data[6]);
            c460.VL_PIS = Library.GetDecimal(data[7]);
            c460.VL_COFINS = Library.GetDecimal(data[8]);
            c460.CPF_CNPJ = Library.GetString(data[9]);
            c460.NOM_ADQ = Library.GetString(data[10]);

            return c460;
        }

    }


}
