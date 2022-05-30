namespace Assets.Scripts.Data
{
    internal interface IUnitOfWork<T>
    {
        IRepository<T> Repository { get; }

        void SaveChanges();
    }
}