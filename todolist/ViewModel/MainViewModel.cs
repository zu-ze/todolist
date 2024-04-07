using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace todolist.ViewModel
{
    public partial class MainViewModel: ObservableObject
    {
        IConnectivity connectivity;
        public MainViewModel(IConnectivity connectivity) 
        {
            Items = new ObservableCollection<string>();
            this.connectivity = connectivity;
        }

        [ObservableProperty]
        ObservableCollection<string> items;

        [ObservableProperty]
        string text;

        [RelayCommand]
        async Task Add()
        {
            // add item
            if (string.IsNullOrWhiteSpace(text)) return;

            if(connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                await Shell.Current.DisplayAlert("Uh Oh!", "No Intenet", "Ok");
                return;
            }

            Items.Add(text);
            Text = string.Empty;
        }

        [RelayCommand]
        void Delete(string s)
        { 
            if (Items.Contains(s))
            {
                Items.Remove(s); 
            }

            return;
        }

        [RelayCommand]
        async Task Tap(string s)
        {
            await Shell.Current.GoToAsync($"{nameof(DetailPage)}?Text={s}");
        }

    }
}
