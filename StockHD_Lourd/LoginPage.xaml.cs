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

        private readonly AuthService _authService;

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Password; 

            if (_authService.Login(username, password))
            {
                MessageBox.Show("Connexion réussie !");
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Identifiants incorrects !");
            }
        }

        //-------------------------------------------------------------------------------------//

    }
}
