using SQLitePCL;
using StockLibrary;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace StockHD_Lourd
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {   

        private StockDbContext _context;
        public MainWindow()
        {
            InitializeComponent();
            _context = new StockDbContext();
        }

        private void AssetClick(object sender, RoutedEventArgs e)
        {
            var asset = _context.Assets.ToList();

            dataGrid.Visibility = Visibility.Visible;

            dataGrid.ItemsSource = asset;
        }

        /*private void LocationClick(object sender, RoutedEventArgs e)
        {
            var location = _context.Locations.ToList();
            dataGrid.ItemsSource = location;
        }

        private void AssetTypeClick(object sender, RoutedEventArgs e)
        {
            var assetType = _context.Types.ToList();
            dataGrid.ItemsSource = assetType;
        }

        private void PropertiesClick(object sender, RoutedEventArgs e)
        {
            var property = _context.Properties.ToList();
            dataGrid.ItemsSource = property;
        }*/
    }
}