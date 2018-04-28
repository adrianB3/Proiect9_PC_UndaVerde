using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace P9_UndaVerde
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Storyboard myStoryboard;
        public MainWindow()
        {
            InitializeComponent();
            // Create a NameScope for the page so that
            // we can use Storyboards.
            NameScope.SetNameScope(this, new NameScope());

            // Create a button.
            Button aButton = new Button();
            aButton.MinWidth = 100;
            aButton.Content = "A Button";

            // Create a MatrixTransform. This transform
            // will be used to move the button.
            MatrixTransform buttonMatrixTransform = new MatrixTransform();
            aButton.RenderTransform = buttonMatrixTransform;

            // Register the transform's name with the page
            // so that it can be targeted by a Storyboard.
            this.RegisterName("ButtonMatrixTransform", buttonMatrixTransform);

            
            main.Children.Add(aButton);
            

            // Create the animation path.
            PathGeometry animationPath = new PathGeometry();
            PathFigure pFigure = new PathFigure();
            pFigure.StartPoint = new Point(10, 100);
            PolyBezierSegment pBezierSegment = new PolyBezierSegment();
            pBezierSegment.Points.Add(new Point(35, 0));
            pBezierSegment.Points.Add(new Point(135, 0));
            pBezierSegment.Points.Add(new Point(160, 100));
            pBezierSegment.Points.Add(new Point(180, 190));
            pBezierSegment.Points.Add(new Point(285, 200));
            pBezierSegment.Points.Add(new Point(310, 100));
            pFigure.Segments.Add(pBezierSegment);
            animationPath.Figures.Add(pFigure);

            // Freeze the PathGeometry for performance benefits.
            animationPath.Freeze();

            // Create a MatrixAnimationUsingPath to move the
            // button along the path by animating
            // its MatrixTransform.
            MatrixAnimationUsingPath matrixAnimation =
                new MatrixAnimationUsingPath();
            matrixAnimation.PathGeometry = animationPath;
            matrixAnimation.Duration = TimeSpan.FromSeconds(5);
            matrixAnimation.RepeatBehavior = RepeatBehavior.Forever;

            // Set the animation to target the Matrix property
            // of the MatrixTransform named "ButtonMatrixTransform".
            Storyboard.SetTargetName(matrixAnimation, "ButtonMatrixTransform");
            Storyboard.SetTargetProperty(matrixAnimation,
                new PropertyPath(MatrixTransform.MatrixProperty));

            // Create a Storyboard to contain and apply the animation.
            Storyboard pathAnimationStoryboard = new Storyboard();
            pathAnimationStoryboard.Children.Add(matrixAnimation);

            // Start the storyboard when the button is loaded.
            aButton.Loaded += delegate (object sender, RoutedEventArgs e)
            {
                // Start the storyboard.
                pathAnimationStoryboard.Begin(this);
            };
        }

        
        private void aplicationExit(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void startSimulation(object sender, RoutedEventArgs e)
        {

        }
    }
}
