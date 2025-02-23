using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Microsoft.Extensions.DependencyInjection;
using SQLitePCL;
using StockLibrary;
using StockLibrary.Models;
using System.Collections.ObjectModel;
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

using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Cryptography;

namespace StockHD_Lourd
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {   

        private StockDbContext _context;
        private ObservableCollection<Asset> Assets = new ObservableCollection<Asset>();
        private ObservableCollection<AssetType> AssetTypes = new ObservableCollection<AssetType>();
        private ObservableCollection<Location> Locations = new ObservableCollection<Location>();
        private ObservableCollection<SrNumber> SrNumbers = new ObservableCollection<SrNumber>();
        private Asset eAsset;
        public MainWindow()
        {
            
            App app = (App)Application.Current;
            
            _context = app.ServiceProvider.GetService<StockDbContext>();

            this.DataContext = this;    
            this.Assets = new ObservableCollection<Asset>(_context.Assets
                                                .Include(a=>a.AssetType)
                                                .Include(a => a.Location)
                                                .Include(a => a.SrNumber)
                                                .Include(a => a.PropertiesValues)
                                                    .ThenInclude(p=> p.Property)
                                                .ToList());

            this.AssetTypes = new ObservableCollection<AssetType>(_context.Types
                                                                            .Include(t=>t.Properties)
                                                                        .ToList());
            this.Locations = new ObservableCollection<Location>(_context.Locations.ToList());
            this.SrNumbers = new ObservableCollection<SrNumber>(_context.SrNumber.ToList());
            InitializeComponent();

            dg_Hardware.DataContext = Assets;
            cb_AssetType.DataContext = AssetTypes;
            cb_Location.DataContext = Locations;
            cb_SrNumber.DataContext = SrNumbers;



        }

        private void dg_Hardware_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            eAsset = (sender as DataGridRow).DataContext as Asset;
            tb_hardware_Id.Text = eAsset.Id.ToString();
            this.tb_Manufactureur.Text = eAsset.Manufacturer;
            this.cb_AssetType.SelectedItem = eAsset.AssetType;   
            this.cb_Location.SelectedItem = eAsset.Location;
            this.cb_SrNumber.SelectedItem = eAsset.SrNumber;
            this.dg_Harware_Props.DataContext = eAsset.PropertiesValues;

            bt_Hardware_Add.Visibility = Visibility.Collapsed;
            bt_Hardware_Save.Visibility = Visibility.Visible;
        }

        private void resetFrom()
        {
            tb_hardware_Id.Text = "";
            this.tb_Manufactureur.Text = "";
            this.cb_AssetType.SelectedItem = null;
            this.cb_Location.SelectedItem = null;
            this.cb_SrNumber.SelectedItem = null;
            this.dg_Harware_Props.DataContext = null;
            bt_Hardware_Add.Visibility = Visibility.Visible;
            bt_Hardware_Save.Visibility = Visibility.Collapsed;
        }


        private void bt_Hardware_Save_Click(object sender, RoutedEventArgs e)
        {
            int hId = int.Parse(tb_hardware_Id.Text);
            Asset a = _context.Assets.Include(a => a.Location)
                                                .Include(a => a.SrNumber)
                                                .Include(a => a.PropertiesValues)
                                                    .ThenInclude(p => p.Property)
                                    .SingleOrDefault(a => a.Id == hId);

            a.Manufacturer = this.tb_Manufactureur.Text;
            a.AssetType = this.cb_AssetType.SelectedItem as AssetType;
            a.Location = this.cb_Location.SelectedItem as Location;
            a.SrNumber = this.cb_SrNumber.SelectedItem as SrNumber;
            a.PropertiesValues = this.dg_Harware_Props.DataContext as Collection<ExtendedPropertyValue>;

            _context.Update(a);
            _context.SaveChanges();

            this.Assets = new ObservableCollection<Asset>(_context.Assets
                                                .Include(a => a.AssetType)
                                                .Include(a => a.Location)
                                                .Include(a => a.SrNumber)
                                                .Include(a => a.PropertiesValues)
                                                    .ThenInclude(p => p.Property)
                                                .ToList());
            dg_Hardware.DataContext = Assets;
            resetFrom();

        }

        private void bt_Hardware_Add_Click(object sender, RoutedEventArgs e)
        {
            Asset a = new Asset()
            {
                Manufacturer = this.tb_Manufactureur.Text,
                AssetType = this.cb_AssetType.SelectedItem as AssetType,
                Location = this.cb_Location.SelectedItem as Location,
                SrNumber = this.cb_SrNumber.SelectedItem as SrNumber,
                PropertiesValues = this.dg_Harware_Props.DataContext as Collection<ExtendedPropertyValue>
            };
            _context.Add(a);
            _context.SaveChanges();

            this.Assets = new ObservableCollection<Asset>(_context.Assets
                                                .Include(a => a.AssetType)
                                                .Include(a => a.Location)
                                                .Include(a => a.SrNumber)
                                                .Include(a => a.PropertiesValues)
                                                    .ThenInclude(p => p.Property)
                                                .ToList());
            dg_Hardware.DataContext = Assets;
            resetFrom();

        }

        private void bt_Hardware_Cancel_Click(object sender, RoutedEventArgs e)
        {
            resetFrom();

        }

        private void cb_AssetType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            AssetType at = cb_AssetType.SelectedItem as AssetType;
            if (at != null)
            {
                this.dg_Harware_Props.DataContext = new ObservableCollection<ExtendedPropertyValue>(
                    at.Properties.Select(p => new ExtendedPropertyValue() { Property = p }));
            }

        }
    }
}