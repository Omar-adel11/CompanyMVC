
namespace Company.PL.Services
{
    public class ServiceTransient : IServiceTransient
    {
        public ServiceTransient()
        {
            Guid = Guid.NewGuid();
        }
        public Guid Guid { get; set; }

        public string getGuid()
        {
            return Guid.ToString();
        }
    }
}
