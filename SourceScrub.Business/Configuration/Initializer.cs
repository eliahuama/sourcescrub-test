namespace SourceScrub.Business
{
    public class Initializer
    {
        private bool hasInitialized = false;
        private readonly Data.Initializer _dalInitializer;

        public Initializer(Data.Initializer dalInitializer) {
            _dalInitializer = dalInitializer;
        }

        public void Initialize() {
            if(hasInitialized) return;

            _dalInitializer.Initialize();

            hasInitialized = true;
        }
    }
}
