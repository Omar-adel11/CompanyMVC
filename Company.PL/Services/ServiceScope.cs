
namespace Company.PL.Services
{
    public class ServiceScope : IServiceScope
    {
        public ServiceScope() 
        {
            Guid=Guid.NewGuid();
        }
        public Guid Guid { get ; set ; }

        public string getGuid()
        {
            return Guid.ToString();
        }
    }
}
