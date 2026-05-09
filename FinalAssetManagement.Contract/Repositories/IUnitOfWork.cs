namespace FinalAssetManagement.Contract.Repositories
{
    // Ensures consistency: all changes are saved atomically (all or nothing:Atomic Operation).
    // Improves ease of use: access all repositories through a single entry point (just inject IUnitofWork)
    // Handles memory management properly via IDisposable and controlled DbContext lifetime.

    public interface IUnitOfWork
    {

        /* Repositories are exposed as read-only to prevent external modification 
        and ensure they all share the same DbContext and transaction scope.*/
        //In C#, all interface members are implicitly public and cannot have access modifiers.

        IAssetRepository Assets { get; } //هماهنگ با نام دی بی ست ها
        ICategoryRepository Categories { get; }
        IUserRepository Users { get; }
        ITransactionRepository Transactions { get; }

        Task<int> SaveAsync();
    }
}
