namespace MonkeyFinder.View;

public partial class DetailsPage : ContentPage
{
    public DetailsPage(IMonkeyDetailsViewModel monkeyDetailsViewModel)
	{
		InitializeComponent();
		BindingContext = monkeyDetailsViewModel;
    }

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
    }
}