using System.Data;
using dotnet_api.Methods;

namespace dotnet_api.Methods.Classes
{
    public partial class C420 : IC420
    {
        public string REG { get; set; }
        public string COD_TOT_PAR { get; set; }
        public decimal VLR_ACUM_TOT { get; set; }
        public Int32 NR_TOT { get; set; }
        public string DESCR_NR_TOT { get; set; }

        public C420 MountDataC420(string[] data)
        {
            C420 c420 = new C420();

            c420.REG = Library.GetString(data[1]);
            c420.COD_TOT_PAR = Library.GetString(data[2]);
            c420.VLR_ACUM_TOT = Library.GetDecimal(data[3]);
            c420.NR_TOT = Library.GetInt32(data[4]);
            c420.DESCR_NR_TOT = Library.GetString(data[5]);

            return c420;
        }

    }


}
