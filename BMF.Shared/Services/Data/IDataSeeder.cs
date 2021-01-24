using System.Threading.Tasks;

namespace BMF.Shared.Services.Data
{
    public interface IDataSeeder
    {
        Task SeedDatabase();
    }
}
