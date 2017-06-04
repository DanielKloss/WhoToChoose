using Windows.Devices.Input;

namespace WhoToChoose.UI.ViewModels
{
    public class HelpViewModel : BaseViewModel
    {
        private TouchCapabilities _touchCapabilities;
        internal SettingsController _settingsController;

        private int _numberOfSeconds;
        public int numberOfSeconds
        {
            get { return _numberOfSeconds; }
            set
            {
                _numberOfSeconds = value;
                OnPropertyChanged(nameof(numberOfSeconds));
            }
        }

        public HelpViewModel()
        {
            _touchCapabilities = new TouchCapabilities();
            _settingsController = new SettingsController(_touchCapabilities.Contacts);

            numberOfSeconds = _settingsController.GetCountdownTime();
        }
    }
}
