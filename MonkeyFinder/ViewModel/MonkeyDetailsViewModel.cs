namespace MonkeyFinder.ViewModel
{
    [QueryProperty("Monkey", "Monkey")]

    public partial class MonkeyDetailsViewModel : BaseViewModel, IMonkeyDetailsViewModel
    {
        private readonly IMap _map;
        [ObservableProperty]
        private Monkey monkey;
        public MonkeyDetailsViewModel(IMap map)
        {
            Title = "Monkey Details";
            _map = map;
        }

        [RelayCommand]
        public async Task OpenMapAsync()
        {
            if (Monkey is null)
                return;
            try
            {
                var location = new Location(Monkey.Latitude, Monkey.Longitude);
                var options = new MapLaunchOptions
                {
                    Name = Monkey.Name,
                    NavigationMode = NavigationMode.None
                };
                await Map.Default.OpenAsync(Monkey.Latitude, Monkey.Longitude, options);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                await Shell.Current.DisplayAlert("Error", "Unable to open map", "OK");
            }
        }
    }
}
