using System.Data;
using dotnet_api.Methods;

namespace dotnet_api.Methods.Classes
{
    public partial class C400 : IC400
    {
        public string REG { get; set; }
        public string COD_MOD { get; set; }
        public string ECF_MOD { get; set; }
        public string ECF_FAB { get; set; }
        public Int32 ECF_CX { get; set; }

        public C400 MountDataC400(string[] data)
        {
            C400 c400 = new C400();

            c400.REG = Library.GetString(data[1]);
            c400.COD_MOD = Library.GetString(data[2]);
            c400.ECF_MOD = Library.GetString(data[3]);
            c400.ECF_FAB = Library.GetString(data[4]);
            c400.ECF_CX = Library.GetInt32(data[5]);

            return c400;
        }

    }


}
