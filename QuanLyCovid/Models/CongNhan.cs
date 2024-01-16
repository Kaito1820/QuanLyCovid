using System.Security.Policy;

namespace QuanLyCovid.Models
{
    public class CongNhan
    {
        public string macongnhan { get; set; }
        public string tencongnhan { get; set; }
        public bool gioitinh { get; set; }
        public int namsinh {  get; set; }
        public string nuocve { get; set; }
        public string? madcl {  get; set; }
        public CongNhan()
        {

        }

    }
}
