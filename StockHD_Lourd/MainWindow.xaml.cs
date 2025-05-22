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
using Microsoft.EntityFrameworkCore.Metadata.Internal;

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
        private ObservableCollection<ExtendedProperty> Property = new ObservableCollection<ExtendedProperty>();
        private Asset eAsset;
        private ExtendedProperty eProperty;
        private Location eLocation;
        private AssetType eAssetType;
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
            this.Property = new ObservableCollection<ExtendedProperty>(_context.Properties.ToList());
            

            InitializeComponent();

            //Asset
            dg_Hardware.DataContext = Assets;
            cb_AssetType.DataContext = AssetTypes;
            cb_Location.DataContext = Locations;
            cb_SrNumber.DataContext = SrNumbers;

            //Property
            dg_Property.DataContext = Property;

            //Location
            dg_Location.DataContext = Locations;

            //AssetType
            dg_AssetType.DataContext = AssetTypes;
            // lb_cat_AllCat.DataContext = Property;
            // cb_AssetType_Prop.DataContext = Property;
            Property.ToList().ForEach(i=>lb_cat_AllCat.Items.Add(i));

                
        }

        // Pour l'onglet matériel.
        // _______________________________________________________________________________________________

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
            bt_Hardware_Delete.Visibility = Visibility.Visible;
        }

        private void bt_Hardware_Delete_Click(object sender, RoutedEventArgs e)
        {
            int hId = int.Parse(tb_hardware_Id.Text);
            Asset a = _context.Assets.SingleOrDefault(a => a.Id == hId);
            _context.Remove(a);
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
            bt_Hardware_Delete.Visibility = Visibility.Collapsed;
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

        // _______________________________________________________________________________________________




        // Pour l'onglet Properties.
        // _______________________________________________________________________________________________

        private void dg_Property_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            eProperty = (sender as DataGridRow).DataContext as ExtendedProperty;
            tb_Property_Id.Text = eProperty.Id.ToString();
            this.tb_NameProp.Text = eProperty.Name;
            this.tb_DescriptionProp.Text = eProperty.Description;

            bt_Property_Add.Visibility = Visibility.Collapsed;
            bt_Property_Save.Visibility = Visibility.Visible;
        }

       

        private void resetFromProp()
        {
            tb_Property_Id.Text = "";
            this.tb_NameProp.Text = "";
            this.tb_DescriptionProp.Text = "";
            bt_Property_Add.Visibility = Visibility.Visible;
            bt_Property_Save.Visibility = Visibility.Collapsed;
        }

        private void bt_Property_Save_Click(object sender, RoutedEventArgs e)
        {
            int pId = int.Parse(tb_Property_Id.Text);
            ExtendedProperty p = _context.Properties.Include(p => p.AssetTypes)
                                                    .SingleOrDefault(p => p.Id == pId);

            p.Name = this.tb_NameProp.Text;
            p.Description = this.tb_DescriptionProp.Text;

            _context.Update(p);
            _context.SaveChanges();

            this.Property = new ObservableCollection<ExtendedProperty>(_context.Properties.ToList());

            dg_Property.DataContext = Property;
            resetFromProp();

        }
        private void bt_Property_Add_Click(object sender, RoutedEventArgs e)
        {
            ExtendedProperty p = new ExtendedProperty()
            {
                Name = this.tb_NameProp.Text,
                Description = this.tb_DescriptionProp.Text
            };
            _context.Add(p);
            _context.SaveChanges();

            this.Property = new ObservableCollection<ExtendedProperty>(_context.Properties.ToList());

            dg_Property.DataContext = Assets;
            resetFromProp();

        }

        private void bt_Property_Cancel_Click(object sender, RoutedEventArgs e)
        {
            resetFromProp();

        }
        // _______________________________________________________________________________________________


        // Pour l'onglet Location
        // _______________________________________________________________________________________________

        private void dg_Location_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            eLocation = (sender as DataGridRow).DataContext as Location;
            tb_Location_Id.Text = eLocation.Id.ToString();
            this.tb_NameLocation.Text = eLocation.Name;
            this.tb_DescriptionLocation.Text = eLocation.Description;
            this.tb_CodeLocation.Text = eLocation.Code;
            bt_Location_Add.Visibility = Visibility.Collapsed;
            bt_Location_Save.Visibility = Visibility.Visible;
            bt_Location_Delete.Visibility = Visibility.Visible;
        }

        private void resetFromLocation()
        {
            tb_Location_Id.Text = "";
            this.tb_NameLocation.Text = "";
            this.tb_DescriptionLocation.Text = "";
            this.tb_CodeLocation.Text = "";
            bt_Location_Add.Visibility = Visibility.Visible;
            bt_Location_Save.Visibility = Visibility.Collapsed;
            bt_Location_Delete.Visibility = Visibility.Collapsed;
        }

        private void bt_Location_Delete_Click(object sender, RoutedEventArgs e)
        {
            int lid = int.Parse(tb_Location_Id.Text);
            Location l = _context.Locations.SingleOrDefault(p => p.Id == lid);
            _context.Remove(l);
            _context.SaveChanges();
            this.Locations = new ObservableCollection<Location>(_context.Locations.ToList());
            dg_Location.DataContext = Locations;
            resetFromLocation();
        }

        private void bt_Location_Save_Click(object sender, RoutedEventArgs e)
        {
            int lId = int.Parse(tb_Location_Id.Text);
            Location l = _context.Locations.SingleOrDefault(p => p.Id == lId);
            l.Name = this.tb_NameLocation.Text;
            l.Description = this.tb_DescriptionLocation.Text;
            l.Code = this.tb_CodeLocation.Text;
            _context.Update(l);
            _context.SaveChanges();
            this.Locations = new ObservableCollection<Location>(_context.Locations.ToList());
            dg_Location.DataContext = Locations;
            resetFromLocation();
        }

        private void bt_Location_Add_Click(object sender, RoutedEventArgs e)
        {
            Location l = new Location()
            {

                Name = this.tb_NameLocation.Text,
                Description = this.tb_DescriptionLocation.Text,
                Code = this.tb_CodeLocation.Text
            };
            _context.Add(l);
            _context.SaveChanges();
            this.Locations = new ObservableCollection<Location>(_context.Locations.ToList());
            dg_Location.DataContext = Locations;
            resetFromLocation();
        }

        private void bt_Location_Cancel_Click(object sender, RoutedEventArgs e)
        {
            resetFromLocation();
        }

        // _______________________________________________________________________________________________


        // Pour l'onglet Catégorie
        // _______________________________________________________________________________________________

        private void dg_AssetType_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            eAssetType = (sender as DataGridRow).DataContext as AssetType;
            tb_AssetType_Id.Text = eAssetType.Id.ToString();
            this.tb_AssetType_Name.Text = eAssetType.Name;
            this.tb_AssetType_Description.Text = eAssetType.Description;
           // this.cb_AssetType_Prop.SelectedItem = eAssetType.Properties;

            bt_AssetType_Add.Visibility = Visibility.Collapsed;
            bt_AssetType_Save.Visibility = Visibility.Visible;
            bt_AssetType_Delete.Visibility = Visibility.Visible;

            lb_cat_AllCat.Items .Clear();
            lb_cat_UsedCat.Items.Clear();
            Property.ToList().ForEach(i => lb_cat_AllCat.Items.Add(i));

            foreach (var p in eAssetType.Properties)
            {

                lb_cat_AllCat.Items.Remove(p);
                lb_cat_UsedCat.Items.Add(p);
            }



        }

        private void bt_AssetType_Delete_Click(object sender, RoutedEventArgs e)
        {
            int aId = int.Parse(tb_AssetType_Id.Text);
            AssetType a = _context.Types.SingleOrDefault(d => d.Id == aId);
            _context.Remove(a);
            _context.SaveChanges();

            var AssetType = new ObservableCollection<AssetType>(_context.Types.ToList());

            dg_AssetType.DataContext = AssetType;
            resetFormAssetType();
        }

        private void bt_AssetType_Add_Click(object sender, RoutedEventArgs e)
        {
            AssetType a = new AssetType()
            {
                Name = this.tb_AssetType_Name.Text,
                Properties = new Collection<ExtendedProperty> (lb_cat_UsedCat.Items.Cast<ExtendedProperty>().ToList()),
                Description = this.tb_AssetType_Description.Text
            };

            // ExtendedProperty p = this.cb_AssetType_Prop.SelectedItem as ExtendedProperty;

            //if(p != null)
            //{
            //    a.Properties.Add(p);
            //}
            _context.Add(a);
            _context.SaveChanges();

            this.AssetTypes = new ObservableCollection<AssetType>(_context.Types.Include(t => t.Properties).ToList());

            dg_AssetType.DataContext = AssetTypes;
            resetFormAssetType();

        }

        private void bt_AssetType_Save_Click(object sender, RoutedEventArgs e)
        {
            int aId = int.Parse (tb_AssetType_Id.Text);
            AssetType a = _context.Types.Include(a=>a.Properties).SingleOrDefault (d => d.Id == aId);
            a.Name = tb_AssetType_Name.Text;
           // a.Properties = cb_AssetType_Prop.SelectedItem as Collection<ExtendedProperty> ;
            a.Description = tb_AssetType_Description.Text;
            a.Properties = new Collection<ExtendedProperty>(lb_cat_UsedCat.Items.Cast<ExtendedProperty>().ToList());
            _context.Update(a);
            _context.SaveChanges ();

            var AssetType = new ObservableCollection<AssetType>(_context.Types.Include(t => t.Properties).ToList());

            dg_AssetType.DataContext = AssetType;
            resetFormAssetType();
        }

        private void resetFormAssetType()
        {
            tb_AssetType_Id.Text = "";
            this.tb_AssetType_Name.Text = "";
            this.tb_AssetType_Description.Text = "";
           // this.cb_AssetType_Prop.SelectedItem = null;
            this.bt_AssetType_Delete.Visibility = Visibility.Collapsed;
            this.bt_AssetType_Save.Visibility = Visibility.Collapsed;
            this.bt_AssetType_Add.Visibility = Visibility.Visible;
            lb_cat_UsedCat.Items.Clear ();
            lb_cat_AllCat.Items.Clear();
            Property.ToList().ForEach(i => lb_cat_AllCat.Items.Add(i));

        }

        private void bt_AssetType_Cancel_Click(object sender, EventArgs e)
        {
            resetFormAssetType();
        }

        private void bt_cat_Addprop_Click(object sender, RoutedEventArgs e)
        {

            foreach (ExtendedProperty p in lb_cat_AllCat.SelectedItems.Cast<object>().ToList())
            {
                    lb_cat_UsedCat.Items.Add(p);
                lb_cat_AllCat.Items.Remove(p);
            }
        }

        private void bt_cat_Rmprop_Click(object sender, RoutedEventArgs e)
        {
            foreach (ExtendedProperty p in lb_cat_UsedCat.SelectedItems.Cast<object>().ToList())
            {
                lb_cat_AllCat.Items.Add(p);
                lb_cat_UsedCat.Items.Remove(p);
            }
        }

        // _______________________________________________________________________________________________
    }
}   