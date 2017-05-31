using JogoDaVelhaMaratona.Interfaces;
using System.Threading.Tasks;
using Xamarin.Forms;

[assembly: Dependency(typeof(JogoDaVelhaMaratona.Service.NotificationService))]
namespace JogoDaVelhaMaratona.Service
{
    public class NotificationService
    {
        public async Task<string> Register()
        {
            var auth = DependencyService.Get<INotificationService>();
            var mensagemRegistro = await auth.RegisterNotificationAsync();
            MessagingCenter.Send<object, string>(this, "GameStatus", mensagemRegistro);
            return mensagemRegistro;
        }
    }
}
