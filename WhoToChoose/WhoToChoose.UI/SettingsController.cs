using System;
using Windows.Storage;

namespace WhoToChoose.UI
{
    public class SettingsController
    {
        private const string _countdownTime = "countdownTime";
        private const string _numberOfFingersToChooseFrom = "numberOfFingersToChooseBetween";
        private const string _numberOfFingersToChoose = "numberOfFingersToChoose";

        public SettingsController(uint maximumNumberOfContacts)
        {
            if (GetCountdownTime() == 0)
            {
                SetCountdownTime(5);
            }

            if (GetNumberOfFingersToChoose() == 0)
            {
                SetNumberOfFingersToChoose(1);
            }

            if (GetNumberOfFingersToChooseFrom() == 0)
            {
                SetNumberOfFingersToChooseFrom(Convert.ToInt32(maximumNumberOfContacts));
            }
        }

        public int GetCountdownTime()
        {
            return Convert.ToInt32(ApplicationData.Current.LocalSettings.Values[_countdownTime]);
        }

        public void SetCountdownTime(int countdownTime)
        {
            ApplicationData.Current.LocalSettings.Values[_countdownTime] = countdownTime;
        }

        public int GetNumberOfFingersToChooseFrom()
        {
            return Convert.ToInt32(ApplicationData.Current.LocalSettings.Values[_numberOfFingersToChooseFrom]);
        }

        public void SetNumberOfFingersToChooseFrom(int numberOfFingersToChooseFrom)
        {
            ApplicationData.Current.LocalSettings.Values[_numberOfFingersToChooseFrom] = numberOfFingersToChooseFrom;
        }

        public int GetNumberOfFingersToChoose()
        {
            return Convert.ToInt32(ApplicationData.Current.LocalSettings.Values[_numberOfFingersToChoose]);
        }

        public void SetNumberOfFingersToChoose(int numberOfFingersToChoose)
        {
            ApplicationData.Current.LocalSettings.Values[_numberOfFingersToChoose] = numberOfFingersToChoose;
        }
    }
}
