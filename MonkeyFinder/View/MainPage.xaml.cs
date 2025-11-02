namespace MonkeyFinder.View;

public partial class MainPage : ContentPage
{
    public MainPage(IMonkeysViewModel monkeysViewModel)
	{
		InitializeComponent();
        BindingContext = monkeysViewModel;
    }
}