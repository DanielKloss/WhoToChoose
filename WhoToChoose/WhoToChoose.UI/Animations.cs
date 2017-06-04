using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;

namespace WhoToChoose.UI
{
    public class Animations
    {
        public static Storyboard EnlargeAnimation(UIElement element, double scaleTo, double seconds, bool doesReverse, bool doesRepeat)
        {
            element.RenderTransform = new ScaleTransform();

            var story = new Storyboard();

            var xAnim = new DoubleAnimation();
            var yAnim = new DoubleAnimation();

            xAnim.Duration = TimeSpan.FromSeconds(seconds);
            yAnim.Duration = TimeSpan.FromSeconds(seconds);

            xAnim.To = scaleTo;
            yAnim.To = scaleTo;

            xAnim.AutoReverse = doesReverse;
            yAnim.AutoReverse = doesReverse;

            if (doesRepeat)
            {
                xAnim.RepeatBehavior = new RepeatBehavior(3);
                yAnim.RepeatBehavior = new RepeatBehavior(3);
            }

            story.Children.Add(xAnim);
            story.Children.Add(yAnim);

            Storyboard.SetTarget(xAnim, element);
            Storyboard.SetTarget(yAnim, element);

            Storyboard.SetTargetProperty(xAnim, "(UIElement.RenderTransform).(ScaleTransform.ScaleX)");
            Storyboard.SetTargetProperty(yAnim, "(UIElement.RenderTransform).(ScaleTransform.ScaleY)");

            return story;
        }

        public static Storyboard Reset(UIElement element)
        {
            element.RenderTransform = new CompositeTransform();

            var story = new Storyboard();

            var xAnim = new DoubleAnimation();
            var yAnim = new DoubleAnimation();

            xAnim.Duration = TimeSpan.FromMilliseconds(1);
            yAnim.Duration = TimeSpan.FromMilliseconds(1);

            xAnim.To = 1.00;
            yAnim.To = 1.00;

            story.Children.Add(xAnim);
            story.Children.Add(yAnim);

            Storyboard.SetTarget(xAnim, element);
            Storyboard.SetTarget(yAnim, element);

            Storyboard.SetTargetProperty(xAnim, "(UIElement.RenderTransform).(CompositeTransform.ScaleX)");
            Storyboard.SetTargetProperty(yAnim, "(UIElement.RenderTransform).(CompositeTransform.ScaleY)");

            return story;
        }
    }
}
