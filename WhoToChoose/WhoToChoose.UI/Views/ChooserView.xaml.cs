using System;
using System.Collections.Generic;
using System.Linq;
using WhoToChoose.UI.Models;
using WhoToChoose.UI.ViewModels;
using Windows.Devices.Input;
using Windows.Foundation;
using Windows.UI.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;

namespace WhoToChoose.UI.Views
{
    public sealed partial class ChooserView : Page
    {
        ChooserViewModel _viewModel;
        uint _numberOfActiveContacts;
        TouchCapabilities _touchCapabilities;
        List<Finger> _contacts;
        IEnumerable<Finger> _randomFingers;
        DispatcherTimer _timer;

        public ChooserView()
        {
            InitializeComponent();

            _touchCapabilities = new TouchCapabilities();
            _contacts = new List<Finger>((int)_touchCapabilities.Contacts);

            _timer = new DispatcherTimer() { Interval = new TimeSpan(0, 0, 1) };
            _timer.Tick += TickTimer;

            DataContext = new ChooserViewModel(_touchCapabilities.Contacts);
            _viewModel = DataContext as ChooserViewModel;
            _viewModel.contrastColorBrush = (SolidColorBrush)ColourController.ContrastColorBrush;
        }

        #region Target Pointer Methods
        private void TargetPointerMoved(object sender, PointerRoutedEventArgs e)
        {
            Pointer pointer = e.Pointer;
            PointerPoint pointerPoint = e.GetCurrentPoint(targetCanvas);

            // If event source is mouse pointer and the pointer is still 
            // over the target, retain pointer and pointer details.
            // Or pointer is not being used
            // Return without removing pointer from pointers dictionary.        
            if (pointer.PointerDeviceType == PointerDeviceType.Mouse || !PointerAlreadyExists(pointer))
            {
                return;
            }

            MoveCircle(pointerPoint, pointer, FindIndexOfPointer(pointer));

            // Prevent most handlers along the event route from handling the same event again.
            e.Handled = true;
        }

        private void TargetPointerReleased(object sender, PointerRoutedEventArgs e)
        {
            Pointer pointer = e.Pointer;

            // If event source is mouse pointer and the pointer is still 
            // over the target, retain pointer and pointer details.
            // Or pointer is not being used
            // Return without removing pointer from pointers dictionary.
            if (pointer.PointerDeviceType == PointerDeviceType.Mouse || !PointerAlreadyExists(pointer))
            {
                return;
            }

            StopTimer();

            DeleteCircle(pointer, FindIndexOfPointer(pointer));

            RemovePointer(pointer);

            // Prevent most handlers along the event route from handling the same event again.
            e.Handled = true;
        }

        private void TargetPointerPressed(object sender, PointerRoutedEventArgs e)
        {
            if (Convert.ToBoolean(_touchCapabilities.TouchPresent) && (_numberOfActiveContacts > _touchCapabilities.Contacts))
            {
                // Number of supported contacts exceeded.
                return;
            }

            if (Convert.ToBoolean(_touchCapabilities.TouchPresent) && (_numberOfActiveContacts >= _viewModel.numberOfFingersToChooseFrom))
            {
                // Number of desired contacts exceeded
                return;
            }

            Pointer pointer = e.Pointer;
            PointerPoint pointerPoint = e.GetCurrentPoint(targetCanvas);

            // Check if pointer already exists
            if (PointerAlreadyExists(pointer))
            {
                return;
            }

            AddPointer(pointer);

            DrawCircle(pointerPoint, pointer, FindIndexOfPointer(pointer));

            // Check if the timer should start
            if (_contacts.Count == _viewModel.numberOfFingersToChooseFrom)
            {
                StartTimer();
            }

            // Prevent most handlers along the event route from handling the same event again.
            e.Handled = true;
        }
        #endregion

        #region Pointer Helper Methods
        private void AddPointer(Pointer pointer)
        {
            // Add contact to dictionary.
            _contacts.Add(new Finger(pointer));
            ++_numberOfActiveContacts;
        }

        private void RemovePointer(Pointer pointer)
        {
            // Remove contact from dictionary.
            if (PointerAlreadyExists(pointer))
            {
                _contacts.RemoveAt(_contacts.FindIndex(f => f.pointer.PointerId == pointer.PointerId));
                --_numberOfActiveContacts;
            }
        }

        private bool PointerAlreadyExists(Pointer pointer)
        {
            return _contacts.FindIndex(f => f.pointer.PointerId == pointer.PointerId) >= 0;
        }

        private int FindIndexOfPointer(Pointer pointer)
        {
            return _contacts.FindIndex(f => f.pointer.PointerId == pointer.PointerId);
        } 
        #endregion

        #region Timer Methods
        private void StartTimer()
        {
            _contacts.Shuffle();
            _randomFingers = _contacts.Take(_viewModel.numberOfFingersToChoose);

            _viewModel.time = Convert.ToInt32(_viewModel._settingsController.GetCountdownTime()) == 0 ? 5 : Convert.ToInt32(_viewModel._settingsController.GetCountdownTime());

            _timer.Start();
        }

        private void TickTimer(object sender, object e)
        {
            if (_viewModel.time == 1)
            {
                for (int i = _contacts.Count; i-- > 0;)
                {
                    if (!_randomFingers.Contains(_contacts[i]))
                    {
                        DeleteCircle(_contacts[i].pointer, FindIndexOfPointer(_contacts[i].pointer));
                        RemovePointer(_contacts[i].pointer);
                    }
                    else
                    {
                        AnimateCircle(FindIndexOfPointer(_contacts[i].pointer));
                    }
                }

                StopTimer();
            }
            else
            {
                _viewModel.time--;
                Animations.EnlargeAnimation(counter, 1.2, 1, false, false).Begin();
            }
        }

        private void StopTimer()
        {
            _timer.Stop();
            _viewModel.time = 0;
            Animations.Reset(counter).Begin();
        }
        #endregion

        #region Circle methods
        public void DrawCircle(PointerPoint pointerPoint, Pointer pointer, int index)
        {
            var dict = Application.Current.Resources;

            Ellipse circle = new Ellipse();
            circle.Fill = new SolidColorBrush(ColourController.AccentColor);
            circle.Height = 120;
            circle.Width = 120;
            circle.Stroke = new SolidColorBrush(ColourController.ContrastColor);
            circle.StrokeThickness = 10;
            circle.RenderTransformOrigin = new Point(0.5, 0.5);

            _contacts[index].circle = circle;

            double xPosition = pointerPoint.Position.X - (circle.Width / 2);
            double yPosition = pointerPoint.Position.Y - (circle.Height / 2);
            targetCanvas.Children.Add(circle);
            Canvas.SetLeft(circle, xPosition);
            Canvas.SetTop(circle, yPosition);
        }

        public void DeleteCircle(Pointer pointer, int index)
        {
            targetCanvas.Children.Remove(_contacts[index].circle);
            _contacts[index].circle = null;
        }

        public void MoveCircle(PointerPoint pointerPoint, Pointer pointer, int index)
        {
            Ellipse circle = _contacts[index].circle;
            double xPosition = pointerPoint.Position.X - (circle.Width / 2);
            double yPosition = pointerPoint.Position.Y - (circle.Height / 2);
            Canvas.SetLeft(circle, xPosition);
            Canvas.SetTop(circle, yPosition);
        }

        public void AnimateCircle(int index)
        {
            Ellipse circle = _contacts[index].circle;
            Animations.EnlargeAnimation(circle, 1.5, 0.5, true, true).Begin();
        }
        #endregion
    }
}
