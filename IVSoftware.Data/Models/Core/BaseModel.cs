namespace IVSoftware.Data.Models.Core
{
    public abstract class BaseModel<T> where T : struct
    {
        public T Id { get; set; }
    }
}