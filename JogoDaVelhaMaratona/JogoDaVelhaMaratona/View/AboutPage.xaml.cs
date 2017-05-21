
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace JogoDaVelhaMaratona.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AboutPage : ContentPage
    {
        public AboutPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
        }
    }
}