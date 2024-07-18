using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WPF_ASG_EF.View;
using WPF_Q.Models;

namespace WPF_Q.View
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        private readonly DbtestContext _context;

        public Login()
        {
            InitializeComponent();
            _context = new DbtestContext();
        }

        private async void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string username = Username.Text;
            string password = Password.Password;


            User user = await CheckLogin(username, password);

            if (user != null)
            {
                if (user.Role == 1)
                {
                    var window = new AdminView();
                    window.Show();
                    this.Close();
                }
                else
                {
                    var window = new UserView(user);
                    window.Show();
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("Invalid username or password.");
            }
        }

        private async Task<User> CheckLogin(string username, string password)
        {
            User user = await _context.Users
                .Where(u => u.Email == username && u.Password == password)
                .FirstOrDefaultAsync();

            return user;
        }

    }
}
