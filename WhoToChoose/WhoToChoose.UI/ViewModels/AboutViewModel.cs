using System;
using Windows.ApplicationModel;

namespace WhoToChoose.UI.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        private string _version;
        public string version
        {
            get { return _version; }
            set
            {
                _version = value;
                OnPropertyChanged(nameof(version));
            }
        }

        public AboutViewModel()
        {
            version = String.Format("{0}.{1}", Package.Current.Id.Version.Major.ToString(), Package.Current.Id.Version.Minor.ToString());
        }
    }
}
