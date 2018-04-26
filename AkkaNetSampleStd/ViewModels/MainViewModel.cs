using System.Windows.Input;
using Xamarin.Forms;

namespace AkkaNetSampleStd.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private string _url;
        public string Url
        {
            get => _url;
            set => SetProperty(ref _url, value);
        }

        private ICommand _startCrawlingCommand;
        public ICommand StartCrawlingCommand => _startCrawlingCommand ?? (_startCrawlingCommand = new Command(HandleAction));

        private void HandleAction()
        {
        }
    }
}