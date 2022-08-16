namespace WazeCreditGreen.Service.LifeTimeExample {
    public class SingletonService {
        private readonly Guid guid;
        public SingletonService() {
            guid = Guid.NewGuid();
        }
        public string GetGuid() => guid.ToString();
    }
}
