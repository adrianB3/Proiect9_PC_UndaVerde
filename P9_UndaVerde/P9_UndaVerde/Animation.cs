
using P9_UndaVerde;
using System;
using System.Threading;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace TrafficSimTM
{
    class Animation
    {
        MainWindow mainWin = Application.Current.Windows[0] as MainWindow;
        public Storyboard story = new Storyboard();
        Point _endPoint = new Point();

        public Animation(Point endPoint)
        {
            _endPoint = endPoint;
        }

        public void startAnimation(Car _animateObject, int animationTime, int delay)
        {
            NameScope.SetNameScope(mainWin, new NameScope());
            MatrixTransform carTransform = new MatrixTransform();
            _animateObject._carImg.RenderTransform = carTransform;
            mainWin.RegisterName("carTransform", carTransform);

            PathGeometry animPath = new PathGeometry();
            PathFigure pathFigure = new PathFigure();
            pathFigure.StartPoint = new Point(0, 0);
            pathFigure.Segments.Add(new LineSegment(_endPoint, false));
            
            animPath.Figures.Add(pathFigure);
            animPath.Freeze();

            MatrixAnimationUsingPath mAnim = new MatrixAnimationUsingPath();
            mAnim.PathGeometry = animPath;
            mAnim.Duration = TimeSpan.FromSeconds(animationTime);
            
            Storyboard.SetTargetName(mAnim, "carTransform");
            Storyboard.SetTargetProperty(mAnim, new PropertyPath(MatrixTransform.MatrixProperty));
            
            story.Children.Add(mAnim);
            story.Begin(mainWin,true);
        }

        public void stopAnimation()
        {
            story.Stop(mainWin);
        }

        public void pauseAnimation()
        {
            story.Pause(mainWin);
        }
    }
}
