using Control_Esquisitos.Plataforms.Android;

namespace Control_Esquisitos;

public partial class Initial : ContentPage
{
	public Initial()
	{
		InitializeComponent();
        requestPermission();
	}

    public async void requestPermission()
    {
        PermissionStatus status = await Permissions.RequestAsync<ReadWriteStoragePerms>();
        while (status != PermissionStatus.Granted)
        {
            await Permissions.RequestAsync<ReadWriteStoragePerms>();
        }
    }
    private void ImageButton_Clicked(object sender, EventArgs e)
    {
        ServiceActivity.ScreenLandscape();
        var PageGame = new MainPage();
        Navigation.PushAsync(PageGame);
    }

    private void ImageButton_Clicked_1(object sender, EventArgs e)
    {
        ServiceActivity.ScreenPortrait();
        var PageGame = new MainPage2xaml();
        Navigation.PushAsync(PageGame);
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        ServiceActivity.ScreenPortrait();
    }
}