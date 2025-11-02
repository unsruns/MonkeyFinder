using MonkeyFinder.Services;

namespace MonkeyFinder.ViewModel
{
    public partial class MonkeysViewModel : BaseViewModel, IMonkeysViewModel
    {

        private readonly IMonkeyService _monkeyService;

        public ObservableCollection<Monkey> Monkeys { get; } = new();
        IConnectivity _connectivity;
        IGeolocation _geolocation;
        public MonkeysViewModel(IMonkeyService monkeyService, IConnectivity connectivity, IGeolocation geolocation)
        {
            Title = "Monkey Finder";
            _monkeyService = monkeyService;
            _connectivity = connectivity;
            _geolocation = geolocation;
        }

        [RelayCommand]
        public async Task GetClosestMonkeyAsync()
        {
            if (IsBusy || Monkeys.Count == 0)
                return;

            try
            {
                var location = await _geolocation.GetLastKnownLocationAsync();
                if (location is null)
                {
                    location = await _geolocation.GetLocationAsync(new GeolocationRequest
                    {
                        DesiredAccuracy = GeolocationAccuracy.Medium,
                        Timeout = TimeSpan.FromSeconds(30)
                    });
                }

                if (location is null)
                    return;

                var closestMonkey = Monkeys.OrderBy(m => location.CalculateDistance(m.Latitude, m.Longitude, DistanceUnits.Kilometers)).FirstOrDefault();
                if (closestMonkey is null)
                    return;

                await Shell.Current.DisplayAlert("Closest Monkey", $"{closestMonkey.Name} in {closestMonkey.Location} is the closest monkey to you!", "OK");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                await Shell.Current.DisplayAlert("Error", "Unable to get closest monkey", "OK");
            }
        }

        [RelayCommand]
        public async Task GoToDetailsAsync(Monkey monkey)
        {
            if (monkey is null)
                return;


            await Shell.Current.GoToAsync(
                nameof(View.DetailsPage), 
                true,
                new Dictionary<string, object>
                {
                    {"Monkey", monkey }
                });
        }

        [RelayCommand]
        public async Task GetMonkeysAsync()
        {
            if (IsBusy)
                return;
            try
            {
                if (_connectivity.NetworkAccess != NetworkAccess.Internet)
                {
                    await Shell.Current.DisplayAlert("No Internet", "Check your internet and try again.", "OK");
                    return;
                }

                IsBusy = true;
                var monkeys = await _monkeyService.GetMonkeysAsync();

                if(Monkeys.Count != 0)
                    Monkeys.Clear();

                foreach (var monkey in monkeys)
                    Monkeys.Add(monkey);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                await Shell.Current.DisplayAlert($"Unable to get monkeys: {ex.Message}", "OK", "Cancel");
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
