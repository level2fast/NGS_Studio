using Xamarin.Forms;
using System.Windows.Input;
using NGS_Studio.Services;
using System.Threading.Tasks;

namespace NGS_Studio.ViewModels
{
    public class LogoutViewModel : BaseViewModel
    {
        public ICommand LoadCommand { get; protected set; }
        public ICommand OwnerLogoutCommand { get; protected set; }
        public LogoutViewModel()
        {
            OwnerLogoutCommand = new Command(LogoutAsync);
        }

        private async Task LogOutOfFirebase()
        {
            var dp = DependencyService.Resolve<IFireBaseAuthentication>();
            dp.SignOut();
            await Task.Delay(10);
            bool loggedOut = false;
            while (!loggedOut)
            {
                if (!dp.IsSignIn())
                {
                    loggedOut = true;
                }
            }
        }
        private async void LogoutAsync()
        {
            Task t = Task.Run(LogOutOfFirebase);
            var ret = t.Wait(100);
            if (ret == true)
            {
                (Shell.Current as AppShell).SetOwnerFlyoutItemVisibility();
                await Shell.Current.GoToAsync($"..");
            }


        }
    }
}

