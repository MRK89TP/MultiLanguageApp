using System.Windows;
using System.Windows.Input;

namespace WpfApp1
{
    public class MainViewModel : BaseViewModel
    {
        private ICommand _ChangeLanguage;
        public ICommand ChangeLanguage => _ChangeLanguage;

        private ICommand _ChangeTheme;
        public ICommand ChangeTheme => _ChangeTheme;


        public MainViewModel()
        {
            _ChangeLanguage = new BaseCommand<string>((code) =>
            {
                ((App)Application.Current).SetLanguage(code);
            });

            _ChangeTheme = new BaseCommand<string>((palette) =>
            {
                ((App)Application.Current).ChangePalette(palette);
            });
        }
	}
}
