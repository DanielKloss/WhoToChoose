namespace WhoToChoose.UI.Models
{
    public class TimerCountdownOption
    {
        private int _value;
        public int value
        {
            get { return _value; }
            set { _value = value; }
        }

        private string _label;
        public string label
        {
            get { return _label; }
            set { _label = value; }
        }
    }
}
