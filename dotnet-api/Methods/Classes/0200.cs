namespace dotnet_api.Methods.Classes
{
    public class _0200 : I0200
    {
        public string REG { get; set; }
        public string COD_ITEM { get; set; }
        public string DESCR_ITEM { get; set; }
        public string COD_BARRA { get; set; }
        public string COD_ANT_ITEM { get; set; }
        public string UNID_INV { get; set; }
        public string TIPO_ITEM { get; set; }
        public string COD_NCM { get; set; }
        public string EX_IPI { get; set; }
        public string COD_GEN { get; set; }
        public string COD_LST { get; set; }
        public decimal ALIQ_ICMS { get; set; }
        public string CEST { get; set; }

        public _0200 MountData0200(string[] data)
        {
            _0200 _0200 = new _0200();

            _0200.REG = Library.GetString(data[1]);
            _0200.COD_ITEM = Library.GetString(data[2]);
            _0200.DESCR_ITEM = Library.GetString(data[3]);
            _0200.COD_BARRA = Library.GetString(data[4]);
            _0200.COD_ANT_ITEM = Library.GetString(data[5]);
            _0200.UNID_INV = Library.GetString(data[6]);
            _0200.TIPO_ITEM = Library.GetString(data[7]);
            _0200.COD_NCM = Library.GetString(data[8]);
            _0200.EX_IPI = Library.GetString(data[9]);
            _0200.COD_GEN = Library.GetString(data[10]);
            _0200.COD_LST = Library.GetString(data[11]);
            _0200.ALIQ_ICMS = Library.GetDecimal(data[12]);
            _0200.CEST = Library.GetString(data[13]);

            return _0200;
        }
    }
}
