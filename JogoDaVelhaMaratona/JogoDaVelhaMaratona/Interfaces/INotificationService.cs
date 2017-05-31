using System.Threading.Tasks;

namespace JogoDaVelhaMaratona.Interfaces
{
    public interface INotificationService
    {
        Task<string> RegisterNotificationAsync();
    }
}
