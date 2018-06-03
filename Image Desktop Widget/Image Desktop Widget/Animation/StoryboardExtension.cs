using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Animation;

namespace Image_Desktop_Widget.Animation
{
    public static class StoryboardExtension
    {
        public static void AddVerticalSlideAnimation(this Storyboard storyboard, double from, double to, float duration, float deccelerationRatio = 0.9f)
        {
            ThicknessAnimation thicknessAnimation = new ThicknessAnimation
            {
                Duration = new Duration(TimeSpan.FromSeconds(duration)),
                From = new Thickness(from, 0, -from, 0),
                To = new Thickness(to),
                DecelerationRatio = 0.9f
            };

            Storyboard.SetTargetProperty(thicknessAnimation, new PropertyPath("Margin"));
            storyboard.Children.Add(thicknessAnimation);
        }

        public static void AddFadeEffectAnimation(this Storyboard storyboard, double from, double to, float duration)
        {
            DoubleAnimation doubleAnimation = new DoubleAnimation
            {
                Duration = new Duration(TimeSpan.FromSeconds(duration)),
                From = from,
                To = to
            };
            Storyboard.SetTargetProperty(doubleAnimation, new PropertyPath("Opacity"));
            storyboard.Children.Add(doubleAnimation);
        }
    }
}
