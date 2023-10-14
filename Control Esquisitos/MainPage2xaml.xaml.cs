using Android.Bluetooth;
using CommunityToolkit.Maui.Alerts;
using Control_Esquisitos.Plataforms.Android;
using Microsoft.Maui.Graphics.Text;

namespace Control_Esquisitos.Plataforms.Android;

public partial class MainPage2xaml : ContentPage
{
    ConnectThread connectThread;
    BluetoothAdapter bluetoothAdapter = BluetoothAdapter.DefaultAdapter;
    BluetoothDevice device;
    public MainPage2xaml()
	{
		InitializeComponent();
    }
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
        indicator.IsVisible = false;
        listView.SelectedItem = null;
        if (MainPage.conected == true)
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

    private void enviar_Clicked(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(dados.Text) || connectThread == null) return;
        var Byte = Convert.ToInt32(dados.Text);
        byte[] send_data = { (byte)Byte };
        Console.WriteLine(send_data);
        connectThread.Write(send_data);
    }
}