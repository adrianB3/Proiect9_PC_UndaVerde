using GMap.NET;
using GMap.NET.WindowsPresentation;
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
        public MainWindow()
        {
            InitializeComponent();

        }
        private void mapView_Loaded(object sender, RoutedEventArgs e)
        {
            GMaps.Instance.Mode = AccessMode.ServerAndCache;
            
            mapView.MapProvider = GMap.NET.MapProviders.GoogleMapProvider.Instance;
            mapView.MinZoom = 2;
            mapView.MaxZoom = 21;
           
            mapView.Zoom = 16;
            mapView.Position = new PointLatLng(45.7366683, 21.2277931);
            mapView.ShowCenter = false;
            mapView.MouseWheelZoomType = MouseWheelZoomType.MousePositionAndCenter;
            
            mapView.CanDragMap = true;
            
            mapView.DragButton = MouseButton.Left;

            PointLatLng start = new PointLatLng(45.747774, 21.226252);
            PointLatLng end = new PointLatLng(45.735784, 21.228215);

            GMap.NET.MapProviders.GoogleMapProvider.Instance.ApiKey = "AIzaSyCMPm5rJtNTOivNQ8HdfKQDoUTClnDYH_w";
            MapRoute route = GMap.NET.MapProviders.GoogleMapProvider.Instance.GetRoute(start, end, false, false, 15);
           

            if (route != null)
            {
                GMapRoute mRoute = new GMapRoute(route.Points);
                {
                    mRoute.ZIndex = -1;
                }

                mapView.Markers.Add(mRoute);
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("There is no route");
            }

        }

        private void aplicationExit(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
