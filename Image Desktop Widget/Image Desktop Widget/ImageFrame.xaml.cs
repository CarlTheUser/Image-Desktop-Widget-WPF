using Image_Desktop_Widget.ViewModels;
using System;
using System.Windows;
using System.Windows.Input;

namespace Image_Desktop_Widget
{
    /// <summary>
    /// Interaction logic for ImageFrame.xaml
    /// </summary>
    public partial class ImageFrame : Window, IClosable
    {
        
        public ImageFrame()
        {
            InitializeComponent();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Keyboard.ClearFocus();
            if (e.ChangedButton == MouseButton.Left) this.DragMove();
        }
        
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if(this.DataContext != null)
            {
                if(this.DataContext.GetType() == typeof(ImageFrameViewModel))
                {
                    ImageFrameViewModel viewModel = (ImageFrameViewModel)this.DataContext;
                    
                    viewModel.ImageDisposeRequested += ViewModel_ImageDisposeRequested;
                    viewModel.Closable = this;
                }
            }
        }

        private bool isDisposed = false;

        private void ViewModel_ImageDisposeRequested(object sender, EventArgs e)
        {
            image.Source = null;
            isDisposed = true;
        }

        //private void ViewModel_CurrentStateSaved(object sender, EventArgs e)
        //{
        //    this.Close();
        //}
        
        private void ImageFrameWindow_StateChanged(object sender, EventArgs e)
        {
            if(this.WindowState == WindowState.Minimized)
            {
                this.WindowState = WindowState.Normal;
            }
            
        }

        private void ImageFrameWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (isDisposed) return;
            if (this.DataContext == null) return;
            if (this.DataContext.GetType() != typeof(ImageFrameViewModel)) return;
            ((ImageFrameViewModel)this.DataContext).SaveStateCommand.Execute(null);
        }

        private void CommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            SystemCommands.CloseWindow(this);
        }
    }
}
