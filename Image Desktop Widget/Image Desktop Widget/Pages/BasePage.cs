using Image_Desktop_Widget.Animation;
using System.Windows;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Image_Desktop_Widget.Pages
{
    public class BasePage : Page
    {
        public PageTransition PageEnterTransition { get; set; } = PageTransition.SlideAndFadeInFromRight;

        public PageTransition PageExitTransition { get; set; } = PageTransition.SlideAndFadeOutToLeft;

        public float TransitionDuration { get; set; } = 0.3f;

        public BasePage()
        {
            if (PageEnterTransition != PageTransition.None) this.Visibility = Visibility.Collapsed;

            this.Loaded += BasePage_Loaded;
            
        }

        private async void BasePage_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            await PlayPageEnterTransition();
        }
        
        public async Task PlayPageEnterTransition()
        {
            await PlayPageTransition(this.PageEnterTransition);
        }

        public async Task PlayPageExitTransition()
        {
            await PlayPageTransition(this.PageExitTransition);
        }
        
        private async Task PlayPageTransition(PageTransition pageTransition)
        {
            if (pageTransition == PageTransition.None) return;
            
            switch(PageEnterTransition)
            {
                case PageTransition.SlideAndFadeInFromRight:
                    await this.SlideAndFadeInFromRight(TransitionDuration);
                    break;
                case PageTransition.SlideAndFadeOutToLeft:
                    await this.SlideAndFadeOutToLeft(TransitionDuration);
                    break;
            }
            
        }
        
    }
}
