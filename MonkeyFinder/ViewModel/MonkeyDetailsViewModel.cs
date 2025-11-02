namespace MonkeyFinder.ViewModel
{
    [QueryProperty("Monkey", "Monkey")]

    public partial class MonkeyDetailsViewModel : BaseViewModel, IMonkeyDetailsViewModel
    {
        [ObservableProperty]
        private Monkey monkey;
        public MonkeyDetailsViewModel()
        {
            Title = "Monkey Details";
        }
    }
}
