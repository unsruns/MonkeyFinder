namespace MonkeyFinder.ViewModel
{
    public interface IMonkeysViewModel
    {
        Task GetClosestMonkeyAsync();
        Task GoToDetailsAsync(Monkey monkey);
        Task GetMonkeysAsync();
    }
}
