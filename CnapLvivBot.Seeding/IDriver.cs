using System.Threading.Tasks;

namespace CnapLvivBot.Seeding
{
    public interface IDriver
    {
        Task RunAsync();
    }
}