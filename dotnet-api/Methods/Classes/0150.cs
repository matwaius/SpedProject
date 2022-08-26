namespace dotnet_api.Methods.Classes
{
    public class _0150 : I0150
    {
        public string REG { get; set; }
        public string COD_PART { get; set; }
        public string NOME { get; set; }
        public Int32 COD_PAIS { get; set; }
        public string CNPJ { get; set; }
        public string CPF { get; set; }
        public string IE { get; set; }
        public string COD_MUN { get; set; }
        public string SUFRAMA { get; set; }
        public string END { get; set; }
        public string NUM { get; set; }
        public string COMPL { get; set; }
        public string BAIRRO { get; set; }

        public _0150 MountData0150(string[] data)
        {
            _0150 _0150 = new _0150();

            _0150.REG = Library.GetString(data[1]);
            _0150.COD_PART = Library.GetString(data[2]);
            _0150.NOME = Library.GetString(data[3]);
            _0150.COD_PAIS = Library.GetInt32(data[4]);
            _0150.CNPJ = Library.GetString(data[5]);
            _0150.CPF = Library.GetString(data[6]);
            _0150.IE = Library.GetString(data[7]);
            _0150.COD_MUN = Library.GetString(data[8]);
            _0150.SUFRAMA = Library.GetString(data[9]);
            _0150.END = Library.GetString(data[10]);
            _0150.NUM = Library.GetString(data[11]);
            _0150.COMPL = Library.GetString(data[12]);
            _0150.BAIRRO = Library.GetString(data[13]);

            return _0150;
        }
    }
}
