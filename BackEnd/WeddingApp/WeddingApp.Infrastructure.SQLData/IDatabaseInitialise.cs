namespace WeddingApp.Infrastructure.SQLData
{
    public interface IDatabaseInitialise
    {
        void SeedDatabase(DBContext ctx);
    }
}