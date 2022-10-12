using System.Data;
using dotnet_api.Methods;

namespace dotnet_api.Methods.Classes
{
    public partial class C100 : IC100
    {
        public string REG { get; set; }
        public Int16 IND_OPER { get; set; }
        public string IND_EMIT { get; set; }
        public string COD_PART { get; set; }
        public string COD_MOD { get; set; }
        public Int32 COD_SIT { get; set; }
        public string SER { get; set; }
        public Int64 NUM_DOC { get; set; }
        public string CHV_NFE { get; set; }
        public DateTime DT_DOC { get; set; }
        public DateTime DT_E_S { get; set; }
        public decimal VL_DOC { get; set; }
        public Int16 IND_PGTO { get; set; }
        public decimal VL_DESC { get; set; }
        public decimal VL_ABAT_NT { get; set; }
        public decimal VL_MERC { get; set; }
        public Int16 IND_FRT { get; set; }
        public decimal VL_FRT { get; set; }
        public decimal VL_SEG { get; set; }
        public decimal VL_OUT_DA { get; set; }
        public decimal VL_BC_ICMS { get; set; }
        public decimal VL_ICMS { get; set; }
        public decimal VL_BC_ICMS_ST { get; set; }
        public decimal VL_ICMS_ST { get; set; }
        public decimal VL_IPI { get; set; }
        public decimal VL_PIS { get; set; }
        public decimal VL_COFINS { get; set; }
        public decimal VL_PIS_ST { get; set; }
        public decimal VL_COFINS_ST { get; set; }
        public string NOME { get; set; }
        public List<C170> Itens { get; set; }

        public C100 MountDataC100(string[] data)
        {
            C100 c100 = new C100();

            c100.REG = Library.GetString(data[1]);
            c100.IND_OPER = Library.GetInt16(data[2]);
            c100.IND_EMIT = Library.GetString(data[3]);
            c100.COD_PART = Library.GetString(data[4]);
            c100.COD_MOD = Library.GetString(data[5]);
            c100.COD_SIT = Library.GetInt32(data[6]);
            c100.SER = Library.GetString(data[7]);
            c100.NUM_DOC = Library.GetInt64(data[8]);
            c100.CHV_NFE = Library.GetString(data[9]);
            c100.DT_DOC = Library.ToDateTime(data[10], "ddMMyyyy");
            c100.DT_E_S = Library.ToDateTime(data[11], "ddMMyyyy");
            c100.VL_DOC = Library.GetDecimal(data[12]);
            c100.IND_PGTO = Library.GetInt16(data[13]);
            c100.VL_DESC = Library.GetDecimal(data[14]);
            c100.VL_ABAT_NT = Library.GetDecimal(data[15]);
            c100.VL_MERC = Library.GetDecimal(data[16]);
            c100.IND_FRT = Library.GetInt16(data[17]);
            c100.VL_FRT = Library.GetDecimal(data[18]);
            c100.VL_SEG = Library.GetDecimal(data[19]);
            c100.VL_OUT_DA = Library.GetDecimal(data[20]);
            c100.VL_BC_ICMS = Library.GetDecimal(data[21]);
            c100.VL_ICMS = Library.GetDecimal(data[22]);
            c100.VL_BC_ICMS_ST = Library.GetDecimal(data[23]);
            c100.VL_ICMS_ST = Library.GetDecimal(data[24]);
            c100.VL_IPI = Library.GetDecimal(data[25]);
            c100.VL_PIS = Library.GetDecimal(data[26]);
            c100.VL_COFINS = Library.GetDecimal(data[26]);
            c100.VL_PIS_ST = Library.GetDecimal(data[27]);
            c100.VL_COFINS_ST = Library.GetDecimal(data[28]);

            return c100;
        }

    }


}
