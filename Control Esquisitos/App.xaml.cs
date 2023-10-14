
namespace Control_Esquisitos;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();
#if ANDROID
        MainPage = new AppShell();
#endif
    }
}
