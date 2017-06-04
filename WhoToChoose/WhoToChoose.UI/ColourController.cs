using System.Collections.Generic;
using System.Linq;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;

namespace WhoToChoose.UI
{
    public static class ColourController
    {
        private static SolidColorBrush _accentColorBrush = (SolidColorBrush)Application.Current.Resources["SystemControlForegroundAccentBrush"];
        public static Brush AccentColorBrush
        {
            get
            {
                return _accentColorBrush;
            }
        }

        private static Color _accentColor = _accentColorBrush.Color;
        public static Color AccentColor
        {
            get
            {
                return _accentColor;
            }
        }

        private static Color _complementaryColor = GetComplimentaryColour();
        public static Color ComplementaryColor
        {
            get
            {
                return _complementaryColor;
            }
        }

        private static Brush _complementaryColorBrush = new SolidColorBrush(_complementaryColor);
        public static Brush ComplementaryColorBrush
        {
            get
            {
                return _complementaryColorBrush;
            }
        }

        private static Color _contrastColor = new Color()
        {
            R = (byte)(255 - (_accentColor.R)),
            G = (byte)(255 - (_accentColor.G)),
            B = (byte)(255 - (_accentColor.B)),
            A = _accentColor.A
        };
        public static Color ContrastColor
        {
            get
            {
                return _contrastColor;
            }
        }

        private static Brush _contrastColorBrush = new SolidColorBrush(_contrastColor);
        public static Brush ContrastColorBrush
        {
            get
            {
                return _contrastColorBrush;
            }
        }

        private static Color GetComplimentaryColour()
        {
            List<int> accentColour = new List<int>(){
                _accentColor.R,
                _accentColor.G,
                _accentColor.B
            };

            int lowestNumber = accentColour.IndexOf(accentColour.Min());
            accentColour[lowestNumber] += 150;

            return new Color()
            {
                R = (byte)accentColour[0],
                G = (byte)accentColour[1],
                B = (byte)accentColour[2],
                A = 255
            };
        }
    }
}
