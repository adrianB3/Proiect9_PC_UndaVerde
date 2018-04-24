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
            GMap.NET.GMaps.Instance.Mode = GMap.NET.AccessMode.ServerAndCache;
            
            mapView.MapProvider = GMap.NET.MapProviders.GoogleMapProvider.Instance;
            mapView.MinZoom = 2;
            mapView.MaxZoom = 17;
           
            mapView.Zoom = 2;
            
            mapView.MouseWheelZoomType = GMap.NET.MouseWheelZoomType.MousePositionAndCenter;
            
            mapView.CanDragMap = true;
            
            mapView.DragButton = MouseButton.Left;
           
        }

        private void aplicationExit(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
