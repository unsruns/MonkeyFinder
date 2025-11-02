using MonkeyFinder.Services;

namespace MonkeyFinder.ViewModel
{
    public partial class MonkeysViewModel : BaseViewModel, IMonkeysViewModel
    {

        private readonly IMonkeyService _monkeyService;

        public ObservableCollection<Monkey> Monkeys { get; } = new();

        public MonkeysViewModel(IMonkeyService monkeyService)
        {
            Title = "Monkey Finder";
            _monkeyService = monkeyService;
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
