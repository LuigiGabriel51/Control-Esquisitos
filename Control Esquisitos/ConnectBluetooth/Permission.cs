using static Microsoft.Maui.ApplicationModel.Permissions;

namespace Control_Esquisitos.ConnectBluetooth
{
    public class Permission
    {

        public static async ValueTask<bool> CheckPermissionStatus()
        {

            if (DeviceInfo.Platform != DevicePlatform.Android)
            {
                return true;
            }
            PermissionStatus status;
            if (DeviceInfo.Version.Major >= 12)
            {
                status = await Permissions.CheckStatusAsync<MyBluetoothPermission>();
                if (status != PermissionStatus.Granted)
                {
                    if (Permissions.ShouldShowRationale<MyBluetoothPermission>())
                    {
                        bool needPermission = await Shell.Current.DisplayAlert("needPermission ", "Requires permission to scan and connect to Bluetooth", "go", "cancel");
                        if (needPermission == false)
                        {
                            return false;
                        }
                    }

                    status = await Permissions.RequestAsync<MyBluetoothPermission>();
                    if (status != PermissionStatus.Granted)
                    {
                        await Shell.Current.DisplayAlert("needPermission", "The function is temporarily unavailable because the permission is not obtained", "ok");
                        return false;
                    }
                }
            }
            status = await Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>();
            if (status != PermissionStatus.Granted)
            {
                if (Permissions.ShouldShowRationale<Permissions.LocationWhenInUse>())
                {
                    bool needPermission = await Shell.Current.DisplayAlert("needPermission", "Bluetooth requires location information to work", "go", "cancel");
                    if (needPermission == false)
                    {
                        return false;
                    }
                }

                status = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();
                if (status != PermissionStatus.Granted)
                {
                    await Shell.Current.DisplayAlert("needPermission", "The function is temporarily unavailable because the permission is not obtained", "ok");
                    return false;
                }
            }
            return true;
        }
    }
    public class MyBluetoothPermission : BasePlatformPermission
    {
#if ANDROID
        public override (string androidPermission, bool isRuntime)[] RequiredPermissions => new List<(string permission, bool isRuntime)>
        {
            ("android.permission.BLUETOOTH_SCAN",true),
            ("android.permission.BLUETOOTH_CONNECT",true)
        }.ToArray();
#endif
    }
}
