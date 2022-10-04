using System.Data;
using dotnet_api.Methods;

namespace dotnet_api.Methods.Classes
{
    public partial class C170 : IC170
    {
        public string REG { get; set; }
        public Int32 NUM_ITEM { get; set; }
        public string COD_ITEM { get; set; }
        public string DESCR_COMPL { get; set; }
        public decimal QTD { get; set; }
        public string UNID { get; set; }
        public decimal VL_ITEM { get; set; }
        public decimal VL_DESC { get; set; }
        public Int16 IND_MOV { get; set; }
        public string CST_ICMS { get; set; }
        public Int16 CFOP { get; set; }
        public string COD_NAT { get; set; }
        public decimal VL_BC_ICMS { get; set; }
        public decimal ALIQ_ICMS { get; set; }
        public decimal VL_ICMS { get; set; }
        public decimal VL_BC_ICMS_ST { get; set; }
        public decimal ALIQ_ST { get; set; }
        public decimal VL_ICMS_ST { get; set; }
        public Int16 IND_APUR { get; set; }
        public string CST_IPI { get; set; }
        public string COD_ENQ { get; set; }
        public decimal VL_BC_IPI { get; set; }
        public decimal ALIQ_IPI { get; set; }
        public decimal VL_IPI { get; set; }
        public string CST_PIS { get; set; }
        public decimal VL_BC_PIS { get; set; }
        public decimal ALIQ_PIS_PERC { get; set; }
        public decimal QUANT_BC_PIS { get; set; }
        public decimal ALIQ_PIS_REAIS { get; set; }
        public decimal VL_PIS { get; set; }
        public string CST_COFINS { get; set; }
        public decimal VL_BC_COFINS { get; set; }
        public decimal ALIQ_COFINS_PERC { get; set; }
        public decimal QUANT_BC_COFINS { get; set; }
        public decimal ALIQ_COFINS_REAIS { get; set; }
        public decimal VL_COFINS { get; set; }
        public string COD_CTA { get; set; }
        public decimal VL_ABAT_NT { get; set; }
        public decimal PERC { get; set; }

        public C170 MountDataC170(string[] data)
        {
            C170 c170 = new C170();

            c170.REG = Library.GetString(data[1]);
            c170.NUM_ITEM = Library.GetInt32(data[2]);
            c170.COD_ITEM = Library.GetString(data[3]);
            c170.DESCR_COMPL = Library.GetString(data[4]);
            c170.QTD = Library.GetDecimal(data[5]);
            c170.UNID = Library.GetString(data[6]);
            c170.VL_ITEM = Library.GetDecimal(data[7]);
            c170.VL_DESC = Library.GetDecimal(data[8]);
            c170.IND_MOV = Library.GetInt16(data[9]);
            c170.CST_ICMS = Library.GetString(data[10]);
            c170.CFOP = Library.GetInt16(data[11]);
            c170.COD_NAT = Library.GetString(data[12]);
            c170.VL_BC_ICMS = Library.GetDecimal(data[13]);
            c170.ALIQ_ICMS = Library.GetDecimal(data[14]);
            c170.VL_ICMS = Library.GetDecimal(data[15]);
            c170.VL_BC_ICMS_ST = Library.GetDecimal(data[16]);
            c170.ALIQ_ST = Library.GetDecimal(data[17]);
            c170.VL_ICMS_ST = Library.GetDecimal(data[18]);
            c170.IND_APUR = Library.GetInt16(data[19]);
            c170.CST_IPI = Library.GetString(data[20]);
            c170.COD_ENQ = Library.GetString(data[21]);
            c170.VL_BC_IPI = Library.GetDecimal(data[22]);
            c170.ALIQ_IPI = Library.GetDecimal(data[23]);
            c170.VL_IPI = Library.GetDecimal(data[24]);
            c170.CST_PIS = Library.GetString(data[25]);
            c170.VL_BC_PIS = Library.GetDecimal(data[26]);
            c170.ALIQ_PIS_PERC = Library.GetDecimal(data[27]);
            c170.QUANT_BC_PIS = Library.GetDecimal(data[28]);
            c170.ALIQ_PIS_REAIS = Library.GetDecimal(data[29]);
            c170.VL_PIS = Library.GetDecimal(data[30]);
            c170.CST_COFINS = Library.GetString(data[31]);
            c170.VL_BC_COFINS = Library.GetDecimal(data[32]);
            c170.ALIQ_COFINS_PERC = Library.GetDecimal(data[33]);
            c170.QUANT_BC_COFINS = Library.GetDecimal(data[34]);
            c170.ALIQ_COFINS_REAIS = Library.GetDecimal(data[35]);
            c170.VL_COFINS = Library.GetDecimal(data[36]);
            c170.COD_CTA = Library.GetString(data[37]);
            c170.VL_ABAT_NT = Library.GetDecimal(data[38]);

            return c170;
        }

    }


}
