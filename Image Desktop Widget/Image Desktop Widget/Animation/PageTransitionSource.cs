using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace Image_Desktop_Widget.Animation
{
    public static class PageTransitionSource
    {
        public static async Task SlideAndFadeInFromRight(this Page page, float duration)
        {
            Storyboard storyboard = new Storyboard();
            storyboard.AddVerticalSlideAnimation(page.WindowWidth,  0, duration);
            storyboard.AddFadeEffectAnimation(0, 1, duration);
            storyboard.Begin(page);
            page.Visibility = Visibility.Visible;
            await Task.Delay((int)(1000 * duration));
        }

        public static async Task SlideAndFadeOutToLeft(this Page page, float duration)
        {
            Storyboard storyboard = new Storyboard();
            storyboard.AddVerticalSlideAnimation(0, -page.WindowWidth, duration);
            storyboard.AddFadeEffectAnimation(1, 0, duration);
            storyboard.Begin(page);
            await Task.Delay((int)(1000 * duration));
        }
    }
}
