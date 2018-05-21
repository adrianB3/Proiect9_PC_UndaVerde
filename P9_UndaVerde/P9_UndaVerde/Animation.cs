
using P9_UndaVerde;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Threading;


namespace TrafficSimTM
{
    public class Animation
    {
        MainWindow mainWin = Application.Current.Windows[0] as MainWindow; // referinta catre fereastra principala
        public Storyboard story = new Storyboard();   // Obiect ce retine o anumita animatie       
        private Point _startPoint; // punct de pornire al animatiei
        private Point _endPoint; // punct de oprire al animatiei
        private int _additionalAnims; // optiuni ptr animatie (urmarirea tangentei)

        // Constructor clasa Animatie
        public Animation(Point startPoint,Point endPoint, int additionalAnims)
        {
            _startPoint = startPoint;
            _endPoint = endPoint;
            _additionalAnims = additionalAnims;
        }

        /* Functie care porneste o animatie a unei anumite masinute care dureaza un anumit timp*/
        public void startAnimation(Car _animateObject, int animationTime, int delay)
        {
            // Animatia este inserata in coada dispecerului threadului de management al interfetei si este pornita
            Dispatcher.CurrentDispatcher.BeginInvoke(new Action(async () =>
            {
                NameScope.SetNameScope(mainWin, new NameScope()); // este setat contextul animatiei catre fereastra principala
                MatrixTransform carTransform = new MatrixTransform(); // creare obiect ce va retine transformarile vizuale din timpul animatiei asupra masinutei
                _animateObject._carImg.RenderTransform = carTransform; // atasare masinuta la matricea de transformare
                mainWin.RegisterName("carTransform", carTransform); 

                PathGeometry animPath = new PathGeometry(); // creare geometrie animatie
                PathFigure pathFigure = new PathFigure();
                pathFigure.StartPoint = _startPoint;
                pathFigure.Segments.Add(new LineSegment(_endPoint, true));
                animPath.Figures.Add(pathFigure);
                animPath.Freeze(); // optimizare animatie
                
                MatrixAnimationUsingPath mAnim = new MatrixAnimationUsingPath(); // creare obiect animatie
                if (_additionalAnims == 1)
                    mAnim.DoesRotateWithTangent = true;
                mAnim.PathGeometry = animPath; // atasare geometrie 
                mAnim.Duration = TimeSpan.FromSeconds(animationTime); // durata animatie
                
                Storyboard.SetTargetName(mAnim, "carTransform");
                Storyboard.SetTargetProperty(mAnim, new PropertyPath(MatrixTransform.MatrixProperty));

                story.Children.Add(mAnim);
                await Task.Delay(delay);
                story.Begin(mainWin, true); // incepere animatie
            }));                      
        }

        public double speedCalculation(Car car)
        {         
            return (Math.Sqrt(Math.Pow(0.01 * _endPoint.X - 0.01 * _startPoint.X, 2) + Math.Pow(0.01 * _endPoint.Y - 0.01 * _startPoint.Y, 2))*250 / car._speed);
        }
    }
}
