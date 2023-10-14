using Android.App;
using Android.Bluetooth;
using Android.Content;
using Android.Content.PM;
using Android.OS;

namespace Control_Esquisitos.Plataforms.Android;

[Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density | ConfigChanges.Orientation, ScreenOrientation = ScreenOrientation.Unspecified)]
public class MainActivity : MauiAppCompatActivity
{
    protected override void OnCreate(Bundle savedInstanceState)
    {
        base.OnCreate(savedInstanceState);
        ServiceActivity.activity = this;
        ServiceActivity.AtivarBluetooth();
    }
}
public class ServiceActivity
{
    public static Activity activity;

    public static async void AtivarBluetooth()
    {
        PermissionStatus status = await Permissions.RequestAsync<ReadWriteStoragePerms>();
        int requestCode = 1;
        Intent discoverableIntent =
               new Intent(BluetoothAdapter.ActionRequestDiscoverable);
        discoverableIntent.PutExtra(BluetoothAdapter.ExtraDiscoverableDuration, 3600);
        activity.StartActivityForResult(discoverableIntent, requestCode);
    }
    public static void ScreenPortrait()
    {
        activity.RequestedOrientation = ScreenOrientation.Portrait;
    }
    public static void ScreenLandscape()
    {
        activity.RequestedOrientation = ScreenOrientation.SensorLandscape;
    }
}

