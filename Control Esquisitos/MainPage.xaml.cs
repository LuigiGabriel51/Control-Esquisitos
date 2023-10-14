using Android.Bluetooth;
using CommunityToolkit.Maui.Alerts;
using Control_Esquisitos.Model;
using System.Diagnostics;

namespace Control_Esquisitos.Plataforms.Android;

public partial class MainPage : ContentPage
{
    private bool isAccelerating = false;
    private bool isLeft = false;
    private bool isRight = false;
    private bool isBrake = false;
    public static bool conected = false;
    ConnectThread connectThread;
    BluetoothAdapter bluetoothAdapter = BluetoothAdapter.DefaultAdapter;
    BluetoothDevice device;
    Data data;
    bool Speedmode = true;
    public MainPage()
    {
        InitializeComponent();
    }
    private async void ButtonLeft(object sender, EventArgs e)
    {
        if (conected)
        {
            if(Speedmode == true){
                data.SendData(4);

            }
            else
            {
                data.SendData(4);
                await Task.Delay(400);
                data.SendData(0);
            }
        }
        else
        {
            var toast = new Toast()
            {
                Text = "nenhum dispositivo não conectado",
            };
            await toast.Show();
        }
    }
    private async void ButtonRight(object sender, EventArgs e)
    {
        if (conected)
        {
            if (Speedmode == true)
            {
                data.SendData(3);

            }
            else
            {
                data.SendData(3);
                await Task.Delay(400);
                data.SendData(0);
            }
        }
        else
        {
            var toast = new Toast()
            {
                Text = "nenhum dispositivo não conectado",
            };
            await toast.Show();
        }
    }
    private async void ButtonAccelerator(object sender, EventArgs e)
    {
        if (conected)
        {
            if (Speedmode == true)
            {
                data.SendData(1);
            }
            else
            {
                data.SendData(1);
                await Task.Delay(400);
                data.SendData(0);
            }
        }
        else
        {
            var toast = new Toast()
            {
                Text = "nenhum dispositivo conectado",
            };
            await toast.Show();
        }
    }
    private async void ButtonBrake(object sender, EventArgs e)
    {
        if (conected)
        {
            if (Speedmode == true)
            {
                data.SendData(2);

            }
            else
            {
                data.SendData(2);
                await Task.Delay(400);
                data.SendData(0);
            }
        }
        else
        {
            var toast = new Toast()
            {
                Text = "nenhum dispositivo não conectado",
            };
            await toast.Show();
        }
    }
    private async void StopCar(object sender, EventArgs e)
    {
        if (conected)
        {
            data.SendData(0);
        }
        else
        {
            var toast = new Toast()
            {
                Text = "nenhum dispositivo não conectado",
            };
            await toast.Show();
        }
    }

    [Obsolete]
    private async void ScanButton_Clicked(object sender, EventArgs e)
    {
        //#if ANDROID
        PermissionStatus status = await Permissions.RequestAsync<ReadWriteStoragePerms>();
        if (status == PermissionStatus.Granted)
        {
            ICollection<BluetoothDevice> pairedDevices = bluetoothAdapter.BondedDevices;

            if (pairedDevices.Count() > 0)
            {
                ScanButton.IsEnabled = false;
                indicator.IsVisible = true;
                await Task.Delay(TimeSpan.FromSeconds(2));
                indicator.IsVisible = false;
                listView.ItemsSource = pairedDevices;
                ScanButton.IsEnabled = true;
            }
            else
            {
                ServiceActivity.AtivarBluetooth();
            }
        }
    }
    private async void listDevice_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        indicator.IsVisible = true;
        BluetoothDevice deviceSelected = (BluetoothDevice)e.SelectedItem;
        device = deviceSelected;
        if (device == null) { return; }
        connectThread = new ConnectThread(device, bluetoothAdapter);
        connectThread.run();
        data = new Data(connectThread);
        indicator.IsRunning = false;
        listView.SelectedItem = null;
        if (conected == true)
        {
            var toast = new Toast()
            {
                Text = "Dispositivo Conectado",
            };
            await toast.Show();
        }
        else
        {
            var toast = new Toast()
            {
                Text = "Não foi possível conectar",
            };
            listView.SelectedItem = null;
            await toast.Show();
        }
    }

    private async void Button_Clicked_manobra1(object sender, EventArgs e)
    {
        if (conected)
        {
            data.SendData(5);
        }
        else
        {
            var toast = new Toast()
            {
                Text = "nenhum dispositivo não conectado",
            };
            await toast.Show();
        }
    }

    private void alternarCadencia(object sender, EventArgs e)
    {
        if (Speedmode)
        {
            Speedmode = false;
            speedmodeButton.Text = IconFont.HeartCircleMinus;
        }
        else
        {
            Speedmode = true;
            speedmodeButton.Text = IconFont.HeartCircleBolt;
        }
    }
}

public enum Direcao { frente=1, tras, direita, esquerda}
public class Data
{
    ConnectThread ConnectThread;
    public Data(ConnectThread connect)
    {
        ConnectThread = connect;
    }
    public async void SendData(byte direction)
    {
        Vibration.Vibrate();
        await Task.Delay(100);
        Vibration.Cancel();
        byte[] acelerar = { direction };
        ConnectThread.Write(acelerar);
    }
}
