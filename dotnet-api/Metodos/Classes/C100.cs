﻿using System.Data;

namespace dotnet_api.Metodos.Classes
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

        public C100 MountDataC100(string[] data)
        {
            C100 c100 = new C100();

            c100.REG = Metodos.Library.GetString(data[1]);
            c100.IND_OPER = Metodos.Library.GetInt16(data[2]);
            c100.IND_EMIT = Metodos.Library.GetString(data[3]);
            c100.COD_PART = Metodos.Library.GetString(data[4]);
            c100.COD_MOD = Metodos.Library.GetString(data[5]);
            c100.COD_SIT = Metodos.Library.GetInt32(data[6]);
            c100.SER = Metodos.Library.GetString(data[7]);
            c100.NUM_DOC = Metodos.Library.GetInt64(data[8]);
            c100.CHV_NFE = Metodos.Library.GetString(data[9]);
            c100.DT_DOC = Metodos.Library.ToDateTime(data[10], "ddMMyyyy");
            c100.DT_E_S = Metodos.Library.ToDateTime(data[11], "ddMMyyyy");
            c100.VL_DOC = Metodos.Library.GetDecimal(data[12]);
            c100.IND_PGTO = Metodos.Library.GetInt16(data[13]);
            c100.VL_DESC = Metodos.Library.GetDecimal(data[14]);
            c100.VL_ABAT_NT = Metodos.Library.GetDecimal(data[15]);
            c100.VL_MERC = Metodos.Library.GetDecimal(data[16]);
            c100.IND_FRT = Metodos.Library.GetInt16(data[17]);
            c100.VL_FRT = Metodos.Library.GetDecimal(data[18]);
            c100.VL_SEG = Metodos.Library.GetDecimal(data[19]);
            c100.VL_OUT_DA = Metodos.Library.GetDecimal(data[20]);
            c100.VL_BC_ICMS = Metodos.Library.GetDecimal(data[21]);
            c100.VL_ICMS = Metodos.Library.GetDecimal(data[22]);
            c100.VL_BC_ICMS_ST = Metodos.Library.GetDecimal(data[23]);
            c100.VL_ICMS_ST = Metodos.Library.GetDecimal(data[24]);
            c100.VL_IPI = Metodos.Library.GetDecimal(data[25]);
            c100.VL_PIS = Metodos.Library.GetDecimal(data[26]);
            c100.VL_COFINS = Metodos.Library.GetDecimal(data[26]);
            c100.VL_PIS_ST = Metodos.Library.GetDecimal(data[27]);
            c100.VL_COFINS_ST = Metodos.Library.GetDecimal(data[28]);

            return c100;
        }

    }


}
