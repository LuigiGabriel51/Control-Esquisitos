using Android.App;
using Android.Bluetooth;
using Android.Nfc;
using Android.OS;
using Android.Util;
using CommunityToolkit.Maui.Alerts;
using Java.IO;
using Java.Util;

namespace Control_Esquisitos.Plataforms.Android
{
    public class ConnectThread
    {
        private BluetoothSocket mmSocket;
        private BluetoothDevice mmDevice;
        BluetoothAdapter adapter;
        private System.IO.Stream mmInStream;
        private System.IO.Stream mmOutStream;
        private byte[] mmBuffer;

        public ConnectThread(BluetoothDevice device, BluetoothAdapter bluetoothAdapter)
        {
            this.adapter = bluetoothAdapter;
            var guid = System.Guid.NewGuid();
            var guidString = guid.ToString();
            BluetoothSocket tmp = null;
            mmDevice = device;

            System.IO.Stream tmpIn = null;
            System.IO.Stream tmpOut = null;

            BluetoothDevice bluetoothDevice = bluetoothAdapter.GetRemoteDevice(device.Address);
            UUID uuid = device.GetUuids()[0].Uuid;

            try
            {
                tmp = device.CreateRfcommSocketToServiceRecord(uuid);
            }
            catch (Exception e)
            {
                System.Console.WriteLine("Socket's create() method failed", e);
            }
            try
            {
                tmpOut = tmp.OutputStream;
            }
            catch (Exception e)
            {
                System.Console.WriteLine("erro ao criar OutputStream!");
            }
            mmSocket = tmp;
            mmInStream = tmpIn;
            mmOutStream = tmpOut;
        }

        public async void run()
        {
            adapter.CancelDiscovery();

            try
            {
                mmSocket.Connect();
            }
            catch (Exception e)
            {
                try
                {
                    mmSocket.Close();
                }
                catch (System.IO.IOException closeException)
                {
                    System.Console.WriteLine("Could not close the client socket", closeException);
                    MainPage.conected = false;
                }
                MainPage.conected = false;
                return;
            }
            MainPage.conected = true;

        }

        public void cancel()
        {
            try
            {
                mmSocket.Close();
            }
            catch (Exception e)
            {
                System.Console.WriteLine("Could not close the client socket", e);
            }
        }

        public void Write(byte[] bytes)
        {
            try
            {
                mmOutStream.Write(bytes);
            }
            catch (Exception e)
            {
                Toast t = new Toast()
                {
                    Text = "erro ao enviar dados para dispositivos",
                };
                t.Show();
            }
        }
    }
}
