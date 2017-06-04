using System;
using System.Windows.Input;
using WhoToChoose.UI.Views;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;

namespace WhoToChoose.UI.ViewModels
{
    public class ChooserViewModel : BaseViewModel
    {
        private uint _maximumNumberOfPossibleFingers;
        internal SettingsController _settingsController;

        private SolidColorBrush _complimentaryColorBrush;
        public SolidColorBrush complimentaryColorBrush
        {
            get { return _complimentaryColorBrush; }
            set
            {
                _complimentaryColorBrush = value;
                OnPropertyChanged(nameof(complimentaryColorBrush));
            }
        }

        private SolidColorBrush _contrastColorBrush;
        public SolidColorBrush contrastColorBrush
        {
            get { return _contrastColorBrush; }
            set
            {
                _contrastColorBrush = value;
                OnPropertyChanged(nameof(contrastColorBrush));
            }
        }

        private int _numberOfFingersToChooseFrom;
        public int numberOfFingersToChooseFrom
        {
            get { return _numberOfFingersToChooseFrom; }
            set
            {
                _numberOfFingersToChooseFrom = value;
                OnPropertyChanged(nameof(numberOfFingersToChooseFrom));
            }
        }

        private int _numberOfFingersToChoose;
        public int numberOfFingersToChoose
        {
            get { return _numberOfFingersToChoose; }
            set
            {
                _numberOfFingersToChoose = value;
                OnPropertyChanged(nameof(numberOfFingersToChoose));
            }
        }

        private int _time;
        public int time
        {
            get { return _time; }
            set
            {
                _time = value;
                OnPropertyChanged(nameof(time));
            }
        }

        public ChooserViewModel(uint maximumNumber)
        {
            _settingsController = new SettingsController(maximumNumber);
            _maximumNumberOfPossibleFingers = maximumNumber;
            numberOfFingersToChooseFrom = Convert.ToInt32(_settingsController.GetNumberOfFingersToChooseFrom());
            numberOfFingersToChoose = Convert.ToInt32(_settingsController.GetNumberOfFingersToChoose());
            time = 0;
        }

        private ICommand _minusCommand;
        public ICommand minusCommand
        {
            get
            {
                if (_minusCommand == null)
                {
                    _minusCommand = new Command<int>(Minus, CanMinus);
                }
                return _minusCommand;
            }
            set { _minusCommand = value; }
        }

        private ICommand _plusCommand;
        public ICommand plusCommand
        {
            get
            {
                if (_plusCommand == null)
                {
                    _plusCommand = new Command<int>(Plus, CanPlus);
                }
                return _plusCommand;
            }
            set { _plusCommand = value; }
        }

        private ICommand _aboutCommand;
        public ICommand aboutCommand
        {
            get
            {
                if (_aboutCommand == null)
                {
                    _aboutCommand = new Command(NavigateToAbout, () => true);
                }
                return _aboutCommand;
            }
            set { _aboutCommand = value; }
        }

        private ICommand _helpCommand;
        public ICommand helpCommand
        {
            get
            {
                if (_helpCommand == null)
                {
                    _helpCommand = new Command(NavigateToHelp, () => true);
                }
                return _helpCommand;
            }
            set { _helpCommand = value; }
        }

        private ICommand _settingsCommand;
        public ICommand settingsCommand
        {
            get
            {
                if (_settingsCommand == null)
                {
                    _settingsCommand = new Command(NavigateToSettings, () => true);
                }
                return _settingsCommand;
            }
            set { _settingsCommand = value; }
        }

        private void UpdateCommands()
        {
            ((Command<int>)plusCommand).RaiseCanExecuteChanged();
            ((Command<int>)minusCommand).RaiseCanExecuteChanged();
        }

        private bool CanMinus(int optionNumber)
        {
            Option optionToChange = (Option)optionNumber;

            if (optionToChange == Option.numberOfFingersToChooseFrom)
            {
                if (numberOfFingersToChooseFrom > 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else if (optionToChange == Option.numberOfFingersToChoose)
            {
                if (numberOfFingersToChoose > 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        private bool CanPlus(int optionNumber)
        {
            Option optionToChange = (Option)optionNumber;

            if (optionToChange == Option.numberOfFingersToChooseFrom)
            {
                if (numberOfFingersToChooseFrom < _maximumNumberOfPossibleFingers)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else if(optionToChange == Option.numberOfFingersToChoose)
            {
                if (numberOfFingersToChoose < numberOfFingersToChooseFrom)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        private void Minus(int optionNumber)
        {
            Option optionToChange = (Option)optionNumber;

            if (optionToChange == Option.numberOfFingersToChooseFrom)
            {
                numberOfFingersToChooseFrom--;

                if (numberOfFingersToChoose > numberOfFingersToChooseFrom)
                {
                    numberOfFingersToChoose = numberOfFingersToChooseFrom;
                }
            }
            else if(optionToChange == Option.numberOfFingersToChoose)
            {
                numberOfFingersToChoose--;
            }

            UpdateCommands();
            _settingsController.SetNumberOfFingersToChooseFrom(numberOfFingersToChooseFrom);
            _settingsController.SetNumberOfFingersToChoose(numberOfFingersToChoose);
        }

        private void Plus(int optionNumber)
        {
            Option optionToChange = (Option)optionNumber;

            if (optionToChange == Option.numberOfFingersToChooseFrom)
            {
                numberOfFingersToChooseFrom++;
            }
            else if(optionToChange == Option.numberOfFingersToChoose)
            {
                numberOfFingersToChoose++;
            }

            UpdateCommands();
            _settingsController.SetNumberOfFingersToChooseFrom(numberOfFingersToChooseFrom);
            _settingsController.SetNumberOfFingersToChoose(numberOfFingersToChoose);
        }

        private void NavigateToAbout()
        {
            ((App)Application.Current).rootFrame.Navigate(typeof(AboutView));
        }

        private void NavigateToHelp()
        {
            ((App)Application.Current).rootFrame.Navigate(typeof(HelpView));
        }

        private void NavigateToSettings()
        {
            ((App)Application.Current).rootFrame.Navigate(typeof(SettingsView));
        }
    }
}
