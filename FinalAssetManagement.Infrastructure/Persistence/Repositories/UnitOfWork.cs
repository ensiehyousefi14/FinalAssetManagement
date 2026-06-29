using FinalAssetManagement.Contract.Repositories;

namespace FinalAssetManagement.Infrastructure.Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        //Constructor

        private readonly ApplicationDbContext _context;
        public UnitOfWork(ApplicationDbContext context, 
                          IAssetRepository assetRepository,
                          ICategoryRepository categoryRepository, 
                          IUserRepository userRepository, 
                          ITransactionRepository transactionRepository)
        {
            _context = context;
            Assets = assetRepository;
            Categories = categoryRepository;
            Users = userRepository;
            Transactions = transactionRepository;
        }


        //عینا مانند همانهایی که در اینترفیس بودند فقط پابلیک صریح
        public IAssetRepository Assets { get; }

        public ICategoryRepository Categories { get; }

        public IUserRepository Users { get; }

        public ITransactionRepository Transactions { get; }


        // No IDisposable needed because ASP.NET Core DI manages the DbContext lifecycle automatically.
        // async/await removed because the method simply returns the Task from SaveChangesAsync without extra logic.
        public Task<int> SaveAsync()
        {
            return _context.SaveChangesAsync();
        }
    }
}
