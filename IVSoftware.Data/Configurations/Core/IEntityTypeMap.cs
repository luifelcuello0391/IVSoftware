using Microsoft.EntityFrameworkCore;

namespace IVSoftware.Data.Configurations.Core
{
    public interface IEntityTypeMap
    {
        void Map(ModelBuilder builder);
    }
}