using Microsoft.EntityFrameworkCore.Metadata.Internal;
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
using System.Windows.Shapes;
using StockLibrary;
using Microsoft.EntityFrameworkCore;
using StockHD_Lourd.Service;
using StockLibrary.Data;
using Microsoft.AspNetCore.Identity;

namespace StockHD_Lourd
{
    public partial class LoginPage : Window
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        // Sign In
        //-------------------------------------------------------------------------------------//

        protected SignInManager<StockUser> _SignInManager { get; }
        protected UserManager<StockUser> _UserManager { get; }

        private async void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Password;

            var user = await _UserManager.FindByNameAsync(username);
            if (user != null && await _UserManager.CheckPasswordAsync(user, password))
            {
                await _SignInManager.SignInAsync(user, false);

                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Une Erreur est survenu, vérifiez vos identifiants");
            }
        }

        //-------------------------------------------------------------------------------------//

    }
}