using Microsoft.Maps.MapControl.WPF;
using Microsoft.Maps.MapControl.WPF.Design;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;

namespace P9_UndaVerde
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Storyboard myStoryboard;
        LocationConverter locConverter = new LocationConverter();
        public MainWindow()
        {

            InitializeComponent();
           
        }

        
        private void aplicationExit(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void startAnimation(object sender, RoutedEventArgs e)
        {
            /*NameScope.SetNameScope(this, new NameScope());

            TranslateTransform translate = new TranslateTransform();

            this.RegisterName("animTranslate", translate);
            car.RenderTransform = translate;

            PathGeometry animatedPath = new PathGeometry();

            animatedPath.Figures = new PathFigureCollection();

            LineGeometry line = new LineGeometry(new Point(0, 0), new Point(-1000, 0));

            animatedPath.AddGeometry(line);

            animatedPath.Freeze();

            DoubleAnimationUsingPath translateX = new DoubleAnimationUsingPath();

            translateX.PathGeometry = animatedPath;

            translateX.Duration = TimeSpan.FromSeconds(5);

            translateX.Source = PathAnimationSource.X;

            Storyboard.SetTargetName(translateX, "animTranslate");
            Storyboard.SetTargetProperty(translateX, new PropertyPath(TranslateTransform.XProperty));

            Storyboard animX = new Storyboard();

            animX.Children.Add(translateX);

            animX.Begin(this);*/

            NameScope.SetNameScope(this, new NameScope());
            MatrixTransform carTransform = new MatrixTransform();
            car.RenderTransform = carTransform;
            this.RegisterName("carTransform", carTransform);

            PathGeometry animPath = new PathGeometry();
            PathFigure pathFigure = new PathFigure();
            pathFigure.StartPoint = new Point(0, 0);
            pathFigure.Segments = new PathSegmentCollection();
            pathFigure.Segments.Add(new LineSegment(new Point(-250,0), false));
            pathFigure.Segments.Add(new LineSegment(new Point(-250,-200), false));
            animPath.Figures.Add(pathFigure);
            animPath.Freeze();
            MatrixAnimationUsingPath mAnim = new MatrixAnimationUsingPath();
            mAnim.PathGeometry = animPath;
            mAnim.Duration = TimeSpan.FromSeconds(3);
            mAnim.DoesRotateWithTangent = true;

            Storyboard.SetTargetName(mAnim, "carTransform");
            Storyboard.SetTargetProperty(mAnim, new PropertyPath(MatrixTransform.MatrixProperty));

            Storyboard story = new Storyboard();
            story.Children.Add(mAnim);
            story.Begin(this);

        }
        /*private void addImageToMap(object sender, RoutedEventArgs e)
        {
            MapLayer imageLayer = new MapLayer();


            Image image = new Image();
            image.Height = myMap.ActualHeight - myMap.ZoomLevel*10;
            image.Width = myMap.ActualWidth - myMap.ZoomLevel*10;
            //Define the URI location of the image
            BitmapImage myBitmapImage = new BitmapImage();
            myBitmapImage.BeginInit();
            myBitmapImage.UriSource = new Uri(@"pack://application:,,,/Resources/car.png");
            // To save significant application memory, set the DecodePixelWidth or  
            // DecodePixelHeight of the BitmapImage value of the image source to the desired 
            // height or width of the rendered image. If you don't do this, the application will 
            // cache the image as though it were rendered as its normal size rather then just 
            // the size that is displayed.
            // Note: In order to preserve aspect ratio, set DecodePixelWidth
            // or DecodePixelHeight but not both.
            //Define the image display properties
            myBitmapImage.DecodePixelHeight = 30;
            
            myBitmapImage.EndInit();
            image.Source = myBitmapImage;
            image.Opacity = 0.6;
            image.Stretch = System.Windows.Media.Stretch.None;
            

            //The map location to place the image at
            Location location = new Location() { Latitude = 45.737286, Longitude = 21.233488 };
            //Center the image around the location specified
            PositionOrigin position = PositionOrigin.Center;

            //Add the image to the defined map layer
            imageLayer.AddChild(image, location, position);
            //Add the image layer to the map
            myMap.Children.Add(imageLayer);
        }*/
    }
}
