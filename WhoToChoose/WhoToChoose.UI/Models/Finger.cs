using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Shapes;

namespace WhoToChoose.UI.Models
{
    public class Finger
    {
        private Pointer _pointer;
        public Pointer pointer
        {
            get { return _pointer; }
            set { _pointer = value; }
        }

        private Ellipse _circle;
        public Ellipse circle
        {
            get { return _circle; }
            set { _circle = value; }
        }

        public Finger(Pointer Pointer)
        {
            pointer = Pointer;
        }
    }
}
