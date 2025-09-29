
namespace Company.PL.Services
{
    public class SeriveSingleton : ISeriveSingleton
    {
        public SeriveSingleton()
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
