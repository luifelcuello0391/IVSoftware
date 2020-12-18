using IVSoftware.Data.Models.Core;

namespace IVSoftware.Data.Models
{
    public class Gender : BaseModel<int>
    {
        public string Name { get; set; }
    }
}