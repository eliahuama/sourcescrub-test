using Microsoft.EntityFrameworkCore;

namespace SourceScrub.Data
{
    public class Initializer
    {
        private bool hasInitialized = false;
        private readonly ApplicationDbContext _applicationDbContext;

        public Initializer(ApplicationDbContext applicationDbContext) {
            _applicationDbContext = applicationDbContext;
        }

        public void Initialize() {
            if(hasInitialized) return;

            _applicationDbContext.Database.Migrate();

            hasInitialized = true;
        }
    }
}
