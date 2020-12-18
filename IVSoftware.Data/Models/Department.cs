using IVSoftware.Data.Models.Core;

namespace IVSoftware.Data.Models
{
    public class Department : BaseModel<int>
    {
        public string Name { get; set; }
        public string Code { get; set; }
    }
}