using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Image_Desktop_Widget.ViewModels;
using DataLayer.Client.Data;
using DataLayer.Client.Service;
using System.Threading;
using Image_Desktop_Widget.Model;

namespace Image_Desktop_Widget
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        private Mutex mutex;

        private bool isNewInstance;

        public App() : base()
        {
            string appName = "Image Desktop Widget";

            mutex = new Mutex(true, appName, out isNewInstance);

            if(!isNewInstance)
            {
                MessageBox.Show("An instance of the application is already running.", System.Reflection.Assembly.GetExecutingAssembly().GetName().Name);
                App.Current.Shutdown();
            }
        }
        
        protected override void OnStartup(StartupEventArgs e)
        {
            if (isNewInstance)
            {
                //new SliderTest().Show();
                //UserInteraction.MessageBox.Show("test");
                new ViewInitializer().SetupView();
                base.OnStartup(e);
                this.Activated += App_Activated;
            }
        }

        private void App_Activated(object sender, EventArgs e)
        {
            var imageFrames = this.Windows.OfType<ImageFrame>();

            if(imageFrames != null)
            {
                foreach (var frame in imageFrames)
                {
                    if (frame.WindowState == WindowState.Minimized) frame.WindowState = WindowState.Normal;
                    frame.Show();
                    frame.Visibility = Visibility.Visible;
                }
            }
        }

        private class ViewInitializer
        {
            public void SetupView()
            {

                FramedImageOperation framedImageOperation = new FramedImageOperation();

                IEnumerable<FramedImageDTO> framedImages = null;
                try
                {
                    framedImages = framedImageOperation.GetAll();
                }
                catch (Exception e)
                {
                    UserInteraction.MessageBox.Show(e.Message);
                    GetMainWindow().Show();
                }
                
                var framedImageDataList = framedImages.ToList(); 
                if (framedImageDataList.Count > 0)
                {
                    foreach (FramedImageDTO framedImageDataItem in framedImageDataList)
                    {

                        ImageFrameModel model = ImageFrameModel
                            .Existing(
                            framedImageDataItem.Id,
                            framedImageDataItem.ImageFilepath,
                            framedImageDataItem.Caption,
                            framedImageDataItem.EnableCaption,
                            framedImageDataItem.Witdh,
                            framedImageDataItem.Height,
                            framedImageDataItem.FrameThickness,
                            framedImageDataItem.LocationX,
                            framedImageDataItem.LocationY,
                            framedImageDataItem.RotateAngle,
                            framedImageDataItem.EnableShadow,
                            framedImageDataItem.ShadowOpacity,
                            framedImageDataItem.ShadowDepth,
                            framedImageDataItem.ShadowDirection,
                            framedImageDataItem.ShadowBlurRadius
                            );

                        ImageFrame imageFrame = new ImageFrame();
                        imageFrame.GetModel().Parameters = new Dictionary<string, object>()
                        {
                            { ImageFrameViewModel.IMAGE_FRAME_MODEL_PARAMETER, model }
                        };
                        imageFrame.Show();
                        
                    }
                }
                else
                {
                    MainWindow mainWindow = (MainWindow)GetMainWindow();
                    mainWindow.Show();
                    mainWindow.Focus();

                }

            }

            private Window GetMainWindow()
            {
                return Image_Desktop_Widget.MainWindow.Instance;
            }
        }
    }
}
