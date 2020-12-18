using IVSoftware.Data.Models.Core;

namespace IVSoftware.Data.Models
{
    public class Municipality : BaseModel<int>
    {
        public string Name { get; set; }

        public string Code { get; set; }

        public virtual Department Department { get; set; }
    }
}