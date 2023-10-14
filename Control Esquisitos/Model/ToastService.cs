using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;

namespace Control_Esquisitos.Model
{
    public class ToastService
    {
        public async void ShowToast(string message)
        {

            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

            ToastDuration duration = ToastDuration.Long;
            double fontSize = 14;

            var toast = Toast.Make(message, duration, fontSize);

            await toast.Show(cancellationTokenSource.Token);
        }
    }
}
