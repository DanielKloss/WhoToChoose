using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using WhoToChoose.UI.Models;
using Windows.Devices.Input;

namespace WhoToChoose.UI.ViewModels
{
    public class SettingsViewModel : BaseViewModel
    {
        private TouchCapabilities _touchCapabilities;
        internal SettingsController _settingsController;

        private TimerCountdownOption _timerCountdownSelectedOption;
        public TimerCountdownOption timerCountdownSelectedOption
        {
            get { return _timerCountdownSelectedOption; }
            set
            {
                _timerCountdownSelectedOption = value;
                ChangeCountdown(value);
                OnPropertyChanged(nameof(timerCountdownSelectedOption));
            }
        }

        private ObservableCollection<TimerCountdownOption> _timerCountdownOptions;
        public ObservableCollection<TimerCountdownOption> timerCountdownOptions
        {
            get { return _timerCountdownOptions; }
            set
            {
                _timerCountdownOptions = value;
                OnPropertyChanged(nameof(timerCountdownOptions));
            }
        }

        private ICommand _changeCountdownCommand;
        public ICommand changeCountdownCommand
        {
            get
            {
                if (_changeCountdownCommand == null)
                {
                    _changeCountdownCommand = new Command<TimerCountdownOption>(ChangeCountdown, value => true);
                }
                return _changeCountdownCommand;
            }
            set { _changeCountdownCommand = value; }
        }

        public SettingsViewModel()
        {
            timerCountdownOptions = new ObservableCollection<TimerCountdownOption>()
            {
                new TimerCountdownOption() { value=3, label="3 sec"},
                new TimerCountdownOption() { value=4, label="4 sec"},
                new TimerCountdownOption() { value=5, label="5 sec"},
            };

            _touchCapabilities = new TouchCapabilities();
            _settingsController = new SettingsController(_touchCapabilities.Contacts);
            timerCountdownSelectedOption = timerCountdownOptions.SingleOrDefault(o => o.value == _settingsController.GetCountdownTime());
        }

        private void ChangeCountdown(TimerCountdownOption value)
        {
            _settingsController.SetCountdownTime(value.value);
        }
    }
}
