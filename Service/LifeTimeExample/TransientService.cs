namespace WazeCreditGreen.Service.LifeTimeExample {
    public class TransientService {
        private readonly Guid guid;
        public TransientService() { 
            guid = Guid.NewGuid();
        }
        public string GetGuid() => guid.ToString();
    }
}
